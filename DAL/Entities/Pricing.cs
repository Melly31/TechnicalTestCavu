using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using HanaTech.Core;

namespace CavuTechTest.DAL.Entities
{
    public class Pricing
    {
        public int Id { get; set; }
        public decimal PriceIncVat { get; set; }
        public bool IsSummerPrice { get; set; }
    }
}