using AutoMapper;
using CavuTechTest.DAL.Entities;
using CavuTechTest.DAL.IReadOnlyRepository;
using CavuTechTest.Models.Response;
using MediatR;

namespace CavuTechTest.Mediation.Commands.Handlers
{
    /// <summary>
    /// <see cref="CreateBookingCommand"/>
    /// </summary>
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<CreateBookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // Map request model to entity
            var entity = _mapper.Map<Booking>(request.Request);

            // Call repo to create the booking
             await _bookingRepository.CreateBooking(entity);

            // Map entity to response model
            return _mapper.Map<CreateBookingResponse>(entity);
        }
    }
}