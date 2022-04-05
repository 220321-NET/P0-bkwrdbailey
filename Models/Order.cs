namespace Models;

public class Order {

    public int Id { get; set; }
    public User customer;
    public Cart cart = new Cart();
    public Store store = new Store();
}