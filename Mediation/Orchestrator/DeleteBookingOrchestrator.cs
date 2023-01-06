using MediatR;

namespace CavuTechTest.Mediation.Orchestrator
{
    /// <summary>
    ///     Orchestrator to get a customer booking and delete it
    /// </summary>
    public record DeleteBookingOrchestrator(int BookingId) : IRequest;
}