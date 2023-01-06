namespace CavuTechTest.Models.Response
{
    /// <summary>
    ///     Response for creating a car parking space booking
    /// </summary>
    public class CreateBookingResponse
    {
        public int Id { get; set; }
        public int ParkingSpaceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}