using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Models.Enums;
using Web.Models.SeedWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Web.Api.Repositories;
using System.Security.Policy;
using System.Xml;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly UserManager<User> _userManager;
        private readonly ISEORepository _sEORepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IConfiguration _config;
        public FilesController(IWebHostEnvironment env, 
            UserManager<User> userManager, 
            ISEORepository sEORepository,
            ICompanyRepository companyRepository,
            IConfiguration config)
        {
            this.env = env;
            this._userManager = userManager;
            this._sEORepository = sEORepository;
            this._companyRepository = companyRepository;
            this._config = config;
        }

        //[HttpPost]
        //public async Task Post([FromBody] FileData[] files)
        //{
        //    foreach (var file in files)
        //    {
        //        var buf = Convert.FromBase64String(file.Base64data);
        //        var path = Path.Combine(env.ContentRootPath, file.FilePath);
        //        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        //        await System.IO.File.WriteAllBytesAsync(path + System.IO.Path.DirectorySeparatorChar + file.FileName, buf);
        //    }
        //}

        [HttpGet]
        public async Task<List<FileData>> Get()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var fileHelper = new FileHelper(env.ContentRootPath, user.CompanyId.ToString());
            var fileNames = fileHelper.Gets();
            var files = fileNames.Select(e => new FileData()
            {
                Type = FileType.UploadImage,
                FileName = e,
                Folder = fileHelper.Folder
            }).OrderByDescending(e => e.FileName);
            return files.ToList();
        }


        [HttpPost]
        public async Task<FileData> Post([FromBody] FileData file)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var fileHelper = new FileHelper(file, env.ContentRootPath, user.CompanyId.ToString());

            if (file.Base64data != null)
            {
                // trùng tên thì đổi tên
                do
                {
                    fileHelper.File.FileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_{DateTime.Now.Millisecond}_{file.FileName}";
                }
                while (fileHelper.CheckExist());
                await fileHelper.Save();
            }
            else
            {
                // delete the file if necessary
                if (file.FirstUpload == true && fileHelper.CheckExist())
                    fileHelper.Delete();
                await fileHelper.SaveChunk();
            }

            file.Folder = user.CompanyId.ToString();
            return file;
        }

        [HttpDelete]
        [Route("{filename}")]
        public async Task Delete(string filename)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var file = new FileData() { FileName = filename, Type = FileType.UploadImage };
            var fileHelper = new FileHelper(file, env.ContentRootPath, user.CompanyId.ToString());
            fileHelper.Delete();
        }

        [HttpGet]
        [Route("sitemap")]
        public async Task<FileData> GetSiteMap()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var file = new FileData { FileName = "sitemap.xml", Type = FileType.SiteMap, Folder = user.CompanyId.ToString() };
            var fileHelper = new FileHelper(file, env.ContentRootPath, user.CompanyId.ToString());

            var exist = fileHelper.CheckExist();
            if (!exist) file.FileName = string.Empty;
            else
            {
                var path = Path.Combine(env.ContentRootPath, file.FullPath);
                var bytes = System.IO.File.ReadAllBytes(path);
                file.Base64data = Convert.ToBase64String(bytes);
            }

            return file;
        }

        [HttpPost]
        [Route("sitemap")]
        public async Task<FileData> CreateSiteMap()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var file = new FileData { FileName = "sitemap.xml", Type = FileType.SiteMap, Folder = user.CompanyId.ToString() };
            var path = Path.Combine(env.ContentRootPath, file.FullPath);

            var domains = await _companyRepository.GetDomains(user.CompanyId);
            var domainLanguagesCount = domains.Select(e => e.LanguageCode).Distinct().Count();

            var links = await _sEORepository.GetAllSEOs(user.CompanyId);
            var languages = links.Select(e => e.LanguageCode).Distinct().ToList();

            using (var writer = XmlWriter.Create(path))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                var domain = string.Empty;
                if (domainLanguagesCount == 1)
                {
                    domain = domains.Where(e => e.LanguageCode == languages[0]).Select(e => e.Domain).FirstOrDefault(e => !e.StartsWith("www") && !e.StartsWith("localhost"));
                    domain = "https://" + domain;
                    WriteTag("1", "Monthly", domain, writer);
                }

                foreach (var lang in languages)
                {
                    if (domainLanguagesCount > 1)
                    {
                        domain = domains.Where(e => e.LanguageCode == lang)
                            .Select(e => e.Domain).FirstOrDefault(e => !e.StartsWith("www") && !e.StartsWith("localhost"));
                        if (domain != null)
                        {
                            domain = "https://" + domain;
                            WriteTag("1", "Monthly", domain, writer);
                        }
                    }

                    // tao sitemap
                    var maps = new List<MapItem>();
                    foreach (var url in links.Where(e => e.LanguageCode == lang))
                    {
                        var mapItem = new MapItem();
                        mapItem.Navigation = domain + url.SeoUrl;

                        if (url.Url.ToLower().Contains("/scat/"))
                        {
                            mapItem.Freq = "Daily";
                            mapItem.Priority = "0.8";
                        }
                        else if (url.Url.ToLower().Contains("/satrvl") || url.Url.ToLower().Contains("/tag"))
                        {
                            mapItem.Freq = "Monthly";
                            mapItem.Priority = "0.6";
                        }
                        else
                        {
                            mapItem.Freq = "Yearly";
                            mapItem.Priority = "0.2";
                        }

                        maps.Add(mapItem);
                    }
                    maps = maps.OrderBy(e => e.Priority).ToList();
                    foreach (var item in maps)
                    {
                        WriteTag(item.Priority, item.Freq, item.Navigation, writer);
                    }
                }
                writer.WriteEndDocument();
                writer.Close();
            }

            // luu ra web ngoai thong qua ftp
            var fileHelper = new FileHelper(file, env.ContentRootPath, user.CompanyId.ToString());
            var exist = fileHelper.CheckExist();
            if (!exist) file.FileName = string.Empty;
            else
            {
                var bytes = System.IO.File.ReadAllBytes(path);
                file.Base64data = Convert.ToBase64String(bytes);
            }

            var ftp = new FTPHelper();
            ftp.FTPServerIP = _config["FTPServerIP"];
            ftp.FTPRootPath = _config["FTPRootPath"];
            ftp.FTPUserID = _config["FTPUserID"];
            ftp.FTPassword = _config["FTPPassword"];
            ftp.Upload(file);

            return file;
        }

        /// <summary>
        /// The write tag.
        /// </summary>
        /// <param name="priority">
        /// The priority.
        /// </param>
        /// <param name="freq">
        /// The freq.
        /// </param>
        /// <param name="navigation">
        /// The navigation.
        /// </param>
        /// <param name="myWriter">
        /// The my writer.
        /// </param>
        private void WriteTag(string priority, string freq, string navigation, XmlWriter myWriter)
        {
            myWriter.WriteStartElement("url");

            myWriter.WriteStartElement("loc");
            myWriter.WriteValue(navigation);
            myWriter.WriteEndElement();

            myWriter.WriteStartElement("lastmod");
            myWriter.WriteValue(String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            myWriter.WriteEndElement();

            myWriter.WriteStartElement("changefreq");
            myWriter.WriteValue(freq);
            myWriter.WriteEndElement();

            myWriter.WriteStartElement("priority");
            myWriter.WriteValue(priority);
            myWriter.WriteEndElement();

            myWriter.WriteEndElement();
        }

        private class MapItem
        {
            public string Priority { get; set; }
            public string Freq { get; set; }
            public string Navigation { get; set; }
        }
    }
}
