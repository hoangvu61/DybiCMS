using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Data;
using Web.Api.Repositories;
using Microsoft.Extensions.FileProviders;

    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.SetMinimumLevel(LogLevel.Trace);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<WebDbContext>(optionsAction: options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    builder.Services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<WebDbContext>();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["JwtIssuer"],
                            ValidAudience = builder.Configuration["JwtAudience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]))
                        };
                    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder
                .SetIsOriginAllowed((host) => true)
                //.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                );
    });

    builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
    builder.Services.AddTransient<IUserRepository, UserRepository>(); 
    builder.Services.AddTransient<IModuleRepository, ModuleRepository>();
    builder.Services.AddTransient<IModuleConfigRepository, ModuleConfigRepository>();
    builder.Services.AddTransient<ITemplateRepository, TemplateRepository>();
    builder.Services.AddTransient<IThirdPartyRepository, ThirdPartyRepository>();
    builder.Services.AddTransient<IItemRepository, ItemRepository>();
    builder.Services.AddTransient<IAttributeRepository, AttributeRepository>();
    builder.Services.AddTransient<IOrderRepository, OrderRepository>();
    builder.Services.AddTransient<IMenuRepository, MenuRepository>();
    builder.Services.AddTransient<ISEORepository, SEORepository>();
    builder.Services.AddTransient<IWebInfoRepository, WebInfoRepository>();
    builder.Services.AddTransient<IWarehouseRepository, WarehouseRepository>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MigrateDbContext<WebDbContext>(async (context, services) =>
    {
        var logger = services.GetRequiredService<ILogger<WebDbContextSeed>>();
        new WebDbContextSeed().SeedAsync(context, logger).Wait();
    });

    //app.UseHttpsRedirection();
    app.UseCors("CorsPolicy");
    app.UseHttpMethodOverride();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseStaticFiles();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
               Path.Combine(builder.Environment.ContentRootPath, "Files")),
        RequestPath = "/Files"
    });

    app.MapControllers();

    app.Run();