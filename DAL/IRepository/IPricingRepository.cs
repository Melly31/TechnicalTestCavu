namespace CavuTechTest.DAL.IRepository
{
    /// <summary>
    ///     Repository for pricing entity
    /// </summary>
    public interface IPricingRepository
    {
        /// <summary>
        ///     Gets summer pricing per day
        /// </summary>
        /// <returns></returns>
        public decimal GetSummerPricing();

        /// <summary>
        ///     Gets winter pricing per day
        /// </summary>
        /// <returns></returns>
        public decimal GetWinterPricing();
    }
}