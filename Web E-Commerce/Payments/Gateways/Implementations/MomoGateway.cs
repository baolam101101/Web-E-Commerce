using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Payments.Gateways.Interfaces;
using Web_E_Commerce.Utilities;

namespace Web_E_Commerce.Payments.Gateways.Implementations
{
    public class MomoGateway : IPaymentGateway
    {
        public PaymentMethod Method => PaymentMethod.Momo;

        public Task<(string url, string transactionId)> CreatePaymentUrl(Order order)
        {
            var transactionId = TransactionIdHelper.Generate("MM");

            var url = $"https://momo.vn/pay?txnRef={transactionId}";

            return Task.FromResult((url, transactionId));
        }

        public Task HandleCallback(string transactionId, bool success)
        {
            // mock thôi, xử lý thật thì verify chữ ký Momo gửi về, kiểm tra trạng thái giao dịch qua API của MoMo, rồi cập nhật trạng thái đơn hàng trong database
            return Task.CompletedTask;
        }
    }
}
