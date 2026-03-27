using Web_E_Commerce.DTOs.Order.Requests;
using Web_E_Commerce.DTOs.Order.Responses;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<OrderResponse>> Checkout(OrderCheckoutRequest request);
        Task<ApiResponse<List<OrderResponse>>> GetMyOrders();
    }
}
