namespace CavuTechTest.Models.Request
{
    /// <summary>
    ///     Request for creating a car parking space booking
    /// </summary>
    public class CreateBookingRequest
    {
        public int Id { get; set; }
        public int ParkingSpaceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}