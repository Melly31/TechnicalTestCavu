using CavuTechTest.DAL.Entities;
using CavuTechTest.DAL.IReadOnlyRepository;
using Microsoft.EntityFrameworkCore;

namespace CavuTechTest.DAL.ReadOnlyRepository
{
    /// <summary>
    /// <see cref="IBookingRepository"/>
    /// </summary>
    public class BookingRepository : IBookingRepository
    {
        private readonly DatabaseContext _dbContext;

        public BookingRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Booking? GetBooking(int bookingId)
        {
            var booking = _dbContext.Booking.AsNoTracking()
                    .FirstOrDefault(x => x.Id == bookingId);
            return booking;
        }

        public async Task DeleteBooking(Booking booking)
        {
            _dbContext.Remove(booking);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Booking> CreateBooking(Booking bookingRequest)
        { 
             _dbContext.Add(bookingRequest); 
              await _dbContext.SaveChangesAsync();

            return bookingRequest;
        }

        public async Task<Booking> UpdateBooking(Booking booking)
        {
            _dbContext.Update(booking);
            await _dbContext.SaveChangesAsync();

            return booking;
        }
    }
}