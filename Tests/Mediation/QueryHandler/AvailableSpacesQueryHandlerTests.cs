using CavuTechTest.DataAccess.IReadOnlyRepository;
using CavuTechTest.Mediation.Queries;
using CavuTechTest.Mediation.Queries.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CavuTechTest.Tests.Mediation.QueryHandler
{
    [TestClass]
    public class AvailableSpacesQueryHandlerTests
    {
        private readonly Mock<IAvailableSpacesRepository> _repo = new();

        private AvailableSpacesQueryHandler _handler;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _handler = new AvailableSpacesQueryHandler(_repo.Object);
        }

        [TestMethod]
        public async Task Handle_GetAvailableSpacesMethod_IsCalled()
        {
            // arrange 
            var query = new AvailableSpacesQuery(DateTime.Now.AddDays(1), DateTime.Now);

            _repo.Setup(x => x.GetAvailableSpaces(DateTime.Now)).Returns(2);

            // act
            var result = await _handler.Handle(query, CancellationToken.None);

            // assert
            Assert.IsNotNull(result);
            _repo.Verify(r => r.GetAvailableSpaces(It.IsAny<DateTime>()), Times.Once());
        }

        [TestMethod]
        public async Task Handle_GetAvailableSpacesMethod_CalledMultipleTimes_ForMultipleDates()
        {
            // arrange
            var query = new AvailableSpacesQuery(DateTime.Now.AddDays(3), DateTime.Now);

            _repo.Setup(x => x.GetAvailableSpaces(DateTime.Now)).Returns(2);

            
            // act
            var result = await _handler.Handle(query, CancellationToken.None);

            // assert
            Assert.IsNotNull(result);
            _repo.Verify(r => r.GetAvailableSpaces(It.IsAny<DateTime>()), Times.Exactly(3));
        }
    }
}