using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Order.Requests;
using Web_E_Commerce.DTOs.Order.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class OrderService(
    ICartRepositories cartRepositories,
    IProductRepositories productRepositories,
    IOrderRepositories orderRepositories,
    ICurrentUserService currentUser,
    IMapper mapper,
    AppDbContext context) : IOrderService
    {
        public async Task<ApiResponse<OrderResponse>> Checkout(OrderCheckoutRequest request)
        {
            var userId = currentUser.UserId;

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var cart = await cartRepositories.GetCartByUserIdAsync(userId)
                    ?? throw new NotFoundException(
                        MessageKeys.CART_NOT_FOUND,
                        MessageDescriptions.CART_NOT_FOUND
                    );

                if (!cart.CartItems.Any())
                    throw new BadRequestException(
                        MessageKeys.CART_EMPTY,
                        MessageDescriptions.CART_EMPTY
                    );

                // load product 1 lần (đã đúng)
                var productIds = cart.CartItems.Select(x => x.ProductId).ToList();
                var products = await productRepositories.GetByIdsAsync(productIds);
                var productDict = products.ToDictionary(p => p.Id);

                var orderItems = new List<OrderItem>();
                decimal totalAmount = 0;

                foreach (var item in cart.CartItems)
                {
                    if (!productDict.TryGetValue(item.ProductId, out var product))
                        throw new NotFoundException(
                            MessageKeys.PRODUCT_NOT_FOUND,
                            MessageDescriptions.PRODUCT_NOT_FOUND
                        );

                    if (product.Stock < item.Quantity)
                        throw new BadRequestException(
                            MessageKeys.NOT_ENOUGH_STOCK,
                            MessageDescriptions.NOT_ENOUGH_STOCK
                        );

                    product.Stock -= item.Quantity;

                    var orderItem = new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.PriceAtTime,
                        TotalPrice = item.PriceAtTime * item.Quantity
                    };

                    totalAmount += orderItem.TotalPrice;
                    orderItems.Add(orderItem);
                }

                var order = new Order
                {
                    UserId = userId,
                    TotalAmount = totalAmount,
                    OrderStatus = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.Pending,
                    PaymentMethod = request.PaymentMethod,
                    ShippingAddress = request.ShippingAddress,
                    PhoneNumber = request.PhoneNumber,
                    OrderItems = orderItems
                };

                // clear cart TRƯỚC khi save
                cart.CartItems.Clear();
                cart.TotalPrice = 0;

                // chỉ add 1 lần
                await orderRepositories.AddAsync(order);

                // SAVE DUY NHẤT 1 LẦN
                await orderRepositories.SaveChangesAsync();
                // reload lại order từ DB
                await context.Entry(order).ReloadAsync();

                await transaction.CommitAsync();

                return ApiResponse<OrderResponse>.Ok(
                    mapper.Map<OrderResponse>(order),
                    MessageKeys.CREATE_ORDER_SUCCESS,
                    MessageDescriptions.CREATE_ORDER_SUCCESS
                );
            }
            catch
            {
                throw;
            }
        }

        public async Task<ApiResponse<List<OrderResponse>>> GetMyOrders()
        {
            var userId = currentUser.UserId;

            var orders = await orderRepositories.GetByUserIdAsync(userId);

            var response = mapper.Map<List<OrderResponse>>(orders);

            return ApiResponse<List<OrderResponse>>.Ok(
                response,
                MessageKeys.GET_ORDERS_SUCCESS,
                MessageDescriptions.GET_ORDERS_SUCCESS
            );
        }

        public async Task<ApiResponse<OrderResponse>> GetByIdAsync(Guid id)
        {
            var order = await orderRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(
                    MessageKeys.ORDER_NOT_FOUND,
                    MessageDescriptions.ORDER_NOT_FOUND
                );

            var response = mapper.Map<OrderResponse>(order);

            return ApiResponse<OrderResponse>.Ok(
                response,
                MessageKeys.GET_ORDER_SUCCESS,
                MessageDescriptions.GET_ORDER_SUCCESS
            );
        }
    }
}