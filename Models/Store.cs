namespace Models;

public class Store
{
    private readonly string _location;

    private List<Order> OrderHistory;

    public List<Product> Inventory;

    public Store(string location)
    {
        _location = location; // EDITME: Add location from database
    }

    public void DisplayStock()
    {
        foreach (Product product in Inventory)
        {
            Console.WriteLine($"{product.Name} | ${product.Price} | {product.Quantity}");
        }
    }

}