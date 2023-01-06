using AutoBogus;
using AutoMapper;
using CavuTechTest.DAL.Entities;
using CavuTechTest.DAL.IReadOnlyRepository;
using CavuTechTest.Mediation.Commands;
using CavuTechTest.Mediation.Commands.Handlers;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CavuTechTest.Tests.Mediation.CommandHandler
{
    [TestClass]
    public class DeleteBookingCommandHandlerTests
    {
        private DeleteBookingCommandHandler _handler;
        private readonly Mock<IBookingRepository> _repo = new();
        private readonly Mock<IMapper> _mapper = new();
        private readonly AutoFaker<Models.Booking> _bookingFaker = new();
        private readonly AutoFaker<Booking> _entityFaker = new();

        [TestInitialize]
        public void BeforeEachTest()
        {
            _handler = new DeleteBookingCommandHandler(_repo.Object, _mapper.Object);
        }

        [TestMethod]
        public async Task Handle_GetBooking_HasValue_CallsDeleteBooking()
        {
            // arrange 
            const int bookingId = 1;
            var booking = _bookingFaker.Generate();
            var entity = _entityFaker.Generate();

            var command = new DeleteBookingCommand(booking);

            _repo.Setup(x => x.DeleteBooking(entity));

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, Unit.Value);
            _repo.Verify(x => x.DeleteBooking(It.IsAny<Booking>()), Times.Once());

        }
    }
}
