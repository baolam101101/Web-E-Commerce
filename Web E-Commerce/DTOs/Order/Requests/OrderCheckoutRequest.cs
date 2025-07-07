using System.ComponentModel.DataAnnotations;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.DTOs.Order.Requests
{
    public class OrderCheckoutRequest
    {
        [Required]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại phải có 10 hoặc 11 chữ số.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; set; }
        public bool SetAsDefaultAddress { get; set; } = false;
    }
}