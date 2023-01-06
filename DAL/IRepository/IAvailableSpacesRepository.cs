namespace CavuTechTest.DataAccess.IReadOnlyRepository
{
    /// <summary>
    ///     Repository for available car spaces
    /// </summary>
    public interface IAvailableSpacesRepository
    {
        /// <summary>
        ///     Gets available car parking spaces from a stored procedure
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        int GetAvailableSpaces(DateTime date);
    }
}