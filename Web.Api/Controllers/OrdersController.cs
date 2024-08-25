using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class OrdersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository, UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        #region order
        [HttpGet]
        [Route("news")]
        public async Task<IActionResult> GetNewOrders([FromQuery] PagingParameters paging)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var orders = await _orderRepository.GetNewOrders(user.CompanyId, paging);
            var orderDtos = orders.Items.Select(o => new OrderDto()
            {
                Id = o.Id,
                CustomerName = o.Customer.CustomerName,
                CustomerPhone = o.Customer.CustomerPhone,
                CustomerAddress = o.Customer.CustomerAddress,
                CountProducts = o.Products.Count,
                TotalPrice = o.Products.Sum(p => p.Price),
                Date = o.CreateDate
            });
            return Ok(new PagedList<OrderDto>(orderDtos.ToList(),
                        orders.MetaData.TotalCount,
                        orders.MetaData.CurrentPage,
                        orders.MetaData.PageSize));
        }

        [HttpGet]
        [Route("confirms")]
        public async Task<IActionResult> GetConfirmOrders([FromQuery] PagingParameters paging)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var orders = await _orderRepository.GetConfirmOrders(user.CompanyId, paging);
            var orderDtos = orders.Items.Select(o => new OrderDto()
            {
                Id = o.Id,
                CustomerName = o.Customer.CustomerName,
                CustomerPhone = o.Customer.CustomerPhone,
                CustomerAddress = o.Customer.CustomerAddress,
                CountProducts = o.Products.Count,
                TotalPrice = o.Products.Sum(p => p.Price),
                Date = o.ConfirmDate.Value
            });
            return Ok(new PagedList<OrderDto>(orderDtos.ToList(),
                        orders.MetaData.TotalCount,
                        orders.MetaData.CurrentPage,
                        orders.MetaData.PageSize));
        }

        [HttpGet]
        [Route("sends")]
        public async Task<IActionResult> GetSendOrders([FromQuery] PagingParameters paging)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var orders = await _orderRepository.GetSendOrders(user.CompanyId, paging);
            var orderDtos = orders.Items.Select(o => new OrderDto()
            {
                Id = o.Id,
                CustomerName = o.Customer.CustomerName,
                CustomerPhone = o.Customer.CustomerPhone,
                CustomerAddress = o.Customer.CustomerAddress,
                CountProducts = o.Products.Count,
                TotalPrice = o.Products.Sum(p => p.Price),
                Date = o.SendDate.Value
            });
            return Ok(new PagedList<OrderDto>(orderDtos.ToList(),
                        orders.MetaData.TotalCount,
                        orders.MetaData.CurrentPage,
                        orders.MetaData.PageSize));
        }

        [HttpGet]
        [Route("receives")]
        public async Task<IActionResult> GetReceiveOrders([FromQuery] PagingParameters paging)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var orders = await _orderRepository.GetReceiveOrders(user.CompanyId, paging);
            var orderDtos = orders.Items.Select(o => new OrderDto()
            {
                Id = o.Id,
                CustomerName = o.Customer.CustomerName,
                CustomerPhone = o.Customer.CustomerPhone,
                CustomerAddress = o.Customer.CustomerAddress,
                CountProducts = o.Products.Count,
                TotalPrice = o.Products.Sum(p => p.Price),
                Date = o.ReceiveDate.Value
            });
            return Ok(new PagedList<OrderDto>(orderDtos.ToList(),
                        orders.MetaData.TotalCount,
                        orders.MetaData.CurrentPage,
                        orders.MetaData.PageSize));
        }

        [HttpGet]
        [Route("cancels")]
        public async Task<IActionResult> GetCancelOrders([FromQuery] PagingParameters paging)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var orders = await _orderRepository.GetCencelOrders(user.CompanyId, paging);
            var orderDtos = orders.Items.Select(o => new OrderDto()
            {
                Id = o.Id,
                CustomerName = o.Customer.CustomerName,
                CustomerPhone = o.Customer.CustomerPhone,
                CustomerAddress = o.Customer.CustomerAddress,
                CountProducts = o.Products.Count,
                TotalPrice = o.Products.Sum(p => p.Price),
                Date = o.CancelDate.Value
            });
            return Ok(new PagedList<OrderDto>(orderDtos.ToList(),
                        orders.MetaData.TotalCount,
                        orders.MetaData.CurrentPage,
                        orders.MetaData.PageSize));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var order = await _orderRepository.GetOrder(user.CompanyId, id);
            var orderDto = new OrderDetailDto()
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer.CustomerName,
                CustomerPhone = order.Customer.CustomerPhone,
                CustomerAddress = order.Customer.CustomerAddress,
                CreateDate = order.CreateDate,
                ConfirmDate = order.ConfirmDate,
                CancelDate = order.CancelDate,
                SendDate = order.SendDate,
                ReceiveDate = order.ReceiveDate,
                Note = order.Note,
            };
            return Ok(orderDto);
        }

        [HttpPut]
        [Route("{id}/confirm")]
        public async Task<IActionResult> ConfirmOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var order = await _orderRepository.GetOrder(user.CompanyId, id);
            if (order == null) return NotFound($"{id} không tồn tại");

            order.ConfirmDate = DateTime.Now;
            await _orderRepository.UpdateOrder(order);

            return Ok();
        }

        [HttpPut]
        [Route("{id}/send")]
        public async Task<IActionResult> SendOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var order = await _orderRepository.GetOrder(user.CompanyId, id);
            if (order == null) return NotFound($"{id} không tồn tại");

            order.SendDate = DateTime.Now;
            await _orderRepository.UpdateOrder(order);

            var output = await _orderRepository.ExportWarehouse(order);

            return Ok();
        }

        [HttpPut]
        [Route("{id}/receive")]
        public async Task<IActionResult> ReceiveOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var order = await _orderRepository.GetOrder(user.CompanyId, id);
            if (order == null) return NotFound($"{id} không tồn tại");

            order.ReceiveDate = DateTime.Now;
            await _orderRepository.UpdateOrder(order);

            return Ok();
        }

        [HttpPut]
        [Route("{id}/cancel")]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var order = await _orderRepository.GetOrder(user.CompanyId, id);
            if (order == null) return NotFound($"{id} không tồn tại");

            order.CancelDate = DateTime.Now;
            await _orderRepository.UpdateOrder(order);

            var output = await _orderRepository.ImportWarehouse(order);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var order = await _orderRepository.GetOrder(user.CompanyId, id);
            if (order == null) return NotFound($"{id} không tồn tại");

            await _orderRepository.DeleteOrder(order);

            return Ok();
        }
        #endregion

        #region product
        [HttpGet]
        [Route("{id}/products")]
        public async Task<IActionResult> GetProducts([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var products = await _orderRepository.GetOrderProducts(user.CompanyId, id);
            var productDtos = products.Select(p => new OrderProductDto()
            {
                ProductId = p.ProductId,
                Price = p.Price,
                Properties = p.Properties,
                Quantity = p.Quantity,
                Name = p.Product.Item.ItemLanguages.Where(e => e.LanguageCode == "vi").Select(e => e.Title).FirstOrDefault(),
                Image = new FileData { FileName = p.Product.Item.Image, Type = Models.Enums.FileType.ProductImage, Folder = user.CompanyId.ToString()}
            });
            return Ok(productDtos);
        }

        [HttpPost]
        [Route("{id}/products")]
        public async Task<IActionResult> AddOrderProduct([FromRoute] Guid id, OrderProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var product = await _orderRepository.GetOrderProduct(user.CompanyId, id, dto.ProductId);
            if (product != null) return NotFound($"{id} đã tồn tại");

            product = new OrderProduct
            {
                OrderId = id,
                ProductId = dto.ProductId,
                Price = dto.Price,
                Quantity = dto.Quantity,
                Properties = dto.Properties
            };

            await _orderRepository.AddOrderProduct(product);

            return Ok();
        }

        [HttpPut]
        [Route("{id}/products")]
        public async Task<IActionResult> UpdateOrderProduct([FromRoute] Guid id, OrderProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var product = await _orderRepository.GetOrderProduct(user.CompanyId, id, dto.ProductId);
            if (product == null) return NotFound($"{id} không tồn tại");

            product.Quantity = dto.Quantity;
            product.Price = dto.Price;
            await _orderRepository.UpdateOrderProduct(product);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}/products/{productid}")]
        public async Task<IActionResult> DeleteOrderProduct([FromRoute] Guid id, [FromRoute] Guid productid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var product = await _orderRepository.GetOrderProduct(user.CompanyId, id, productid);
            if (product == null) return NotFound($"{id} không tồn tại");

            await _orderRepository.DeleteOrderProduct(product);

            return Ok();
        }
        #endregion

        #region delivery
        [HttpGet]
        [Route("{id}/OrderDeliveries")]
        public async Task<IActionResult> GetOrderDelivery(Guid id)
        {
            var order = await _orderRepository.GetOrderDelivery(id);

            var orderDto = new OrderDeliveryDto();
            if (order != null)
            {
                orderDto.OrderId = order.OrderId;
                orderDto.DeliveryId = order.DeliveryId;
                orderDto.DeliveryName = DataSource.Deliveries[order.DeliveryId];
                orderDto.DeliveryCode = order.DeliveryCode;
                orderDto.DeliveryFee = order.DeliveryFee;
                orderDto.DeliveryNote = order.DeliveryNote;
                orderDto.COD = order.COD;

            }
            return Ok(orderDto);
        }

        [HttpPut]
        [Route("{id}/OrderDeliveries")]
        public async Task<IActionResult> UpdateOrderDelivery([FromRoute] Guid id, OrderDeliveryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var delivery = await _orderRepository.GetOrderDelivery(id);
            if (delivery == null)
            {
                delivery = new OrderDelivery
                {
                    OrderId = id,
                    COD = request.COD,
                    DeliveryCode = request.DeliveryCode,
                    DeliveryFee = request.DeliveryFee,
                    DeliveryId = request.DeliveryId,
                    DeliveryNote = request.DeliveryNote
                };
                await _orderRepository.AddOrderDelivery(delivery);
            }
            else
            {
                delivery.COD = request.COD;
                delivery.DeliveryId = request.DeliveryId;
                delivery.DeliveryCode = request.DeliveryCode;
                delivery.DeliveryFee = request.DeliveryFee;
                delivery.DeliveryNote = request.DeliveryNote;
                await _orderRepository.UpdateOrderDelivery(delivery);
            }

            return Ok(delivery);
        }

        [HttpDelete]
        [Route("{id}/OrderDeliveries")]
        public async Task<IActionResult> DeleteOrderDelivery([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderRepository.GetOrderDelivery(id);
            if (order == null) return NotFound($"{id} không tồn tại");

            await _orderRepository.DeleteOrderDelivery(order);

            return Ok();
        }
        #endregion

        [HttpPut]
        [Route("customers/{id}")]
        public async Task<IActionResult> UpdateOrderCustomer([FromRoute] Guid id, OrderCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var customer = await _orderRepository.GetCustomer(user.CompanyId, dto.Id);
            if (customer == null) return NotFound($"{id} không tồn tại");

            customer.CustomerName = dto.CustomerName;
            customer.CustomerPhone = dto.CustomerPhone;
            customer.CustomerAddress = dto.CustomerAddress;
            await _orderRepository.UpdateCustomer(customer);

            return Ok();
        }
    }
}
