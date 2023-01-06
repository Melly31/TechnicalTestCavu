using CavuTechTest.DataAccess.IReadOnlyRepository;
using CavuTechTest.Models;
using MediatR;
namespace CavuTechTest.Mediation.Queries.Handlers
{
    /// <summary>
    /// <see cref="AvailableSpacesQuery"/>
    /// </summary>
    public class AvailableSpacesQueryHandler : IRequestHandler<AvailableSpacesQuery, List<AvailableSpaces>>
    {
        private readonly IAvailableSpacesRepository _availableSpacesRepository;

        public AvailableSpacesQueryHandler(IAvailableSpacesRepository availableSpacesRepository)
        {
            _availableSpacesRepository = availableSpacesRepository;
        }

        public Task<List<AvailableSpaces>> Handle(AvailableSpacesQuery request, CancellationToken cancellationToken)
        {
            var availableSpaces = new List<AvailableSpaces>();
            var dateList = new List<DateTime>();
            var fromDate = request.FromDate;

            while (fromDate <= request.ToDate)
            {
                dateList.Add(fromDate);
                fromDate = fromDate.AddDays(1);
            }

            var datesToCheck = dateList.ToList();
            foreach (var date in datesToCheck)
            {
                var entity = _availableSpacesRepository.GetAvailableSpaces(date);
                availableSpaces.Add(new AvailableSpaces {Date = date, ParkingSpaceCount = entity});
            }

            return Task.FromResult(availableSpaces);
        }
    }
}