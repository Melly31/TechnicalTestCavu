using CavuTechTest.Models;
using MediatR;

namespace CavuTechTest.Mediation.Commands
{
    /// <summary>
    ///     Command to delete a customer booking
    /// </summary>
    /// <param name="Booking"></param>
    public record DeleteBookingCommand(Booking Booking) : IRequest;
}