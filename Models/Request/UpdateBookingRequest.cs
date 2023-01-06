namespace CavuTechTest.Models.Request
{
    /// <summary>
    ///     Request for updating customer booking
    /// </summary>
    public class UpdateBookingRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ParkingSpaceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}