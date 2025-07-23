using BeverageDistributor.Services;
using BeverageDistributor.Models;

namespace BeverageDistributor.Forms
{
    /// <summary>
    /// Main application form that handles all navigation in a single window
    /// </summary>
    public partial class MainApplicationForm : Form
    {
        private readonly DataService _dataService;
        private User? _currentUser;
        
        // Panels for different screens
        private Panel pnlLogin = null!;
        private Panel pnlDashboard = null!;
        private Panel pnlCustomerManagement = null!;
        private Panel pnlProductManagement = null!;
        private Panel pnlOrderManagement = null!;
        private Panel pnlSalesPersonManagement = null!;

        // Current active panel
        private Panel? _currentPanel;

        /// <summary>
        /// Initializes a new instance of the MainApplicationForm class
        /// </summary>
        /// <param name="dataService">Data service instance</param>
        public MainApplicationForm(DataService dataService)
        {
            _dataService = dataService;
            SetupForm();
            CreatePanels();
            ShowLoginPanel();
        }

        /// <summary>
        /// Sets up the main form appearance
        /// </summary>
        private void SetupForm()
        {
            this.Text = "Beverage Distributor - Sales Representative Portal";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Creates all the panels for different screens
        /// </summary>
        private void CreatePanels()
        {
            // Login Panel
            pnlLogin = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            CreateLoginPanel();

            // Dashboard Panel
            pnlDashboard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            CreateDashboardPanel();

            // Customer Management Panel
            pnlCustomerManagement = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            CreateCustomerManagementPanel();

            // Product Management Panel
            pnlProductManagement = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            CreateProductManagementPanel();

            // Order Management Panel
            pnlOrderManagement = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            CreateOrderManagementPanel();

            pnlSalesPersonManagement = new Panel { Dock = DockStyle.Fill, Visible = false };
            CreateSalesPersonManagementPanel();
            this.Controls.Add(pnlSalesPersonManagement);

            // Add all panels to form
            this.Controls.AddRange(new Panel[] 
            { 
                pnlLogin, pnlDashboard, pnlCustomerManagement, 
                pnlProductManagement, pnlOrderManagement, pnlSalesPersonManagement 
            });
        }

        /// <summary>
        /// Creates the login panel
        /// </summary>
        private void CreateLoginPanel()
        {
            // Title labels
            var lblTitle = new Label
            {
                Text = "Beverage Distributor",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(350, 30),
                Location = new Point((this.ClientSize.Width - 350) / 2, 100)
            };

            var lblSubtitle = new Label
            {
                Text = "Administration",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(350, 25),
                Location = new Point((this.ClientSize.Width - 350) / 2, 130)
            };

            var lblLogin = new Label
            {
                Text = "Login",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(350, 20),
                Location = new Point((this.ClientSize.Width - 350) / 2, 155)
            };

            // Username
            var lblUsername = new Label
            {
                Text = "UserName",
                Font = new Font("Arial", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                Size = new Size(100, 20),
                Location = new Point((this.ClientSize.Width - 250) / 2, 200)
            };

            var txtUsername = new TextBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(250, 25),
                Location = new Point((this.ClientSize.Width - 250) / 2, 225),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Password
            var lblPassword = new Label
            {
                Text = "Password",
                Font = new Font("Arial", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                Size = new Size(100, 20),
                Location = new Point((this.ClientSize.Width - 250) / 2, 270)
            };

            var txtPassword = new TextBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(250, 25),
                Location = new Point((this.ClientSize.Width - 250) / 2, 295),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };

            // Buttons
            var btnLogin = new Button
            {
                Text = "Login",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(120, 30),
                Location = new Point((this.ClientSize.Width - 250) / 2, 350),
                BackColor = Color.LightBlue,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Demo credentials
            var lblDemo = new Label
            {
                Text = "Demo Credentials:\nUsername: admin, Password: admin123\nUsername: jsmith, Password: password123",
                Font = new Font("Arial", 8, FontStyle.Italic),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(350, 60),
                Location = new Point((this.ClientSize.Width - 350) / 2, 400)
            };

            // Login button event
            btnLogin.Click += (sender, e) =>
            {
                var username = txtUsername.Text.Trim();
                var password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Login Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var users = _dataService.LoadUsers();
                    var user = users.FirstOrDefault(u => 
                        u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && 
                        u.Password == password);

                    if (user != null)
                    {
                        _currentUser = user;
                        ShowDashboardPanel();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Login Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Login error: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            pnlLogin.Controls.AddRange(new Control[]
            {
                lblTitle, lblSubtitle, lblLogin, lblUsername, txtUsername,
                lblPassword, txtPassword, btnLogin, lblDemo
            });
        }

        /// <summary>
        /// Creates the dashboard panel
        /// </summary>
        private void CreateDashboardPanel()
        {
            // Header panel
            var headerPanel = new Panel
            {
                BackColor = Color.LightBlue,
                Size = new Size(this.ClientSize.Width, 60),
                Location = new Point(0, 0),
                Dock = DockStyle.Top
            };

            var lblHeader = new Label
            {
                Text = "Admin Home Page",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 40),
                Location = new Point((this.ClientSize.Width - 400) / 2, 10)
            };

            var btnLogout = new Button
            {
                Text = "Logout",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(80, 30),
                Location = new Point(headerPanel.Width - 100, 15),
                BackColor = Color.LightBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btnLogout.Click += (sender, e) => ShowLoginPanel();

            headerPanel.Controls.AddRange(new Control[] { lblHeader, btnLogout });

            // Navigation buttons panel
            var navPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(450, 300),
                Location = new Point((this.ClientSize.Width - 450) / 2, 100),
                BorderStyle = BorderStyle.None
            };

            var btnCustomerManagement = new Button
            {
                Font = new Font("Arial", 12, FontStyle.Bold),
                Size = new Size(200, 50),
                Location = new Point(50, 50),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                Text = "Customer Management",
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            var btnProductManagement = new Button
            {
                Font = new Font("Arial", 12, FontStyle.Bold),
                Size = new Size(200, 50),
                Location = new Point(50, 120),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                Text = "Product Management",
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            var btnOrderManagement = new Button
            {
                Font = new Font("Arial", 12, FontStyle.Bold),
                Size = new Size(200, 50),
                Location = new Point(50, 190),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                Text = "Order Management",
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            var btnSalesPersonManagement = new Button
            {
                Font = new Font("Arial", 12, FontStyle.Bold),
                Size = new Size(250, 50),
                Location = new Point(25, 260),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                Text = "Sales Person Management",
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSalesPersonManagement.Click += (sender, e) => ShowSalesPersonManagementPanel();

            btnCustomerManagement.Click += (sender, e) => ShowCustomerManagementPanel();
            btnProductManagement.Click += (sender, e) => ShowProductManagementPanel();
            btnOrderManagement.Click += (sender, e) => ShowOrderManagementPanel();

            navPanel.Controls.AddRange(new Control[] { btnCustomerManagement, btnProductManagement, btnOrderManagement, btnSalesPersonManagement });

            pnlDashboard.Controls.AddRange(new Control[] { headerPanel, navPanel });
        }

        /// <summary>
        /// Creates the customer management panel
        /// </summary>
        private void CreateCustomerManagementPanel()
        {
            // Header panel (matching ACME design)
            var headerPanel = new Panel
            {
                BackColor = Color.LightBlue,
                Size = new Size(this.ClientSize.Width, 60),
                Location = new Point(0, 0),
                Dock = DockStyle.Top
            };

            var lblHeader = new Label
            {
                Text = "Customer Management",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 40),
                Location = new Point((this.ClientSize.Width - 400) / 2, 10)
            };

            // Admin Home button
            var btnAdminHome = new Button
            {
                Text = "Admin Home",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(100, 30),
                Location = new Point(20, 15),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdminHome.Click += (sender, e) => ShowDashboardPanel();

            // Logout button
            var btnLogout = new Button
            {
                Text = "Logout",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(80, 30),
                Location = new Point(headerPanel.Width - 100, 15),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogout.Click += (sender, e) => ShowLoginPanel();

            // Underline indicator
            var underlinePanel = new Panel
            {
                BackColor = Color.Transparent,
                Size = new Size(200, 3),
                Location = new Point((headerPanel.Width - 200) / 2, 45)
            };

            var lightGrayLine = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(100, 1),
                Location = new Point(0, 1)
            };

            var darkBlueLine = new Panel
            {
                BackColor = Color.DarkBlue,
                Size = new Size(50, 2),
                Location = new Point(0, 0)
            };

            underlinePanel.Controls.AddRange(new Control[] { lightGrayLine, darkBlueLine });
            headerPanel.Controls.AddRange(new Control[] { lblHeader, btnAdminHome, btnLogout, underlinePanel });

            // Main content panel
            var contentPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 60),
                Location = new Point(0, 60),
                Dock = DockStyle.Fill
            };

            // Search and filter panel
            var filterPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(this.ClientSize.Width - 40, 60),
                Location = new Point(20, 120), // moved down to match product management
                BorderStyle = BorderStyle.None
            };

            var lblSearch = new Label
            {
                Text = "Search:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Size = new Size(60, 25),
                Location = new Point(20, 20)
            };

            var txtSearch = new TextBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(250, 30),
                Location = new Point(90, 17),
                PlaceholderText = "Search by store name, store ID, email, or phone..."
            };

            var lblCustomerType = new Label
            {
                Text = "Type:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Size = new Size(70, 25),
                Location = new Point(360, 20)
            };

            var cboCustomerType = new ComboBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(150, 30),
                Location = new Point(440, 17),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var btnRefresh = new Button
            {
                Text = "Refresh",
                Font = new Font("Arial", 9, FontStyle.Regular),
                Size = new Size(80, 30),
                Location = new Point(610, 17),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            filterPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblCustomerType, cboCustomerType, btnRefresh });

            // Create Customer button
            var btnCreateCustomer = new Button
            {
                Text = "+ Create Customer",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(20, 190), // moved down to match product management
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Delete Customer button
            var btnDeleteCustomer = new Button
            {
                Text = "- Delete Customer",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(190, 190),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Reset Customers button
            var btnResetCustomers = new Button
            {
                Text = "Reset to Sample Data",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(360, 190),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Customers data grid
            var dgvCustomers = new DataGridView
            {
                Font = new Font("Arial", 9, FontStyle.Regular),
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 240), // adjusted height
                Location = new Point(20, 240), // moved down to match product management
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            // Tip label
            var lblTip = new Label
            {
                Text = "Tip: double click a customer to edit/manage them",
                Font = new Font("Arial", 8, FontStyle.Regular),
                ForeColor = Color.Black,
                Size = new Size(300, 20),
                Location = new Point(20, this.ClientSize.Height - 50)
            };

            // Event handlers
            btnCreateCustomer.Click += (sender, e) => CreateNewCustomer();
            btnDeleteCustomer.Click += (sender, e) => DeleteSelectedCustomer(dgvCustomers);
            btnResetCustomers.Click += (sender, e) => ResetCustomersData(dgvCustomers);
            dgvCustomers.DoubleClick += (sender, e) => EditSelectedCustomer(dgvCustomers);
            btnRefresh.Click += (sender, e) => LoadCustomersData(dgvCustomers);
            txtSearch.TextChanged += (sender, e) => FilterCustomers(dgvCustomers, txtSearch.Text, cboCustomerType.Text);
            cboCustomerType.SelectedIndexChanged += (sender, e) => FilterCustomers(dgvCustomers, txtSearch.Text, cboCustomerType.Text);

            // Load customers data
            LoadCustomersData(dgvCustomers);
            LoadCustomerTypes(cboCustomerType);

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] { filterPanel, btnCreateCustomer, btnDeleteCustomer, btnResetCustomers, dgvCustomers, lblTip });

            // Add panels to customer management panel
            pnlCustomerManagement.Controls.AddRange(new Control[] { headerPanel, contentPanel });
        }

        /// <summary>
        /// Creates the product management panel
        /// </summary>
        private void CreateProductManagementPanel()
        {
            // Header panel (matching ACME design)
            var headerPanel = new Panel
            {
                BackColor = Color.LightBlue,
                Size = new Size(this.ClientSize.Width, 60),
                Location = new Point(0, 0),
                Dock = DockStyle.Top
            };

            var lblHeader = new Label
            {
                Text = "Product Management",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 40),
                Location = new Point((this.ClientSize.Width - 400) / 2, 10)
            };

            // Admin Home button
            var btnAdminHome = new Button
            {
                Text = "Admin Home",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(100, 30),
                Location = new Point(20, 15),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdminHome.Click += (sender, e) => ShowDashboardPanel();

            // Logout button
            var btnLogout = new Button
            {
                Text = "Logout",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(80, 30),
                Location = new Point(headerPanel.Width - 100, 15),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogout.Click += (sender, e) => ShowLoginPanel();

            // Underline indicator
            var underlinePanel = new Panel
            {
                BackColor = Color.Transparent,
                Size = new Size(200, 3),
                Location = new Point((headerPanel.Width - 200) / 2, 45)
            };

            var lightGrayLine = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(100, 1),
                Location = new Point(0, 1)
            };

            var darkBlueLine = new Panel
            {
                BackColor = Color.DarkBlue,
                Size = new Size(50, 2),
                Location = new Point(0, 0)
            };

            underlinePanel.Controls.AddRange(new Control[] { lightGrayLine, darkBlueLine });
            headerPanel.Controls.AddRange(new Control[] { lblHeader, btnAdminHome, btnLogout, underlinePanel });

            // Main content panel
            var contentPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 60),
                Location = new Point(0, 60),
                Dock = DockStyle.Fill
            };

            // Search and filter panel
            var filterPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(this.ClientSize.Width - 40, 60),
                Location = new Point(20, 120), // moved down from 20 to 120
                BorderStyle = BorderStyle.None
            };

            var lblSearch = new Label
            {
                Text = "Search:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Size = new Size(60, 25),
                Location = new Point(20, 20)
            };

            var txtSearch = new TextBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(250, 30),
                Location = new Point(90, 17),
                PlaceholderText = "Search by name, brand, or SKU..."
            };

            var lblCategory = new Label
            {
                Text = "Category:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Size = new Size(70, 25),
                Location = new Point(360, 20)
            };

            var cboCategory = new ComboBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(150, 30),
                Location = new Point(440, 17),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var btnRefresh = new Button
            {
                Text = "Refresh",
                Font = new Font("Arial", 9, FontStyle.Regular),
                Size = new Size(80, 30),
                Location = new Point(610, 17),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            filterPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblCategory, cboCategory, btnRefresh });

            // Create Product button
            var btnCreateProduct = new Button
            {
                Text = "+ Create Product",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(20, 190), // moved down from 90 to 190
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Delete Product button
            var btnDeleteProduct = new Button
            {
                Text = "- Delete Product",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(190, 190),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Products data grid
            var dgvProducts = new DataGridView
            {
                Font = new Font("Arial", 9, FontStyle.Regular),
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 240), // adjusted height
                Location = new Point(20, 240), // moved down from 140 to 240
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            // Tip label
            var lblTip = new Label
            {
                Text = "Tip: double click a product to edit/manage them",
                Font = new Font("Arial", 8, FontStyle.Regular),
                ForeColor = Color.Black,
                Size = new Size(300, 20),
                Location = new Point(20, this.ClientSize.Height - 50)
            };

            // Event handlers
            btnCreateProduct.Click += (sender, e) => CreateNewProduct();
            btnDeleteProduct.Click += (sender, e) => DeleteSelectedProduct(dgvProducts);
            dgvProducts.DoubleClick += (sender, e) => EditSelectedProduct(dgvProducts);
            btnRefresh.Click += (sender, e) => LoadProductsData(dgvProducts);
            txtSearch.TextChanged += (sender, e) => FilterProducts(dgvProducts, txtSearch.Text, cboCategory.Text);
            cboCategory.SelectedIndexChanged += (sender, e) => FilterProducts(dgvProducts, txtSearch.Text, cboCategory.Text);

            // Load products data
            LoadProductsData(dgvProducts);
            LoadCategories(cboCategory);

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] { filterPanel, btnCreateProduct, btnDeleteProduct, dgvProducts, lblTip });

            // Add panels to product management panel
            pnlProductManagement.Controls.AddRange(new Control[] { headerPanel, contentPanel });
        }

        /// <summary>
        /// Creates the order management panel
        /// </summary>
        private void CreateOrderManagementPanel()
        {
            // Header panel (matching ACME design)
            var headerPanel = new Panel
            {
                BackColor = Color.LightBlue,
                Size = new Size(this.ClientSize.Width, 60),
                Location = new Point(0, 0),
                Dock = DockStyle.Top
            };

            var lblHeader = new Label
            {
                Text = "Order Management",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 40),
                Location = new Point((this.ClientSize.Width - 400) / 2, 10)
            };

            // Admin Home button
            var btnAdminHome = new Button
            {
                Text = "Admin Home",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(100, 30),
                Location = new Point(20, 15),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdminHome.Click += (sender, e) => ShowDashboardPanel();

            // Logout button
            var btnLogout = new Button
            {
                Text = "Logout",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(80, 30),
                Location = new Point(headerPanel.Width - 100, 15),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogout.Click += (sender, e) => ShowLoginPanel();

            // Underline indicator
            var underlinePanel = new Panel
            {
                BackColor = Color.Transparent,
                Size = new Size(200, 3),
                Location = new Point((headerPanel.Width - 200) / 2, 45)
            };

            var lightGrayLine = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(100, 1),
                Location = new Point(0, 1)
            };

            var darkBlueLine = new Panel
            {
                BackColor = Color.DarkBlue,
                Size = new Size(50, 2),
                Location = new Point(0, 0)
            };

            underlinePanel.Controls.AddRange(new Control[] { lightGrayLine, darkBlueLine });
            headerPanel.Controls.AddRange(new Control[] { lblHeader, btnAdminHome, btnLogout, underlinePanel });

            // Main content panel
            var contentPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 60),
                Location = new Point(0, 60),
                Dock = DockStyle.Fill
            };

            // Search and filter panel
            var filterPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(this.ClientSize.Width - 40, 60),
                Location = new Point(20, 120), // moved down to match product management
                BorderStyle = BorderStyle.None
            };

            var lblSearch = new Label
            {
                Text = "Search:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Size = new Size(60, 25),
                Location = new Point(20, 20)
            };

            var txtSearch = new TextBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(250, 30),
                Location = new Point(90, 17),
                PlaceholderText = "Search by order ID or customer..."
            };

            var lblStatus = new Label
            {
                Text = "Status:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Size = new Size(70, 25),
                Location = new Point(360, 20)
            };

            var cboStatus = new ComboBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(150, 30),
                Location = new Point(440, 17),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var btnRefresh = new Button
            {
                Text = "Refresh",
                Font = new Font("Arial", 9, FontStyle.Regular),
                Size = new Size(80, 30),
                Location = new Point(610, 17),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            filterPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblStatus, cboStatus, btnRefresh });

            // Create Order button
            var btnCreateOrder = new Button
            {
                Text = "+ Create Order",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(20, 190), // moved down to match product management
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Delete Order button
            var btnDeleteOrder = new Button
            {
                Text = "- Delete Order",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(190, 190),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Print to CSV button
            var btnPrintOrder = new Button
            {
                Text = "Print to CSV",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(360, 190),
                BackColor = Color.ForestGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Orders data grid
            var dgvOrders = new DataGridView
            {
                Font = new Font("Arial", 9, FontStyle.Regular),
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 240), // adjusted height
                Location = new Point(20, 240), // moved down to match product management
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            // Tip label
            var lblTip = new Label
            {
                Text = "Tip: double click an order to edit/manage them",
                Font = new Font("Arial", 8, FontStyle.Regular),
                ForeColor = Color.Black,
                Size = new Size(300, 20),
                Location = new Point(20, this.ClientSize.Height - 50)
            };

            // Event handlers
            btnCreateOrder.Click += (sender, e) => CreateNewOrder();
            btnDeleteOrder.Click += (sender, e) => DeleteSelectedOrder(dgvOrders);
            btnPrintOrder.Click += (sender, e) => PrintSelectedOrderToCsv(dgvOrders);
            dgvOrders.DoubleClick += (sender, e) => EditSelectedOrder(dgvOrders);
            btnRefresh.Click += (sender, e) => LoadOrdersData(dgvOrders);
            txtSearch.TextChanged += (sender, e) => FilterOrders(dgvOrders, txtSearch.Text, cboStatus.Text);
            cboStatus.SelectedIndexChanged += (sender, e) => FilterOrders(dgvOrders, txtSearch.Text, cboStatus.Text);

            // Load orders data
            LoadOrdersData(dgvOrders);
            LoadOrderStatuses(cboStatus);

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] { filterPanel, btnCreateOrder, btnDeleteOrder, btnPrintOrder, dgvOrders, lblTip });

            // Add panels to order management panel
            pnlOrderManagement.Controls.AddRange(new Control[] { headerPanel, contentPanel });
        }

        /// <summary>
        /// Shows the login panel
        /// </summary>
        private void ShowLoginPanel()
        {
            HideAllPanels();
            pnlLogin.Visible = true;
            _currentPanel = pnlLogin;
            _currentUser = null;
        }

        /// <summary>
        /// Shows the dashboard panel
        /// </summary>
        private void ShowDashboardPanel()
        {
            HideAllPanels();
            pnlDashboard.Visible = true;
            _currentPanel = pnlDashboard;
        }

        /// <summary>
        /// Shows the customer management panel
        /// </summary>
        private void ShowCustomerManagementPanel()
        {
            HideAllPanels();
            pnlCustomerManagement.Visible = true;
            _currentPanel = pnlCustomerManagement;
        }

        /// <summary>
        /// Shows the product management panel
        /// </summary>
        private void ShowProductManagementPanel()
        {
            HideAllPanels();
            pnlProductManagement.Visible = true;
            _currentPanel = pnlProductManagement;
        }

        /// <summary>
        /// Shows the order management panel
        /// </summary>
        private void ShowOrderManagementPanel()
        {
            HideAllPanels();
            pnlOrderManagement.Visible = true;
            _currentPanel = pnlOrderManagement;
        }

        /// <summary>
        /// Hides all panels
        /// </summary>
        private void HideAllPanels()
        {
            pnlLogin.Visible = false;
            pnlDashboard.Visible = false;
            pnlCustomerManagement.Visible = false;
            pnlProductManagement.Visible = false;
            pnlOrderManagement.Visible = false;
            pnlSalesPersonManagement.Visible = false;
        }

        /// <summary>
        /// Loads customers data into the grid
        /// </summary>
        private void LoadCustomersData(DataGridView dgv)
        {
            try
            {
                var customers = _dataService.LoadCustomers();
                
                dgv.Columns.Clear();
                dgv.Columns.AddRange(new DataGridViewColumn[]
                {
                    new DataGridViewTextBoxColumn { HeaderText = "Customer ID", DataPropertyName = "CustomerId", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Store Name", DataPropertyName = "StoreName", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Store ID", DataPropertyName = "StoreId", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Street", DataPropertyName = "Street", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "City", DataPropertyName = "City", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "State", DataPropertyName = "State", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Phone Number", DataPropertyName = "PhoneNumber", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Sales Contact", DataPropertyName = "SalesContact", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Zip Code", DataPropertyName = "PostalCode", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Customer Type", DataPropertyName = "CustomerType", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Tax ID", DataPropertyName = "TaxId", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Credit Limit", DataPropertyName = "CreditLimit", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Current Balance", DataPropertyName = "CurrentBalance", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewCheckBoxColumn { HeaderText = "Is Active", DataPropertyName = "IsActive", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Date Created", DataPropertyName = "DateAdded", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Last Updated", DataPropertyName = "LastUpdated", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Sales Person", DataPropertyName = "SalesRepresentative", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Preferred Payment", DataPropertyName = "PreferredDeliveryDay", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                    new DataGridViewTextBoxColumn { HeaderText = "Notes", DataPropertyName = "Notes", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells }
                });

                dgv.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        private void CreateNewCustomer()
        {
            try
            {
                var customers = _dataService.LoadCustomers();
                var newCustomer = new Customer
                {
                    CustomerId = _dataService.GetNextId<Customer>(customers, "CustomerId"),
                    StoreName = "",
                    StoreId = "",
                    Street = "",
                    City = "",
                    State = "",
                    PhoneNumber = "",
                    SalesContact = "",
                    Email = "",
                    PostalCode = ""
                };

                ShowCustomerDetailForm(newCustomer, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating customer: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Edits the selected customer
        /// </summary>
        private void EditSelectedCustomer(DataGridView dgv)
        {
            if (dgv.CurrentRow?.DataBoundItem is Customer customer)
            {
                ShowCustomerDetailForm(customer, false);
            }
        }

        /// <summary>
        /// Deletes the selected customer
        /// </summary>
        private void DeleteSelectedCustomer(DataGridView dgv)
        {
            if (dgv.CurrentRow?.DataBoundItem is Customer customer)
            {
                var result = MessageBox.Show($"Are you sure you want to delete customer '{customer.StoreName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var customers = _dataService.LoadCustomers();
                    customers.RemoveAll(c => c.CustomerId == customer.CustomerId);
                    _dataService.SaveCustomers(customers);
                    LoadCustomersData(dgv);
                }
            }
        }

        /// <summary>
        /// Resets customers data to the sample data
        /// </summary>
        /// <param name="dgv">DataGridView to refresh</param>
        private void ResetCustomersData(DataGridView dgv)
        {
            var result = MessageBox.Show("This will replace all existing customers with the sample data. Are you sure?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var sampleCustomers = SampleDataGenerator.GenerateSampleCustomers();
                    _dataService.SaveCustomers(sampleCustomers);
                    LoadCustomersData(dgv);
                    MessageBox.Show("Customers data has been reset to sample data.", "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error resetting customers data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Shows the customer detail form
        /// </summary>
        private void ShowCustomerDetailForm(Customer customer, bool isNew)
        {
            // Create a simple customer detail form
            var detailForm = new Form
            {
                Text = isNew ? "Create Customer" : "Edit Customer",
                Size = new Size(500, 420), // Increased height
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Create controls
            var lblStoreName = new Label { Text = "Store Name:", Location = new Point(20, 20), Size = new Size(100, 20) };
            var txtStoreName = new TextBox { Location = new Point(120, 20), Size = new Size(300, 20), Text = customer.StoreName };

            var lblStoreId = new Label { Text = "Store ID:", Location = new Point(20, 50), Size = new Size(100, 20) };
            var txtStoreId = new TextBox { Location = new Point(120, 50), Size = new Size(300, 20), Text = customer.StoreId };

            var lblStreet = new Label { Text = "Street:", Location = new Point(20, 80), Size = new Size(100, 20) };
            var txtStreet = new TextBox { Location = new Point(120, 80), Size = new Size(300, 20), Text = customer.Street };

            var lblCity = new Label { Text = "City:", Location = new Point(20, 110), Size = new Size(100, 20) };
            var txtCity = new TextBox { Location = new Point(120, 110), Size = new Size(300, 20), Text = customer.City };

            var lblState = new Label { Text = "State:", Location = new Point(20, 140), Size = new Size(100, 20) };
            var txtState = new TextBox { Location = new Point(120, 140), Size = new Size(300, 20), Text = customer.State };

            var lblPhoneNumber = new Label { Text = "Phone Number:", Location = new Point(20, 170), Size = new Size(100, 20) };
            var txtPhoneNumber = new TextBox { Location = new Point(120, 170), Size = new Size(300, 20), Text = customer.PhoneNumber };

            var lblSalesContact = new Label { Text = "Sales Contact:", Location = new Point(20, 200), Size = new Size(100, 20) };
            var txtSalesContact = new TextBox { Location = new Point(120, 200), Size = new Size(300, 20), Text = customer.SalesContact };

            var lblEmail = new Label { Text = "Email:", Location = new Point(20, 230), Size = new Size(100, 20) };
            var txtEmail = new TextBox { Location = new Point(120, 230), Size = new Size(300, 20), Text = customer.Email };

            var lblZipCode = new Label { Text = "Zip Code:", Location = new Point(20, 260), Size = new Size(100, 20) };
            var txtZipCode = new TextBox { Location = new Point(120, 260), Size = new Size(300, 20), Text = customer.PostalCode };

            var btnSave = new Button
            {
                Text = "Save",
                Location = new Point(120, 310), // Moved down
                Size = new Size(80, 30),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(220, 310), // Moved down
                Size = new Size(80, 30),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            // Event handlers
            btnSave.Click += (sender, e) =>
            {
                try
                {
                    customer.StoreName = txtStoreName.Text.Trim();
                    customer.StoreId = txtStoreId.Text.Trim();
                    customer.Street = txtStreet.Text.Trim();
                    customer.City = txtCity.Text.Trim();
                    customer.State = txtState.Text.Trim();
                    customer.PhoneNumber = txtPhoneNumber.Text.Trim();
                    customer.SalesContact = txtSalesContact.Text.Trim();
                    customer.Email = txtEmail.Text.Trim();
                    customer.PostalCode = txtZipCode.Text.Trim();

                    if (string.IsNullOrEmpty(customer.StoreName))
                    {
                        MessageBox.Show("Store name is required.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var customers = _dataService.LoadCustomers();
                    if (isNew)
                    {
                        customers.Add(customer);
                    }
                    else
                    {
                        var existingIndex = customers.FindIndex(c => c.CustomerId == customer.CustomerId);
                        if (existingIndex >= 0)
                        {
                            customers[existingIndex] = customer;
                        }
                    }

                    _dataService.SaveCustomers(customers);
                    detailForm.Close();
                    
                    // Refresh the grid
                    var dgv = pnlCustomerManagement.Controls.OfType<DataGridView>().FirstOrDefault();
                    if (dgv != null)
                    {
                        LoadCustomersData(dgv);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving customer: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (sender, e) => detailForm.Close();

            // Add controls to form
            detailForm.Controls.AddRange(new Control[]
            {
                lblStoreName, txtStoreName, lblStoreId, txtStoreId, lblStreet, txtStreet,
                lblCity, txtCity, lblState, txtState, lblPhoneNumber, txtPhoneNumber,
                lblSalesContact, txtSalesContact, lblEmail, txtEmail, lblZipCode, txtZipCode,
                btnSave, btnCancel
            });

            detailForm.ShowDialog();
        }

        /// <summary>
        /// Loads products data into the grid
        /// </summary>
        private void LoadProductsData(DataGridView dgv)
        {
            try
            {
                var products = _dataService.LoadProducts();
                
                dgv.Columns.Clear();
                dgv.Columns.AddRange(new DataGridViewColumn[]
                {
                    new DataGridViewTextBoxColumn { HeaderText = "Product ID", DataPropertyName = "ProductId", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", Width = 200 },
                    new DataGridViewTextBoxColumn { HeaderText = "Brand", DataPropertyName = "Brand", Width = 150 },
                    new DataGridViewTextBoxColumn { HeaderText = "Category", DataPropertyName = "Category", Width = 120 },
                    new DataGridViewTextBoxColumn { HeaderText = "SKU", DataPropertyName = "SKU", Width = 100 },
                    new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = "UnitPrice", Width = 80 },
                    new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "StockQuantity", Width = 80 },
                    new DataGridViewTextBoxColumn { HeaderText = "Description", DataPropertyName = "Description", Width = 300 }
                });

                dgv.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads categories into the combo box
        /// </summary>
        private void LoadCategories(ComboBox cbo)
        {
            try
            {
                var products = _dataService.LoadProducts();
                var categories = products.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();
                categories.Insert(0, "All Categories");
                
                cbo.Items.Clear();
                cbo.Items.AddRange(categories.ToArray());
                if (cbo.Items.Count > 0)
                {
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Filters products based on search text and category
        /// </summary>
        private void FilterProducts(DataGridView dgv, string searchText, string category)
        {
            try
            {
                var products = _dataService.LoadProducts();
                var filteredProducts = products.AsEnumerable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    filteredProducts = filteredProducts.Where(p => 
                        p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        p.Brand.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        p.SKU.Contains(searchText, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(category) && category != "All Categories")
                {
                    filteredProducts = filteredProducts.Where(p => p.Category == category);
                }

                dgv.DataSource = filteredProducts.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering products: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        private void CreateNewProduct()
        {
            try
            {
                var products = _dataService.LoadProducts();
                var newProduct = new Product
                {
                    ProductId = _dataService.GetNextId<Product>(products, "ProductId"),
                    Name = "",
                    Brand = "",
                    Category = "",
                    SKU = "",
                    UnitPrice = 0.0m,
                    StockQuantity = 0,
                    Description = ""
                };

                ShowProductDetailForm(newProduct, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating product: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Edits the selected product
        /// </summary>
        private void EditSelectedProduct(DataGridView dgv)
        {
            if (dgv.CurrentRow?.DataBoundItem is Product product)
            {
                ShowProductDetailForm(product, false);
            }
        }

        /// <summary>
        /// Deletes the selected product
        /// </summary>
        private void DeleteSelectedProduct(DataGridView dgv)
        {
            if (dgv.CurrentRow?.DataBoundItem is Product product)
            {
                var result = MessageBox.Show($"Are you sure you want to delete product '{product.Name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var products = _dataService.LoadProducts();
                        products.RemoveAll(p => p.ProductId == product.ProductId);
                        _dataService.SaveProducts(products);
                        LoadProductsData(dgv);
                        MessageBox.Show($"Product '{product.Name}' deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Shows the product detail form
        /// </summary>
        private void ShowProductDetailForm(Product product, bool isNew)
        {
            // Create a simple product detail form
            var detailForm = new Form
            {
                Text = isNew ? "Create Product" : "Edit Product",
                Size = new Size(600, 500),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Create controls
            var lblName = new Label { Text = "Name:", Location = new Point(20, 20), Size = new Size(100, 20) };
            var txtName = new TextBox { Location = new Point(120, 20), Size = new Size(400, 20), Text = product.Name };

            var lblBrand = new Label { Text = "Brand:", Location = new Point(20, 50), Size = new Size(100, 20) };
            var txtBrand = new TextBox { Location = new Point(120, 50), Size = new Size(400, 20), Text = product.Brand };

            var lblCategory = new Label { Text = "Category:", Location = new Point(20, 80), Size = new Size(100, 20) };
            var txtCategory = new TextBox { Location = new Point(120, 80), Size = new Size(400, 20), Text = product.Category };

            var lblSKU = new Label { Text = "SKU:", Location = new Point(20, 110), Size = new Size(100, 20) };
            var txtSKU = new TextBox { Location = new Point(120, 110), Size = new Size(400, 20), Text = product.SKU };

            var lblPrice = new Label { Text = "Price:", Location = new Point(20, 140), Size = new Size(100, 20) };
            var txtPrice = new TextBox { Location = new Point(120, 140), Size = new Size(400, 20), Text = product.UnitPrice.ToString() };

            var lblStock = new Label { Text = "Stock Quantity:", Location = new Point(20, 170), Size = new Size(100, 20) };
            var txtStock = new TextBox { Location = new Point(120, 170), Size = new Size(400, 20), Text = product.StockQuantity.ToString() };

            var lblDescription = new Label { Text = "Description:", Location = new Point(20, 200), Size = new Size(100, 20) };
            var txtDescription = new TextBox 
            { 
                Location = new Point(120, 200), 
                Size = new Size(400, 60), 
                Text = product.Description,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            var btnSave = new Button
            {
                Text = "Save",
                Location = new Point(120, 280),
                Size = new Size(80, 30),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(220, 280),
                Size = new Size(80, 30),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            // Event handlers
            btnSave.Click += (sender, e) =>
            {
                try
                {
                    product.Name = txtName.Text.Trim();
                    product.Brand = txtBrand.Text.Trim();
                    product.Category = txtCategory.Text.Trim();
                    product.SKU = txtSKU.Text.Trim();
                    product.Description = txtDescription.Text.Trim();

                    if (!decimal.TryParse(txtPrice.Text, out decimal price))
                    {
                        MessageBox.Show("Please enter a valid price.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    product.UnitPrice = price;

                    if (!int.TryParse(txtStock.Text, out int stock))
                    {
                        MessageBox.Show("Please enter a valid stock quantity.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    product.StockQuantity = stock;

                    if (string.IsNullOrEmpty(product.Name))
                    {
                        MessageBox.Show("Product name is required.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var products = _dataService.LoadProducts();
                    if (isNew)
                    {
                        products.Add(product);
                    }
                    else
                    {
                        var existingIndex = products.FindIndex(p => p.ProductId == product.ProductId);
                        if (existingIndex >= 0)
                        {
                            products[existingIndex] = product;
                        }
                    }

                    _dataService.SaveProducts(products);
                    detailForm.Close();
                    
                    // Refresh the grid
                    var dgv = pnlProductManagement.Controls.OfType<DataGridView>().FirstOrDefault();
                    if (dgv != null)
                    {
                        LoadProductsData(dgv);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving product: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (sender, e) => detailForm.Close();

            // Add controls to form
            detailForm.Controls.AddRange(new Control[]
            {
                lblName, txtName, lblBrand, txtBrand, lblCategory, txtCategory,
                lblSKU, txtSKU, lblPrice, txtPrice, lblStock, txtStock,
                lblDescription, txtDescription, btnSave, btnCancel
            });

            detailForm.ShowDialog();
        }

        /// <summary>
        /// Loads orders data into the specified DataGridView
        /// </summary>
        /// <param name="dgv">DataGridView to populate</param>
        private void LoadOrdersData(DataGridView dgv)
        {
            try
            {
                var orders = _dataService.LoadOrders();
                var customers = _dataService.LoadCustomers();
                var salesPeople = _dataService.LoadSalesPeople();

                // Create a list of order display objects with customer and sales person information
                var orderDisplayList = orders.Select(order =>
                {
                    var customer = customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                    var salesPerson = salesPeople.FirstOrDefault(s => s.SalesPersonId == order.SalesPersonId);
                    
                    return new
                    {
                        order.OrderId,
                        CustomerName = customer?.StoreName ?? "Unknown",
                        SalesPersonName = salesPerson?.Name ?? "Not Assigned",
                        order.OrderDate,
                        order.Status,
                        order.TotalAmount
                    };
                }).ToList();

                dgv.DataSource = orderDisplayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        private void CreateNewOrder()
        {
            try
            {
                var orders = _dataService.LoadOrders();
                var newOrder = new Order
                {
                    OrderId = _dataService.GetNextId<Order>(orders, "OrderId"),
                    CustomerId = 0, // Will be selected from customer list
                    OrderDate = DateTime.Now,
                    Status = "Pending",
                    OrderItems = new List<OrderItem>()
                };

                ShowOrderDetailForm(newOrder, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating order: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Edits the selected order
        /// </summary>
        private void EditSelectedOrder(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                var orderIdObj = dgv.CurrentRow.Cells["OrderId"].Value;
                if (orderIdObj != null && int.TryParse(orderIdObj.ToString(), out int orderId))
                {
                    var orders = _dataService.LoadOrders();
                    var order = orders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order != null)
                    {
                        ShowOrderDetailForm(order, false);
                        return;
                    }
                }
            }
            MessageBox.Show("Please select a valid order to edit.", "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Deletes the selected order
        /// </summary>
        private void DeleteSelectedOrder(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                var orderIdObj = dgv.CurrentRow.Cells["OrderId"].Value;
                if (orderIdObj != null && int.TryParse(orderIdObj.ToString(), out int orderId))
                {
                    var orders = _dataService.LoadOrders();
                    var order = orders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order != null)
                    {
                        var result = MessageBox.Show($"Are you sure you want to delete order #{order.OrderId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                orders.RemoveAll(o => o.OrderId == order.OrderId);
                                _dataService.SaveOrders(orders);
                                LoadOrdersData(dgv);
                                MessageBox.Show($"Order #{order.OrderId} deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error deleting order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        return;
                    }
                }
            }
            MessageBox.Show("Please select an order to delete.", "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows the order detail form
        /// </summary>
        private void ShowOrderDetailForm(Order order, bool isNew)
        {
            // Create a simple order detail form
            var detailForm = new Form
            {
                Text = isNew ? "Create Order" : "Edit Order",
                Size = new Size(800, 750), // Increased height to accommodate new field and repositioned controls
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Create controls
            var lblOrderId = new Label { Text = "Order ID:", Location = new Point(20, 20), Size = new Size(100, 20) };
            var txtOrderId = new TextBox { Location = new Point(120, 20), Size = new Size(150, 20), Text = order.OrderId.ToString(), ReadOnly = true };

            var lblCustomer = new Label { Text = "Customer:", Location = new Point(280, 20), Size = new Size(100, 20) };
            var cboCustomer = new ComboBox { Location = new Point(390, 20), Size = new Size(200, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            LoadCustomersForOrder(cboCustomer);
            if (!isNew && cboCustomer.Items.Count > 0)
            {
                cboCustomer.SelectedValue = order.CustomerId;
            }

            var lblSalesPerson = new Label { Text = "Sales Person:", Location = new Point(20, 80), Size = new Size(100, 20) };
            var cboSalesPerson = new ComboBox { Location = new Point(120, 80), Size = new Size(200, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            LoadSalesPeopleForOrder(cboSalesPerson);
            if (!isNew && cboSalesPerson.Items.Count > 0)
            {
                cboSalesPerson.SelectedValue = order.SalesPersonId;
            }

            var lblOrderDate = new Label { Text = "Order Date:", Location = new Point(280, 80), Size = new Size(100, 20) };
            var dtpOrderDate = new DateTimePicker { Location = new Point(390, 80), Size = new Size(200, 20) };
            dtpOrderDate.Value = order.OrderDate;

            var lblStatus = new Label { Text = "Status:", Location = new Point(20, 110), Size = new Size(100, 20) };
            var cboStatus = new ComboBox { Location = new Point(120, 110), Size = new Size(150, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cboStatus.Items.AddRange(new object[] { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" });
            if (!isNew && !string.IsNullOrEmpty(order.Status))
            {
                var statusIndex = cboStatus.Items.IndexOf(order.Status);
                if (statusIndex >= 0)
                {
                    cboStatus.SelectedIndex = statusIndex;
                }
                else
                {
                    cboStatus.SelectedIndex = 0; // Default to first item if status not found
                }
            }
            else
            {
                cboStatus.SelectedIndex = 0; // Default to first item for new orders
            }

            var lblTotalAmount = new Label { Text = "Total Amount:", Location = new Point(280, 110), Size = new Size(100, 20) };
            var txtTotalAmount = new TextBox { Location = new Point(390, 110), Size = new Size(150, 20), Text = order.TotalAmount.ToString("C2"), ReadOnly = true };

            var lblOrderItems = new Label { Text = "Order Items:", Location = new Point(20, 140), Size = new Size(100, 20) };
            
            // Add a label to show current total
            var lblCurrentTotal = new Label 
            { 
                Text = $"Current Total: {order.TotalAmount:C2}", 
                Location = new Point(20, 170), 
                Size = new Size(200, 20),
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            var dgvOrderItems = new DataGridView
            {
                Font = new Font("Arial", 9, FontStyle.Regular),
                Size = new Size(detailForm.ClientSize.Width - 40, 350), // Fixed height for DataGridView
                Location = new Point(20, 200), // Moved down
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            // Add columns for OrderItems
            dgvOrderItems.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { HeaderText = "Product ID", DataPropertyName = "ProductId", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "ProductName", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                new DataGridViewTextBoxColumn { HeaderText = "Quantity", DataPropertyName = "Quantity", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = "UnitPrice", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "TotalPrice", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells }
            });

            // Add rows for existing order items
            foreach (var item in order.OrderItems)
            {
                dgvOrderItems.Rows.Add(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice, item.TotalPrice);
            }

            // Add a label to show when order is empty
            var lblEmptyOrder = new Label 
            { 
                Text = "No items in this order. Click 'Add Item' to add products.", 
                Location = new Point(20, 560), // Moved down
                Size = new Size(400, 20),
                Font = new Font("Arial", 9, FontStyle.Italic),
                ForeColor = Color.Gray,
                Visible = order.OrderItems.Count == 0
            };

            var btnAddItem = new Button
            {
                Text = "+ Add Item",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(20, 590), // Moved down
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            var btnRemoveItem = new Button
            {
                Text = "- Remove Item",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(180, 590), // Moved down
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            var btnSave = new Button
            {
                Text = "Save",
                Location = new Point(120, 640), // Moved down
                Size = new Size(80, 30),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(220, 640), // Moved down
                Size = new Size(80, 30),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            // Event handlers
            btnAddItem.Click += (sender, e) => AddOrderItem(dgvOrderItems, order, txtTotalAmount, lblCurrentTotal, lblEmptyOrder);
            btnRemoveItem.Click += (sender, e) => RemoveOrderItem(dgvOrderItems, order, txtTotalAmount, lblCurrentTotal, lblEmptyOrder);
            btnSave.Click += (sender, e) =>
            {
                try
                {
                    // Check if customers are available
                    if (cboCustomer.Items.Count == 0 || cboCustomer.SelectedIndex == -1)
                    {
                        MessageBox.Show("No customers available. Please create a customer first.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    order.CustomerId = (int)(cboCustomer.SelectedValue ?? 0);
                    order.SalesPersonId = cboSalesPerson.SelectedValue != null ? (int)cboSalesPerson.SelectedValue : null;
                    order.OrderDate = dtpOrderDate.Value;
                    order.Status = cboStatus.SelectedItem?.ToString() ?? "";
                    order.TotalAmount = order.OrderItems.Sum(item => item.TotalPrice);

                    if (order.CustomerId == 0)
                    {
                        MessageBox.Show("Customer is required for the order.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var orders = _dataService.LoadOrders();
                    if (isNew)
                    {
                        orders.Add(order);
                    }
                    else
                    {
                        var existingIndex = orders.FindIndex(o => o.OrderId == order.OrderId);
                        if (existingIndex >= 0)
                        {
                            orders[existingIndex] = order;
                        }
                    }

                    _dataService.SaveOrders(orders);
                    detailForm.Close();
                    
                    // Refresh the grid
                    var dgv = pnlOrderManagement.Controls.OfType<DataGridView>().FirstOrDefault();
                    if (dgv != null)
                    {
                        LoadOrdersData(dgv);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving order: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (sender, e) => detailForm.Close();

            // Add controls to form
            detailForm.Controls.AddRange(new Control[]
            {
                lblOrderId, txtOrderId, lblCustomer, cboCustomer, lblSalesPerson, cboSalesPerson, lblOrderDate, dtpOrderDate,
                lblStatus, cboStatus, lblTotalAmount, txtTotalAmount, lblOrderItems, dgvOrderItems,
                lblCurrentTotal, lblEmptyOrder, btnAddItem, btnRemoveItem, btnSave, btnCancel
            });

            detailForm.ShowDialog();
        }

        /// <summary>
        /// Loads customers for the order combo box
        /// </summary>
        /// <param name="cbo">ComboBox to populate</param>
        private void LoadCustomersForOrder(ComboBox cbo)
        {
            try
            {
                var customers = _dataService.LoadCustomers();
                cbo.DataSource = customers;
                cbo.DisplayMember = "StoreName";
                cbo.ValueMember = "CustomerId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads sales people for the order combo box
        /// </summary>
        /// <param name="cbo">ComboBox to populate</param>
        private void LoadSalesPeopleForOrder(ComboBox cbo)
        {
            try
            {
                var salesPeople = _dataService.LoadSalesPeople();
                cbo.DataSource = salesPeople;
                cbo.DisplayMember = "Name";
                cbo.ValueMember = "SalesPersonId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales people: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads order statuses into the combo box
        /// </summary>
        private void LoadOrderStatuses(ComboBox cbo)
        {
            try
            {
                var orders = _dataService.LoadOrders();
                var statuses = orders.Select(o => o.Status).Distinct().OrderBy(s => s).ToList();
                statuses.Insert(0, "All Statuses");
                
                cbo.Items.Clear();
                cbo.Items.AddRange(statuses.ToArray());
                if (cbo.Items.Count > 0)
                {
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order statuses: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Filters orders based on search text and status
        /// </summary>
        private void FilterOrders(DataGridView dgv, string searchText, string status)
        {
            try
            {
                var orders = _dataService.LoadOrders();
                var filteredOrders = orders.AsEnumerable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    filteredOrders = filteredOrders.Where(o => 
                        o.OrderId.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        (_dataService.LoadCustomers().FirstOrDefault(c => c.CustomerId == o.CustomerId)?.StoreName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false));
                }

                if (!string.IsNullOrEmpty(status) && status != "All Statuses")
                {
                    filteredOrders = filteredOrders.Where(o => o.Status == status);
                }

                dgv.DataSource = filteredOrders.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering orders: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads customer types into the combo box
        /// </summary>
        private void LoadCustomerTypes(ComboBox cbo)
        {
            try
            {
                var customers = _dataService.LoadCustomers();
                var types = customers.Select(c => c.CustomerType).Distinct().OrderBy(t => t).ToList();
                types.Insert(0, "All Types");
                
                cbo.Items.Clear();
                cbo.Items.AddRange(types.ToArray());
                if (cbo.Items.Count > 0)
                {
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer types: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Filters customers based on search text and customer type
        /// </summary>
        private void FilterCustomers(DataGridView dgv, string searchText, string customerType)
        {
            try
            {
                var customers = _dataService.LoadCustomers();
                var filteredCustomers = customers.AsEnumerable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    filteredCustomers = filteredCustomers.Where(c => 
                        c.StoreName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        c.StoreId.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        c.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        c.PhoneNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(customerType) && customerType != "All Types")
                {
                    filteredCustomers = filteredCustomers.Where(c => c.CustomerType == customerType);
                }

                dgv.DataSource = filteredCustomers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering customers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Removes the selected item from the order
        /// </summary>
        private void RemoveOrderItem(DataGridView dgvOrderItems, Order order, TextBox txtTotalAmount, Label lblCurrentTotal, Label lblEmptyOrder)
        {
            if (dgvOrderItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.", "No Item Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvOrderItems.SelectedRows[0];
            // Instead of accessing by column name, use the column index for ProductId (which is 0 based on the AddRange order)
            var productIdToRemove = (int)selectedRow.Cells[0].Value;

            var itemToRemove = order.OrderItems.FirstOrDefault(item => item.ProductId == productIdToRemove);
            if (itemToRemove == null)
            {
                MessageBox.Show("Selected item not found in the order.", "Item Not Found", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            order.OrderItems.Remove(itemToRemove);
            dgvOrderItems.Rows.Remove(selectedRow);

            // Update the total amount display
            var totalAmount = order.OrderItems.Sum(item => item.TotalPrice);
            txtTotalAmount.Text = totalAmount.ToString("C2");
            lblCurrentTotal.Text = $"Current Total: {totalAmount:C2}";

            // Show the empty order label if no items remain
            lblEmptyOrder.Visible = order.OrderItems.Count == 0;

            MessageBox.Show($"Item '{itemToRemove.ProductName}' removed successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Creates the sales person management panel
        /// </summary>
        private void CreateSalesPersonManagementPanel()
        {
            // Header panel (matching ACME design)
            var headerPanel = new Panel
            {
                BackColor = Color.LightBlue,
                Size = new Size(this.ClientSize.Width, 60),
                Location = new Point(0, 0),
                Dock = DockStyle.Top
            };

            var lblHeader = new Label
            {
                Text = "Sales Person Management",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 40),
                Location = new Point((this.ClientSize.Width - 400) / 2, 10)
            };

            // Admin Home button
            var btnAdminHome = new Button
            {
                Text = "Admin Home",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(100, 30),
                Location = new Point(20, 15),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdminHome.Click += (sender, e) => ShowDashboardPanel();

            // Logout button
            var btnLogout = new Button
            {
                Text = "Logout",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(80, 30),
                Location = new Point(headerPanel.Width - 100, 15),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogout.Click += (sender, e) => ShowLoginPanel();

            // Underline indicator
            var underlinePanel = new Panel
            {
                BackColor = Color.Transparent,
                Size = new Size(200, 3),
                Location = new Point((headerPanel.Width - 200) / 2, 45)
            };

            var lightGrayLine = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(100, 1),
                Location = new Point(0, 1)
            };

            var darkBlueLine = new Panel
            {
                BackColor = Color.DarkBlue,
                Size = new Size(50, 2),
                Location = new Point(0, 0)
            };

            underlinePanel.Controls.AddRange(new Control[] { lightGrayLine, darkBlueLine });
            headerPanel.Controls.AddRange(new Control[] { lblHeader, btnAdminHome, btnLogout, underlinePanel });

            // Main content panel
            var contentPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 60),
                Location = new Point(0, 60),
                Dock = DockStyle.Fill
            };

            // Search and filter panel
            var filterPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(this.ClientSize.Width - 40, 60),
                Location = new Point(20, 120),
                BorderStyle = BorderStyle.None
            };

            var lblSearch = new Label
            {
                Text = "Search:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Size = new Size(60, 25),
                Location = new Point(20, 20)
            };

            var txtSearch = new TextBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(250, 30),
                Location = new Point(90, 17),
                PlaceholderText = "Search by name, email, or phone..."
            };

            var lblRegion = new Label
            {
                Text = "Region:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                Size = new Size(70, 25),
                Location = new Point(360, 20)
            };

            var cboRegion = new ComboBox
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(150, 30),
                Location = new Point(440, 17),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var btnRefresh = new Button
            {
                Text = "Refresh",
                Font = new Font("Arial", 9, FontStyle.Regular),
                Size = new Size(80, 30),
                Location = new Point(610, 17),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            filterPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblRegion, cboRegion, btnRefresh });

            // Create Sales Person button
            var btnCreateSalesPerson = new Button
            {
                Text = "+ Create Sales Person",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(20, 190),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Delete Sales Person button
            var btnDeleteSalesPerson = new Button
            {
                Text = "- Delete Sales Person",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(190, 190),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Reset Sales People button
            var btnResetSalesPeople = new Button
            {
                Text = "Reset to Sample Data",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(150, 35),
                Location = new Point(360, 190),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Sales People data grid
            var dgvSalesPeople = new DataGridView
            {
                Font = new Font("Arial", 9, FontStyle.Regular),
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 240),
                Location = new Point(20, 240),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            // Add columns
            dgvSalesPeople.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "SalesPersonId", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                new DataGridViewTextBoxColumn { HeaderText = "Sales ID", DataPropertyName = "SalesId", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                new DataGridViewTextBoxColumn { HeaderText = "Phone", DataPropertyName = "Phone", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells },
                new DataGridViewTextBoxColumn { HeaderText = "Region", DataPropertyName = "Region", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells }
            });

            // Tip label
            var lblTip = new Label
            {
                Text = "Tip: double click a sales person to edit/manage them",
                Font = new Font("Arial", 8, FontStyle.Regular),
                ForeColor = Color.Black,
                Size = new Size(300, 20),
                Location = new Point(20, this.ClientSize.Height - 50)
            };

            // Event handlers
            btnCreateSalesPerson.Click += (sender, e) => CreateNewSalesPerson(dgvSalesPeople);
            btnDeleteSalesPerson.Click += (sender, e) => DeleteSelectedSalesPerson(dgvSalesPeople);
            btnResetSalesPeople.Click += (sender, e) => ResetSalesPeopleData(dgvSalesPeople);
            dgvSalesPeople.DoubleClick += (sender, e) => EditSelectedSalesPerson(dgvSalesPeople);
            btnRefresh.Click += (sender, e) => LoadSalesPeopleData(dgvSalesPeople);
            txtSearch.TextChanged += (sender, e) => FilterSalesPeople(dgvSalesPeople, txtSearch.Text, cboRegion.Text);
            cboRegion.SelectedIndexChanged += (sender, e) => FilterSalesPeople(dgvSalesPeople, txtSearch.Text, cboRegion.Text);

            // Load data
            LoadSalesPeopleData(dgvSalesPeople);
            LoadRegions(cboRegion);

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] { filterPanel, btnCreateSalesPerson, btnDeleteSalesPerson, btnResetSalesPeople, dgvSalesPeople, lblTip });

            // Add panels to sales person management panel
            pnlSalesPersonManagement.Controls.AddRange(new Control[] { headerPanel, contentPanel });
        }

        /// <summary>
        /// Loads sales people data into the grid
        /// </summary>
        private void LoadSalesPeopleData(DataGridView dgv)
        {
            var salesPeople = _dataService.LoadSalesPeople();
            dgv.DataSource = null;
            dgv.Rows.Clear();
            dgv.DataSource = salesPeople;
        }

        /// <summary>
        /// Creates a new sales person
        /// </summary>
        private void CreateNewSalesPerson(DataGridView dgv)
        {
            var salesPeople = _dataService.LoadSalesPeople();
            var newSalesPerson = new SalesPerson
            {
                SalesPersonId = _dataService.GetNextId<SalesPerson>(salesPeople, "SalesPersonId"),
                Name = "",
                Email = "",
                Phone = "",
                Region = ""
            };
            ShowSalesPersonDetailForm(newSalesPerson, true, dgv);
        }

        /// <summary>
        /// Edits the selected sales person
        /// </summary>
        private void EditSelectedSalesPerson(DataGridView dgv)
        {
            if (dgv.CurrentRow?.DataBoundItem is SalesPerson salesPerson)
            {
                ShowSalesPersonDetailForm(salesPerson, false, dgv);
            }
        }

        /// <summary>
        /// Shows the sales person detail form
        /// </summary>
        private void ShowSalesPersonDetailForm(SalesPerson salesPerson, bool isNew, DataGridView dgv)
        {
            var detailForm = new Form
            {
                Text = isNew ? "Create Sales Person" : "Edit Sales Person",
                Size = new Size(400, 280), // Increased height for new field
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Create controls
            var lblSalesId = new Label { Text = "Sales ID:", Location = new Point(20, 20), Size = new Size(100, 20) };
            var txtSalesId = new TextBox { Location = new Point(120, 20), Size = new Size(200, 20), Text = salesPerson.SalesId };

            var lblName = new Label { Text = "Name:", Location = new Point(20, 50), Size = new Size(100, 20) };
            var txtName = new TextBox { Location = new Point(120, 50), Size = new Size(200, 20), Text = salesPerson.Name };

            var lblEmail = new Label { Text = "Email:", Location = new Point(20, 80), Size = new Size(100, 20) };
            var txtEmail = new TextBox { Location = new Point(120, 80), Size = new Size(200, 20), Text = salesPerson.Email };

            var lblPhone = new Label { Text = "Phone:", Location = new Point(20, 110), Size = new Size(100, 20) };
            var txtPhone = new TextBox { Location = new Point(120, 110), Size = new Size(200, 20), Text = salesPerson.Phone };

            var lblRegion = new Label { Text = "Region:", Location = new Point(20, 140), Size = new Size(100, 20) };
            var txtRegion = new TextBox { Location = new Point(120, 140), Size = new Size(200, 20), Text = salesPerson.Region };

            var btnSave = new Button
            {
                Text = "Save",
                Location = new Point(120, 180), // Moved down
                Size = new Size(80, 30),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(220, 180), // Moved down
                Size = new Size(80, 30),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            // Event handlers
            btnSave.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSalesId.Text) || string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Sales ID and Name are required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                salesPerson.SalesId = txtSalesId.Text.Trim();
                salesPerson.Name = txtName.Text.Trim();
                salesPerson.Email = txtEmail.Text.Trim();
                salesPerson.Phone = txtPhone.Text.Trim();
                salesPerson.Region = txtRegion.Text.Trim();

                var salesPeople = _dataService.LoadSalesPeople();
                if (isNew)
                {
                    salesPeople.Add(salesPerson);
                }
                else
                {
                    var idx = salesPeople.FindIndex(s => s.SalesPersonId == salesPerson.SalesPersonId);
                    if (idx >= 0) salesPeople[idx] = salesPerson;
                }

                _dataService.SaveSalesPeople(salesPeople);
                detailForm.Close();
                LoadSalesPeopleData(dgv);
            };

            btnCancel.Click += (sender, e) => detailForm.Close();

            // Add controls to form
            detailForm.Controls.AddRange(new Control[] { lblSalesId, txtSalesId, lblName, txtName, lblEmail, txtEmail, lblPhone, txtPhone, lblRegion, txtRegion, btnSave, btnCancel });
            detailForm.ShowDialog();
        }

        /// <summary>
        /// Shows the sales person management panel
        /// </summary>
        private void ShowSalesPersonManagementPanel()
        {
            HideAllPanels();
            pnlSalesPersonManagement.Visible = true;
            pnlSalesPersonManagement.BringToFront();
        }

        private void DeleteSelectedSalesPerson(DataGridView dgv)
        {
            if (dgv.CurrentRow?.DataBoundItem is SalesPerson salesPerson)
            {
                var result = MessageBox.Show($"Are you sure you want to delete sales person '{salesPerson.Name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var salesPeople = _dataService.LoadSalesPeople();
                    salesPeople.RemoveAll(s => s.SalesPersonId == salesPerson.SalesPersonId);
                    _dataService.SaveSalesPeople(salesPeople);
                    LoadSalesPeopleData(dgv);
                }
            }
        }

        /// <summary>
        /// Resets sales people data to the sample data
        /// </summary>
        /// <param name="dgv">DataGridView to refresh</param>
        private void ResetSalesPeopleData(DataGridView dgv)
        {
            var result = MessageBox.Show("This will replace all existing sales people with the sample data. Are you sure?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var sampleSalesPeople = SampleDataGenerator.GenerateSampleSalesPeople();
                    _dataService.SaveSalesPeople(sampleSalesPeople);
                    LoadSalesPeopleData(dgv);
                    MessageBox.Show("Sales people data has been reset to sample data.", "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error resetting sales people data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Filters sales people based on search text and region
        /// </summary>
        private void FilterSalesPeople(DataGridView dgv, string searchText, string region)
        {
            try
            {
                var salesPeople = _dataService.LoadSalesPeople();
                var filteredSalesPeople = salesPeople.AsEnumerable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    filteredSalesPeople = filteredSalesPeople.Where(s => 
                        s.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        s.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        s.Phone.Contains(searchText, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(region) && region != "All Regions")
                {
                    filteredSalesPeople = filteredSalesPeople.Where(s => s.Region == region);
                }

                dgv.DataSource = filteredSalesPeople.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering sales people: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads regions into the combo box
        /// </summary>
        private void LoadRegions(ComboBox cbo)
        {
            try
            {
                cbo.Items.Clear();
                cbo.Items.Add("All Regions");

                var salesPeople = _dataService.LoadSalesPeople();
                var regions = salesPeople.Select(s => s.Region).Distinct().OrderBy(r => r).ToList();

                foreach (var region in regions)
                {
                    if (!string.IsNullOrEmpty(region))
                    {
                        cbo.Items.Add(region);
                    }
                }

                cbo.SelectedIndex = 0; // Select "All Regions"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading regions: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Adds a new item to the order
        /// </summary>
        private void AddOrderItem(DataGridView dgvOrderItems, Order order, TextBox txtTotalAmount, Label lblCurrentTotal, Label lblEmptyOrder)
        {
            // Check if there are any products available
            var availableProducts = _dataService.LoadProducts();
            if (availableProducts == null || availableProducts.Count == 0)
            {
                MessageBox.Show("No products available. Please create some products first.", "No Products", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product = new Product();
            if (dgvOrderItems.CurrentRow?.DataBoundItem is OrderItem item)
            {
                product = availableProducts.FirstOrDefault(p => p.ProductId == item.ProductId) ?? new Product();
            }

            var detailForm = new Form
            {
                Text = "Add Order Item",
                Size = new Size(500, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblProduct = new Label { Text = "Product:", Location = new Point(20, 20), Size = new Size(100, 20) };
            var cboProduct = new ComboBox { Location = new Point(120, 20), Size = new Size(250, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            LoadProductsForOrderItem(cboProduct, product.ProductId);

            var lblQuantity = new Label { Text = "Quantity:", Location = new Point(20, 50), Size = new Size(100, 20) };
            var txtQuantity = new TextBox { Location = new Point(120, 50), Size = new Size(100, 20), Text = "1" };

            var lblPrice = new Label { Text = "Price:", Location = new Point(240, 50), Size = new Size(100, 20) };
            var txtPrice = new TextBox { Location = new Point(350, 50), Size = new Size(100, 20), Text = product.UnitPrice.ToString(), ReadOnly = true };

            // Update price when product selection changes
            cboProduct.SelectedIndexChanged += (sender, e) =>
            {
                if (cboProduct.SelectedValue != null && int.TryParse(cboProduct.SelectedValue.ToString(), out int productId))
                {
                    var selectedProduct = availableProducts.FirstOrDefault(p => p.ProductId == productId);
                    if (selectedProduct != null)
                    {
                        txtPrice.Text = selectedProduct.UnitPrice.ToString("C2");
                    }
                }
            };

            var btnAdd = new Button
            {
                Text = "Add Item",
                Location = new Point(120, 80),
                Size = new Size(100, 30),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(240, 80),
                Size = new Size(100, 30),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnAdd.Click += (sender, e) =>
            {
                try
                {
                    if (cboProduct.SelectedValue == null || cboProduct.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please select a product.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!int.TryParse(txtQuantity.Text, out int quantity))
                    {
                        MessageBox.Show("Please enter a valid quantity.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (quantity <= 0)
                    {
                        MessageBox.Show("Quantity must be at least 1.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var selectedProduct = availableProducts.FirstOrDefault(p => p.ProductId == (int)cboProduct.SelectedValue);
                    if (selectedProduct == null)
                    {
                        MessageBox.Show("Selected product not found.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        ProductId = selectedProduct.ProductId,
                        ProductName = selectedProduct.Name,
                        Quantity = quantity,
                        UnitPrice = selectedProduct.UnitPrice,
                        TotalPrice = quantity * selectedProduct.UnitPrice
                    };

                    order.OrderItems.Add(orderItem);
                    dgvOrderItems.Rows.Add(orderItem.ProductId, orderItem.ProductName, orderItem.Quantity, orderItem.UnitPrice, orderItem.TotalPrice);
                    
                    // Update the total amount display
                    var totalAmount = order.OrderItems.Sum(item => item.TotalPrice);
                    txtTotalAmount.Text = totalAmount.ToString("C2");
                    lblCurrentTotal.Text = $"Current Total: {totalAmount:C2}";
                    
                    // Hide the empty order label since we now have items
                    lblEmptyOrder.Visible = false;
                    
                    detailForm.Close();
                    
                    MessageBox.Show($"Item '{selectedProduct.Name}' added successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding order item: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (sender, e) => detailForm.Close();

            detailForm.Controls.AddRange(new Control[]
            {
                lblProduct, cboProduct, lblQuantity, txtQuantity, lblPrice, txtPrice,
                btnAdd, btnCancel
            });

            detailForm.ShowDialog();
        }

        /// <summary>
        /// Loads products for the order item detail form
        /// </summary>
        private void LoadProductsForOrderItem(ComboBox cbo, int selectedProductId)
        {
            try
            {
                var products = _dataService.LoadProducts();
                cbo.DataSource = products;
                cbo.DisplayMember = "Name";
                cbo.ValueMember = "ProductId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintSelectedOrderToCsv(DataGridView dgv)
        {
            if (dgv.CurrentRow != null)
            {
                var orderIdObj = dgv.CurrentRow.Cells["OrderId"].Value;
                if (orderIdObj != null && int.TryParse(orderIdObj.ToString(), out int orderId))
                {
                    var orders = _dataService.LoadOrders();
                    var customers = _dataService.LoadCustomers();
                    var salesPeople = _dataService.LoadSalesPeople();
                    var order = orders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order != null)
                    {
                        var customer = customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                        var salesPerson = salesPeople.FirstOrDefault(s => s.SalesPersonId == order.SalesPersonId);

                        using (var sfd = new SaveFileDialog())
                        {
                            sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                            sfd.FileName = $"ORD_{order.OrderId}_{order.CustomerId}_{order.OrderDate:yyyyMMdd}.csv";
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    using (var writer = new StreamWriter(sfd.FileName))
                                    {
                                        // Write order header
                                        writer.WriteLine("OrderId,Customer,SalesPerson,OrderDate,Status,TotalAmount");
                                        writer.WriteLine($"{order.OrderId}," +
                                            $"'{customer?.StoreName.Replace(",", "") ?? ""}','{salesPerson?.Name.Replace(",", "") ?? "Not Assigned"}'," +
                                            $"{order.OrderDate},{order.Status},{order.TotalAmount}");
                                        writer.WriteLine();
                                        // Write order items
                                        writer.WriteLine("Order Items:");
                                        writer.WriteLine("ProductId,ProductName,Quantity,UnitPrice,TotalPrice");
                                        foreach (var item in order.OrderItems)
                                        {
                                            writer.WriteLine($"{item.ProductId},'{item.ProductName.Replace(",", "")}',{item.Quantity},{item.UnitPrice},{item.TotalPrice}");
                                        }
                                    }
                                    MessageBox.Show("Order exported to CSV successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error exporting order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        return;
                    }
                }
            }
            MessageBox.Show("Please select a valid order to print.", "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
} 