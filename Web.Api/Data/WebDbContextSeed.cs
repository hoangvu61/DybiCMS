using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Api.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Api.Data
{
    public class WebDbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public async Task SeedAsync(WebDbContext context, ILogger<WebDbContextSeed> logger)
        {
            if (!context.Languages.Any())
            {
                context.Languages.Add(new Language()
                {
                    Code = "vi",
                    Name = "Tiếng Việt",
                });

                context.Languages.Add(new Language()
                {
                    Code = "en",
                    Name = "English",
                });

                context.Languages.Add(new Language()
                {
                    Code = "zh",
                    Name = "Chinese",
                });

                context.Languages.Add(new Language()
                {
                    Code = "ja",
                    Name = "Japanese",
                });

                await context.SaveChangesAsync();
            }

            if (!context.Companies.Any())
            {
                var template = new Template()
                {
                    TemplateName = "T01",
                    IsPublic = true,
                    IsPublished = true,
                };
                context.Templates.Add(template);

                var company = new Company()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                };
                context.Companies.Add(company);

                var webconfig = new WebConfig()
                {
                    Id = company.Id,
                    TemplateName = template.TemplateName,
                    CanRightClick = true,
                    CanSelectCopy = true,
                    Hierarchy = true
                };
                context.WebConfigs.Add(webconfig);

                context.CompanyLanguages.Add(new CompanyLanguage
                {
                    CompanyId = company.Id,
                    LanguageCode = "vi"
                });

                context.CompanyDomains.Add(new CompanyDomain
                {
                    CompanyId = company.Id,
                    LanguageCode = "vi",
                    Domain = "có-nên-mua.vn"
                });
                context.CompanyDomains.Add(new CompanyDomain
                {
                    CompanyId = company.Id,
                    LanguageCode = "vi",
                    Domain = "www.có-nên-mua.vn"
                });
                context.CompanyBranches.Add(new CompanyBranch
                {
                    CompanyId = company.Id,
                    LanguageCode = "vi",
                    Email = "hoangvu61@gmail.com",
                    Phone = "0987877602",
                    Address = "37 Hai Bà Trưng, P. Vĩnh Ninh, tp. Huế"
                });
                context.CompanyDetails.Add(new CompanyDetail
                {
                    CompanyId = company.Id,
                    LanguageCode = "vi",
                    AboutUs = "",
                    Brief = "Review sản phẩm, Dịch vụ quảng cáo, thiết kế website, thiết kế gấu bông, bán hàng giảm giá, cung cấp mã giảm giá, sản phẩm khuyến mãi",
                    FullName = "Review sản phẩm - Sản xuất Gia công Gấu bông - Thiết kế website",
                    DisplayName = "Trang dịch vụ tổng hợp",
                    Slogan = "Tuyệt đối an toàn -  giá thành hợp lý"
                });

                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var domains = await context.CompanyDomains.Include(e => e.Company).ToListAsync();
                var branches = await context.CompanyBranches.Where(e => e.LanguageCode == "vi").ToListAsync();
                foreach (var domain in domains)
                {
                    if (domain.Domain.Contains(".") && !domain.Domain.StartsWith("www")
                        && !domain.Domain.StartsWith("localhost")
                        && !domain.Domain.StartsWith("210")
                        && !domain.Domain.StartsWith("xn-")
                        && !domain.Domain.StartsWith("www.xn-")
                        && !domain.Domain.StartsWith("thubongnhuanthien")
                        && !domain.Domain.StartsWith("truyencuoi.top"))
                    {
                        var domainName = domain.Domain;
                        if (domainName == "khánh-tâm.vn") domainName = "khanhtam";
                        if (domainName == "truyện-cười.vn") domainName = "truyencuoi";
                        if (domainName == "có-nên-mua.vn") domainName = "conenmua";
                        var branch = branches.Where(e => e.CompanyId == domain.CompanyId).FirstOrDefault();
                        if (branch != null)
                        {
                            var u = new User()
                            {
                                Id = Guid.NewGuid(),
                                CompanyId = domain.CompanyId,
                                FirstName = "Mr",
                                LastName = domainName,
                                Email = branch.Email,
                                NormalizedEmail = branch.Email.ToUpper(),
                                PhoneNumber = branch.Phone,
                                UserName = domainName.Split('.')[0],
                                NormalizedUserName = domainName.Split('.')[0].ToUpper(),
                                SecurityStamp = Guid.NewGuid().ToString()
                            };
                            u.PasswordHash = _passwordHasher.HashPassword(u, password: "123654789");
                            context.Users.Add(u);
                        }
                    }
                }
                await context.SaveChangesAsync();
            }

            if (!context.Roles.Any(e => e.Name == "Product"))
            {
                context.Roles.Add(new Role { Name = "Product", NormalizedName = "PRODUCT", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Quản lý sản phẩm" });
                await context.SaveChangesAsync();
            }
            if (!context.Roles.Any(e => e.Name == "Admin"))
            {
                context.Roles.Add(new Role { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Admin" });
                await context.SaveChangesAsync();
            }
            if (!context.Roles.Any(e => e.Name == "Document"))
            {
                context.Roles.Add(new Role { Name = "Document", NormalizedName = "DOCUMENT", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Quản lý tài liệu" });
                await context.SaveChangesAsync();
            }
            if (!context.Roles.Any(e => e.Name == "Video"))
            {
                context.Roles.Add(new Role { Name = "Video", NormalizedName = "VIDEO", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Quản lý Video" });
                await context.SaveChangesAsync();
            }
            if (!context.Roles.Any(e => e.Name == "Picture"))
            {
                context.Roles.Add(new Role { Name = "Picture", NormalizedName = "PICTURE", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Quản lý hình ảnh" });
                await context.SaveChangesAsync();
            }
            if (!context.Roles.Any(e => e.Name == "Audio"))
            {
                context.Roles.Add(new Role { Name = "Audio", NormalizedName = "AUDIO", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Quản lý Audio" });
                await context.SaveChangesAsync();
            }

            if (!context.Roles.Any(e => e.Name == "Event"))
            {
                context.Roles.Add(new Role { Name = "Event", NormalizedName = "EVENT", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Quản lý sự kiện" });
                await context.SaveChangesAsync();
            }

            if (!context.Roles.Any(e => e.Name == "Config"))
            {
                context.Roles.Add(new Role { Name = "Config", NormalizedName = "CONFIG", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Cấu hình website" });
                await context.SaveChangesAsync();
            }

            if (!context.Roles.Any(e => e.Name == "ActionByThemself"))
            {
                context.Roles.Add(new Role { Name = "ActionByThemself", NormalizedName = "ACTIONBYTHEMSELF", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString(), Description = "Không có quyền trên nội dung không phải do mình tạo ra" });
                await context.SaveChangesAsync();
            }

            //var tags = await context.ItemTags.Where(e => e.TagName.Contains(",")).ToListAsync();
            //foreach(var tag in tags)
            //{
            //    var names = tag.TagName.Split(",");
            //    foreach(var name in names)
            //    {
            //        var checkExist = await context.ItemTags.AnyAsync(e => e.ItemId == tag.ItemId && e.TagName == name);
            //        if (!checkExist)
            //        {
            //            context.ItemTags.Add(new ItemTag { TagName = name, ItemId = tag.ItemId });
            //            await context.SaveChangesAsync();
            //        }    
            //    }
            //    context.ItemTags.Remove(tag);
            //    await context.SaveChangesAsync();
            //}

            //var seos = await context.SEOs.ToListAsync();
            //foreach (var seo in seos)
            //{
            //    if (seo.ItemId == null) context.SEOs.Remove(seo);
            //    else
            //    {
            //        var url = seo.Url;
            //        if (url.Contains("sdoc")) url = url.Replace("sdoc", "smid");
            //        var index = url.LastIndexOf("-vit-");
            //        var title = url.Substring(0, index);
            //        var query = url.Substring(index + 5);
            //        var arr = query.Split('-');
            //        var component = arr[arr.Length - 1];
            //        var send = arr[0];
            //        seo.Url = $"/{component}{title}/{send}/{seo.ItemId}";
            //        context.SEOs.Update(seo);
            //    }
            //    await context.SaveChangesAsync();
            //}    
        }
    }
}
