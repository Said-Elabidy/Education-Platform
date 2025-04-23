using Education.Application.Services.PaymentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;
        public PaymentController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }
        [HttpGet("ByuserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaymentsByUserId()
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");
            var payments = await _paymentServices.GetPaymentsByUserId(userId);
            if (payments == null || !payments.Any())
            {
                return NotFound("No payments found for the specified user.");
            }
            return Ok(payments);
        }
        [HttpGet("GetPaymentsByCourseId/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaymentsByCourseId(int courseId)
        {
            var payments = await _paymentServices.GetPaymentsByCourseId(courseId);
            if (payments == null || !payments.Any())
            {
                return NotFound("No payments found for the specified course.");
            }
            return Ok(payments);
        }
        [HttpGet("ByCourseIdAndUserId/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPayment( int courseId)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");
            var payment = await _paymentServices.GetPayment(userId, courseId);
            if (payment == null)
            {
                return NotFound("Payment not found for the specified user and course.");
            }
            return Ok(payment);
        }
        [HttpGet("IsPaidByCourseIdAndUserId/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IsPaid( int courseId)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            var isPaid = await _paymentServices.IsPaid(userId, courseId);
            if (!isPaid)
            {
                return NotFound("Payment not found for the specified user and course.");
            }
            return Ok(isPaid);
        }
        [HttpGet("IsPaidByuserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IsPaid()
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");
            var isPaid = await _paymentServices.IsPaid(userId);
            if (!isPaid)
            {
                return NotFound("Payment not found for the specified user.");
            }
            return Ok(isPaid);
        }
    }
}
