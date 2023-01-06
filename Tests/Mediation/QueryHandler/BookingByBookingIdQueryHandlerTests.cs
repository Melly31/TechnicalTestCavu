using AutoMapper;
using CavuTechTest.DAL.Entities;
using CavuTechTest.DAL.IReadOnlyRepository;
using CavuTechTest.Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CavuTechTest.Mediation.Queries.Handlers;
using CavuTechTest.Mediation.Queries;
using AutoBogus;

namespace CavuTechTest.Tests.Mediation.QueryHandler
{
    [TestClass]
    public class BookingByBookingIdQueryHandlerTests
    {
        private readonly Mock<IBookingRepository> _bookingRepository = new();
        private readonly AutoFaker<Booking> _bookingFaker = new();

        private BookingByBookingIdQueryHandler _handler;

        [TestInitialize]
        public void BeforeEachTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            _handler = new BookingByBookingIdQueryHandler(_bookingRepository.Object, mapper);
        }

        [TestMethod]
        public async Task Handle_GetBookingMethod_IsCalled()
        {
            // arrange 
            const int bookingId = 1;
            var booking = _bookingFaker.Generate();
            var query = new BookingByBookingIdQuery(bookingId);

            _bookingRepository.Setup(x => x.GetBooking(bookingId)).Returns(booking);

            // act
            var result = await _handler.Handle(query, CancellationToken.None);

            // assert
            Assert.IsNotNull(result);
            _bookingRepository.Verify(r => r.GetBooking(bookingId), Times.Once());
        }

        [TestMethod]
        public async Task Handle_GetBookingMethod_IsCalled_ReturnsNull()
        {
            // arrange 
            const int bookingId = 0;
            var booking = default(Booking);
            var query = new BookingByBookingIdQuery(bookingId);

            _bookingRepository.Setup(x => x.GetBooking(bookingId)).Returns(booking);

            // act
            var result = await _handler.Handle(query, CancellationToken.None);

            // assert
            Assert.IsNull(result);
            _bookingRepository.Verify(r => r.GetBooking(bookingId), Times.Once());
        }
    }
}