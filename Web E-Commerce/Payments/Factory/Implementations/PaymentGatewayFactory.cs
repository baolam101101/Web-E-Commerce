using Web_E_Commerce.Enums;
using Web_E_Commerce.Payments.Factory.Interfaces;
using Web_E_Commerce.Payments.Gateways.Interfaces;

namespace Web_E_Commerce.Payments.Factory.Implementations
{
    public class PaymentGatewayFactory : IPaymentGatewayFactory
    {
        private readonly IEnumerable<IPaymentGateway> _gateways;

        public PaymentGatewayFactory(IEnumerable<IPaymentGateway> gateways)
        {
            _gateways = gateways;
        }

        public IPaymentGateway Get(PaymentMethod method)
        {
            return _gateways.First(g => g.Method == method);
        }
    }
}
