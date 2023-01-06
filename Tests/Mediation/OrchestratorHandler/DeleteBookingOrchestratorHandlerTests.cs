using AutoBogus;
using CavuTechTest.Mediation.Commands;
using CavuTechTest.Mediation.Orchestrator;
using CavuTechTest.Mediation.Orchestrator.Handlers;
using CavuTechTest.Mediation.Queries;
using CavuTechTest.Models;
using CavuTechTest.Models.Exceptions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CavuTechTest.Tests.Mediation.OrchestratorHandler
{
    [TestClass]
    public class DeleteBookingOrchestratorHandlerTests
    {
        private readonly Mock<IMediator> _mediator = new();
        private readonly Mock<ILogger<DeleteBookingOrchestrator>> _logger = new();
        private readonly AutoFaker<Booking> _bookingFaker = new();

        private DeleteBookingOrchestratorHandler _handler;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _handler = new DeleteBookingOrchestratorHandler(_mediator.Object, _logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(BookingNotFoundException))]
        public async Task Handle_Throws_BookingNotFoundException()
        {
            // arrange
            const int bookingId = 1;
            var booking = default(Booking);
            var orchestrator = new DeleteBookingOrchestrator(bookingId);

            _mediator.Setup(x => x.Send(It.IsAny<BookingByBookingIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);
            _mediator.Setup(x => x.Send(It.IsAny<DeleteBookingOrchestrator>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new BookingNotFoundException("Booking was not found"));

            // act
            await _handler.Handle(orchestrator, CancellationToken.None);
        }


        [TestMethod]
        public async Task Handle_GetBookingNotNull_ReturnsUnit()
        {
            // arrange
            const int bookingId = 1;
            var booking = _bookingFaker.Generate();
            var orchestrator = new DeleteBookingOrchestrator(bookingId);

            _mediator.Setup(x => x.Send(It.IsAny<BookingByBookingIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);
            _mediator.Setup(x => x.Send(It.IsAny<DeleteBookingOrchestrator>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new BookingNotFoundException("Booking was not found"));

            // act
           var result = await _handler.Handle(orchestrator, CancellationToken.None);

            // assert
            Assert.AreEqual(Unit.Value, result);
            _mediator.Verify(x => x.Send(It.IsAny<BookingByBookingIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<DeleteBookingCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
