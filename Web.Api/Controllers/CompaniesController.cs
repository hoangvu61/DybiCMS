using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Repositories;
using Web.Models;
using Web.Models.Enums;
using Web.Models.SeedWork;
using System.Security.Cryptography;
using System.Text;
using Dybi.Library;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompaniesController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly ICompanyRepository _companyRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IWebInfoRepository _webinfoRepository;
        private readonly UserManager<User> _userManager;

        public CompaniesController(ICompanyRepository companyRepository,
            IWebInfoRepository webinfoRepository,
            UserManager<User> userManager,
            IWebHostEnvironment env,
            ITemplateRepository templateRepository)
        {
            _companyRepository = companyRepository;
            _webinfoRepository = webinfoRepository;
            _userManager = userManager;
            this.env = env;
            _templateRepository = templateRepository;
        }

        #region company
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingParameters paging)
        {
            var pagedList = await _companyRepository.GetCompanyList(paging);
            return Ok(pagedList);
        }

        [HttpGet]
        [Route("demos/{template}")]
        public async Task<IActionResult> GetDomainByTemplate([FromRoute] string template)
        {
            var pagedList = await _companyRepository.GetDomainsByTemplate(template);
            return Ok(pagedList);
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetByUser([FromQuery] string languageCode)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var dto = await _companyRepository.GetWebInfo(user.CompanyId, languageCode);
            return Ok(dto);
        }

        [HttpGet]
        [Route("me/webinfo/{language}")]
        public async Task<IActionResult> GetWebInfo([FromRoute] string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var webInfo = await _webinfoRepository.GetWebInfo(user.CompanyId, language);

            var dto = new SEOWebdto();
            if (webInfo != null)
            {
                dto.Title = webInfo.Title;
                dto.LanguageCode = webInfo.LanguageCode;
                dto.MetaDescription = webInfo.Brief;
                dto.MetaKeyWord = webInfo.Keywords;
            };

            return Ok(dto);
        }

        [HttpPut]
        [Route("me")]
        public async Task<IActionResult> UpdateCompanyInfo([FromBody] WebInfoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _companyRepository.UpdateWebInfo(user.CompanyId, request);

            return Ok(result);
        }

        [HttpPut]
        [Route("me/webinfo")]
        public async Task<IActionResult> UpdateWebInfo([FromBody] SEOWebdto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var webInfo = await _webinfoRepository.GetWebInfo(user.CompanyId, request.LanguageCode);
            if (webInfo == null)
            {
                webInfo = new WebInfo()
                {
                    CompanyId = user.CompanyId,
                    LanguageCode = request.LanguageCode,
                    Title = request.Title,
                    Brief = request.MetaDescription,
                    Keywords = request.MetaKeyWord,
                };
                await _webinfoRepository.CreateWebInfo(webInfo);
            }
            else
            {
                webInfo.Title = request.Title;
                webInfo.Brief = request.MetaDescription;
                webInfo.Keywords = request.MetaKeyWord;
                await _webinfoRepository.UpdateWebInfo(webInfo);
            }

            return Ok(webInfo);
        }

        [HttpPut]
        [Route("me/webconfig")]
        public async Task<IActionResult> UpdateWebConfig([FromBody] WebConfigRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            // nền là màu hoặc không nền thì xóa hình nền cũ nếu có
            if (request.Background != null)
            {
                var webconfig = await _companyRepository.GetWebConfig(user.CompanyId);
                if (!string.IsNullOrEmpty(webconfig.Background) && !webconfig.Background.StartsWith('#'))
                {
                    var path = Path.Combine(env.ContentRootPath, FilePath.Background, webconfig.Background);
                    path = string.Format(path, user.CompanyId);
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    if (System.IO.File.Exists(path + ".webp")) System.IO.File.Delete(path + ".webp");
                }
            }

            var result = await _companyRepository.UpdateWebConfig(user.CompanyId, request);

            return Ok(result);
        }

        [HttpPut]
        [Route("me/upload")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task UpdateWebImage([FromBody] FileData file)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var oldFile = string.Empty;
            switch (file.Type)
            {
                case FileType.WebLogo:
                    var company = await _companyRepository.GetCompany(user.CompanyId);
                    oldFile = company.Image;
                    company.Image = file.FileName;
                    await _companyRepository.Update(company);
                    break;
                case FileType.WebIcon:
                    var webconfig = await _companyRepository.GetWebConfig(user.CompanyId);
                    oldFile = webconfig.WebIcon;
                    webconfig.WebIcon = file.FileName;
                    await _companyRepository.UpdateWebConfig(webconfig);
                    break;
                case FileType.WebImage:
                    var webimage = await _companyRepository.GetWebConfig(user.CompanyId);
                    oldFile = webimage.Image;
                    webimage.Image = file.FileName;
                    await _companyRepository.UpdateWebConfig(webimage);
                    break;
                case FileType.Background:
                    var background = await _companyRepository.GetWebConfig(user.CompanyId);
                    oldFile = background.Background;
                    background.Background = file.FileName;
                    await _companyRepository.UpdateWebConfig(background);
                    break;
            }

            var fileHelper = new FileHelper(oldFile, file, env.ContentRootPath, user.CompanyId.ToString());
            await fileHelper.Save();
        }

        [HttpDelete]
        [Route("{companyid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid companyid)
        {
            var result = await _companyRepository.Delete(companyid);
            if (result)
            {
                var fileHelper = new FileHelper(env.ContentRootPath, companyid.ToString());
                fileHelper.DeleteFolder();
            } 
                
            return Ok(new CompanyDto() { Id = companyid });
        }
        #endregion

        #region language
        [HttpGet]
        [Route("me/languages")]
        public async Task<IActionResult> GetMyLanguages()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var languages = await _companyRepository.GetLanguages(user.CompanyId);
            var dtos = languages.Select(e => e.LanguageCode);
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{companyid}/languages")]
        public async Task<IActionResult> GetLanguages(Guid companyid)
        {
            var languages = await _companyRepository.GetLanguages(companyid);
            var dtos = languages.Select(e => e.LanguageCode);
            return Ok(dtos);
        }

        [HttpPost]
        [Route("{companyid}/languages")]
        public async Task<IActionResult> CreateLanguage(Guid companyid, [FromBody] CompanyLanguageDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var company = await _companyRepository.GetCompany(companyid);
            if (company == null) return NotFound($"Company {companyid} không tồn tại");

            var obj = await _companyRepository.CreateLanguage(new CompanyLanguage
            {
                LanguageCode = request.LanguageCode,
                CompanyId = companyid
            });

            return Ok(obj.LanguageCode);
        }

        [HttpDelete]
        [Route("{companyid}/languages/{language}")]
        public async Task<IActionResult> DeleteLanguage([FromRoute] Guid companyid, [FromRoute] string language)
        {
            var obj = await _companyRepository.GetLanguage(companyid, language);
            if (obj == null) return NotFound($"{companyid}.{language} không tồn tại");

            await _companyRepository.DeleteLanguage(obj);
            return Ok(obj.LanguageCode);
        }
        #endregion

        #region language config
        [HttpGet]
        [Route("me/languages/{language}/configs")]
        public async Task<IActionResult> GetLanguageConfigs(string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            
            var webconfig = await _companyRepository.GetWebConfig(user.CompanyId);
            if (webconfig == null) return NotFound($"Company {user.CompanyId} không tồn tại");

            var configs = await _companyRepository.GetLanguageConfigs(user.CompanyId, language);
            var dtos = configs.Select(e => new CompanyLanguageConfigDto
            {
                LanguageCode = e.LanguageCode,
                LanguageKey = e.LanguageKey,
                Describe = e.Describe,
            }).ToList();


            var languageKeys = await _templateRepository.GetLanguageKeys(webconfig.TemplateName, new PagingParameters { PageSize = 0 });
            var keys = languageKeys.Items.Select(e => e.LanguageKey).ToList();
            var notexists = configs.Where(e => !keys.Contains(e.LanguageKey)).ToList();
            foreach(var not in notexists)
            {
                await _companyRepository.DeleteLanguageConfig(not);
                dtos.RemoveAll(e => e.LanguageKey == not.LanguageKey);
            }

            var exists = keys.Where(e => !dtos.Select(d => d.LanguageKey).Contains(e)).ToList();
            foreach(var exist in exists)
            {
                dtos.Add(new CompanyLanguageConfigDto
                {
                    LanguageCode = language,
                    LanguageKey = exist,
                    Describe = ""
                });
            }    


            return Ok(dtos);
        }

        [HttpPost]
        [Route("me/languages/configs")]
        public async Task<IActionResult> UpdateLanguageConfig([FromBody] CompanyLanguageConfigDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _companyRepository.GetLanguageConfig(user.CompanyId, request.LanguageKey, request.LanguageCode);
            if (obj == null)
            {
                var check = await _companyRepository.CheckLanguageConfigs(user.CompanyId, request.LanguageKey);
                if (!check) return NotFound($"{request.LanguageKey} không tồn tại");

                obj = await _companyRepository.CreateLanguageConfig(new CompanyLanguageConfig
                {
                    CompanyId = user.CompanyId,
                    LanguageCode = request.LanguageCode,
                    LanguageKey = request.LanguageKey,
                    Describe = request.Describe

                });
            }
            else
            {
                obj.Describe = request.Describe;
                await _companyRepository.UpdateLanguageConfig(obj);
            }

            return Ok(request);
        }

        #endregion

        #region domain
        [HttpGet]
        [Route("{companyid}/domains")]
        public async Task<IActionResult> GetDomains(Guid companyid)
        {
            var domains = await _companyRepository.GetDomains(companyid);
            var domainDtos = domains.Select(e => new CompanyDomainDto()
            {
                Domain = e.Domain,
                CompanyId = e.CompanyId,
                LanguageCode = e.LanguageCode
            });
            return Ok(domainDtos);
        }

        [HttpGet]
        [Route("domains/{domain}")]
        public async Task<IActionResult> GetDomain(string domain)
        {
            domain = WebUtility.UrlDecode(domain);
            var obj = await _companyRepository.GetDomain(domain);
            if (obj == null) return NotFound($"{domain} không tồn tại");
            return Ok(new CompanyDomainDto()
            {
                Domain = obj.Domain,
                CompanyId = obj.CompanyId,
                LanguageCode = obj.LanguageCode
            });
        }

        [HttpPost]
        [Route("{companyid}/domains")]
        public async Task<IActionResult> CreateDomain(Guid companyid, [FromBody] CompanyDomainDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var company = await _companyRepository.GetCompany(companyid);
            if (company == null) return NotFound($"Company {companyid} không tồn tại");

            var obj = await _companyRepository.CreateDomain(new CompanyDomain
            {
                Domain = request.Domain,
                LanguageCode = request.LanguageCode,
                CompanyId = companyid
            });

            return CreatedAtAction(nameof(GetDomain), new { companyid = obj.CompanyId, domain = obj.Domain }, request);
        }

        [HttpPut]
        [Route("{companyid}/domains/{domain}")]
        public async Task<IActionResult> UpdateDomain(Guid companyid, string domain, [FromBody] CompanyDomainDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var obj = await _companyRepository.GetDomain(domain);
            if (obj == null) return NotFound($"{companyid}.{domain} không tồn tại");

            obj.LanguageCode = request.LanguageCode;

            var paramResult = await _companyRepository.UpdateDomain(obj);

            return Ok(new CompanyDomainDto()
            {
                Domain = obj.Domain,
                CompanyId = obj.CompanyId,
                LanguageCode = obj.LanguageCode
            });
        }

        [HttpDelete]
        [Route("{companyid}/domains/{domain}")]
        public async Task<IActionResult> DeleteDomain([FromRoute] Guid companyid, [FromRoute] string domain)
        {
            var obj = await _companyRepository.GetDomain( domain);
            if (obj == null) return NotFound($"{companyid}.{domain} không tồn tại");

            domain = WebUtility.UrlDecode(domain);
            await _companyRepository.DeleteDomain(obj);
            return Ok(new CompanyDomainDto()
            {
                Domain = obj.Domain,
                CompanyId = obj.CompanyId,
                LanguageCode = obj.LanguageCode
            });
        }
        #endregion

        #region branch
        [HttpGet]
        [Route("me/branches")]
        public async Task<IActionResult> GetBranches([FromQuery] string languageCode)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var branches = await _companyRepository.GetBranches(user.CompanyId, languageCode);
            var domainDtos = branches.OrderBy(e => e.Order).Select(e => new CompanyBranchDto()
            {
                Id = e.Id,
                Address = e.Address,
                Email = e.Email,
                Name = e.Name,
                Phone = e.Phone,
                LanguageCode = e.LanguageCode
            });
            return Ok(domainDtos);
        }

        [HttpGet]
        [Route("me/branches/{branchid}")]
        public async Task<IActionResult> GetBranch(Guid branchid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _companyRepository.GetBranch(user.CompanyId, branchid);
            if (obj == null) return NotFound($"{user.CompanyId}.{branchid} không tồn tại");
            return Ok(new CompanyBranchDto()
            {
                Id = obj.Id,
                Address = obj.Address,
                Email = obj.Email,
                Name = obj.Name,
                Phone = obj.Phone,
                LanguageCode = obj.LanguageCode,
                Order = obj.Order
            });
        }

        [HttpPost]
        [Route("me/branches")]
        public async Task<IActionResult> CreateBranch([FromBody] CompanyBranchDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var company = await _companyRepository.GetCompany(user.CompanyId);
            if (company == null) return NotFound($"Company {user.CompanyId} không tồn tại");

            var obj = await _companyRepository.CreateBranch(new CompanyBranch
            {
                CompanyId = company.Id,
                LanguageCode = request.LanguageCode,
                Id = request.Id,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                Name = request.Name,
                Order = request.Order
            });

            return CreatedAtAction(nameof(GetBranch), new { branchid = obj.Id }, request);
        }

        [HttpPut]
        [Route("me/branches/{branchid}")]
        public async Task<IActionResult> UpdateBranch(Guid branchid, [FromBody] CompanyBranchDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _companyRepository.GetBranch(user.CompanyId, branchid);
            if (obj == null) return NotFound($"{user.CompanyId}.{branchid} không tồn tại");

            obj.LanguageCode = request.LanguageCode;
            obj.Name = request.Name;
            obj.Address = request.Address;  
            obj.Phone = request.Phone;
            obj.Email = request.Email;
            obj.Order = request.Order;

            await _companyRepository.UpdateBranch(obj);

            return Ok(new CompanyBranchDto()
            {
                Id = obj.Id,
                LanguageCode = obj.LanguageCode,
                Name = obj.Name,
                Address = obj.Address,
                Phone = obj.Phone,
                Email = obj.Email
            });
        }

        [HttpDelete]
        [Route("me/branches/{branchid}")]
        public async Task<IActionResult> DeleteBranch(Guid branchid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _companyRepository.GetBranch(user.CompanyId, branchid);
            if (obj == null) return NotFound($"{user.CompanyId}.{branchid} không tồn tại");

            var countBranches = await _companyRepository.CountBranches(user.CompanyId, obj.LanguageCode);
            if (countBranches < 2) return BadRequest("Phải có ít nhất 1 chi nhánh");

            await _companyRepository.DeleteBranch(obj);
            return Ok(new CompanyBranchDto()
            {
                Id = obj.Id,
                LanguageCode = obj.LanguageCode,
                Name = obj.Name,
                Address = obj.Address,
                Phone = obj.Phone,
                Email = obj.Email
            });
        }
        #endregion

        #region tra cuu mst
        [AllowAnonymous]
        [HttpGet]
        [Route("taxes/{taxcode}")]
        public async Task<IActionResult> GetCompanyByTax(string taxcode)
        {
            var map = CreatePasswordDigrest("m0nkey62", "290893326");

            var url = "https://esb.gdt.gov.vn:443/t2b/gip_prod/request";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var body = @"<soapenv:Envelope xmlns:gip=""http://gip.request.com/"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
            " + @"<soapenv:Header>
            " + @" <wsse:Security soapenv:mustUnderstand=""1"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
            " + @" <wsse:UsernameToken wsu:Id=""UsernameToken-218B1070E7DEF4C28216430981626917"">
            " + @" <wsse:Username>" + map["strUsername"] + @"</wsse:Username>
            " + @"				<wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest"">" + map["strPassword"] + @"</wsse:Password>
            " + @"				<wsse:Nonce EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"">" + map["strNonce"] + @"</wsse:Nonce>
            " + @"				<wsu:Created>" + map["strCreated"] + @"</wsu:Created>
            " + @"			</wsse:UsernameToken>
            " + @"		</wsse:Security>
            " + @"	</soapenv:Header>
            " + @"	<soapenv:Body>
            " + @"		<gip:xuLyTruyVanMsg>
            " + @"			<in_msg><![CDATA[<DATA>
            " + @"	<HEADER>
            " + @"		<VERSION>1.0</VERSION>
            " + @"		<SENDER_CODE>" + map["strUsername"] + @"</SENDER_CODE>
            " + @"		<SENDER_NAME>" + map["strUsername"] + @"</SENDER_NAME>
            " + @"		<RECEIVER_CODE>GIP</RECEIVER_CODE>
            " + @"		<RECEIVER_NAME>Hệ Thống GIP</RECEIVER_NAME>
            " + @"		<TRAN_CODE>" + RandomNumber(5) + @"</TRAN_CODE>
            " + @"		<MSG_ID>" + RandomNumber(22) + @"</MSG_ID>
            " + @"		<MSG_REFID/>
            " + @"		<ID_LINK/>
            " + @"		<SEND_DATE>" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + @"</SEND_DATE>
            " + @"		<ORIGINAL_CODE/>
            " + @"		<ORIGINAL_NAME/>
            " + @"		<ORIGINAL_DATE/>
            " + @"		<ERROR_CODE/>
            " + @"		<ERROR_DESC/>
            " + @"		<SPARE1/>
            " + @"		<SPARE2/>
            " + @"		<SPARE3/>
            " + @"	</HEADER>
            " + @"	<BODY>
            " + @"		<ROW>
            " + @"			<TRUYVAN>
            " + @"				<MST>" + taxcode + @"</MST>
            " + @"			</TRUYVAN>
            " + @"			<TYPE>00003</TYPE>
            " + @"			<NAME>Truy vấn theo MST</NAME>
            " + @"		</ROW>
            " + @"	</BODY>
            " + @"	<SECURITY/>
            " + @"</DATA>]]></in_msg>
            " + @"		</gip:xuLyTruyVanMsg>
            " + @"	</soapenv:Body>
            " + @"</soapenv:Envelope>";

            var content = new StringContent(body);
            var responseMessage = client.PostAsync(url, content).Result;
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = responseMessage.Content;
                var readStream = responseContent.ReadAsStreamAsync().Result;
                var streamReader = new StreamReader(readStream);
                var result = streamReader.ReadToEnd();
                var xmlResult = new XMLHelper(result);
                var data = xmlResult.GetValue("return");
                var xmlData = new XMLHelper(data);


                var nnt = new NNTModel();
                nnt.TRANG_THAI = xmlData.GetValue("TRANG_THAI");
                nnt.MST = xmlData.GetValue("MST");
                nnt.TEN_NNT = xmlData.GetValue("TEN_NNT");
                nnt.LOAI_NNT = xmlData.GetValue("LOAI_NNT");
                nnt.NGAYCAP_MST = xmlData.GetValue("NGAYCAP_MST");
                nnt.SO = xmlData.GetValue("SO");
                nnt.CQT_QL = xmlData.GetValue("CQT_QL");
                nnt.CAP_CHUONG = xmlData.GetValue("CAP_CHUONG");
                nnt.CHUONG = xmlData.GetValue("CHUONG");
                nnt.CHAN_DATE = xmlData.GetValue("CHAN_DATE");
                nnt.MOTA_DIACHI = xmlData.GetValue("MOTA_DIACHI");
                nnt.MA_TINH = xmlData.GetValue("MA_TINH");
                nnt.MA_HUYEN = xmlData.GetValue("MA_HUYEN");

                nnt.Loai = DataSource.LoaiNNTs[nnt.LOAI_NNT];
                nnt.TrangThai = DataSource.TrangThaiNNTs[nnt.TRANG_THAI];

                //var districts = provinceBLL.GetAllDistrict().ToList().Select(e =>
                //{
                //    e.TEN = e.TEN.Replace("Chi cục thuế ", "")
                //                                .Replace("Chi cục Thuế ", "")
                //                                .Replace("Cục thuế ", "").Trim();
                //    return e;
                //}).ToList();

                //if (!string.IsNullOrEmpty(nnt.MOTA_DIACHI)) nnt.MOTA_DIACHI = nnt.MOTA_DIACHI.Replace(" (hết hiệu lực)", "");
                //if (nnt.MA_TINH == "79TTT" && nnt.MA_HUYEN == null) nnt.MA_HUYEN = "762HH";

                //nnt.Tinh = districts.Where(e => e.MA_DBHC_KB == nnt.MA_TINH).Select(e => e.TEN).FirstOrDefault();
                //nnt.Huyen = districts.Where(e => e.MA_DBHC_KB == nnt.MA_HUYEN).Select(e => e.TEN).FirstOrDefault();
                //nnt.DiaChi = nnt.MOTA_DIACHI + ", " + nnt.Huyen + ", " + nnt.Tinh;
                //nnt.DiaChi = nnt.DiaChi.Replace(" ,", ",");
                return Ok(nnt);
            }
            return NoContent();
        }

        private Dictionary<string, string> CreatePasswordDigrest(string username, string password)
        {
            var mapInfo = new Dictionary<string, string>();
            // From the spec: Password_Digest = Base64(SHA-1(nonce + created + password))
            try
            {
                var nonceBytes = new byte[16];
                var rng = new RNGCryptoServiceProvider();
                rng.GetBytes(nonceBytes);

                // Make the created date
                string createdDate = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
                var createdDateBytes = Encoding.ASCII.GetBytes(createdDate);

                // Make the password
                var passwordBytes = Encoding.ASCII.GetBytes(password);

                // SHA-1 hash the bunch of it.
                byte[] digestedPassword;
                using (var stream = new MemoryStream())
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        writer.Write(nonceBytes);
                        writer.Write(createdDateBytes);
                        writer.Write(passwordBytes);
                    }

                    SHA1 sha1 = SHA1Managed.Create();
                    digestedPassword = sha1.ComputeHash(stream.ToArray());
                }

                // Encode the password and nonce for sending
                string passwordB64 = Convert.ToBase64String(digestedPassword);
                string nonceB64 = Convert.ToBase64String(nonceBytes);

                // ----------return------------
                mapInfo.Add("strUsername", username);
                mapInfo.Add("strPassword", passwordB64);
                mapInfo.Add("strNonce", nonceB64);
                mapInfo.Add("strCreated", createdDate);
            }
            catch (Exception e)
            {
                return null;
            }

            return mapInfo;
        }

        private string RandomNumber(int size)
        {
            Random random = new Random();
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < size; i++)
                s.Append(random.Next(10).ToString());
            return s.ToString();
        }
        #endregion
    }
}
