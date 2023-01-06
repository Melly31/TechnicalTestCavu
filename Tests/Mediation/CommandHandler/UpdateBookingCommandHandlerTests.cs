using AutoBogus;
using AutoMapper;
using CavuTechTest.DAL.IReadOnlyRepository;
using CavuTechTest.Mediation;
using CavuTechTest.Mediation.Commands.Handlers;
using CavuTechTest.Mediation.Commands;
using CavuTechTest.Models.Request;
using CavuTechTest.Models.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Booking = CavuTechTest.DAL.Entities.Booking;

namespace CavuTechTest.Tests.Mediation.CommandHandler
{
    [TestClass]
    public class UpdateBookingCommandHandlerTests
    {
        private readonly AutoFaker<UpdateBookingRequest> _request = new();
        private readonly AutoFaker<Booking> _bookingEntityFaker = new();

        private readonly Mock<IBookingRepository> _repo = new();

        private UpdateBookingCommandHandler _handler;

        [TestInitialize]
        public void BeforeEachTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            _handler = new UpdateBookingCommandHandler(_repo.Object, mapper);
        }

        [TestMethod]
        public async Task Handle_CreateBookingMethod_IsCalled()
        {
            // arrange 
            var entity = _bookingEntityFaker.Generate();
            var request = _request.Generate();

            var command = new UpdateBookingCommand(request);

            _repo.Setup(x => x.UpdateBooking(entity)).ReturnsAsync(entity);

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<UpdateBookingResponse>(result);
            _repo.Verify(r => r.UpdateBooking(It.IsAny<Booking>()), Times.Once());

        }
    }
}