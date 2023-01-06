using CavuTechTest.DAL.IRepository;
using MediatR;

namespace CavuTechTest.Mediation.Queries.Handlers
{
    /// <summary>
    /// <see cref="SummerPriceQuery"/>
    /// </summary>
    public class SummerPriceQueryHandler : IRequestHandler<SummerPriceQuery, decimal>
    {
        private readonly IPricingRepository _pricingRepository;

        public SummerPriceQueryHandler(IPricingRepository pricingRepository)
        {
            _pricingRepository = pricingRepository;
        }

        public Task<decimal> Handle(SummerPriceQuery request, CancellationToken cancellationToken)
        {
            // get price entity
            var price = _pricingRepository.GetSummerPricing();

            return Task.FromResult(price);
        }
    }
}