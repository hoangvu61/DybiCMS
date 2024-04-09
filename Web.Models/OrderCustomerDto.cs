using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class OrderCustomerDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        [MaxLength(200, ErrorMessage = "Tên khách hàng không thể dài hơn 200 ký tự")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [MaxLength(30, ErrorMessage = "Số điện thoại không thể dài hơn 30 ký tự")]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [MaxLength(300, ErrorMessage = "Địa chỉ không thể dài hơn 300 ký tự")]
        public string CustomerAddress { get; set; }
    }
}
