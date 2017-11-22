using System.Linq;

namespace BookingsApi.Data
{
    public static class DbInitialiser
    {
        public static void Initialise(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Booking.Any())
            {
                Model.Booking[] seats = new Model.Booking[100];
                char[] characters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

                for (short i = 0; i < 100; i++)
                {
                    seats[i] = new Model.Booking
                    {
                        SeatId = (characters[i / 10] + ((i % 10) + 1).ToString()).ToString()
                    };
                }
                
                foreach (var seat in seats)
                {
                    context.Booking.Add(seat);
                }
                context.SaveChanges();
            }
        }
    }
}
