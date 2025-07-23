using BeverageDistributor.Models;

namespace BeverageDistributor.Services
{
    /// <summary>
    /// Service class for generating sample data for the beverage distributor system
    /// </summary>
    public static class SampleDataGenerator
    {
        /// <summary>
        /// Generates sample products with over 300 inventory items
        /// </summary>
        /// <returns>List of sample products</returns>
        public static List<Product> GenerateSampleProducts()
        {
            var products = new List<Product>();
            var random = new Random();

            // Beer Products
            var beerBrands = new[] { "Budweiser", "Coors", "Miller", "Heineken", "Corona", "Stella Artois", "Guinness", "Blue Moon", "Sam Adams", "Sierra Nevada", "Lagunitas", "Stone", "Dogfish Head", "New Belgium", "Founders", "Bell's", "Deschutes", "Firestone Walker", "Ballast Point", "Victory" };
            var beerNames = new[] { "Lager", "Pilsner", "IPA", "Stout", "Porter", "Wheat Beer", "Amber Ale", "Pale Ale", "Brown Ale", "Saison", "Belgian Ale", "German Lager", "Czech Pilsner", "Irish Stout", "American IPA", "Double IPA", "Imperial Stout", "Wheat Ale", "Golden Ale", "Red Ale" };
            var beerSizes = new[] { "330ml", "355ml", "473ml", "500ml", "650ml", "750ml", "1L", "12oz", "16oz", "22oz" };

            for (int i = 0; i < 150; i++)
            {
                var brand = beerBrands[random.Next(beerBrands.Length)];
                var name = beerNames[random.Next(beerNames.Length)];
                var size = beerSizes[random.Next(beerSizes.Length)];
                var price = Math.Round(random.Next(200, 1500) / 100.0m, 2);
                var stock = random.Next(10, 500);
                var alcohol = Math.Round(random.Next(40, 85) / 10.0m, 1);

                products.Add(new Product
                {
                    ProductId = i + 1,
                    Name = name,
                    Brand = brand,
                    Category = "Beer",
                    Size = size,
                    UnitPrice = price,
                    StockQuantity = stock,
                    MinimumStockLevel = 20,
                    SKU = $"BEER-{brand.Substring(0, 3).ToUpper()}-{name.Substring(0, 3).ToUpper()}-{size}",
                    Description = $"{brand} {name} - A premium {name.ToLower()} with {alcohol}% alcohol content.",
                    IsActive = true,
                    DateAdded = DateTime.Now.AddDays(-random.Next(1, 365)),
                    LastUpdated = DateTime.Now
                });
            }

            // Wine Products
            var wineBrands = new[] { "Robert Mondavi", "Kendall-Jackson", "Beringer", "Chateau Ste. Michelle", "Columbia Crest", "Stag's Leap", "Opus One", "Silver Oak", "Caymus", "Duckhorn", "Jordan", "Rombauer", "Stags' Leap", "Ridge", "Ravenswood", "Bogle", "14 Hands", "Josh Cellars", "Meiomi", "Decoy" };
            var wineNames = new[] { "Cabernet Sauvignon", "Chardonnay", "Merlot", "Pinot Noir", "Sauvignon Blanc", "Zinfandel", "Syrah", "Malbec", "Pinot Grigio", "Riesling", "Sangiovese", "Nebbiolo", "Barbera", "Dolcetto", "Barolo", "Barbaresco", "Chianti", "Brunello", "Amarone", "Prosecco" };
            var wineSizes = new[] { "375ml", "750ml", "1.5L", "3L" };

            for (int i = 0; i < 80; i++)
            {
                var brand = wineBrands[random.Next(wineBrands.Length)];
                var name = wineNames[random.Next(wineNames.Length)];
                var size = wineSizes[random.Next(wineSizes.Length)];
                var price = Math.Round(random.Next(800, 5000) / 100.0m, 2);
                var stock = random.Next(5, 200);
                var alcohol = Math.Round(random.Next(110, 160) / 10.0m, 1);

                products.Add(new Product
                {
                    ProductId = 151 + i,
                    Name = name,
                    Brand = brand,
                    Category = "Wine",
                    Size = size,
                    UnitPrice = price,
                    StockQuantity = stock,
                    MinimumStockLevel = 10,
                    SKU = $"WINE-{brand.Substring(0, 3).ToUpper()}-{name.Substring(0, 3).ToUpper()}-{size}",
                    Description = $"{brand} {name} - A fine {name.ToLower()} with {alcohol}% alcohol content.",
                    IsActive = true,
                    DateAdded = DateTime.Now.AddDays(-random.Next(1, 365)),
                    LastUpdated = DateTime.Now
                });
            }

            // Spirits Products
            var spiritBrands = new[] { "Jack Daniel's", "Jim Beam", "Maker's Mark", "Wild Turkey", "Bulleit", "Woodford Reserve", "Knob Creek", "Evan Williams", "Heaven Hill", "Buffalo Trace", "Grey Goose", "Absolut", "Ketel One", "Tito's", "Belvedere", "Ciroc", "Patron", "Don Julio", "Herradura", "Casamigos" };
            var spiritNames = new[] { "Bourbon", "Whiskey", "Vodka", "Gin", "Rum", "Tequila", "Scotch", "Brandy", "Cognac", "Liqueur", "Single Malt", "Blended Scotch", "Irish Whiskey", "Canadian Whisky", "Rye Whiskey", "Moonshine", "Schnapps", "Amaretto", "Triple Sec", "Kahlua" };
            var spiritSizes = new[] { "375ml", "750ml", "1L", "1.75L" };

            for (int i = 0; i < 60; i++)
            {
                var brand = spiritBrands[random.Next(spiritBrands.Length)];
                var name = spiritNames[random.Next(spiritNames.Length)];
                var size = spiritSizes[random.Next(spiritSizes.Length)];
                var price = Math.Round(random.Next(1500, 8000) / 100.0m, 2);
                var stock = random.Next(5, 150);
                var alcohol = Math.Round(random.Next(350, 500) / 10.0m, 1);

                products.Add(new Product
                {
                    ProductId = 231 + i,
                    Name = name,
                    Brand = brand,
                    Category = "Spirits",
                    Size = size,
                    UnitPrice = price,
                    StockQuantity = stock,
                    MinimumStockLevel = 8,
                    SKU = $"SPIRIT-{brand.Substring(0, 3).ToUpper()}-{name.Substring(0, 3).ToUpper()}-{size}",
                    Description = $"{brand} {name} - Premium {name.ToLower()} with {alcohol}% alcohol content.",
                    IsActive = true,
                    DateAdded = DateTime.Now.AddDays(-random.Next(1, 365)),
                    LastUpdated = DateTime.Now
                });
            }

            // Soft Drinks
            var softDrinkBrands = new[] { "Coca-Cola", "Pepsi", "Sprite", "Fanta", "Mountain Dew", "Dr Pepper", "7-Up", "A&W", "Barq's", "Mello Yello", "Sunkist", "Crush", "Schweppes", "Canada Dry", "Vernors", "Stewart's", "Jones Soda", "Boylan", "Fitz's", "Cheerwine" };
            var softDrinkNames = new[] { "Cola", "Lemon-Lime", "Orange", "Grape", "Cherry", "Vanilla", "Root Beer", "Ginger Ale", "Cream Soda", "Fruit Punch", "Lemonade", "Iced Tea", "Energy Drink", "Sports Drink", "Sparkling Water", "Flavored Water", "Juice", "Smoothie", "Milkshake", "Hot Chocolate" };
            var softDrinkSizes = new[] { "250ml", "330ml", "355ml", "473ml", "500ml", "591ml", "1L", "1.5L", "2L", "12oz", "16oz", "20oz" };

            for (int i = 0; i < 50; i++)
            {
                var brand = softDrinkBrands[random.Next(softDrinkBrands.Length)];
                var name = softDrinkNames[random.Next(softDrinkNames.Length)];
                var size = softDrinkSizes[random.Next(softDrinkSizes.Length)];
                var price = Math.Round(random.Next(50, 400) / 100.0m, 2);
                var stock = random.Next(20, 1000);

                products.Add(new Product
                {
                    ProductId = 291 + i,
                    Name = name,
                    Brand = brand,
                    Category = "Soft Drinks",
                    Size = size,
                    UnitPrice = price,
                    StockQuantity = stock,
                    MinimumStockLevel = 50,
                    SKU = $"SOFT-{brand.Substring(0, 3).ToUpper()}-{name.Substring(0, 3).ToUpper()}-{size}",
                    Description = $"{brand} {name} - Refreshing {name.ToLower()} beverage.",
                    IsActive = true,
                    DateAdded = DateTime.Now.AddDays(-random.Next(1, 365)),
                    LastUpdated = DateTime.Now
                });
            }

            // Energy Drinks
            var energyBrands = new[] { "Red Bull", "Monster", "Rockstar", "NOS", "Full Throttle", "Amp", "Venom", "Xyience", "Bang", "Reign", "Celsius", "Guru", "5-Hour Energy", "Rip It", "Cocaine", "Wired", "Spike", "Bawls", "Go Fast", "Hype" };
            var energyNames = new[] { "Energy Drink", "Energy Shot", "Performance Drink", "Focus Drink", "Stamina Drink", "Power Drink", "Boost Drink", "Charge Drink", "Rush Drink", "Kick Drink" };
            var energySizes = new[] { "250ml", "330ml", "355ml", "473ml", "500ml", "591ml", "750ml", "1L", "8.4oz", "12oz", "16oz", "24oz" };

            for (int i = 0; i < 30; i++)
            {
                var brand = energyBrands[random.Next(energyBrands.Length)];
                var name = energyNames[random.Next(energyNames.Length)];
                var size = energySizes[random.Next(energySizes.Length)];
                var price = Math.Round(random.Next(150, 600) / 100.0m, 2);
                var stock = random.Next(15, 300);

                products.Add(new Product
                {
                    ProductId = 341 + i,
                    Name = name,
                    Brand = brand,
                    Category = "Energy Drinks",
                    Size = size,
                    UnitPrice = price,
                    StockQuantity = stock,
                    MinimumStockLevel = 25,
                    SKU = $"ENERGY-{brand.Substring(0, 3).ToUpper()}-{name.Substring(0, 3).ToUpper()}-{size}",
                    Description = $"{brand} {name} - High-energy {name.ToLower()} with caffeine and vitamins.",
                    IsActive = true,
                    DateAdded = DateTime.Now.AddDays(-random.Next(1, 365)),
                    LastUpdated = DateTime.Now
                });
            }

            return products;
        }

        /// <summary>
        /// Generates sample users (sales representatives)
        /// </summary>
        /// <returns>List of sample users</returns>
        public static List<User> GenerateSampleUsers()
        {
            return new List<User>
            {
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    Password = "admin123",
                    FirstName = "System",
                    LastName = "Administrator",
                    Email = "admin@beveragedistributor.com",
                    Role = "Administrator",
                    IsActive = true,
                    DateAdded = DateTime.Now.AddDays(-365)
                },
                new User
                {
                    UserId = 2,
                    Username = "manager",
                    Password = "manager123",
                    FirstName = "John",
                    LastName = "Manager",
                    Email = "manager@beveragedistributor.com",
                    Role = "Manager",
                    IsActive = true,
                    DateAdded = DateTime.Now.AddDays(-300)
                },
                new User
                {
                    UserId = 3,
                    Username = "sales1",
                    Password = "sales123",
                    FirstName = "Sarah",
                    LastName = "Johnson",
                    Email = "sarah.johnson@beveragedistributor.com",
                    Role = "Sales Representative",
                    IsActive = true,
                    DateAdded = DateTime.Now.AddDays(-250)
                },
                new User
                {
                    UserId = 4,
                    Username = "sales2",
                    Password = "sales123",
                    FirstName = "Mike",
                    LastName = "Davis",
                    Email = "mike.davis@beveragedistributor.com",
                    Role = "Sales Representative",
                    IsActive = true,
                    DateAdded = DateTime.Now.AddDays(-200)
                }
            };
        }

        /// <summary>
        /// Generates sample sales people
        /// </summary>
        /// <returns>List of sample sales people</returns>
        public static List<SalesPerson> GenerateSampleSalesPeople()
        {
            return new List<SalesPerson>
            {
                new SalesPerson
                {
                    SalesPersonId = 1,
                    SalesId = "SMITHERSJ_57842",
                    Name = "Smithers, John",
                    Email = "jsmithers@acmebev.com",
                    Phone = "3823284031",
                    Region = "South"
                },
                new SalesPerson
                {
                    SalesPersonId = 2,
                    SalesId = "RODRIGUEZ_78451",
                    Name = "Rodriguez, Maria",
                    Email = "mrodriguez@acmebev.com",
                    Phone = "4156928473",
                    Region = "North"
                },
                new SalesPerson
                {
                    SalesPersonId = 3,
                    SalesId = "CHEN_92367",
                    Name = "Chen, David",
                    Email = "dchen@acmebev.com",
                    Phone = "5628914567",
                    Region = "West"
                },
                new SalesPerson
                {
                    SalesPersonId = 4,
                    SalesId = "WILLIAMS_45689",
                    Name = "Williams, Sarah",
                    Email = "swilliams@acmebev.com",
                    Phone = "6783459123",
                    Region = "East"
                },
                new SalesPerson
                {
                    SalesPersonId = 5,
                    SalesId = "PATEL_71234",
                    Name = "Patel, Amit",
                    Email = "apatel@acmebev.com",
                    Phone = "8475623901",
                    Region = "Central"
                },
                new SalesPerson
                {
                    SalesPersonId = 6,
                    SalesId = "JOHNSON_34567",
                    Name = "Johnson, Lisa",
                    Email = "ljohnson@acmebev.com",
                    Phone = "9012345678",
                    Region = "South"
                },
                new SalesPerson
                {
                    SalesPersonId = 7,
                    SalesId = "GARCIA_89012",
                    Name = "Garcia, Carlos",
                    Email = "cgarcia@acmebev.com",
                    Phone = "2345678901",
                    Region = "North"
                },
                new SalesPerson
                {
                    SalesPersonId = 8,
                    SalesId = "MILLER_56789",
                    Name = "Miller, Jennifer",
                    Email = "jmiller@acmebev.com",
                    Phone = "4567890123",
                    Region = "West"
                },
                new SalesPerson
                {
                    SalesPersonId = 9,
                    SalesId = "DAVIS_12345",
                    Name = "Davis, Michael",
                    Email = "mdavis@acmebev.com",
                    Phone = "6789012345",
                    Region = "East"
                },
                new SalesPerson
                {
                    SalesPersonId = 10,
                    SalesId = "BROWN_67890",
                    Name = "Brown, Amanda",
                    Email = "abrown@acmebev.com",
                    Phone = "8901234567",
                    Region = "Central"
                }
            };
        }

        /// <summary>
        /// Generates sample customers
        /// </summary>
        /// <returns>List of sample customers</returns>
        public static List<Customer> GenerateSampleCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    StoreName = "ABC Package Store",
                    StoreId = "ABCPS_123432109",
                    Street = "3324 Sandie Plains Rd",
                    City = "Marietta",
                    State = "GA",
                    PhoneNumber = "558 555 1213",
                    SalesContact = "Nolen Ryann",
                    Email = "nolen.ryann@abcpackagestore.com",
                    PostalCode = "30062",
                    Country = "USA",
                    CustomerType = "Package Store",
                    SalesRepresentative = "Smithers, John",
                    Notes = "Premium craft beer focus"
                },
                new Customer
                {
                    CustomerId = 2,
                    StoreName = "Downtown Liquor Mart",
                    StoreId = "DLM_456789012",
                    Street = "125 Main Street",
                    City = "Atlanta",
                    State = "GA",
                    PhoneNumber = "404 555 9876",
                    SalesContact = "Maria Rodriguez",
                    Email = "maria.rodriguez@downtownliquor.com",
                    PostalCode = "30301",
                    Country = "USA",
                    CustomerType = "Liquor Store",
                    SalesRepresentative = "Rodriguez, Maria",
                    Notes = "Family business, all categories"
                },
                new Customer
                {
                    CustomerId = 3,
                    StoreName = "Highland Spirits",
                    StoreId = "HS_789012345",
                    Street = "789 Highland Avenue",
                    City = "Decatur",
                    State = "GA",
                    PhoneNumber = "678 555 3456",
                    SalesContact = "David Chen",
                    Email = "david.chen@highlandspirits.com",
                    PostalCode = "30030",
                    Country = "USA",
                    CustomerType = "Spirits Store",
                    SalesRepresentative = "Chen, David",
                    Notes = "Full liquor store, all categories"
                },
                new Customer
                {
                    CustomerId = 4,
                    StoreName = "Buckhead Beverages",
                    StoreId = "BB_234567890",
                    Street = "456 Peachtree Road",
                    City = "Atlanta",
                    State = "GA",
                    PhoneNumber = "404 555 6789",
                    SalesContact = "Sarah Williams",
                    Email = "sarah.williams@buckheadbev.com",
                    PostalCode = "30305",
                    Country = "USA",
                    CustomerType = "Beverage Store",
                    SalesRepresentative = "Williams, Sarah",
                    Notes = "Upscale location, premium products"
                },
                new Customer
                {
                    CustomerId = 5,
                    StoreName = "Midtown Wine & Spirits",
                    StoreId = "MWS_567890123",
                    Street = "321 10th Street",
                    City = "Atlanta",
                    State = "GA",
                    PhoneNumber = "404 555 4321",
                    SalesContact = "Amit Patel",
                    Email = "amit.patel@midtownwine.com",
                    PostalCode = "30309",
                    Country = "USA",
                    CustomerType = "Wine & Spirits",
                    SalesRepresentative = "Patel, Amit",
                    Notes = "Premium wine and spirits focus"
                },
                new Customer
                {
                    CustomerId = 6,
                    StoreName = "Eastside Beverage Co.",
                    StoreId = "EBC_890123456",
                    Street = "654 East Ponce de Leon",
                    City = "Decatur",
                    State = "GA",
                    PhoneNumber = "404 555 1111",
                    SalesContact = "Lisa Johnson",
                    Email = "lisa.johnson@eastsidebev.com",
                    PostalCode = "30030",
                    Country = "USA",
                    CustomerType = "Beverage Store",
                    SalesRepresentative = "Johnson, Lisa",
                    Notes = "Local craft beer and wine"
                },
                new Customer
                {
                    CustomerId = 7,
                    StoreName = "Westside Liquor",
                    StoreId = "WL_012345678",
                    Street = "987 Howell Mill Road",
                    City = "Atlanta",
                    State = "GA",
                    PhoneNumber = "404 555 2222",
                    SalesContact = "Carlos Garcia",
                    Email = "carlos.garcia@westsideliquor.com",
                    PostalCode = "30318",
                    Country = "USA",
                    CustomerType = "Liquor Store",
                    SalesRepresentative = "Garcia, Carlos",
                    Notes = "Neighborhood liquor store"
                },
                new Customer
                {
                    CustomerId = 8,
                    StoreName = "Northside Package Store",
                    StoreId = "NPS_345678901",
                    Street = "123 Roswell Road",
                    City = "Marietta",
                    State = "GA",
                    PhoneNumber = "770 555 3333",
                    SalesContact = "Jennifer Miller",
                    Email = "jennifer.miller@northsidepackage.com",
                    PostalCode = "30062",
                    Country = "USA",
                    CustomerType = "Package Store",
                    SalesRepresentative = "Miller, Jennifer",
                    Notes = "Convenience store with liquor"
                },
                new Customer
                {
                    CustomerId = 9,
                    StoreName = "Southside Spirits",
                    StoreId = "SS_678901234",
                    Street = "456 Moreland Avenue",
                    City = "Atlanta",
                    State = "GA",
                    PhoneNumber = "404 555 4444",
                    SalesContact = "Michael Davis",
                    Email = "michael.davis@southsidespirits.com",
                    PostalCode = "30316",
                    Country = "USA",
                    CustomerType = "Spirits Store",
                    SalesRepresentative = "Davis, Michael",
                    Notes = "Full service spirits store"
                },
                new Customer
                {
                    CustomerId = 10,
                    StoreName = "Central Liquor Mart",
                    StoreId = "CLM_901234567",
                    Street = "789 Central Avenue",
                    City = "Atlanta",
                    State = "GA",
                    PhoneNumber = "404 555 5555",
                    SalesContact = "Amanda Brown",
                    Email = "amanda.brown@centralliquor.com",
                    PostalCode = "30308",
                    Country = "USA",
                    CustomerType = "Liquor Store",
                    SalesRepresentative = "Brown, Amanda",
                    Notes = "Downtown location, all categories"
                }
            };
        }
    }
} 