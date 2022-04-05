using Microsoft.Data.SqlClient;
using System.Data;
using Models;

namespace DL;
public class DBRepository
{

    private readonly string _connectionString;

    public DBRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Gets all products from Product Table in Azure Database
    public List<Product> GetAllProducts()
    {

        List<Product> products = new List<Product>();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM Product");
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {

        }

        reader.Close();
        connection.Close();

        return products;
    }

    public Store GetStoreInventory(Store currentStore)
    {

        List<Product> storeInventory = new List<Product>();

        DataSet inventorySet = new DataSet();

        SqlConnection connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("SELECT ProductId, StoreFrontId, Product.Name as ProductName, Description, Price, Quantity FROM Inventory JOIN Product ON (Product.Id = ProductId) JOIN StoreFront ON (StoreFront.Id = StoreFrontId) WHERE StoreFrontId = @id;", connection);
        cmd.Parameters.AddWithValue("@id", currentStore.Id);

        SqlDataAdapter inventoryAdapter = new SqlDataAdapter(cmd);

        inventoryAdapter.Fill(inventorySet, "StoreInventoryTable");
        DataTable? storeInventoryTable = inventorySet.Tables["StoreInventoryTable"];
        if (storeInventoryTable != null && storeInventoryTable.Rows.Count > 0)
        {
            foreach (DataRow row in storeInventoryTable.Rows)
            {
                Product product = new Product();

                product.Id = (int)row["ProductId"];
                product.Name = (string)row["ProductName"];
                product.Description = (string)row["Description"];
                product.Price = (double)row["Price"];
                product.Quantity = (int)row["Quantity"];

                storeInventory.Add(product);
            }
        }
        currentStore.Inventory = storeInventory;

        return currentStore;
    }

    public List<User> GetAllUsers()
    {

        List<User> users = new List<User>();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM Users", connection);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string userName = reader.GetString(1);
            string password = reader.GetString(2);
            bool _isEmployed = reader.GetBoolean(3);

            // Creates new employee or customer object based on employment status
            if (_isEmployed == true)
            {
                Employee employee = new Employee
                {
                    Id = id,
                    UserName = userName,
                    Password = password,
                    IsEmployed = _isEmployed,
                };
                users.Add(employee);
            }
            else
            {
                User customer = new User
                {
                    Id = id,
                    UserName = userName,
                    Password = password,
                    IsEmployed = _isEmployed
                };
                users.Add(customer);
            }
        }

        reader.Close();
        connection.Close();

        return users;
    }

    public List<Store> GetAllStores()
    {

        List<Store> stores = new List<Store>();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM StoreFront", connection);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string address = reader.GetString(2);

            Store store = new Store
            {
                Id = id,
                Name = name,
                Address = address
            };

            stores.Add(store);
        }

        reader.Close();
        connection.Close();

        return stores;
    }

    public User CreateUser(User userToAdd)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO Users(UserName, Password) OUTPUT INSERTED.Id VALUES(@username, @password)", connection);

        cmd.Parameters.AddWithValue("@username", userToAdd.UserName);
        cmd.Parameters.AddWithValue("@password", userToAdd.Password);

        try
        {
            userToAdd.Id = (int)cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        connection.Close();

        return userToAdd;
    }

    public void CreateOrder(Order order)
    {
        DateTime currentDate = DateTime.Now;
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO Orders(StoreFrontId, CustomerId, TotalCost, DateOrdered) OUTPUT INSERTED.Id VALUES (@storeId, @customerId, @totalCost, @dateOrdered)", connection);

        cmd.Parameters.AddWithValue("@storeId", order.store.Id);
        cmd.Parameters.AddWithValue("@customerId", order.customer.Id);
        cmd.Parameters.AddWithValue("@totalCost", order.cart.GetTotalCost());
        cmd.Parameters.AddWithValue("@dateOrdered", currentDate);

        try
        {
            order.Id = (int)cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        connection.Close();
        CreateCart(order);
    }

    private void CreateCart(Order order)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd;

        foreach (Product product in order.cart.AllProducts())
        {
            cmd = new SqlCommand("INSERT INTO Cart (ProductId, Quantity, OrderId) OUTPUT INSERTED.Id VALUES (@productId, @quantity, @orderId)", connection);
            cmd.Parameters.AddWithValue("@productId", product.Id);
            cmd.Parameters.AddWithValue("@quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@orderId", order.Id);

            int id = (int)cmd.ExecuteScalar();
        }

        connection.Close();

        UpdateInventory(order);
    }

    private void UpdateInventory(Order order)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd;

        foreach (Product product in order.store.Inventory)
        {
            cmd = new SqlCommand("UPDATE Inventory SET Quantity = @quantity OUTPUT INSERTED.Id WHERE ProductId = @productId AND StoreFrontId = @storeFront", connection);
            cmd.Parameters.AddWithValue("@quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@productId", product.Id);
            cmd.Parameters.AddWithValue("@storeFront", order.store.Id);

            int id = (int)cmd.ExecuteScalar();
        }

        connection.Close();
    }
}