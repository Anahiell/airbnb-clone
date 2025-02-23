using Airbnb.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbAPI.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateBooking([FromBody] BookingEntity booking)
        {
            booking.Id = Guid.NewGuid();

            return Ok(booking);
        }
    }
}
