using CavuTechTest.DAL.IRepository;

namespace CavuTechTest.DAL.Repository
{
    /// <summary>
    /// <see cref="IPricingRepository"/>
    /// </summary>
    public class PricingRepository : IPricingRepository
    {
        private readonly DatabaseContext _dbContext;

        public PricingRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public decimal GetSummerPricing()
        {
            var summerPrice = _dbContext.Pricing.Where(x => x.IsSummerPrice == true).Select(x => x.PriceIncVat);
            return summerPrice.Sum();
        }

        public decimal GetWinterPricing()
        {
            var winterPrice = _dbContext.Pricing.Where(x => x.IsSummerPrice == false).Select(x => x.PriceIncVat);
            return winterPrice.Sum();
        }
    }
}