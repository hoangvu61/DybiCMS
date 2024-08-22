using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Repositories;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DebtsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IDebtRepository _debtRepository;

        public DebtsController(IDebtRepository debtRepository, UserManager<User> userManager)
        {
            _debtRepository = debtRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("debtors")]
        public async Task<IActionResult> GetSupplierDebts([FromQuery] DebtSupplierSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var depts = await _debtRepository.GetSupplierDebts(user.CompanyId, search);
            var dtos = depts.Items.Select(e => new DebtDto()
            {
                Id = e.Id,
                CreateDate = e.CreateDate,
                DebtorOrCreditor_Address = e.Supplier.Address,
                DebtorOrCreditor_Id = e.SupplierId,
                DebtorOrCreditor_Name = e.Supplier.Name,
                DebtorOrCreditor_Phone = e.Supplier.Phone,
                Price = e.Price,
                TotalDebt = e.TotalDebt,
                Type = e.Type
            }).OrderByDescending(e => e.CreateDate).ToList();

            return Ok(new PagedList<DebtDto>(dtos,
                       depts.MetaData.TotalCount,
                       depts.MetaData.CurrentPage,
                       depts.MetaData.PageSize));
        }

        [HttpGet]
        [Route("debtors/{id}")]
        public async Task<IActionResult> GetSupplierDebt([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var dept = await _debtRepository.GetSupplierDebt(user.CompanyId, id);
            if (dept == null) return ValidationProblem($"Không tồn tại nợ [{id}]");

            var dto = new DebtDto()
            {
                Id = dept.Id,
                CreateDate = dept.CreateDate,
                DebtorOrCreditor_Address = dept.Supplier.Address,
                DebtorOrCreditor_Id = dept.SupplierId,
                DebtorOrCreditor_Name = dept.Supplier.Name,
                DebtorOrCreditor_Phone = dept.Supplier.Phone,
                Price = dept.Price,
                TotalDebt = dept.TotalDebt,
                Type = dept.Type
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("debtors")]
        public async Task<IActionResult> CreateSupplierDebt(DebtRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = new DebtSupplier
            {
                Id = request.Id,
                CreateDate = DateTime.Now,
                SupplierId = request.DebtorOrCreditor_Id,
                Price = request.Repayment,
                Type = 2,
            };

            var resultData = await _debtRepository.CreateSupplierDebt(obj);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không có nợ để trả");
            }
            return Ok(resultData);
        }

        [HttpDelete]
        [Route("debtors/{id}")]
        public async Task<IActionResult> DeleteSupplierDebt(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var dept = await _debtRepository.GetSupplierDebt(user.CompanyId, id);
            if (dept == null) return ValidationProblem("[1] Không thể xóa nợ nhà tài trợ");

            var resultData = await _debtRepository.DeleteSupplierDebt(dept);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không có nợ để trả");
                case 2: return ValidationProblem("[2] Không thể xóa nợ nhà cung cấp");
                case 3: return ValidationProblem("[3] Chỉ có thể xóa lần trả nợ cuối cùng");
            }
            return Ok(resultData);
        }


        [HttpGet]
        [Route("creditors")]
        public async Task<IActionResult> GetCustomerDebts([FromQuery] DebtCustomerSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var depts = await _debtRepository.GetCustomerDebts(user.CompanyId, search);
            var dtos = depts.Items.Select(e => new DebtDto()
            {
                Id = e.Id,
                CreateDate = e.CreateDate,
                DebtorOrCreditor_Address = e.Customer.CustomerAddress,
                DebtorOrCreditor_Id = e.CustomerId,
                DebtorOrCreditor_Name = e.Customer.CustomerName,
                DebtorOrCreditor_Phone = e.Customer.CustomerPhone,
                Price = e.Debt,
                TotalDebt = e.TotalDebt,
                Type = e.Type
            }).OrderByDescending(e => e.CreateDate).ToList();

            return Ok(new PagedList<DebtDto>(dtos,
                       depts.MetaData.TotalCount,
                       depts.MetaData.CurrentPage,
                       depts.MetaData.PageSize));
        }

        [HttpGet]
        [Route("creditors/{id}")]
        public async Task<IActionResult> GetCustomerDebt([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var dept = await _debtRepository.GetCustomerDebt(user.CompanyId, id);
            if (dept == null) return ValidationProblem($"Không tồn tại nợ [{id}]");

            var dto = new DebtDto()
            {
                Id = dept.Id,
                CreateDate = dept.CreateDate,
                DebtorOrCreditor_Address = dept.Customer.CustomerAddress,
                DebtorOrCreditor_Id = dept.CustomerId,
                DebtorOrCreditor_Name = dept.Customer.CustomerName,
                DebtorOrCreditor_Phone = dept.Customer.CustomerPhone,
                Price = dept.Debt,
                TotalDebt = dept.TotalDebt,
                Type = dept.Type
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("creditors")]
        public async Task<IActionResult> CreateCustomerDebt(DebtRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = new DebtCustomer
            {
                Id = request.Id,
                CreateDate = DateTime.Now,
                CustomerId = request.DebtorOrCreditor_Id,
                Debt = request.Repayment
            };

            var resultData = await _debtRepository.CreateCustomerDebt(obj);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không có nợ để trả");
            }
            return Ok(resultData);
        }

        [HttpDelete]
        [Route("creditors/{id}")]
        public async Task<IActionResult> DeleteCustomerDebt(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var dept = await _debtRepository.GetCustomerDebt(user.CompanyId, id);
            if (dept == null) return ValidationProblem($"Không tồn tại nợ [{id}]");

            var resultData = await _debtRepository.DeleteCustomerDebt(dept);
            switch (resultData)
            {
                case 0: return Ok(resultData);
                case 1: return ValidationProblem("[1] Không có nợ để trả");
                case 2: return ValidationProblem("[2] Không thể xóa nợ của khách hàng");
                case 3: return ValidationProblem("[3] Chỉ có thể xóa lần trả nợ cuối cùng");
            }
            return Ok(resultData);
        }
    }
}
