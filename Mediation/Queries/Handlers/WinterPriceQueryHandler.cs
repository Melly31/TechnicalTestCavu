using CavuTechTest.DAL.IRepository;
using MediatR;

namespace CavuTechTest.Mediation.Queries.Handlers
{
    /// <summary>
    /// <see cref="WinterPriceQuery"/>
    /// </summary>
    public class WinterPriceQueryHandler : IRequestHandler<WinterPriceQuery, decimal>
    {
        private readonly IPricingRepository _pricingRepository;

        public WinterPriceQueryHandler(IPricingRepository pricingRepository)
        {
            _pricingRepository = pricingRepository;
        }
        public Task<decimal> Handle(WinterPriceQuery request, CancellationToken cancellationToken)
        {
            // get price entity
            var price = _pricingRepository.GetWinterPricing();

            return Task.FromResult(price);
        }
    }
}