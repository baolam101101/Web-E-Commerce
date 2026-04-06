using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Payments.Gateways.Interfaces;
using Web_E_Commerce.Utilities;

namespace Web_E_Commerce.Payments.Gateways.Implementations
{
    public class VNPayGateway : IPaymentGateway
    {
        public PaymentMethod Method => PaymentMethod.VNPay;

        public Task<(string url, string transactionId)> CreatePaymentUrl(Order order)
        {
            var transactionId = TransactionIdHelper.Generate("VNP");

            var url = $"https://sandbox.vnpay.vn/pay?txnRef={transactionId}";

            return Task.FromResult((url, transactionId));
        }

        public Task HandleCallback(string transactionId, bool success)
        {
            // mock thôi, xử lý thật thì verify chữ ký VNPay
            return Task.CompletedTask;
        }
    }
}
