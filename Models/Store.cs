namespace Models;

public class Store
{
    public int Id { get; set; }


    public string Name { get; set; }
    public string Address { get; set; }

    private List<Order> OrderHistory;

    public List<Product> Inventory;

    public void DisplayStock()
    {
        foreach (Product product in Inventory)
        {
            Console.WriteLine($"{product.Name} | ${product.Price} | {product.Quantity}");
        }
    }

}