using Microsoft.Data.SqlClient;

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

        SqlConnection connection = new SqlConnection(/*Add Database Connection string*/);
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
}
