using Web_E_Commerce.DTOs.Cart.Requests;
using Web_E_Commerce.DTOs.Cart.Responses;
using Web_E_Commerce.DTOs.Shared;
namespace Web_E_Commerce.Services.Interfaces
{
    public interface ICartService
    {
        Task<ApiResponse<CartResponse>> GetMyCart();
        Task<ApiResponse<CartResponse>> AddToCart(AddToCartRequest request);
        Task<ApiResponse<CartResponse>> UpdateCart(UpdateCartRequest request);
        Task<ApiResponse<bool>> RemoveItem(Guid cartItemId);
    }
}