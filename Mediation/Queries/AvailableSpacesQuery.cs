using CavuTechTest.Models;
using MediatR;
namespace CavuTechTest.Mediation.Queries
{
    /// <summary>
    ///  Query to get the available car park spaces
    /// </summary>
    public record AvailableSpacesQuery(DateTime ToDate, DateTime FromDate) : IRequest<List<AvailableSpaces>>;
}