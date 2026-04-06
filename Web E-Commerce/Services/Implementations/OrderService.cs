using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.Orders;
using Web_E_Commerce.DTOs.Checkout.Responses;
using Web_E_Commerce.DTOs.Order.Requests;
using Web_E_Commerce.DTOs.Order.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Payments.Factory.Implementations;
using Web_E_Commerce.Payments.Factory.Interfaces;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;
using Web_E_Commerce.Utilities;

namespace Web_E_Commerce.Services.Implementations
{
    public class OrderService(
    ICartRepositories cartRepositories,
    IProductRepositories productRepositories,
    IOrderRepositories orderRepositories,
    ICurrentUserService currentUser,
    IPaymentGatewayFactory paymentGatewayFactory,
    IMapper mapper,
    AppDbContext context) : IOrderService
    {
        public async Task<ApiResponse<CheckoutResponse>> Checkout(OrderCheckoutRequest request)
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

                if (cart.CartItems.Count == 0)
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
                    var success = await productRepositories.UpdateStockAsync(
                        item.ProductId,
                        item.Quantity
                    );

                    if (!success)
                        throw new BadRequestException(
                            MessageKeys.NOT_ENOUGH_STOCK,
                            MessageDescriptions.NOT_ENOUGH_STOCK
                        );

                    var product = productDict[item.ProductId];

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
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    TotalAmount = totalAmount,
                    OrderStatus = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.Pending,
                    PaymentMethod = request.PaymentMethod,
                    ShippingAddress = request.ShippingAddress,
                    PhoneNumber = request.PhoneNumber,
                    OrderItems = orderItems
                };

                var payment = new Payment
                {
                    OrderId = order.Id,
                    Amount = order.TotalAmount,
                    PaymentStatus = PaymentStatus.Pending,
                    PaymentMethod = request.PaymentMethod
                };

                context.Payments.Add(payment);

                //  paymentgateway
                string? paymentUrl = null;

                var gateway = paymentGatewayFactory.Get(request.PaymentMethod);

                // LẤY BOTH url + transactionId
                var (url, transactionId) = await gateway.CreatePaymentUrl(order);

                paymentUrl = url;

                // lưu transactionId vào db
                payment.TransactionId = transactionId;

                // clear cart TRƯỚC khi save
                cart.CartItems.Clear();
                cart.TotalPrice = 0;

                await orderRepositories.AddAsync(order);
                await orderRepositories.SaveChangesAsync();

                await transaction.CommitAsync();

                var response = mapper.Map<OrderResponse>(order);

                var result = new CheckoutResponse
                {
                    Order = response,
                    PaymentUrl = paymentUrl
                };

                return ApiResponse<CheckoutResponse>.Ok(
                    result,
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

        // FOR ADMIN
        public async Task<ApiResponse<PaginationWrapper<OrderResponse>>> GetAllAsync(OrderQueryRequest request)
        {
            var query = context.Orders
                .Include(o => o.OrderItems)
                .AsQueryable();

            // FILTER
            if (request.OrderStatus.HasValue)
            {
                query = query.Where(o => o.OrderStatus == request.OrderStatus.Value);
            }

            if (request.PaymentStatus.HasValue)
            {
                query = query.Where(o => o.PaymentStatus == request.PaymentStatus.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(o => o.UserId == request.UserId.Value);
            }

            // SORT
            query = query.OrderByDescending(o => o.CreatedAt);

            var totalItems = await query.CountAsync();

            var orders = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var mapped = mapper.Map<IEnumerable<OrderResponse>>(orders);

            var pagination = new PaginationWrapper<OrderResponse>(
                request.Page,
                request.PageSize,
                totalItems,
                mapped
            );

            return ApiResponse<PaginationWrapper<OrderResponse>>.Ok(
                pagination,
                MessageKeys.GET_ORDERS_SUCCESS,
                MessageDescriptions.GET_ORDERS_SUCCESS
            );
        }

        public async Task<ApiResponse<OrderResponse>> UpdateStatusAsync(Guid id, UpdateOrderStatusRequest request)
        {
            var order = await orderRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(
                    MessageKeys.ORDER_NOT_FOUND,
                    MessageDescriptions.ORDER_NOT_FOUND
                );

            var currentStatus = order.OrderStatus;

            // Không cho update nếu đã finalized
            if (currentStatus == OrderStatus.Completed ||
                currentStatus == OrderStatus.Cancelled)
            {
                throw new BadRequestException(
                    MessageKeys.ORDER_ALREADY_FINALIZED,
                    MessageDescriptions.ORDER_ALREADY_FINALIZED
                );
            }

            // Validate BEFORE update
            if (!StatusTransitionHelper.IsValidStatusTransition(currentStatus, request.Status))
            {
                throw new BadRequestException(
                    MessageKeys.INVALID_ORDER_STATUS_TRANSITION,
                    $"Cannot change status from {currentStatus} to {request.Status}"
                );
            }

            // ROLLBACK STOCK OPTIMIZED
            if (request.Status == OrderStatus.Cancelled)
            {
                var productIds = order.OrderItems
                    .Select(x => x.ProductId)
                    .Distinct()
                    .ToList();

                var products = await productRepositories.GetByIdsAsync(productIds);

                var productDict = products.ToDictionary(p => p.Id);

                foreach (var item in order.OrderItems)
                {
                    if (!productDict.TryGetValue(item.ProductId, out var product))
                    {
                        throw new NotFoundException(
                            MessageKeys.PRODUCT_NOT_FOUND,
                            MessageDescriptions.PRODUCT_NOT_FOUND
                        );
                    }

                    product.Stock += item.Quantity;
                }
            }

            // UPDATE STATUS
            order.OrderStatus = request.Status;

            // AUTO PAYMENT WHEN COMPLETED
            if (request.Status == OrderStatus.Completed &&
                order.PaymentStatus == PaymentStatus.Pending)
            {
                order.PaymentStatus = PaymentStatus.Paid;
                order.PaidAt = DateTime.UtcNow;
            }

            await orderRepositories.SaveChangesAsync();

            var response = mapper.Map<OrderResponse>(order);

            return ApiResponse<OrderResponse>.Ok(
                response,
                MessageKeys.UPDATE_ORDER_STATUS_SUCCESS,
                MessageDescriptions.UPDATE_ORDER_STATUS_SUCCESS
            );
        }
    }
}