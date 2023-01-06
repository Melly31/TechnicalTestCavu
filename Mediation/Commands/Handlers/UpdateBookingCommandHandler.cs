using AutoMapper;
using CavuTechTest.DAL.Entities;
using CavuTechTest.DAL.IReadOnlyRepository;
using CavuTechTest.Models.Response;
using MediatR;

namespace CavuTechTest.Mediation.Commands.Handlers
{
    /// <summary>
    /// <see cref="UpdateBookingCommand"/>
    /// </summary>
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, UpdateBookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public UpdateBookingCommandHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<UpdateBookingResponse> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            // Map request model to entity
            var entity = _mapper.Map<Booking>(request.Request);

            // Call repo to update the booking
            await _bookingRepository.UpdateBooking(entity);
            
            // Map to booking model to return
            return _mapper.Map<UpdateBookingResponse>(entity);
        }
    }
}