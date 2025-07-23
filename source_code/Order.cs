namespace BeverageDistributor.Models
{
    /// <summary>
    /// Represents an order in the beverage distributor system
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Unique identifier for the order
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Customer ID associated with this order
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Customer object (navigation property)
        /// </summary>
        public Customer? Customer { get; set; }

        /// <summary>
        /// Sales Person ID associated with this order
        /// </summary>
        public int? SalesPersonId { get; set; }

        /// <summary>
        /// Sales Person object (navigation property)
        /// </summary>
        public SalesPerson? SalesPerson { get; set; }

        /// <summary>
        /// The date when the order was created
        /// </summary>
        public DateTime OrderDate { get; set; } = DateTime.Now;

        /// <summary>
        /// The current status of the order
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// The total amount of the order
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Additional notes for the order
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// The list of items in the order
        /// </summary>
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        /// <summary>
        /// Whether the order is active (not deleted)
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Returns a string representation of the order
        /// </summary>
        /// <returns>String representation of the order</returns>
        public override string ToString()
        {
            return $"Order #{OrderId} - {Status} - ${TotalAmount:F2}";
        }
    }

    /// <summary>
    /// Represents an individual item within an order
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Unique identifier for the order item
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// Order ID this item belongs to
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Product ID
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product object (navigation property)
        /// </summary>
        public Product? Product { get; set; }

        /// <summary>
        /// Product name (stored for historical reference)
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Quantity ordered
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Unit price at the time of order
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// The total price for this item (quantity * unit price)
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Additional notes for this order item
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Returns a string representation of the order item
        /// </summary>
        /// <returns>String representation of the order item</returns>
        public override string ToString()
        {
            return $"{ProductName} - Qty: {Quantity} - ${TotalPrice:F2}";
        }
    }
} 