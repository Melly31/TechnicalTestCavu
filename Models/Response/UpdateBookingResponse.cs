namespace CavuTechTest.Models.Response
{
    /// <summary>
    ///     Response for updating customer booking
    /// </summary>
    public class UpdateBookingResponse
    {
        public int Id { get; set; }
        public int ParkingSpaceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}