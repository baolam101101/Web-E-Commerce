using Web_E_Commerce.DTOs.Admin.Orders;
using Web_E_Commerce.DTOs.Checkout.Responses;
using Web_E_Commerce.DTOs.Order.Requests;
using Web_E_Commerce.DTOs.Order.Responses;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<CheckoutResponse>> Checkout(OrderCheckoutRequest request);
        Task<ApiResponse<List<OrderResponse>>> GetMyOrders();
        Task<ApiResponse<OrderResponse>> GetByIdAsync(Guid id);
        Task<ApiResponse<PaginationWrapper<OrderResponse>>> GetAllAsync(OrderQueryRequest request);
        Task<ApiResponse<OrderResponse>> UpdateStatusAsync(Guid id, UpdateOrderStatusRequest request);
    }
}