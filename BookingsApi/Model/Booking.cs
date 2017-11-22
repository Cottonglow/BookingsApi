using System.ComponentModel.DataAnnotations;

namespace BookingsApi.Model
{
    public class Booking
    {
        [Key]
        [Required]
        public string SeatId { get; set; }

        public string UserName { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; }

        public BookingType BookingType { get; set; }
    }

    public enum BookingType
    {
        None,
        // Todo: Add a reservation system so that users don't lose their seats while paying or screen loading.
        Reserved,
        Booked
    }
}
