using System.Text.Json;
using BeverageDistributor.Models;

namespace BeverageDistributor.Services
{
    /// <summary>
    /// Service class for managing data persistence using JSON files
    /// </summary>
    public class DataService
    {
        private readonly string _dataDirectory;
        private readonly string _productsFile;
        private readonly string _customersFile;
        private readonly string _ordersFile;
        private readonly string _usersFile;
        private readonly string _salesPeopleFile;

        /// <summary>
        /// Initializes a new instance of the DataService class
        /// </summary>
        public DataService()
        {
            _dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BeverageDistributor");
            _productsFile = Path.Combine(_dataDirectory, "products.json");
            _customersFile = Path.Combine(_dataDirectory, "customers.json");
            _ordersFile = Path.Combine(_dataDirectory, "orders.json");
            _usersFile = Path.Combine(_dataDirectory, "users.json");
            _salesPeopleFile = Path.Combine(_dataDirectory, "salespeople.json");

            // Ensure data directory exists
            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
            }

            // Initialize data files if they don't exist
            InitializeDataFiles();
        }

        /// <summary>
        /// Initializes data files with sample data if they don't exist
        /// </summary>
        private void InitializeDataFiles()
        {
            if (!File.Exists(_productsFile))
            {
                var products = SampleDataGenerator.GenerateSampleProducts();
                SaveProducts(products);
            }

            if (!File.Exists(_usersFile))
            {
                var users = SampleDataGenerator.GenerateSampleUsers();
                SaveUsers(users);
            }

            if (!File.Exists(_customersFile))
            {
                var customers = SampleDataGenerator.GenerateSampleCustomers();
                SaveCustomers(customers);
            }

            if (!File.Exists(_ordersFile))
            {
                SaveOrders(new List<Order>());
            }

            if (!File.Exists(_salesPeopleFile))
            {
                var salesPeople = SampleDataGenerator.GenerateSampleSalesPeople();
                SaveSalesPeople(salesPeople);
            }
        }

        /// <summary>
        /// Loads all products from the data file
        /// </summary>
        /// <returns>List of products</returns>
        public List<Product> LoadProducts()
        {
            try
            {
                if (File.Exists(_productsFile))
                {
                    var json = File.ReadAllText(_productsFile);
                    return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new List<Product>();
        }

        /// <summary>
        /// Saves all products to the data file
        /// </summary>
        /// <param name="products">List of products to save</param>
        public void SaveProducts(List<Product> products)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(products, options);
                File.WriteAllText(_productsFile, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads all customers from the data file
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> LoadCustomers()
        {
            try
            {
                if (File.Exists(_customersFile))
                {
                    var json = File.ReadAllText(_customersFile);
                    return JsonSerializer.Deserialize<List<Customer>>(json) ?? new List<Customer>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new List<Customer>();
        }

        /// <summary>
        /// Saves all customers to the data file
        /// </summary>
        /// <param name="customers">List of customers to save</param>
        public void SaveCustomers(List<Customer> customers)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(customers, options);
                File.WriteAllText(_customersFile, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads all orders from the data file
        /// </summary>
        /// <returns>List of orders</returns>
        public List<Order> LoadOrders()
        {
            try
            {
                if (File.Exists(_ordersFile))
                {
                    var json = File.ReadAllText(_ordersFile);
                    return JsonSerializer.Deserialize<List<Order>>(json) ?? new List<Order>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new List<Order>();
        }

        /// <summary>
        /// Saves all orders to the data file
        /// </summary>
        /// <param name="orders">List of orders to save</param>
        public void SaveOrders(List<Order> orders)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(orders, options);
                File.WriteAllText(_ordersFile, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads all users from the data file
        /// </summary>
        /// <returns>List of users</returns>
        public List<User> LoadUsers()
        {
            try
            {
                if (File.Exists(_usersFile))
                {
                    var json = File.ReadAllText(_usersFile);
                    return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new List<User>();
        }

        /// <summary>
        /// Saves all users to the data file
        /// </summary>
        /// <param name="users">List of users to save</param>
        public void SaveUsers(List<User> users)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(users, options);
                File.WriteAllText(_usersFile, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads all sales people from the data file
        /// </summary>
        /// <returns>List of sales people</returns>
        public List<SalesPerson> LoadSalesPeople()
        {
            try
            {
                if (File.Exists(_salesPeopleFile))
                {
                    var json = File.ReadAllText(_salesPeopleFile);
                    return JsonSerializer.Deserialize<List<SalesPerson>>(json) ?? new List<SalesPerson>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales people: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new List<SalesPerson>();
        }

        /// <summary>
        /// Saves all sales people to the data file
        /// </summary>
        /// <param name="salesPeople">List of sales people to save</param>
        public void SaveSalesPeople(List<SalesPerson> salesPeople)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(salesPeople, options);
                File.WriteAllText(_salesPeopleFile, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving sales people: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Authenticates a user with the given username and password
        /// </summary>
        /// <param name="username">Username to authenticate</param>
        /// <param name="password">Password to authenticate</param>
        /// <returns>Authenticated user or null if authentication fails</returns>
        public User? AuthenticateUser(string username, string password)
        {
            var users = LoadUsers();
            return users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) 
                                           && u.Password == password && u.IsActive);
        }

        /// <summary>
        /// Gets the next available ID for a given entity type
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entities">List of entities</param>
        /// <param name="idProperty">ID property name</param>
        /// <returns>Next available ID</returns>
        public int GetNextId<T>(List<T> entities, string idProperty = "Id")
        {
            if (!entities.Any())
                return 1;

            var maxId = entities.Max(e => 
            {
                var property = e.GetType().GetProperty(idProperty);
                var value = property?.GetValue(e);
                return value != null ? (int)value : 0;
            });
            return maxId + 1;
        }
    }
} 