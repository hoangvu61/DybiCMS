using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Api.Entities;

namespace Web.Api.Data
{
    public class WebDbContext : IdentityDbContext<User, Role, Guid>
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CompanyDetail>()
                  .HasKey(m => new { m.CompanyId, m.LanguageCode });
            modelBuilder.Entity<CompanyLanguage>()
                  .HasKey(m => new { m.CompanyId, m.LanguageCode });
            modelBuilder.Entity<CompanyLanguageConfig>()
                  .HasKey(m => new { m.CompanyId, m.LanguageKey, m.LanguageCode });
            modelBuilder.Entity<WebInfo>()
                 .HasKey(m => new { m.CompanyId, m.LanguageCode });

            modelBuilder.Entity<AttributeCategory>()
                  .HasKey(m => new { m.AttributeId, m.CompanyId, m.CategoryId });
            modelBuilder.Entity<AttributeLanguage>()
                  .HasKey(m => new { m.AttributeId, m.CompanyId, m.LanguageCode });
            modelBuilder.Entity<AttributeSourceLanguage>()
                  .HasKey(m => new { m.AttributeSourceId, m.LanguageCode });
            modelBuilder.Entity<AttributeValueLanguage>()
                  .HasKey(m => new { m.AttributeValueId, m.LanguageCode });
            modelBuilder.Entity<Entities.Attribute>()
                .HasKey(m => new { m.Id, m.CompanyId });

            modelBuilder.Entity<AttributeCategory>()
                .HasOne(a => a.Attribute)
                .WithMany(a => a.AttributeCategories)
                .HasForeignKey(a => new { a.AttributeId, a.CompanyId });
            modelBuilder.Entity<AttributeLanguage>()
                .HasOne(a => a.Attribute)
                .WithMany(a => a.AttributeLanguages)
                .HasForeignKey(a => new { a.AttributeId, a.CompanyId });

            modelBuilder.Entity<TemplatePosition>()
                  .HasKey(m => new { m.TemplateName, m.PositionName, m.ComponentName });
            modelBuilder.Entity<TemplateSkin>()
                  .HasKey(m => new { m.TemplateName, m.SkinName });
            modelBuilder.Entity<TemplateComponent>()
                 .HasKey(m => new { m.TemplateName, m.ComponentName });
            modelBuilder.Entity<TemplateLanguage>()
                 .HasKey(m => new { m.TemplateName, m.LanguageKey });

            modelBuilder.Entity<ModuleParam>()
                 .HasKey(m => new { m.ModuleName, m.ParamName });
            modelBuilder.Entity<ModuleConfigDetail>()
                 .HasKey(m => new { m.ModuleId, m.LanguageCode });
            modelBuilder.Entity<ModuleConfigParam>()
                 .HasKey(m => new { m.ModuleId, m.ParamName });

            modelBuilder.Entity<ItemLanguage>()
                 .HasKey(m => new { m.ItemId, m.LanguageCode });
            modelBuilder.Entity<ItemTag>()
                 .HasKey(m => new { m.ItemId, m.Slug });
            modelBuilder.Entity<ItemImage>()
                 .HasKey(m => new { m.ItemId, m.Image });
            modelBuilder.Entity<ItemRelated>()
                .HasKey(m => new { m.ItemId, m.RelatedId });
            modelBuilder.Entity<ItemAttribute>()
                .HasKey(m => new { m.ItemId, m.AttributeId, m.LanguageCode });

            modelBuilder.Entity<ItemProductAddOn>()
                .HasKey(m => new { m.ProductId, m.ProductAddOnId });
            
            modelBuilder.Entity<OrderProduct>()
                .HasKey(m => new { m.OrderId, m.ProductId });

            modelBuilder.Entity<CustomerInfo>()
                .HasKey(m => new { m.Id, m.InfoKey });

            //modelBuilder.Entity<Role>().HasData(new Role { Name = "Product", NormalizedName = "PRODUCT", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Product" });
            //modelBuilder.Entity<Role>().HasData(new Role { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Admin" });
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyDomain> CompanyDomains { get; set; }
        public DbSet<CompanyLanguage> CompanyLanguages { get; set; }
        public DbSet<CompanyLanguageConfig> CompanyLanguageConfigs { get; set; }
        public DbSet<CompanyBranch> CompanyBranches { get; set; }
        public DbSet<CompanyDetail> CompanyDetails { get; set; }
        public DbSet<WebConfig> WebConfigs { get; set; }
        public DbSet<WebInfo> WebInfos { get; set; }

        public DbSet<Entities.Attribute> Attributes { get; set; }
        public DbSet<AttributeCategory> AttributeCategories { get; set; }
        public DbSet<AttributeOrder> AttributeOrders { get; set; }
        public DbSet<AttributeContact> AttributeContacts { get; set; }
        public DbSet<AttributeLanguage> AttributeLanguages { get; set; }
        public DbSet<AttributeSource> AttributeSources { get; set; }
        public DbSet<AttributeSourceLanguage> AttributeSourceLanguages { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<AttributeValueLanguage> AttributeValueLanguages { get; set; }

        public DbSet<ThirdParty> ThirdParties { get; set; }

        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleParam> ModuleParams { get; set; }
        

        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateComponent> TemplateComponents { get; set; }
        public DbSet<TemplatePosition> TemplatePositions { get; set; }
        public DbSet<TemplateSkin> TemplateSkins { get; set; }
        public DbSet<TemplateLanguage> TemplateLanguageKeys { get; set; }

        public DbSet<ModuleConfig> ModuleConfigs { get; set; }
        public DbSet<ModuleSkin> ModuleSkins { get; set; }
        public DbSet<ModuleConfigDetail> ModuleConfigDetails { get; set; }
        public DbSet<ModuleConfigParam> ModuleConfigParams { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemLanguage> ItemLanguages { get; set; }
        public DbSet<ItemTag> ItemTags { get; set; }
        public DbSet<ItemReview> ItemReviews { get; set; }
        public DbSet<ItemRelated> ItemRelateds { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<ItemAttribute> ItemAttributes { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ItemArticle> ItemArticles { get; set; }
        public DbSet<ItemMedia> ItemMedias { get; set; }
        public DbSet<ItemProduct> ItemProducts { get; set; }
        public DbSet<ItemProductAddOn> ItemProductAddOns { get; set; }
        public DbSet<ItemProductGroupon> ItemProductGroupons { get; set; }
        public DbSet<ItemEvent> ItemEvents { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderDelivery> OrderDeliveries { get; set; }
        

        public DbSet<SEO> SEOs { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}
