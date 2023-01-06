using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CavuTechTest.DAL.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Id")]
        public int ParkingSpaceId { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}