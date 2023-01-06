using CavuTechTest.Models;
using MediatR;

namespace CavuTechTest.Mediation.Queries
{
    /// <summary>
    ///     Query to get a customer booking by booking Id
    /// </summary>
    /// <param name="BookingId"></param>
    public record BookingByBookingIdQuery(int BookingId) : IRequest<Booking>;
}