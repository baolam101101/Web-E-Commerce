using AutoMapper;
using Web_E_Commerce.DTOs.Cart.Requests;
using Web_E_Commerce.DTOs.Cart.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class CartService(
        ICartRepositories cartRepositories,
        IProductRepositories productRepositories,
        ICurrentUserService currentUser,
        IMapper mapper) : ICartService
    {
        public async Task<ApiResponse<CartResponse>> GetMyCart()
        {
            var userId = currentUser.UserId;

            var cart = await cartRepositories.GetCartByUserIdAsync(userId)
                ?? throw new NotFoundException(
                    MessageKeys.CART_NOT_FOUND,
                    MessageDescriptions.CART_NOT_FOUND
                );

            var response = mapper.Map<CartResponse>(cart);

            return ApiResponse<CartResponse>.Ok(
                response,
                MessageKeys.GET_CART_SUCCESS,
                MessageDescriptions.GET_CART_SUCCESS
            );
        }
        public async Task<ApiResponse<CartResponse>> AddToCart(AddToCartRequest request)
        {
            var userId = currentUser.UserId;

            var product = await productRepositories.GetByIdAsync(request.ProductId)
                ?? throw new NotFoundException(
                    MessageKeys.PRODUCT_NOT_FOUND,
                    MessageDescriptions.PRODUCT_NOT_FOUND
                );

            if (product.Stock < request.Quantity)
                throw new BadRequestException(
                    MessageKeys.NOT_ENOUGH_STOCK,
                    MessageDescriptions.NOT_ENOUGH_STOCK
                );

            var cart = await cartRepositories.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = []
                };

                await cartRepositories.AddAsync(cart);
                await cartRepositories.SaveChangesAsync();
            }

            var existingItem = cart.CartItems
                .FirstOrDefault(x => x.ProductId == request.ProductId);

            if (existingItem != null)
            {
                var newQuantity = existingItem.Quantity + request.Quantity;

                if (newQuantity > product.Stock)
                    throw new BadRequestException(
                        MessageKeys.NOT_ENOUGH_STOCK,
                        $"Chỉ còn {product.Stock - existingItem.Quantity} sản phẩm"
                    );

                existingItem.Quantity = newQuantity;
            }
            else
            {
                if (request.Quantity > product.Stock)
                    throw new BadRequestException(
                        MessageKeys.NOT_ENOUGH_STOCK,
                        MessageDescriptions.NOT_ENOUGH_STOCK
                    );

                cart.CartItems.Add(new CartItem
                {
                    ProductId = product.Id,
                    Quantity = request.Quantity,
                    PriceAtTime = product.Price
                });
            }

            cart.TotalPrice = cart.CartItems.Sum(x => x.PriceAtTime * x.Quantity);

            await cartRepositories.SaveChangesAsync();

            var response = mapper.Map<CartResponse>(cart);

            return ApiResponse<CartResponse>.Ok(
                response,
                MessageKeys.ADD_TO_CART_SUCCESS,
                MessageDescriptions.ADD_TO_CART_SUCCESS
            );
        }
        public async Task<ApiResponse<CartResponse>> UpdateCart(UpdateCartRequest request)
        {
            var cart = await cartRepositories.GetCartByUserIdAsync(currentUser.UserId)
                ?? throw new NotFoundException(
                    MessageKeys.CART_NOT_FOUND,
                    MessageDescriptions.CART_NOT_FOUND
                );

            var item = cart.CartItems.FirstOrDefault(x => x.Id == request.CartItemId)
                ?? throw new NotFoundException(
                    MessageKeys.CART_ITEM_NOT_FOUND,
                    MessageDescriptions.CART_ITEM_NOT_FOUND
                );

            var product = await productRepositories.GetByIdAsync(item.ProductId)
                ?? throw new NotFoundException(
                    MessageKeys.PRODUCT_NOT_FOUND,
                    MessageDescriptions.PRODUCT_NOT_FOUND
                );

            if (product.Stock < request.Quantity)
                throw new BadRequestException(
                    MessageKeys.NOT_ENOUGH_STOCK,
                    MessageDescriptions.NOT_ENOUGH_STOCK
                );

            item.Quantity = request.Quantity;

            cart.TotalPrice = cart.CartItems.Sum(x => x.PriceAtTime * x.Quantity);

            await cartRepositories.UpdateAsync(cart);
            await cartRepositories.SaveChangesAsync();

            return ApiResponse<CartResponse>.Ok(
                mapper.Map<CartResponse>(cart),
                MessageKeys.UPDATE_CART_SUCCESS,
                MessageDescriptions.UPDATE_CART_SUCCESS
            );
        }
        public async Task<ApiResponse<bool>> RemoveItem(Guid cartItemId)
        {
            var cart = await cartRepositories.GetCartByUserIdAsync(currentUser.UserId)
                ?? throw new NotFoundException(
                    MessageKeys.CART_NOT_FOUND,
                    MessageDescriptions.CART_NOT_FOUND
                );

            var item = cart.CartItems.FirstOrDefault(x => x.Id == cartItemId)
                ?? throw new NotFoundException(
                    MessageKeys.CART_ITEM_NOT_FOUND,
                    MessageDescriptions.CART_ITEM_NOT_FOUND
                );

            cart.CartItems.Remove(item);

            cart.TotalPrice = cart.CartItems.Sum(x => x.PriceAtTime * x.Quantity);

            await cartRepositories.UpdateAsync(cart);
            await cartRepositories.SaveChangesAsync();

            return ApiResponse<bool>.Ok(
                true,
                MessageKeys.REMOVE_CART_ITEM_SUCCESS,
                MessageDescriptions.REMOVE_CART_ITEM_SUCCESS
            );
        }
    }
}
