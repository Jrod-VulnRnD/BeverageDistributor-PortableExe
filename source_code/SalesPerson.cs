using System;

namespace BeverageDistributor.Models
{
    public class SalesPerson
    {
        public int SalesPersonId { get; set; }
        public string SalesId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name} ({SalesId}) - {Region}";
        }
    }
} 