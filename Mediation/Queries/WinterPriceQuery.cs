using MediatR;

namespace CavuTechTest.Mediation.Queries
{
    /// <summary>
    ///     Gets summer price per day
    /// </summary>
    public record WinterPriceQuery : IRequest<decimal>;
}