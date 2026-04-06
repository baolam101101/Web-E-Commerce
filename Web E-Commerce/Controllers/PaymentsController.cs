using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Customer")]
    public class PaymentsController(IPaymentService paymentService) : ControllerBase
    {
        // giả lập VNPay callback
        [HttpGet("callback")]
        public async Task<IActionResult> Callback(
            [FromQuery] string txnRef,
            [FromQuery] bool success)
        {
            await paymentService.HandlePaymentCallback(txnRef, success);

            return Ok("Payment processed");
        }
    }
}
