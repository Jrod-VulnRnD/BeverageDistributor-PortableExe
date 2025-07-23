namespace BeverageDistributor.Models
{
    /// <summary>
    /// Represents a product in the beverage distributor inventory
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for the product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product name or description
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Product category (e.g., Beer, Wine, Spirits, Soft Drinks, Energy Drinks)
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Brand name of the product
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// Product size/volume (e.g., 330ml, 500ml, 750ml, 1L)
        /// </summary>
        public string Size { get; set; } = string.Empty;

        /// <summary>
        /// Unit price of the product
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Current stock quantity available
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Minimum stock level before reorder alert
        /// </summary>
        public int MinimumStockLevel { get; set; }

        /// <summary>
        /// The product's SKU (Stock Keeping Unit)
        /// </summary>
        public string SKU { get; set; } = string.Empty;

        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Whether the product is currently active/available
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Date when the product was added to inventory
        /// </summary>
        public DateTime DateAdded { get; set; } = DateTime.Now;

        /// <summary>
        /// Last date when the product was updated
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        /// <summary>
        /// Returns a string representation of the product
        /// </summary>
        /// <returns>String representation of the product</returns>
        public override string ToString()
        {
            return $"{Brand} {Name} - {Size} (${UnitPrice:F2})";
        }
    }
} 