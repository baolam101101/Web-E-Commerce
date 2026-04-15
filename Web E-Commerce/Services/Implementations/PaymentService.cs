using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Payments.Factory.Interfaces;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class PaymentService(
    //IOrderRepositories orderRepo,
    IPaymentGatewayFactory paymentFactory,
    AppDbContext context) : IPaymentService
    {
        public async Task HandlePaymentCallback(string transactionId, bool success)
        {
            var payment = await context.Payments
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId)
                ?? throw new NotFoundException(
                    MessageKeys.PAYMENT_NOT_FOUND,
                    MessageDescriptions.PAYMENT_NOT_FOUND
                );

            var order = await context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == payment.OrderId)
                ?? throw new NotFoundException(
                    MessageKeys.ORDER_NOT_FOUND,
                    MessageDescriptions.ORDER_NOT_FOUND
                );

            var shipping = await context.Shippings
                .FirstOrDefaultAsync(s => s.OrderId == order.Id);

            var gateway = paymentFactory.Get(order.PaymentMethod);
            await gateway.HandleCallback(transactionId, success);

            // ❗ Tránh xử lý lại nhiều lần
            if (payment.PaymentStatus == PaymentStatus.Paid)
                return;

            if (success)
            {
                payment.PaymentStatus = PaymentStatus.Paid;

                order.PaymentStatus = PaymentStatus.Paid;
                order.OrderStatus = OrderStatus.Processing;
                order.PaidAt = DateTime.UtcNow;

                if (shipping != null)
                    shipping.Status = ShippingStatus.Packing;
            }
            else
            {
                payment.PaymentStatus = PaymentStatus.Failed;

                order.PaymentStatus = PaymentStatus.Failed;
                order.OrderStatus = OrderStatus.Cancelled;

                // 🔥 ROLLBACK STOCK
                foreach (var item in order.OrderItems)
                {
                    var product = await context.Products
                        .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                    if (product != null)
                    {
                        product.Stock += item.Quantity;
                    }
                }

                if (shipping != null)
                    shipping.Status = ShippingStatus.Failed;
            }

            await context.SaveChangesAsync();
        }
    }
}
