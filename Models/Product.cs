namespace Models;

public class Product {
    public string Name { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }

    public void IncreaseQty(int amtToAdd) 
    {
        Quantity += amtToAdd;
    }

    public void DecreaseQty(int amtToDecrease) 
    {
        Quantity -= amtToDecrease;
    }

    public void ChangePrice(float newPrice) {
        Price = newPrice;
    }
}