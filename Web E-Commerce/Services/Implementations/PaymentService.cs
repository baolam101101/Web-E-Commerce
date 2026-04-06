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
                    MessageDescriptions.PAYMENT_NOT_FOUND);

            var order = await context.Orders
                    .FirstOrDefaultAsync(o => o.Id == payment.OrderId)
                ?? throw new NotFoundException(
                    MessageKeys.ORDER_NOT_FOUND,
                    MessageDescriptions.ORDER_NOT_FOUND);

            // 🔥 gọi gateway (nếu cần verify chữ ký sau này)
            var gateway = paymentFactory.Get(order.PaymentMethod);
            await gateway.HandleCallback(transactionId, success);

            if (success)
            {
                order.PaymentStatus = PaymentStatus.Paid;
                order.PaidAt = DateTime.UtcNow;
                order.OrderStatus = OrderStatus.Completed;

                payment.PaymentStatus = PaymentStatus.Paid;
            }
            else
            {
                order.PaymentStatus = PaymentStatus.Failed;
                order.OrderStatus = OrderStatus.Cancelled;
                payment.PaymentStatus = PaymentStatus.Failed;
            }

            await context.SaveChangesAsync();
        }
    }
}
