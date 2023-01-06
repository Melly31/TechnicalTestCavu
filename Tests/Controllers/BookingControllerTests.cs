using AutoBogus;
using CavuTechTest.API.Controllers;
using CavuTechTest.Mediation.Commands;
using CavuTechTest.Mediation.Orchestrator;
using CavuTechTest.Models.Exceptions;
using CavuTechTest.Models.Request;
using CavuTechTest.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CavuTechTest.Tests.Controllers
{
    [TestClass]
    public class BookingControllerTests
    {
        private BookingController _controller;
        private readonly Mock<IMediator> _mediator = new();
        private readonly Mock<ILogger<BookingController>> _logger = new();
        private readonly AutoFaker<CreateBookingRequest> _createRequestFaker = new();
        private readonly AutoFaker<UpdateBookingRequest> _updateRequestFaker = new();
        private readonly AutoFaker<CreateBookingResponse> _createResponseFaker = new();
        private readonly AutoFaker<UpdateBookingResponse> _updateResponseFaker = new();


        [TestInitialize]
        public void BeforeEachTest()
        {
             _controller = new BookingController(_mediator.Object, _logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateBooking_Throws_Exception()
        {
            // arrange
            var request = _createRequestFaker.Generate();
            _mediator.Setup(x => x.Send(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Oops something went wrong"));

            // act
            await _controller.CreateBooking(request, CancellationToken.None);
        }

        [TestMethod]
        public async Task CreateBooking_Returns_OkRequest()
        {
            // arrange
            var request = _createRequestFaker.Generate();
            var response = _createResponseFaker.Generate();
            _mediator.Setup(x => x.Send(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // act
            var result = await _controller.CreateBooking(request, CancellationToken.None);

            // assert
            _mediator.Verify(x => x.Send(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsInstanceOfType<OkObjectResult>(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task UpdateBooking_Throws_Exception()
        {
            // arrange
            var request = _updateRequestFaker.Generate();
            _mediator.Setup(x => x.Send(It.IsAny<UpdateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Oops something went wrong"));

            // act
            await _controller.UpdateBooking(request, CancellationToken.None);
        }

        [TestMethod]
        public async Task UpdateBooking_Returns_BadRequest()
        {
            // arrange
            var request = _updateRequestFaker.Generate();
            var response = _updateResponseFaker.Generate();
            request.Id = -1;
            _mediator.Setup(x => x.Send(It.IsAny<UpdateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // act
            var result = await _controller.UpdateBooking(request, CancellationToken.None);

            // assert
            _mediator.Verify(x => x.Send(It.IsAny<UpdateBookingCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public async Task UpdateBooking_Returns_OkRequest()
        {
            // arrange
            var request = _updateRequestFaker.Generate();
            var response = _updateResponseFaker.Generate();
            request.Id = 1;
            _mediator.Setup(x => x.Send(It.IsAny<UpdateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // act
            var result = await _controller.UpdateBooking(request, CancellationToken.None);

            // assert
            _mediator.Verify(x => x.Send(It.IsAny<UpdateBookingCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsInstanceOfType<OkObjectResult>(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteBooking_Throws_Exception()
        {
            // arrange
            const int bookingId = 1;
            _mediator.Setup(x => x.Send(It.IsAny<DeleteBookingOrchestrator>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Oops something went wrong"));

            // act
            await _controller.DeleteBooking(bookingId, CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(BookingNotFoundException))]
        public async Task DeleteBooking_Throws_BookingNotFoundException()
        {
            // arrange
            const int bookingId = 1;
            _mediator.Setup(x => x.Send(It.IsAny<DeleteBookingOrchestrator>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new BookingNotFoundException("Booking was not found"));

            // act
            await _controller.DeleteBooking(bookingId, CancellationToken.None);
        }

        [TestMethod]
        public async Task DeleteBooking_Returns_OkResult()
        {
            // arrange
            const int bookingId = 1;
            _mediator.Setup(x => x.Send(It.IsAny<DeleteBookingOrchestrator>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            // act
            var result = await _controller.DeleteBooking(bookingId, CancellationToken.None);

            // assert
            _mediator.Verify(x => x.Send(It.IsAny<DeleteBookingOrchestrator>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkResult>(result);

        }
    }
}
