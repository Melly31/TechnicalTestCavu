using Azure.Core;

namespace CavuTechTest.Models.Exceptions
{
    /// <summary>
    ///     Exception thrown when a booking is not found by booking id
    /// </summary>
    public class BookingNotFoundException : Exception
    {
        public BookingNotFoundException(string message)
            : base(message)
        {
        }
    }
}
