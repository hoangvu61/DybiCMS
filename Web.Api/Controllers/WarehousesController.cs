using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Repositories;
using Web.Models;
using Web.Models.Enums;
using Web.Models.SeedWork;
using ZXing;
using ZXing.Rendering;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WarehousesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;

        public WarehousesController(UserManager<User> userManager, IWarehouseRepository warehouseRepository, IItemRepository itemRepository, IOrderRepository orderRepository)
        {
            _warehouseRepository = warehouseRepository;
            _userManager = userManager;
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
        }

        #region config
        [HttpGet]
        [Route("configs/default")]
        public async Task<IActionResult> GetConfigDefaults()
        {
            var list = await _warehouseRepository.GetConfigDefaults();
            var dtos = list.Select(e => new ConfigDefaultDto()
            {
                Key = e.Key,
                Type = e.Type,
                Value = e.DefaultValue,
                Describe = e.Describe
            });
            return Ok(dtos);
        }

        [HttpGet]
        [Route("configs")]
        public async Task<IActionResult> GetConfigs()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var list = await _warehouseRepository.GetConfigs(user.CompanyId);
            var dtos = list.Select(e => new ConfigDto()
            {
                Key = e.Key,
                Value = e.Value
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("configs/{key}")]
        public async Task<IActionResult> GetConfig(string key)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var value = await _warehouseRepository.GetConfig(user.CompanyId, key);
            return Ok(value);
        }

        [HttpPut]
        [Route("configs/{key}/{value}")]
        public async Task<IActionResult> UpdateConfig([FromRoute] string key, string value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _warehouseRepository.SetConfig(user.CompanyId, key, value);

            var returnData = new ConfigDto();
            if (obj != null)
            {
                returnData.Key = obj.Key;
                returnData.Value = obj.Value?.Trim();
            }
            return Ok(returnData);
        }
        #endregion

        #region kho
        [HttpGet]
        public async Task<IActionResult> GetWarehouses()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var list = await _warehouseRepository.GetWarehouses(user.CompanyId);
            var dtos = list.Select(e => new WarehouseDto()
            {
                Id = e.Id,
                Type = e.Type,
                Address = e.Address,
                Email = e.Email,
                Name = e.Name,
                Phone = e.Phone,
                IsActive = e.IsActive
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetWarehouse(Guid id)
        {
            var obj = await _warehouseRepository.GetWarehouse(id);
            if (obj == null) return BadRequest($"Kho [{id}] không tồn tại");
            return Ok(new WarehouseDto()
            {
                Id = obj.Id,
                Type = obj.Type,
                Address = obj.Address,
                Email = obj.Email,
                Name = obj.Name,
                Phone = obj.Phone,
                IsActive = obj.IsActive
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _warehouseRepository.CreateWarehouse(new Warehouse
            {
                Id = request.Id,
                Type = request.Type,
                Address = request.Address?.Trim(),
                Email = request.Email?.Trim(),
                Name = request.Name,
                Phone = request.Phone?.Trim(),
                IsActive = request.IsActive,
                CompanyId = user.CompanyId
            });
            return CreatedAtAction(nameof(GetWarehouse), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateWarehouse(Guid id, [FromBody] WarehouseDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = await _warehouseRepository.GetWarehouse(id);
            if (obj == null) return ValidationProblem($"Kho [{id}] không tồn tại");
            if (obj.Inputs.Count > 0 && obj.Type != request.Type) return ValidationProblem($"Kho [{id}] đã được nhập hàng không thể đổi loại");

            obj.Type = request.Type;
            obj.Address = request.Address?.Trim();
            obj.Email = request.Email?.Trim();
            obj.Name = request.Name;
            obj.Phone = request.Phone?.Trim();
            obj.IsActive = request.IsActive;

            var moduleResult = await _warehouseRepository.UpdateWarehouse(obj);

            return Ok(request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWarehouse([FromRoute] Guid id)
        {
            var obj = await _warehouseRepository.GetWarehouse(id);
            if (obj == null) return ValidationProblem($"Kho [{id}] không tồn tại");
            if (obj.Inputs.Count > 0) return ValidationProblem($"Kho [{id}] đã được nhập hàng không thể xóa");

            var resultData= await _warehouseRepository.DeleteWarehouse(obj);
            return Ok(resultData);
        }


        [HttpGet]
        [Route("inputs")]
        public async Task<IActionResult> GetInputs([FromQuery] WarehouseIOSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var inputs = await _warehouseRepository.GetInputs(user.CompanyId, search);

            var dtos = inputs.Items.Select(e => new WarehouseInputDto()
            {
                Id = e.Id,
                InputCode = e.InputCode,
                TotalPrice = e.TotalPrice,
                Note = e.Note,
                CreateDate = e.CreateDate,
                ProductCount = e.Products.Sum(p => p.Quantity),
                WarehouseId = e.WarehouseId,
                WarehouseType = DataSource.WarehouseTypes[e.Warehouse.Type],
                WarehouseName = e.Warehouse.Name,
                Type = e.Type,
                TypeName = DataSource.WarehouseInputTypes.First(s => s.Key == e.Type).Value,
                Debt = e.Debt?.Debit ?? 0,
                SourceName = e.FromSupplier?.SupplierName ?? e.FromFactory?.FactoryName ?? e.FromOrder?.OrderId.ToString() ?? string.Empty,
            }).ToList();

            return Ok(new PagedList<WarehouseInputDto>(dtos,
                       inputs.MetaData.TotalCount,
                       inputs.MetaData.CurrentPage,
                       inputs.MetaData.PageSize));
        }

        [HttpGet]
        [Route("inputs/{id}")]
        public async Task<IActionResult> GetInput([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var input = await _warehouseRepository.GetInput(user.CompanyId, id);
            if (input == null) return ValidationProblem($"Phiếu nhập kho [{id}] không tồn tại");

            var dto = new WarehouseInputDto();
            dto.Id = input.Id;
            dto.InputCode = input.InputCode;
            dto.TotalPrice = input.TotalPrice;
            dto.Note = input.Note;
            dto.CreateDate = input.CreateDate;
            dto.ProductCount = input.Products.Sum(p => p.Quantity);
            dto.WarehouseId = input.WarehouseId;
            dto.WarehouseType = DataSource.WarehouseTypes[input.Warehouse.Type];
            dto.WarehouseName = input.Warehouse.Name;
            dto.Type = input.Type;
            dto.TypeName = DataSource.WarehouseInputTypes.First(s => s.Key == input.Type).Value;
            dto.Debt = input.Debt?.Debit ?? 0;
            dto.SourceName = input.FromSupplier?.SupplierName ?? input.FromFactory?.FactoryName ?? input.FromOrder?.OrderId.ToString() ?? string.Empty;

            return Ok(dto);
        }

        [HttpPost]
        [Route("inputs")]
        public async Task<IActionResult> CreateInput([FromBody] WarehouseInputRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var code = request.InputCode?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(code))
            {
                var checkExistCode = await _warehouseRepository.CheckExistInputCode(user.CompanyId, code);
                if (checkExistCode) return ValidationProblem($"Mã nhập kho [{code}] đã tồn tại");
            }

            var obj = new WarehouseInput
            {
                Id = request.Id,
                InputCode = request.InputCode?.Trim() ?? request.Id.ToString(),
                WarehouseId = request.WarehouseId,
                Type = request.Type,
                CreateDate = DateTime.Now,
                TotalPrice = request.TotalPrice,
                Note = request.Note,
            };
            
            switch (request.Type)
            {
                case 1:
                    if (request.FromId == null) return ValidationProblem($"Mã NCC không thể NULL");
                    var supplier = await _warehouseRepository.GetSupplier(request.FromId.Value);
                    if (supplier == null) return ValidationProblem($"Không tìm thấy NCC [{request.FromId.Value}]");
                    obj.FromSupplier = new WarehouseInputFromSupplier();
                    obj.FromSupplier.SourceId = request.FromId.Value;
                    obj.FromSupplier.SupplierName = supplier.Name;
                    obj.FromSupplier.SupplierPhone = supplier.Phone;
                    obj.FromSupplier.SupplierEmail = supplier.Email;
                    obj.FromSupplier.SupplierAddress = supplier.Address;

                    if (request.TotalPrice > request.Payment)
                    {
                        obj.Debt = new WarehouseInputDebt();
                        obj.Debt.Debit = request.TotalPrice - request.Payment;
                        obj.Debt.DebitExpire = request.DebitExpire;
                    }

                    break;
                case 2:
                    if (request.FromId == null) return ValidationProblem($"Mã nơi sản xuất không thể NULL");
                    var factory = await _warehouseRepository.GetFactory(request.FromId.Value);
                    if (factory == null) return ValidationProblem($"Không tìm thấy Nơi sản xuất [{request.FromId.Value}]");
                    obj.FromFactory = new WarehouseInputFromFactory();
                    obj.FromFactory.FactoryId = request.FromId.Value;
                    obj.FromFactory.FactoryName = factory.Name;
                    obj.FromFactory.FactoryPhone = factory.Phone;
                    obj.FromFactory.FactoryEmail = factory.Email;
                    obj.FromFactory.FactoryAddress = factory.Address;
                    break;
                case 3:
                    if (request.FromId == null) return ValidationProblem($"Mã kho không thể NULL");
                    var warehouse = await _warehouseRepository.GetWarehouse(request.FromId.Value);
                    if (warehouse == null) return ValidationProblem($"Không tìm thấy Kho [{request.FromId.Value}]");
                    obj.FromWarehouse = new WarehouseInputFromWarehouse();
                    obj.FromWarehouse.WarehouseId = request.FromId.Value;
                    obj.FromWarehouse.WarehouseName = warehouse.Name;
                    obj.FromWarehouse.WarehousePhone = warehouse.Phone;
                    obj.FromWarehouse.WarehouseEmail = warehouse.Email;
                    obj.FromWarehouse.WarehouseAddress = warehouse.Address;
                    break;
                case 4:
                    if (request.FromId == null) return ValidationProblem($"Mã đơn hàng không thể NULL");
                    var order = await _warehouseRepository.GetOutputOrder(user.CompanyId, request.FromId.Value);
                    if (order == null) return ValidationProblem($"Không tìm thấy Hóa đơn [{request.FromId}]");
                    obj.FromOrder = new WarehouseInputFromOrder();
                    obj.FromOrder.OrderId = request.FromId.Value;
                    obj.TotalPrice = order.Products.Sum(p => p.Price * p.Quantity);
                    foreach (var outputProduct in order.Products)
                    {
                        obj.Products.Add(new WarehouseInputProduct
                        {
                            Quantity = outputProduct.Quantity,
                            ProductId = outputProduct.ProductId,
                            Price = 0
                        });
                    }
                    break;
            };

            var result = await _warehouseRepository.CreateInput(obj);

            return Ok(result);
        }

        [HttpDelete]
        [Route("inputs/{id}")]
        public async Task<IActionResult> DeleteInput([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var resultData = await _warehouseRepository.DeleteInput(user.CompanyId, id);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem($"[1] Phiếu nhập kho [{id}] không tồn tại");
                case 2: return ValidationProblem($"[2] Phải xóa hết sản phẩm trước");
                case 3: return ValidationProblem("[3] Số lượng hàng tồn ít hơn số lượng sản phẩm cần xóa");
                case 4: return ValidationProblem("[4] Không tồn tại sản phẩm trong đợt nhập hàng");
                case 5: return ValidationProblem("[5] Số lượng hàng tồn trong lô nhập ít hơn số lượng sản phẩm cần xóa");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu sản phẩm");
            }
            return Ok(resultData);
        }


        [HttpGet]
        [Route("inputs/{id}/products")]
        public async Task<IActionResult> GetInputProducts([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var list = await _warehouseRepository.GetInputProducts(user.CompanyId, id);
            var dtos = list.Select(e => new WarehouseProductInputDto()
            {
                Id = e.ProductId,
                Code = e.Product.Code,
                CategoryName = e.Product.CategoryId.ToString(),
                Price = e.Price,
                Quantity = e.Quantity,
                SeriCount = e.Codes.Count(),
            }).ToList();

            var productIds = list.Select(e => e.ProductId).Distinct().ToList();
            var productLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, productIds);
            var categoryIds = list.Select(e => e.Product.CategoryId).Distinct().ToList();
            var categoryLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, categoryIds);

            foreach (var dto in dtos)
            {
                dto.Title = productLanguages.Where(e => e.ItemId == dto.Id && e.LanguageCode == "vi").Select(e => e.Title).FirstOrDefault() ?? string.Empty;
                dto.CategoryName = categoryLanguages.Where(e => e.ItemId == Guid.Parse(dto?.CategoryName ?? string.Empty) && e.LanguageCode == "vi").Select(e => e.Title).FirstOrDefault() ?? string.Empty;
            }

            return Ok(dtos);
        }

        [HttpGet]
        [Route("inputs/{inputid}/products/{productid}")]
        public async Task<IActionResult> GetInputProduct([FromRoute] Guid inputid, [FromRoute] Guid productid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _warehouseRepository.GetInputProduct(user.CompanyId, inputid, productid);
            var dto = new WarehouseProductInputDto()
            {
                Id = obj.ProductId,
                Code = obj.Product.Code,
                CategoryName = obj.Product.CategoryId.ToString(),
                Price = obj.Price,
                Quantity = obj.Quantity,
                SeriCount = obj.Codes.Count(),
            };

            var productLanguage = await _itemRepository.GetItemLanguage(user.CompanyId, dto.Id, "vi");
            dto.Title = productLanguage.Title;

            var categoryLanguage = await _itemRepository.GetItemLanguage(user.CompanyId, obj.Product.CategoryId, "vi");
            dto.CategoryName = categoryLanguage.Title;

            return Ok(dto);
        }

        [HttpPost]
        [Route("inputs/{id}/products")]
        public async Task<IActionResult> CreateInputProduct([FromRoute] Guid id, [FromBody] WarehouseProductInputRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var product = await _itemRepository.GetProduct(user.CompanyId, request.Id);
            if (product == null) return ValidationProblem($"Sản phẩm [{request.Id}] không tồn tại");

            var checkExistProductInput = await _warehouseRepository.CheckExistProductInInput(user.CompanyId, id, request.Id);
            if (checkExistProductInput) return ValidationProblem($"Sản phẩm  [{product.Item.ItemLanguages.Where(e => e.LanguageCode=="vi").Select(e => e.Title).FirstOrDefault()}] đã tồn tại trên phiếu nhập kho");

            var obj = new WarehouseInputProduct
            {
                ProductId = request.Id,
                InputId = id,
                Price = request.Price,
                Quantity = request.Quantity
            };

            var resultData = await _warehouseRepository.CreateInputProduct(obj);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không tồn tại phiếu nhập kho");
                case 2: return ValidationProblem("[2] Không tồn tại sản phẩm");
            }
            return Ok(resultData);
        }

        [HttpDelete]
        [Route("inputs/{inputid}/products/{productid}")]
        public async Task<IActionResult> DeleteInputProduct([FromRoute] Guid inputid, [FromRoute] Guid productid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _warehouseRepository.GetInputProduct(user.CompanyId, inputid, productid);
            if (obj == null) return ValidationProblem($"Sản phẩm [{productid}] không tồn tại trong phiếu nhập kho {inputid}");

            var resultData = await _warehouseRepository.DeleteInputProduct(obj);
            switch(resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không tồn tại phiếu nhập kho");
                case 2: return ValidationProblem("[2] Không tồn tại sản phẩm trong hàng tồn kho");
                case 3: return ValidationProblem("[3] Số lượng hàng tồn ít hơn số lượng sản phẩm cần xóa");
                case 4: return ValidationProblem("[4] Không tồn tại sản phẩm trong đợt nhập hàng");
                case 5: return ValidationProblem("[5] Số lượng hàng tồn trong lô nhập ít hơn số lượng sản phẩm cần xóa");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu sản phẩm");
            }
            return Ok(resultData);
        }


        [HttpGet]
        [Route("inputs/{inputid}/products/{productid}/codes")]
        public async Task<IActionResult> GetInputProductCodes([FromRoute] Guid inputid, [FromRoute] Guid productid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var list = await _warehouseRepository.GetInputProductCodes(user.CompanyId, inputid, productid);
            var codes = list.Select(e => e.ProductCode).ToList();

            return Ok(codes);
        }

        [HttpPost]
        [Route("inputs/{inputid}/products/{productid}/codes")]
        public async Task<IActionResult> CreateInputProduct([FromRoute] Guid inputid, [FromRoute] Guid productid, [FromBody] WarehouseProductCodeRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var product = await _itemRepository.GetProduct(user.CompanyId, productid);
            if (product == null) return ValidationProblem($"Sản phẩm [{productid}] không tồn tại");

            var checkExistProductInput = await _warehouseRepository.CheckExistProductInputCode(user.CompanyId, productid, request.Code);
            if (checkExistProductInput) return ValidationProblem($"Mã [{request.Code}] đã tồn tại trong sản phẩm [{product.Item.ItemLanguages.Where(e => e.LanguageCode == "vi").Select(e => e.Title).FirstOrDefault()}]");

            var obj = new WarehouseInputProductCode
            {
                ProductId = productid,
                InputId = inputid,
                ProductCode = request.Code
            };

            var productInput = await _warehouseRepository.CreateInputProductCode(obj);

            return Ok(productInput);
        }

        [HttpDelete]
        [Route("inputs/{inputid}/products/{productid}/codes/{code}")]
        public async Task<IActionResult> DeleteInputProductCode([FromRoute] Guid inputid, [FromRoute] Guid productid, [FromRoute] string code)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            code = WebUtility.UrlDecode(code);
            var obj = await _warehouseRepository.GetInputProductCode(user.CompanyId, inputid, productid, code);
            if (obj == null) return ValidationProblem($"Mã [{productid}] không tồn tại");

            var resultData = await _warehouseRepository.DeleteInputProductCode(obj);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không tồn tại phiếu nhập kho");
                case 2: return ValidationProblem("[2] Không tồn tại sản phẩm phiếu nhập kho");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu mã");
            }
            return Ok(resultData);
        }


        [HttpGet]
        [Route("outputs")]
        public async Task<IActionResult> GetOutputs([FromQuery] WarehouseIOSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var inputs = await _warehouseRepository.GetOutputs(user.CompanyId, search);
            
            var dtos = inputs.Items.Select(e => new WarehouseOutputDto()
            {
                Id = e.Id,
                Note = e.Note,
                CreateDate = e.CreateDate,
                ProductCount = e.Products.Sum(p => p.Quantity),
                WarehouseId = e.WarehouseId,
                WarehouseType = DataSource.WarehouseTypes[e.Warehouse.Type],
                WarehouseName = e.Warehouse.Name,
                Type = e.Type,
                TypeName = DataSource.WarehouseOutputTypes[e.Type],
                ToName = e.ToSupplier?.SupplierName ?? e.ToFactory?.FactoryName ?? e.ToOrder?.OrderId.ToString() ?? string.Empty,
            }).ToList();

            return Ok(new PagedList<WarehouseOutputDto>(dtos,
                       inputs.MetaData.TotalCount,
                       inputs.MetaData.CurrentPage,
                       inputs.MetaData.PageSize));
        }

        [HttpGet]
        [Route("outputs/{id}")]
        public async Task<IActionResult> GetOutput([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var input = await _warehouseRepository.GetOutput(user.CompanyId, id);
            if (input == null) return ValidationProblem($"Phiếu xuất kho [{id}] không tồn tại");

            var dto = new WarehouseOutputDto();
            dto.Id = input.Id;
            dto.Note = input.Note;
            dto.CreateDate = input.CreateDate;
            dto.ProductCount = input.Products.Sum(p => p.Quantity);
            dto.WarehouseId = input.WarehouseId;
            dto.WarehouseType = DataSource.WarehouseTypes[input.Warehouse.Type];
            dto.WarehouseName = input.Warehouse.Name;
            dto.Type = input.Type;
            dto.TypeName = DataSource.WarehouseOutputTypes[input.Type];
            dto.ToName = input.ToSupplier?.SupplierName ?? input.ToFactory?.FactoryName ?? input.ToOrder?.OrderId.ToString() ?? string.Empty;

            return Ok(dto);
        }

        [HttpPost]
        [Route("outputs")]
        public async Task<IActionResult> CreateOutput([FromBody] WarehouseOutputRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = new WarehouseOutput
            {
                Id = request.Id,
                WarehouseId = request.WarehouseId,
                Type = request.Type,
                CreateDate = DateTime.Now,
                Note = request.Note,
            };

            switch (request.Type)
            {
                case 1:
                    if (request.ToId == null) return ValidationProblem($"Mã đơn hàng không thể NULL");
                    var order = await _orderRepository.GetOrder(user.CompanyId, request.ToId.Value);
                    if (order == null) return ValidationProblem($"Không tìm thấy Hóa đơn [{request.ToId.Value}]");
                    obj.ToOrder = new WarehouseOutputToOrder();
                    obj.ToOrder.OrderId = request.ToId.Value;
                    foreach (var outputProduct in order.Products)
                    {
                        obj.Products.Add(new WarehouseOutputProduct
                        {
                            Quantity = outputProduct.Quantity,
                            ProductId = outputProduct.ProductId,
                        });
                    }
                    break;
                case 2:
                    if (request.ToId == null) return ValidationProblem($"Mã nơi sản xuất không thể NULL");
                    var factory = await _warehouseRepository.GetFactory(request.ToId.Value);
                    if (factory == null) return ValidationProblem($"Không tìm thấy Nơi sản xuất [{request.ToId.Value}]");
                    obj.ToFactory = new WarehouseOutputToFactory();
                    obj.ToFactory.FactoryId = request.ToId.Value;
                    obj.ToFactory.FactoryName = factory.Name;
                    obj.ToFactory.FactoryPhone = factory.Phone;
                    obj.ToFactory.FactoryEmail = factory.Email;
                    obj.ToFactory.FactoryAddress = factory.Address;
                    break;
                case 3:
                    if (request.ToId == null) return ValidationProblem($"Mã kho không thể NULL");
                    var warehouse = await _warehouseRepository.GetWarehouse(request.ToId.Value);
                    if (warehouse == null) return ValidationProblem($"Không tìm thấy Kho [{request.ToId.Value}]");
                    obj.ToWarehouse = new WarehouseOutputToWarehouse();
                    obj.ToWarehouse.WarehouseId = request.ToId.Value;
                    obj.ToWarehouse.WarehouseName = warehouse.Name;
                    obj.ToWarehouse.WarehousePhone = warehouse.Phone;
                    obj.ToWarehouse.WarehouseEmail = warehouse.Email;
                    obj.ToWarehouse.WarehouseAddress = warehouse.Address;
                    break;
                case 4:
                    if (request.ToId == null) return ValidationProblem($"Mã NCC không thể NULL");
                    var supplier = await _warehouseRepository.GetSupplier(request.ToId.Value);
                    if (supplier == null) return ValidationProblem($"Không tìm thấy NCC [{request.ToId.Value}]");
                    obj.ToSupplier = new WarehouseOutputToSupplier();
                    obj.ToSupplier.SourceId = request.ToId.Value;
                    obj.ToSupplier.SupplierName = supplier.Name;
                    obj.ToSupplier.SupplierPhone = supplier.Phone;
                    obj.ToSupplier.SupplierEmail = supplier.Email;
                    obj.ToSupplier.SupplierAddress = supplier.Address;
                    break;
            };

            var resultData = await _warehouseRepository.CreateOutput(user.CompanyId, obj);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Chưa cấu hình");
                case 2: return ValidationProblem("[2] Không tồn tại sản phẩm trong hàng tồn kho");
                case 3: return ValidationProblem("[3] Không đủ hàng tồn kho để xuất");
                case 4: return ValidationProblem("[4] Không tồn tại hàng trong lô nhập");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu sản phẩm");
            }

            return Ok(resultData);
        }

        [HttpDelete]
        [Route("outputs/{id}")]
        public async Task<IActionResult> DeleteOutput([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _warehouseRepository.GetOutput(user.CompanyId, id);
            if (obj == null) return ValidationProblem($"Phiếu xuất kho [{id}] không tồn tại");

            var checkExistProduct = await _warehouseRepository.CheckExistProductInOutputs(user.CompanyId, id, Guid.Empty);
            if (checkExistProduct) return ValidationProblem($"Phải xóa hết sản phẩm trước");

            var resultData = await _warehouseRepository.DeleteOutput(obj);
            return Ok(resultData);
        }


        [HttpGet]
        [Route("outputs/{id}/products")]
        public async Task<IActionResult> GetOutputProducts([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var list = await _warehouseRepository.GetOutputProducts(user.CompanyId, id);
            var dtos = list.Select(e => new WarehouseProductOutputDto()
            {
                Id = e.ProductId,
                Code = e.ProductInput.Product.Code,
                CategoryName = e.ProductInput.Product.CategoryId.ToString(),
                Price = e.Price,
                Quantity = e.Quantity,
                SeriCount = e.ProducOutput.Codes.Count(),
                InputId = e.ProductInput.InputId,
                InputCreateDate = e.ProductInput.Input.CreateDate
            }).OrderBy(e => e.InputCreateDate).ThenBy(e => e.Code).ToList();

            var productIds = list.Select(e => e.ProductId).Distinct().ToList();
            var productLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, productIds);
            var categoryIds = list.Select(e => e.ProductInput.Product.CategoryId).Distinct().ToList();
            var categoryLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, categoryIds);

            foreach (var dto in dtos)
            {
                dto.Title = productLanguages.Where(e => e.ItemId == dto.Id && e.LanguageCode == "vi").Select(e => e.Title).FirstOrDefault() ?? string.Empty;
                dto.CategoryName = categoryLanguages.Where(e => e.ItemId == Guid.Parse(dto?.CategoryName ?? string.Empty) && e.LanguageCode == "vi").Select(e => e.Title).FirstOrDefault() ?? string.Empty;
            }

            return Ok(dtos);
        }

        [HttpPost]
        [Route("outputs/{id}/products")]
        public async Task<IActionResult> CreateOutputProduct([FromRoute] Guid id, [FromBody] WarehouseProductOutputRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var product = await _itemRepository.GetProduct(user.CompanyId, request.Id);
            if (product == null) return ValidationProblem($"Sản phẩm [{request.Id}] không tồn tại");

            var checkExistProductInput = await _warehouseRepository.CheckExistProductInOutputs(user.CompanyId, id, request.Id);
            if (checkExistProductInput) return ValidationProblem($"Sản phẩm  [{product.Item.ItemLanguages.Where(e => e.LanguageCode == "vi").Select(e => e.Title).FirstOrDefault()}] đã tồn tại trên phiếu xuất kho");

            var obj = new WarehouseOutputProduct
            {
                ProductId = request.Id,
                OutputId = id,
                Quantity = request.Quantity
            };

            var resultData = await _warehouseRepository.CreateOutputProduct(user.CompanyId, obj);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không tồn tại phiếu xuất kho");
                case 2: return ValidationProblem("[2] Không tồn tại sản phẩm");
                case 3: return ValidationProblem("[3] Không tồn tại tồn trong kho để xuất");
                case 4: return ValidationProblem("[4] Không đủ hàng tồn trong kho để xuất");
                case 5: return ValidationProblem("[5] Không tồn tại tồn trong lô nhập để xuất");
            }
            return Ok(resultData);
        }

        [HttpPost]
        [Route("outputs/{id}/products/codes")]
        public async Task<IActionResult> CreateOutputProductByCode([FromRoute] Guid id, [FromBody] WarehouseProductCodeRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var resultData = await _warehouseRepository.CreateOutputProductCode(user.CompanyId, id, request.Code);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem($"[1] Không tồn tại phiếu xuất kho [{id}]");
                case 2: return ValidationProblem($"[2] Không tồn tại mã [{request.Code}] trong lô nhập");
                case 3: return ValidationProblem($"[3] Có nhiều hơn 1 sản phẩm có mã [{request.Code}]");
                case 4: return ValidationProblem($"[4] Đã tồn tại mã [{request.Code}]");
                case 5: return ValidationProblem("[5] Không tồn tại sản phẩm tồn kho");
                case 6: return ValidationProblem("[6] Sản phẩm tồn kho đã hết");
                case 7: return ValidationProblem("[7] Không tìm thấy sản phẩm tồn kho trong lô nhập");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu mã");
            }
            return Ok(resultData);
        }

        [HttpDelete]
        [Route("outputs/{outputid}/products/{productid}")]
        public async Task<IActionResult> DeleteOutputProduct([FromRoute] Guid outputid, [FromRoute] Guid productid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _warehouseRepository.GetOutputProduct(user.CompanyId, outputid, productid);
            if (obj == null) return ValidationProblem($"Sản phẩm [{productid}] không tồn tại trong phiếu xuất kho {outputid}");

            var resultData = await _warehouseRepository.DeleteOutputProduct(obj);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không tồn tại phiếu nhập kho");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu sản phẩm");
            }
            return Ok(resultData);
        }

        [HttpDelete]
        [Route("outputs/{outputid}/products/codes/{code}")]
        public async Task<IActionResult> DeleteOutputProductByCode([FromRoute] Guid outputid, [FromRoute] string code)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            code = WebUtility.UrlDecode(code);
            var resultData = await _warehouseRepository.DeleteOutputProductCode(user.CompanyId, outputid, code);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem($"[1] Không tồn tại mã [{code}] của sản phẩm trong phiếu xuất kho");
                case 2: return ValidationProblem($"[2] Tồn tại nhiều hơn một sản phẩm có mã [{code}]");
                case 3: return ValidationProblem($"[3] Không tìm thấy phiếu xuất kho [{outputid}]");
                case 4: return ValidationProblem("[4] Không tìm thấy sản phẩm với phiếu xuất kho tương ứng");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu vào db");
            }
            return Ok(resultData);
        }


        [HttpGet]
        [Route("outputs/{outputid}/products/{productid}/codes")]
        public async Task<IActionResult> GetOutputProductCodes([FromRoute] Guid outputid, [FromRoute] Guid productid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var list = await _warehouseRepository.GetOutputProductCodes(user.CompanyId, outputid, productid);
            var codes = list.Select(e => e.ProductCode).ToList();

            return Ok(codes);
        }

        [HttpPost]
        [Route("outputs/{outputid}/products/{productid}/codes")]
        public async Task<IActionResult> CreateOutputProductCode([FromRoute] Guid outputid, [FromRoute] Guid productid, [FromBody] WarehouseProductCodeRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var resultData = await _warehouseRepository.CreateOutputProductCode(user.CompanyId, outputid, productid, request.Code);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem($"[1] Không tồn tại phiếu xuất kho [{outputid}]");
                case 2: return ValidationProblem($"[2] Không tồn tại mã [{request.Code}] trong lô nhập");
                case 3: return ValidationProblem($"[3] Đã tồn tại mã [{request.Code}]");
                case 4: return ValidationProblem("[4] Không tồn tại sản phẩm tồn kho");
                case 5: return ValidationProblem("[5] Sản phẩm tồn kho đã hết");
                case 6: return ValidationProblem("[6] Không tìm thấy sản phẩm tồn kho trong lô nhập");
                case 7: return ValidationProblem($"[7] Không tìm thấy sản phẩm mã [{productid}] trong phiếu xuất [{outputid}]");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu mã");
            }
            return Ok(resultData);
        }

        [HttpDelete]
        [Route("outputs/{outputid}/products/{productid}/codes/{code}")]
        public async Task<IActionResult> DeleteOutputProductCode([FromRoute] Guid outputid, [FromRoute] Guid productid, [FromRoute] string code)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            code = WebUtility.UrlDecode(code);
            var resultData = await _warehouseRepository.DeleteOutputProductCode(user.CompanyId, outputid, productid, code);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không tồn tại phiếu nhập kho");
                case 2: return ValidationProblem("[2] Không tồn tại sản phẩm phiếu nhập kho");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu mã");
            }
            return Ok(resultData);
        }
        #endregion

        #region tồn kho
        [HttpGet]
        [Route("inventories")]
        public async Task<IActionResult> GetInventories([FromQuery] WarehouseInventorySearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var languageCode = "vi";

            var products = await _warehouseRepository.GetInventories(user.CompanyId, search);
            var productDtos = products.Items.Select(e => new WarehouseInventoryDto()
            {
                WarehouseId = e.WarehouseId,
                WarehouseName = e.Warehouse.Name,
                InventoryNumber = e.InventoryNumber,
                ProductId = e.ProductId,
                ProductCode = e.Product.Code,
                ProductName = e.Product.Item.ItemLanguages.Where(e => e.LanguageCode == languageCode).Select(e => e.Title).FirstOrDefault() ?? string.Empty,
                ProductCategory = e.Product.Category.Item.ItemLanguages.Where(e => e.LanguageCode == languageCode).Select(e => e.Title).FirstOrDefault() ?? string.Empty,
            }).ToList();

            return Ok(new PagedList<WarehouseInventoryDto>(productDtos,
                       products.MetaData.TotalCount,
                       products.MetaData.CurrentPage,
                       products.MetaData.PageSize));
        }
        [HttpGet]
        [Route("inventories/{productid}")]
        public async Task<IActionResult> GetInventorieInputs([FromRoute] Guid productid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var products = await _warehouseRepository.GetInventories(user.CompanyId, productid);
            var productDtos = products.Select(e => new WarehouseInputInventoryDto()
            {
                InputId = e.InputId,
                InputCode = e.Input.InputCode,
                ProductPrice = e.Input.Products.FirstOrDefault()?.Price ?? 0,
                InputCreateDate = e.Input.CreateDate,
                TotalProduct = e.Input.Products.FirstOrDefault(e => e.ProductId == productid)?.Quantity ?? 0,
                InventoryNumber = e.InventoryNumber,
                InputType = DataSource.WarehouseInputTypes.First(s => s.Key == e.Input.Type).Value,
                InputSourceName = e.Input.FromSupplier?.SupplierName ?? e.Input.FromFactory?.FactoryName ?? e.Input.FromOrder?.OrderId.ToString() ?? string.Empty,
                WarehouseId = e.Input.WarehouseId,
                WarehouseName = e.Input.Warehouse.Name
            }).ToList();

            return Ok(productDtos);
        }
        #endregion

        #region xưởng sản xuất
        [HttpGet]
        [Route("factories")]
        public async Task<IActionResult> GetFactories()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _warehouseRepository.GetFactories(user.CompanyId);
            var dtos = list.Select(e => new WarehouseFactoryDto()
            {
                Id = e.Id,
                Address = e.Address,
                Email = e.Email,
                Name = e.Name,
                Phone = e.Phone,
                Note = e.Note,
                IsActive = e.IsActive
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("factories/{id}")]
        public async Task<IActionResult> GetFactory(Guid id)
        {
            var obj = await _warehouseRepository.GetFactory(id);
            if (obj == null) return BadRequest($"Nơi sản xuất [{id}] không tồn tại");
            return Ok(new WarehouseFactoryDto()
            {
                Id = obj.Id,
                Address = obj.Address,
                Email = obj.Email,
                Name = obj.Name,
                Phone = obj.Phone,
                Note = obj.Note,
                IsActive = obj.IsActive
            });
        }

        [HttpPost]
        [Route("factories")]
        public async Task<IActionResult> CreateFactory([FromBody] WarehouseFactoryDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _warehouseRepository.CreateFactory(new WarehouseFactory
            {
                Id = request.Id,
                Address = request.Address?.Trim(),
                Email = request.Email?.Trim(),
                Name = request.Name,
                Phone = request.Phone?.Trim(),
                IsActive = request.IsActive,
                Note = request.Note?.Trim(),
                CompanyId = user.CompanyId
            });
            return CreatedAtAction(nameof(GetWarehouse), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("factories/{id}")]
        public async Task<IActionResult> UpdateFactory(Guid id, [FromBody] WarehouseFactoryDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = await _warehouseRepository.GetFactory(id);
            if (obj == null) return BadRequest($"Nơi sản xuất [{id}] không tồn tại");

            obj.Address = request.Address?.Trim();
            obj.Email = request.Email?.Trim();
            obj.Name = request.Name;
            obj.Phone = request.Phone?.Trim();
            obj.Note = request.Note?.Trim();
            obj.IsActive = request.IsActive;

            var moduleResult = await _warehouseRepository.UpdateFactory(obj);

            return Ok(request);
        }

        [HttpDelete]
        [Route("factories/{id}")]
        public async Task<IActionResult> DeleteFactory([FromRoute] Guid id)
        {
            var obj = await _warehouseRepository.GetFactory(id);
            if (obj == null) return BadRequest($"Nơi sản xuất [{id}] không tồn tại");

            var resultData = await _warehouseRepository.DeleteFactory(obj);
            return Ok(resultData);
        }
        #endregion

        #region Nhà cung cấp
        [HttpGet]
        [Route("suppliers")]
        public async Task<IActionResult> GetSuppliers()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var list = await _warehouseRepository.GetSuppliers(user.CompanyId);
            var dtos = list.Select(e => new WarehouseSupplierDto()
            {
                Id = e.Id,
                Address = e.Address,
                Email = e.Email,
                Name = e.Name,
                Phone = e.Phone,
                Note = e.Note,
                IsActive = e.IsActive
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("suppliers/{id}")]
        public async Task<IActionResult> GetSupplier(Guid id)
        {
            var obj = await _warehouseRepository.GetSupplier(id);
            if (obj == null) return BadRequest($"Nhà cung cấp [{id}] không tồn tại");
            return Ok(new WarehouseSupplierDto()
            {
                Id = obj.Id,
                Address = obj.Address,
                Email = obj.Email,
                Name = obj.Name,
                Phone = obj.Phone,
                Note = obj.Note,
                IsActive = obj.IsActive
            });
        }

        [HttpPost]
        [Route("suppliers")]
        public async Task<IActionResult> CreateSupplier([FromBody] WarehouseSupplierDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _warehouseRepository.CreateSupplier(new WarehouseSupplier
            {
                Id = request.Id,
                Address = request.Address?.Trim(),
                Email = request.Email?.Trim(),
                Name = request.Name,
                Phone = request.Phone?.Trim(),
                IsActive = request.IsActive,
                Note = request.Note?.Trim(),
                CompanyId = user.CompanyId
            });
            return CreatedAtAction(nameof(GetWarehouse), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("suppliers/{id}")]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] WarehouseSupplierDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = await _warehouseRepository.GetSupplier(id);
            if (obj == null) return BadRequest($"Nhà cung cấp [{id}] không tồn tại");

            obj.Address = request.Address?.Trim();
            obj.Email = request.Email?.Trim();
            obj.Name = request.Name;
            obj.Phone = request.Phone?.Trim();
            obj.Note = request.Note?.Trim();
            obj.IsActive = request.IsActive;

            var moduleResult = await _warehouseRepository.UpdateSupplier(obj);

            return Ok(request);
        }

        [HttpDelete]
        [Route("suppliers/{id}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] Guid id)
        {
            var obj = await _warehouseRepository.GetSupplier(id);
            if (obj == null) return BadRequest($"Nhà cung cấp [{id}] không tồn tại");

            var resultData = await _warehouseRepository.DeleteSupplier(obj);
            return Ok(resultData);
        }
        #endregion

        #region danh mục
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var categoryType = "PRO";
            var languageCode = "vi";

            var itemLanguage = await _itemRepository.GetCategories(user.CompanyId, categoryType);
            var categorydtos = itemLanguage
                .Select(e => new WarehouseCategoryDto()
                {
                    Id = e.ItemId,
                    IsPuslished = e.Item.IsPublished,
                    ParentId = e.Item.Category?.ParentId,
                    Title = e.Item.ItemLanguages.Where(e => e.LanguageCode == languageCode).Select(e => e.Title).FirstOrDefault() ?? string.Empty,
                    Describe = e.Item.ItemLanguages.Where(e => e.LanguageCode == languageCode).Select(e => e.Title).FirstOrDefault() ?? string.Empty,
                    ComponentDetail = e.CategoryComponent?.ComponentDetail,
                    ComponentList = e.CategoryComponent?.ComponentList,
                }).ToList();

            return Ok(categorydtos);
        }
        [HttpGet]
        [Route("categories/{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var languageCode = "vi";

            var itemLanguage = await _itemRepository.GetItemLanguage(user.CompanyId, id, languageCode);
            if (itemLanguage == null) itemLanguage = new ItemLanguage
            {
                LanguageCode = languageCode,
                Item = await _itemRepository.GetItem(user.CompanyId, id),
            };
            var category = await _itemRepository.GetCategory(user.CompanyId, id);
            var dto = new WarehouseCategoryDto()
            {
                Id = id,
                Describe = itemLanguage.Brief,
                Title = itemLanguage.Title,
                ParentId = category.ParentId,
                IsPuslished = category.Item.IsPublished,
                ComponentDetail = category.CategoryComponent?.ComponentDetail,
                ComponentList = category.CategoryComponent?.ComponentList,
            };

            return Ok(dto);
        }
        [HttpPost]
        [Route("Categories")]
        public async Task<IActionResult> CreateCategory([FromBody] WarehouseCategoryDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var categoryType = "PRO";
            var languageCode = "vi";

            var languageItem = new ItemLanguage { Title = request.Title.Trim(), LanguageCode = languageCode, Brief = request.Describe.Trim(), Content = "" };
            var item = new Item()
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                IsPublished = false,
                Order = 20,
                View = 0,
                CreateDate = DateTime.Now,
                Category = new ItemCategory()
                {
                    Type = categoryType,
                    ParentId = request.ParentId
                },
                ItemLanguages = new List<ItemLanguage>() { languageItem }
            };
            await _itemRepository.CreateItem(item);

            return Ok();
        }
        [HttpPut]
        [Route("categories/update/{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, WarehouseCategoryDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var languageCode = "vi";

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, id);
            if (category == null) return ValidationProblem($"Danh mục {id} không tồn tại");

            await _itemRepository.UpdateCategory(request, languageCode);

            return Ok(request);
        }
        [HttpPut]
        [Route("categories/publish/{id}")]
        public async Task<IActionResult> PublishCategory([FromRoute] Guid id, WarehouseCategoryDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, id);
            if (category == null) return ValidationProblem($"Danh mục {id} không tồn tại");

            await _itemRepository.PublishCategory(request);

            return Ok(request);
        }
        [HttpDelete]
        [Route("categories/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var category = await _itemRepository.GetCategory(user.CompanyId, id);
            if (category == null) return NotFound($"{id} không tồn tại");

            var result = await _itemRepository.DeleteCategory(category);

            return Ok(result);
        }
        #endregion

        #region product
        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductListSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var languageCode = "vi";

            var products = await _itemRepository.GetProducts(user.CompanyId, search);
            var productDtos = products.Items.Select(e => new WarehouseProductDto()
            {
                Id = e.ItemId,
                Code = e.Code,
                CategoryId = e.CategoryId,
                CountSeries = e.Series.Count,
                Title = e.Item.ItemLanguages.Where(e => e.LanguageCode == languageCode).Select(e => e.Title).FirstOrDefault() ?? string.Empty,
                Image = new FileData() { FileName = e.Item.Image, Type = FileType.ProductImage, Folder = user.CompanyId.ToString() },
            }).ToList();
            var categoryIds = productDtos.Select(e => e.CategoryId).Distinct().ToList();
            var categoryLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, categoryIds);
            foreach (var dto in productDtos)
            {
                dto.CategoryName = categoryLanguages.Where(e => e.LanguageCode == languageCode).Select(e => e.Title).FirstOrDefault() ?? string.Empty;
            }

            return Ok(new PagedList<WarehouseProductDto>(productDtos,
                       products.MetaData.TotalCount,
                       products.MetaData.CurrentPage,
                       products.MetaData.PageSize));
        }

        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> CreateProduct([FromBody] WarehouseProductDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, request.CategoryId);
            if (category == null) return ValidationProblem($"Danh mục [{request.CategoryId}] không tồn tại");

            if (!string.IsNullOrEmpty(request.Code))
            {
                var checkExistCode = await _itemRepository.CheckExistProductCode(user.CompanyId, request.Code);
                if (checkExistCode) return ValidationProblem($"Mã sản phẩm [{request.Code}] đã tồn tại");
            }

            var languageCode = "vi";

            var languageItem = new ItemLanguage { Title = request.Title.Trim(), LanguageCode = languageCode, Brief = request.Title };
            var item = new Item()
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                IsPublished = false,
                Order = 50,
                View = 0,
                CreateDate = DateTime.Now,
                Product = new ItemProduct()
                {
                    CategoryId = request.CategoryId,
                    Code = request.Code,
                    Price = 0,
                    Discount = 0,
                    DiscountType = 0,
                    SaleMin = 0
                },
                ItemLanguages = new List<ItemLanguage>() { languageItem },
            };

            await _itemRepository.CreateProduct(item, null, null, null);

            return Ok();
        }

        [HttpGet]
        [Route("products/{productId}/series")]
        public async Task<IActionResult> GetSeriesByProduct([FromRoute] Guid productId)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var searies = await _warehouseRepository.GetProductCodes(user.CompanyId, productId);
            
            return Ok(searies);
        }

        [HttpGet]
        [Route("products/{productId}/series/barcodes/{type}")]
        public async Task<IActionResult> GetBarCodesByProduct([FromRoute] Guid productId, string type)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var fotmat = BarcodeFormat.CODE_128;
            var width = 300;
            var height = 100;
            switch (type)
            {
                case "EAN_8":
                    fotmat = BarcodeFormat.EAN_8;
                    width = 300;
                    height = 100;
                    break;
                case "EAN_13": 
                    fotmat = BarcodeFormat.EAN_13;
                    width = 300;
                    height = 100;
                    break;
                case "UPC_A": 
                    fotmat = BarcodeFormat.UPC_A;
                    width = 300;
                    height = 100;
                    break;
                case "UPC_E": 
                    fotmat = BarcodeFormat.UPC_E;
                    width = 300;
                    height = 100; 
                    break;
                case "CODE_39": 
                    fotmat = BarcodeFormat.CODE_39;
                    width = 300;
                    height = 100; 
                    break;
                case "CODE_93": 
                    fotmat = BarcodeFormat.CODE_93;
                    width = 300;
                    height = 100;
                    break;
                case "CODE_128": 
                    fotmat = BarcodeFormat.CODE_128;
                    width = 300;
                    height = 100;
                    break;
                case "ITF": 
                    fotmat = BarcodeFormat.ITF;
                    width = 300;
                    height = 100; 
                    break;
                case "MSI": 
                    fotmat = BarcodeFormat.MSI;
                    width = 300;
                    height = 100; 
                    break;
                case "RSS_14": 
                    fotmat = BarcodeFormat.RSS_14;
                    width = 300;
                    height = 100; 
                    break;
                case "RSS_EXPANDED": 
                    fotmat = BarcodeFormat.RSS_EXPANDED;
                    width = 300;
                    height = 100; 
                    break;
                case "QR_CODE": 
                    fotmat = BarcodeFormat.QR_CODE;
                    width = 250;
                    height = 250; 
                    break;
                case "DATA_MATRIX": 
                    fotmat = BarcodeFormat.DATA_MATRIX;
                    width = 250;
                    height = 250; 
                    break;
                case "AZTEC": 
                    fotmat = BarcodeFormat.AZTEC;
                    width = 250;
                    height = 250; 
                    break;
                case "PDF_417": 
                    fotmat = BarcodeFormat.PDF_417;
                    width = 250;
                    height = 250; 
                    break;
                case "MAXICODE": 
                    fotmat = BarcodeFormat.MAXICODE;
                    width = 250;
                    height = 250;
                    break;
                case "CODABAR": 
                    fotmat = BarcodeFormat.CODABAR;
                    width = 300;
                    height = 100; 
                    break;
                case "PHARMA_CODE": 
                    fotmat = BarcodeFormat.PHARMA_CODE;
                    width = 300;
                    height = 100; 
                    break;
            }    

            var images = new List<string>();
            var searies = await _warehouseRepository.GetProductCodes(user.CompanyId, productId);
            foreach (var code in searies)
            {
                try
                {
                    // Tạo mã vạch
                    var barcodeWriter = new BarcodeWriterPixelData
                    {
                        Format = fotmat,
                        Options = new ZXing.Common.EncodingOptions
                        {
                            Width = width,
                            Height = height,
                            Margin = 1
                        },
                    };
                    var pixelData = barcodeWriter.Write(code);

                    // Chuyển đổi dữ liệu pixel thành Bitmap
                    using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
                    {
                        var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                        try
                        {
                            // Sao chép dữ liệu pixel vào Bitmap
                            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }

                        // Chuyển đổi Bitmap thành chuỗi Base64
                        var barcodeBase64 = ConvertToBase64(bitmap);
                        images.Add(barcodeBase64);
                    }
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
            }
            
            return Ok(images);
        }

        [HttpPost]
        [Route("products/{productId}/{number}")]
        public async Task<IActionResult> AddSearies([FromRoute] Guid productId,[FromRoute] int number)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetProduct(user.CompanyId, productId);
            if (category == null) return ValidationProblem($"Sản phẩn [{productId}] không tồn tại");

            var series = new List<string>();
            for (var i = 0; i < number; i++)
            {
                var checkExistCode = false;
                var seri = string.Empty;
                do
                {
                    seri = Guid.NewGuid().ToString();
                    checkExistCode = await _warehouseRepository.CheckExistProductCode(user.CompanyId, seri);
                } while (checkExistCode);
                series.Add(seri);
            }

            var result = await _warehouseRepository.CreateProductCode(productId, series);

            return Ok(result.Select(e => e.Seri).ToList());
        }

        [HttpDelete]
        [Route("products/{productId}/delete/{seri}")]
        public async Task<IActionResult> DeleteSearies([FromRoute] Guid productId, [FromRoute] string seri)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetProduct(user.CompanyId, productId);
            if (category == null) return ValidationProblem($"Sản phẩn [{productId}] không tồn tại");

            if (!string.IsNullOrEmpty(seri))
            {
                var checkExistCode = await _warehouseRepository.CheckExistProductCode(user.CompanyId, seri);
                if (!checkExistCode) return ValidationProblem($"Searies [{seri}] đã tồn tại");
            }

            var resultData = await _warehouseRepository.DeleteProductCode(productId, seri);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem($"[1] Không tồn tại số series {seri}");
            }
            return Ok(resultData);
        }
        #endregion

        #region report
        [HttpGet]
        [Route("reports/statistic/current")]
        public async Task<IActionResult> GetReportStatistic()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var data = new ReportStatisticDto();
            data.CostofGoodsSold = await _warehouseRepository.GetCOGS(user.CompanyId);
            data.Revenue = await _orderRepository.GetRevenue(user.CompanyId);
            data.GrossProfit = data.Revenue - data.CostofGoodsSold;
            data.Expenses = await _warehouseRepository.GetExpensive(user.CompanyId);

            return Ok(data);
        }

        [HttpGet]
        [Route("reports/statistic/total")]
        public async Task<IActionResult> GetReportStatisticAll()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var data = new ReportStatisticDto();
            data.CostofGoodsSold = await _warehouseRepository.GetTotalCOGS(user.CompanyId);
            data.Revenue = await _orderRepository.GetTotalRevenue(user.CompanyId);
            data.GrossProfit = data.Revenue - data.CostofGoodsSold;
            data.Expenses = await _warehouseRepository.GetTotalExpensive(user.CompanyId);

            return Ok(data);
        }

        [HttpGet]
        [Route("reports/statistic/cogs/stage")]
        public async Task<IActionResult> GetReportCostOfGrossSoldStage()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var data = await _warehouseRepository.GetStageCOGS(user.CompanyId);

            return Ok(data);
        }

        [HttpGet]
        [Route("reports/statistic/cogs/date")]
        public async Task<IActionResult> GetReportCostOfGrossSoldByDate([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var data = await _warehouseRepository.GetCOGS(user.CompanyId, fromDate, toDate);
            return Ok(data);
        }

        [HttpGet]
        [Route("reports/statistic/revenue/stage")]
        public async Task<IActionResult> GetReportRevenueStage()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var data = await _orderRepository.GetStageRevenue(user.CompanyId);

            return Ok(data);
        }

        [HttpGet]
        [Route("reports/statistic/revenue/date")]
        public async Task<IActionResult> GetReportRevenueByDate([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var data = await _orderRepository.GetRevenue(user.CompanyId, fromDate, toDate);
            return Ok(data);
        }

        [HttpGet]
        [Route("reports/statistic/expensive/stage")]
        public async Task<IActionResult> GetReportExpensiveStage()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var data = await _warehouseRepository.GetStageExpensive(user.CompanyId);

            return Ok(data);
        }
        [HttpGet]
        [Route("reports/statistic/expensive/date")]
        public async Task<IActionResult> GetReportExpensiveByDate([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var data = await _warehouseRepository.GetExpensive(user.CompanyId, fromDate, toDate);
            return Ok(data);
        }
        #endregion

        private string ConvertToBase64(Bitmap bitmap)
        {
            using var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            return $"data:image/png;base64,{Convert.ToBase64String(memoryStream.ToArray())}";
        }
    }
}
