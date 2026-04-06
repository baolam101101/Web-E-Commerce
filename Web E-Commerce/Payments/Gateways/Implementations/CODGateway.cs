using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Payments.Gateways.Interfaces;
using Web_E_Commerce.Utilities;

namespace Web_E_Commerce.Payments.Gateways.Implementations
{
    public class CODGateway : IPaymentGateway
    {
        public PaymentMethod Method => PaymentMethod.COD;

        public Task<(string url, string transactionId)> CreatePaymentUrl(Order order)
        {
            // COD không cần URL nhưng vẫn cần transactionId để tracking
            var transactionId = TransactionIdHelper.Generate("COD");

            return Task.FromResult<(string, string)>((null!, transactionId));
        }

        public Task HandleCallback(string transactionId, bool success)
        {
            // COD không có callback thật
            return Task.CompletedTask;
        }
    }
}