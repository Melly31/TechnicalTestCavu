using AutoMapper;
using CavuTechTest.DAL.IReadOnlyRepository;
using CavuTechTest.Models;
using MediatR;

namespace CavuTechTest.Mediation.Queries.Handlers
{
    /// <summary>
    /// <see cref="BookingByBookingIdQuery"/>
    /// </summary>
    public class BookingByBookingIdQueryHandler : IRequestHandler<BookingByBookingIdQuery, Booking>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingByBookingIdQueryHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<Booking> Handle(BookingByBookingIdQuery request, CancellationToken cancellationToken)
        {
            // get customer booking by bookingId
            var entity = _bookingRepository.GetBooking(request.BookingId);

            return _mapper.Map<Booking>(entity);
        }
    }
}