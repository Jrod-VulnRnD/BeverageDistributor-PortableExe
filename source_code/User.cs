namespace BeverageDistributor.Models
{
    /// <summary>
    /// Represents a user (sales representative) in the beverage distributor system
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User's username for login
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// User's password (should be hashed in production)
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's phone number
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// The user's role in the system
        /// </summary>
        public string Role { get; set; } = "Sales Representative";

        /// <summary>
        /// Whether the user account is active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Date when the user was added to the system
        /// </summary>
        public DateTime DateAdded { get; set; } = DateTime.Now;

        /// <summary>
        /// Additional notes about the user
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Returns the user's full name
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Returns a string representation of the user
        /// </summary>
        /// <returns>String representation of the user</returns>
        public override string ToString()
        {
            return $"{FullName} - {Role}";
        }
    }
} 