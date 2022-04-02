namespace Models;

public class Order {

    public int Id { get; set; }
    public Customer customer;
    public List<Product> allProducts;

    public float totalCost;
}