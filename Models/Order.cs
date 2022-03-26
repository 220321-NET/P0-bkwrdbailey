namespace Models;

public class Order {
    public Customer customer;
    public List<Product> allProducts;

    public float totalCost;
}