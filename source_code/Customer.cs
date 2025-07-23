namespace BeverageDistributor.Models
{
    /// <summary>
    /// Represents a customer in the beverage distributor system
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Unique identifier for the customer
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Store name
        /// </summary>
        public string StoreName { get; set; } = string.Empty;

        /// <summary>
        /// Store ID for external reference
        /// </summary>
        public string StoreId { get; set; } = string.Empty;

        /// <summary>
        /// Street address
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// State/province
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Sales contact person
        /// </summary>
        public string SalesContact { get; set; } = string.Empty;

        /// <summary>
        /// Customer's email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Customer's postal/zip code
        /// </summary>
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Customer's country
        /// </summary>
        public string Country { get; set; } = "USA";

        /// <summary>
        /// The type of customer (Restaurant, Bar, Retail Store, etc.)
        /// </summary>
        public string CustomerType { get; set; } = string.Empty;

        /// <summary>
        /// Whether the customer is active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Date when the customer was added to the system
        /// </summary>
        public DateTime DateAdded { get; set; } = DateTime.Now;

        /// <summary>
        /// Date when the customer information was last updated
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        /// <summary>
        /// Sales representative assigned to this customer
        /// </summary>
        public string SalesRepresentative { get; set; } = string.Empty;

        /// <summary>
        /// Additional notes about the customer
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Returns a string representation of the customer
        /// </summary>
        /// <returns>String representation of the customer</returns>
        public override string ToString()
        {
            return $"{StoreName} - {City}, {State}";
        }
    }
} 