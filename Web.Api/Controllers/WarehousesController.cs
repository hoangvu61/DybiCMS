using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Net;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Repositories;
using Web.Models;
using Web.Models.Enums;
using Web.Models.SeedWork;

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
            if (obj == null) return BadRequest($"Kho [{id}] không tồn tại");

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

            if (request.TotalPrice < request.Payment)
            {
                obj.Debt = new WarehouseInputDebt();
                obj.Debt.Debit = request.TotalPrice - request.Payment;
                obj.Debt.DebitExpire = request.DebitExpire;
            }    
            
            switch (request.Type)
            {
                case 1:
                    var supplier = await _warehouseRepository.GetSupplier(request.FromId);
                    if (supplier == null) return ValidationProblem($"Không tìm thấy NCC [{request.FromId}]");
                    obj.FromSupplier = new WarehouseInputFromSupplier();
                    obj.FromSupplier.SourceId = request.FromId;
                    obj.FromSupplier.SupplierName = supplier.Name;
                    obj.FromSupplier.SupplierPhone = supplier.Phone;
                    obj.FromSupplier.SupplierEmail = supplier.Email;
                    obj.FromSupplier.SupplierAddress = supplier.Address;
                    break;
                case 2:
                    var factory = await _warehouseRepository.GetFactory(request.FromId);
                    if (factory == null) return ValidationProblem($"Không tìm thấy Nơi sản xuất [{request.FromId}]");
                    obj.FromFactory = new WarehouseInputFromFactory();
                    obj.FromFactory.FactoryId = request.FromId;
                    obj.FromFactory.FactoryName = factory.Name;
                    obj.FromFactory.FactoryPhone = factory.Phone;
                    obj.FromFactory.FactoryEmail = factory.Email;
                    obj.FromFactory.FactoryAddress = factory.Address;
                    break;
                case 3:
                    var warehouse = await _warehouseRepository.GetWarehouse(request.FromId);
                    if (warehouse == null) return ValidationProblem($"Không tìm thấy Kho [{request.FromId}]");
                    obj.FromWarehouse = new WarehouseInputFromWarehouse();
                    obj.FromWarehouse.WarehouseId = request.FromId;
                    obj.FromWarehouse.WarehouseName = warehouse.Name;
                    obj.FromWarehouse.WarehousePhone = warehouse.Phone;
                    obj.FromWarehouse.WarehouseEmail = warehouse.Email;
                    obj.FromWarehouse.WarehouseAddress = warehouse.Address;
                    break;
                case 4:
                    var order = await _orderRepository.GetOrder(user.CompanyId, request.FromId);
                    if (order == null) return ValidationProblem($"Không tìm thấy Hóa đơn [{request.FromId}]");
                    obj.FromOrder = new WarehouseInputFromOrder();
                    obj.FromOrder.OrderId = request.FromId;
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

            var obj = await _warehouseRepository.GetInput(user.CompanyId, id);
            if (obj == null) return ValidationProblem($"Phiếu nhập kho [{id}] không tồn tại");

            var checkExistProduct = await _warehouseRepository.CheckExistProductInInput(user.CompanyId, id, Guid.Empty);
            if (checkExistProduct) return ValidationProblem($"Phải xóa hết sản phẩm trước");

            var resultData = await _warehouseRepository.DeleteInput(obj);
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
        public async Task<IActionResult> CreateInputProduct([FromRoute] Guid inputid, [FromRoute] Guid productid, [FromBody] WarehouseProductInputCodeRequest request)
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
                WarehouseName = e.Warehouse.Name,
                Type = e.Type,
                TypeName = DataSource.WarehouseInputTypes.First(s => s.Key == e.Type).Value,
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
            dto.WarehouseName = input.Warehouse.Name;
            dto.Type = input.Type;
            dto.TypeName = DataSource.WarehouseInputTypes.First(s => s.Key == input.Type).Value;
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
                    var order = await _orderRepository.GetOrder(user.CompanyId, request.ToId);
                    if (order == null) return ValidationProblem($"Không tìm thấy Hóa đơn [{request.ToId}]");
                    obj.ToOrder = new WarehouseOutputToOrder();
                    obj.ToOrder.OrderId = request.ToId;
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
                    var factory = await _warehouseRepository.GetFactory(request.ToId);
                    if (factory == null) return ValidationProblem($"Không tìm thấy Nơi sản xuất [{request.ToId}]");
                    obj.ToFactory = new WarehouseOutputToFactory();
                    obj.ToFactory.FactoryId = request.ToId;
                    obj.ToFactory.FactoryName = factory.Name;
                    obj.ToFactory.FactoryPhone = factory.Phone;
                    obj.ToFactory.FactoryEmail = factory.Email;
                    obj.ToFactory.FactoryAddress = factory.Address;
                    break;
                case 3:
                    var warehouse = await _warehouseRepository.GetWarehouse(request.ToId);
                    if (warehouse == null) return ValidationProblem($"Không tìm thấy Kho [{request.ToId}]");
                    obj.ToWarehouse = new WarehouseOutputToWarehouse();
                    obj.ToWarehouse.WarehouseId = request.ToId;
                    obj.ToWarehouse.WarehouseName = warehouse.Name;
                    obj.ToWarehouse.WarehousePhone = warehouse.Phone;
                    obj.ToWarehouse.WarehouseEmail = warehouse.Email;
                    obj.ToWarehouse.WarehouseAddress = warehouse.Address;
                    break;
                case 4:
                    var supplier = await _warehouseRepository.GetSupplier(request.ToId);
                    if (supplier == null) return ValidationProblem($"Không tìm thấy NCC [{request.ToId}]");
                    obj.ToSupplier = new WarehouseOutputToSupplier();
                    obj.ToSupplier.SourceId = request.ToId;
                    obj.ToSupplier.SupplierName = supplier.Name;
                    obj.ToSupplier.SupplierPhone = supplier.Phone;
                    obj.ToSupplier.SupplierEmail = supplier.Email;
                    obj.ToSupplier.SupplierAddress = supplier.Address;
                    break;
            };

            var input = await _warehouseRepository.CreateOutput(user.CompanyId, obj);

            return CreatedAtAction(nameof(GetWarehouse), new { id = input.Id }, request);
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
                Price = e.Price,
                Quantity = e.Quantity,
                SeriCount = e.ProducOutput.Codes.Count(),
                InputId = e.ProductInput.InputId,
                InputCreateDate = e.ProductInput.Input.CreateDate
            }).ToList();

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
                case 2: return ValidationProblem("[2] Không tồn tại sản phẩm trong hàng tồn kho");
                case -1: return ValidationProblem("[-1] Lỗi khi lưu sản phẩm");
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
        #endregion
    }
}
