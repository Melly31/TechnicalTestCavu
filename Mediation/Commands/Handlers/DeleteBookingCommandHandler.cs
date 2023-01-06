using AutoMapper;
using CavuTechTest.DAL.Entities;
using CavuTechTest.DAL.IReadOnlyRepository;
using MediatR;

namespace CavuTechTest.Mediation.Commands.Handlers
{
    /// <summary>
    /// <see cref="DeleteBookingCommand"/>
    /// </summary>
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public DeleteBookingCommandHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            // map model to entity
            var entity = _mapper.Map<Booking>(request.Booking);

            // delete booking row
            await _bookingRepository.DeleteBooking(entity);

            return Unit.Value;
        }
    }
}