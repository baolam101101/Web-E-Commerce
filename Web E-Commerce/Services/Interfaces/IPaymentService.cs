namespace Web_E_Commerce.Services.Interfaces
{
    public interface IPaymentService
    {
        Task HandlePaymentCallback(string transactionId, bool success);
    }
}
