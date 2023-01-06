using CavuTechTest.Mediation.Commands;
using CavuTechTest.Mediation.Queries;
using CavuTechTest.Models.Exceptions;
using MediatR;

namespace CavuTechTest.Mediation.Orchestrator.Handlers
{
    /// <summary>
    /// <see cref="DeleteBookingOrchestrator"/>
    /// </summary>
    public class DeleteBookingOrchestratorHandler : IRequestHandler<DeleteBookingOrchestrator>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DeleteBookingOrchestrator> _logger;

        public DeleteBookingOrchestratorHandler(IMediator mediator, ILogger<DeleteBookingOrchestrator> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteBookingOrchestrator request, CancellationToken cancellationToken)
        {
            // get customer booking by bookingId
            var booking = _mediator.Send(new BookingByBookingIdQuery(request.BookingId), cancellationToken);
            
            // throw error if no booking
            if (booking.Result == null)
            {
                _logger.LogWarning("Booking not found for booking Id {bookingId}", request.BookingId);
                throw new BookingNotFoundException("Booking not found for requested booking Id");
            }

            // delete booking row
            await _mediator.Send(new DeleteBookingCommand(booking.Result), cancellationToken);
            return Unit.Value;
        }
    }
}