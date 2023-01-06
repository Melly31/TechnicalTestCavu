using CavuTechTest.DAL.Entities;

namespace CavuTechTest.DAL.IReadOnlyRepository
{
    /// <summary>
    ///     Repository for booking entity
    /// </summary>
    public interface IBookingRepository
    {
        /// <summary>
        ///     Gets a list of all customer bookings by bookingId
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        public Booking? GetBooking(int bookingId);

        /// <summary>
        ///     Deletes a customer booking entity
        /// </summary>
        /// <param name="booking"></param>
        public Task DeleteBooking(Booking booking);

        /// <summary>
        ///     Creates a customer booking entity
        /// </summary>
        /// <param name="bookingRequest"></param>
        /// <returns></returns>
        public Task<Booking> CreateBooking(Booking bookingRequest);
        /// <summary>
        ///     Updates a customer booking entity
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public Task<Booking> UpdateBooking(Booking booking);
    }
}