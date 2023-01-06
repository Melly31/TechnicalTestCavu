using CavuTechTest.API.Controllers;
using CavuTechTest.Mediation.Queries;
using CavuTechTest.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CavuTechTest.Tests.Controllers
{
    [TestClass]
    public class AvailabilityControllerTests
    {
        private AvailabilityController _controller;
        private readonly Mock<IMediator> _mediator = new();
        private readonly Mock<ILogger<AvailabilityController>> _logger = new();

        [TestInitialize]
        public void BeforeEachTest()
        {
            _controller = new AvailabilityController(_mediator.Object, _logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetCarSpaceAvailability_Throws_Exception()
        {
            // arrange
            _mediator.Setup(x => x.Send(It.IsAny<AvailableSpacesQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Oops something went wrong"));

            // act
            await _controller.GetCarSpaceAvailability(DateTime.Now, DateTime.Now.AddDays(2), CancellationToken.None);
        }

        [TestMethod]
        public async Task GetCarSpaceAvailability_Returns_OkResult()
        {
            // arrange
            var availableSpaces = new List<AvailableSpaces>
            {
                new() { ParkingSpaceCount = 1, Date = DateTime.Now.Date },
                new() { ParkingSpaceCount = 2, Date = DateTime.Now.AddDays(1).Date}

            };

                _mediator.Setup(x => x.Send(It.IsAny<AvailableSpacesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(availableSpaces);

            // act
            var result = await _controller.GetCarSpaceAvailability(DateTime.Now, DateTime.Now.AddDays(2), CancellationToken.None);

            // assert
            _mediator.Verify(x => x.Send(It.IsAny<AvailableSpacesQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreNotEqual(availableSpaces[0].ParkingSpaceCount, availableSpaces[1].ParkingSpaceCount);
        }

        [TestMethod]
        public async Task GetCarSpaceAvailability_Returns_NoContentResult()
        {
            // arrange
            var availableSpaces = new List<AvailableSpaces>();

            _mediator.Setup(x => x.Send(It.IsAny<AvailableSpacesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(availableSpaces);

            // act
            var result = await _controller.GetCarSpaceAvailability(DateTime.Now, DateTime.Now.AddDays(2), CancellationToken.None);

            // assert
            _mediator.Verify(x => x.Send(It.IsAny<AvailableSpacesQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.IsNotNull(result);
            Assert.IsTrue(availableSpaces.IsNullOrEmpty());
        }
    }
}
