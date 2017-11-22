using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BookingsApi.Data;
using BookingsApi.Model;

namespace BookingsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/bookings")]
    public class BookingsController : Controller
    {
        private readonly DatabaseContext context;

        public BookingsController(DatabaseContext context)
        {
            this.context = context;
        }

        // Todo: Add access restrictions.
        [HttpGet]
        public IActionResult GetAll()
        {
            return new ObjectResult(context.Booking.ToList());
        }

        [Route("available")]
        [HttpGet()]
        public IActionResult GetAllAvailableSeats()
        {
            List<string> availableSeats = context.Booking
                .Where(booking => booking.BookingType == BookingType.None)
                .Select(seat => seat.SeatId)
                .ToList();
            return new ObjectResult(availableSeats);
        }

        // Todo: Add access restrictions.
        [HttpGet("{seatId}", Name = "GetBooking")]
        public IActionResult GetBookingById(string seatId)
        {
            var booking = context.Booking.FirstOrDefault(b => b.SeatId == seatId);

            if (booking == null)
            {
                return NotFound("Seat " + seatId + " not found");
            }

            return new ObjectResult(booking);
        }

        [HttpPut]
        public IActionResult CreateBooking([FromBody] List<Booking> bookingRequests)
        {
            if (bookingRequests == null || bookingRequests.Count > 5)
            {
                return BadRequest("No booking request entered or booking exceeds the maximum allowed seats (4).");
            }

            List<Booking> successfulBookings = new List<Booking>();

            foreach (Booking bookingRequest in bookingRequests)
            {
                if (bookingRequest == null || bookingRequest.SeatId == null || bookingRequest.UserName == null || bookingRequest.UserEmail == null)
                {
                    return BadRequest("Booking request is incomplete. Please ensure seat number, name and email are specified.");
                }

                var booking = context.Booking.FirstOrDefault(b => b.SeatId == bookingRequest.SeatId);

                if (booking == null || booking.BookingType == BookingType.Booked)
                {
                    return BadRequest("Seat " + bookingRequest.SeatId + " has already been booked or doesn't exist. Please select a different seat.");
                }

                if ((context.Booking.FirstOrDefault(u => u.UserName == bookingRequest.UserName && u.UserEmail == bookingRequest.UserEmail)) != null)
                {
                    return BadRequest("The requested person " + bookingRequest.UserName + " already has a seat booked.");
                }

                booking.UserName = bookingRequest.UserName;
                booking.UserEmail = bookingRequest.UserEmail;
                booking.BookingType = BookingType.Booked;

                successfulBookings.Add(booking);
            }

            foreach (Booking booking in successfulBookings)
            {
                context.Booking.Update(booking);
                context.SaveChanges();
            }

            return GetAll();
        }
    }
}
