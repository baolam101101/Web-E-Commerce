using Web_E_Commerce.Enums;
using Web_E_Commerce.Payments.Gateways.Interfaces;

namespace Web_E_Commerce.Payments.Factory.Interfaces
{
    public interface IPaymentGatewayFactory
    {
        IPaymentGateway Get(PaymentMethod method);
    }
}
