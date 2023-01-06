using CavuTechTest.API.Controllers;
using CavuTechTest.Mediation.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CavuTechTest.Tests.Controllers
{
    [TestClass]
    public class PriceControllerTests
    {
        private PriceController _controller;
        private readonly Mock<IMediator> _mediator = new();
        private readonly Mock<ILogger<PriceController>> _logger = new();



        [TestInitialize]
        public void BeforeEachTest()
        {
            _controller = new PriceController(_mediator.Object, _logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetSummerPricePerDay_Throws_Exception()
        {
            // arrange
            _mediator.Setup(x => x.Send(It.IsAny<SummerPriceQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Oops something went wrong"));

            // act
            await _controller.GetSummerPricePerDay(CancellationToken.None);
        }

        [TestMethod]
        public async Task GetSummerPricePerDay_Returns_Ok()
        {
            // arrange
            const decimal price = (decimal)2.89;
            _mediator.Setup(x => x.Send(It.IsAny<SummerPriceQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(price);

            // act
            var result = await _controller.GetSummerPricePerDay(CancellationToken.None);

            // assert
            _mediator.Verify(x => x.Send(It.IsAny<SummerPriceQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetWinterPricePerDay_Throws_Exception()
        {
            // arrange
            _mediator.Setup(x => x.Send(It.IsAny<WinterPriceQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Oops something went wrong"));

            // act
            await _controller.GetWinterPricePerDay(CancellationToken.None);
        }

        [TestMethod]
        public async Task GetWinterPricePerDay_Returns_Ok()
        {
            // arrange
            const decimal price = (decimal)2.89;
            _mediator.Setup(x => x.Send(It.IsAny<WinterPriceQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(price);

            // act
            var result = await _controller.GetWinterPricePerDay(CancellationToken.None);

            // assert
            _mediator.Verify(x => x.Send(It.IsAny<WinterPriceQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            Assert.IsNotNull(result);
        }
    }
}
