using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Payments.Gateways.Interfaces
{
    public interface IPaymentGateway
    {
        PaymentMethod Method { get; }

        Task<(string url, string transactionId)> CreatePaymentUrl(Order order);

        Task HandleCallback(string transactionId, bool success);
    }
}
