using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Utilities
{
    public static class StatusTransitionHelper
    {
        public static bool IsValidStatusTransition(OrderStatus current, OrderStatus next)
        {
            return (current, next) switch
            {
                (OrderStatus.Pending, OrderStatus.Processing) => true,
                (OrderStatus.Pending, OrderStatus.Cancelled) => true,

                (OrderStatus.Processing, OrderStatus.Cancelled) => true,
                (OrderStatus.Processing, OrderStatus.Shipped) => true,

                (OrderStatus.Shipped, OrderStatus.Completed) => true,
                _ => false
            };
        }
    }
}
