using AutoBogus;
using AutoMapper;
using CavuTechTest.DAL.Entities;
using CavuTechTest.DAL.IReadOnlyRepository;
using CavuTechTest.Mediation;
using CavuTechTest.Mediation.Commands.Handlers;
using CavuTechTest.Mediation.Commands;
using CavuTechTest.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CavuTechTest.Tests.Mediation.CommandHandler
{
    [TestClass]
    public class CreateBookingCommandHandlerTests
    {
        private readonly AutoFaker<CreateBookingRequest> _createBookingRequestFaker = new();
        private readonly AutoFaker<Booking> _bookingFaker = new();
        private readonly Mock<IBookingRepository> _repo = new();

        private CreateBookingCommandHandler _handler;

        [TestInitialize]
        public void BeforeEachTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            _handler = new CreateBookingCommandHandler(_repo.Object, mapper);
        }

        [TestMethod]
        public async Task Handle_CreateBookingMethod_IsCalled()
        {
            // arrange 
            var booking = _bookingFaker.Generate();
            var request = _createBookingRequestFaker.Generate();
            var command = new CreateBookingCommand(request);

            _repo.Setup(x => x.CreateBooking(booking)).ReturnsAsync(booking);

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.IsNotNull(result);
            _repo.Verify(r => r.CreateBooking(It.IsAny<Booking>()), Times.Once());
        }
    }
}
