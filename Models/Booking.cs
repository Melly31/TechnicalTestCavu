using System.ComponentModel.DataAnnotations.Schema;

namespace CavuTechTest.Models
{
    /// <summary>
    ///     Model for Booking
    /// </summary>
    public class Booking
    { 
        public int Id { get; set; }
        public int ParkingSpaceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
