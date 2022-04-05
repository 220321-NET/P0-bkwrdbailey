using Models;
using BL;

namespace UI;

public class CustomerMenu
{
    private readonly StoreBL _bl;
    private User _user = new User();
    private Cart cart = new Cart();
    private Store currentStore = new Store();

    public CustomerMenu(StoreBL bl, User user)
    {
        _bl = bl;
        _user = user;
    }

    public void StoreMenu()
    {
        List<Store> stores = _bl.GetAllStores();

    StoreLocation:
        Console.WriteLine("We have two stores currently that you can shop from\nWhich one would you like to shop at?"); // Maybe change later if I want to add more than two store locations

        int i = 1;
        foreach (Store store in stores)
        {
            Console.WriteLine($"[{i}] {store.Name} | {store.Address}");
            i++;
        }

        string? storeAnswer = Console.ReadLine().Trim() ?? "";
        Console.WriteLine("==================================================================");

        if (storeAnswer == "1")
        {
            currentStore = stores[0];
        }
        else if (storeAnswer == "2")
        {
            currentStore = stores[1];
        }
        else
        {
            Console.WriteLine("Invalid Input");
            goto StoreLocation;
        }

        currentStore = _bl.GetStoreInventory(currentStore);
        string result = Menu();

        if (result == "6")
        {
            goto StoreLocation;
        }
    }

    private string Menu()
    {
    MenuChoices:
        Console.WriteLine("[1] See inventory");
        Console.WriteLine("[2] Add product to cart");
        Console.WriteLine("[3] Remove product from cart");
        Console.WriteLine("[4] Show cart's contents");
        Console.WriteLine("[5] Checkout");
        Console.WriteLine("[6] Change store location");
        Console.WriteLine("[x] Logout");

        string? choice = Console.ReadLine().Trim() ?? "";
        Console.WriteLine("==================================================================");

        switch (choice)
        {
            case "1":
                Inventory();
                Console.WriteLine("==================================================================");

                break;

            case "2":
                Inventory();
                AddProduct();
                Console.WriteLine("==================================================================");
                break;

            case "3":
                if (cart.IsCartEmpty())
                {
                    Console.WriteLine("There is nothing in the cart");
                    Console.WriteLine("==================================================================");
                }
                else
                {
                    RemoveProduct();
                    Console.WriteLine("==================================================================");
                }
                break;

            case "4":
                if (cart.IsCartEmpty())
                {
                    Console.WriteLine("There is nothing in the cart");
                    Console.WriteLine("==================================================================");

                }
                else
                {
                    cart.CartContents();
                    Console.WriteLine("==================================================================");

                }
                break;

            case "5":
                bool HasCheckedOut = Checkout();

                if (HasCheckedOut)
                {
                    return "x";
                }
                break;

            case "6":
                if (cart.IsCartEmpty())
                {
                    return choice;
                }
                else
                {
                ChangeStore:
                    Console.WriteLine("There are items in your cart\nAre you sure you want to change the store? [Y/N]");
                    string? decision = Console.ReadLine().Trim().ToUpper() ?? "";

                    if (decision == "Y")
                    {
                        cart.ClearCart();
                        Console.WriteLine("Your cart has been cleared!");
                        Console.WriteLine("==================================================================");

                        return choice;
                    }
                    else if (decision == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        goto ChangeStore;
                    }
                }

            case "x":
                return choice;

            default:
                Console.WriteLine("Invalid Input");
                goto MenuChoices;
        }

        goto MenuChoices;

    }

    private void AddProduct()
    {
        Console.WriteLine("Which item would you like to add:");
        string? choice = Console.ReadLine().Trim() ?? "";

        int productId = Convert.ToInt32(choice);

        foreach (Product product in currentStore.Inventory)
        {
            if (product.Id == productId)
            {
                Console.WriteLine("How many of this item would you like:");
                int amount = Convert.ToInt32(Console.ReadLine());

                Product item = new Product();

                item.Id = product.Id;
                item.Name = product.Name;
                item.Price = product.Price;
                item.Description = product.Description;
                item.Quantity = amount;

                for (int i = 0; i < currentStore.Inventory.Count; i++)
                {
                    if (productId == currentStore.Inventory[i].Id)
                    {
                        currentStore.Inventory[i].Quantity -= amount;
                    }
                }

                cart.AddItem(item);
            }
        }
    }

    private void RemoveProduct()
    {
        cart.CartContents();

        Console.WriteLine("Which item would you like removed:");
        string? choice = Console.ReadLine().Trim() ?? "";

        int numChoice = Convert.ToInt32(choice);

        Product product = cart.RemoveItem(numChoice - 1);

        foreach (Product item in currentStore.Inventory)
        {
            if (item.Name == product.Name)
            {
                item.Quantity += product.Quantity;
            }
        }
    }

    private bool Checkout()
    {
    CheckoutChoice:
        cart.CartContents();
        Console.WriteLine("Are you sure you are ready to checkout? [Y/N]");
        string? choice = Console.ReadLine().Trim().ToUpper() ?? "";

        if (choice == "Y")
        {
            Console.WriteLine("Finalizing Order...");
            Order order = new Order();
            order.customer = _user;
            order.cart = cart;
            order.store = currentStore;
            _bl.AddOrder(order);
            return true;
        }
        else if (choice == "N")
        {
            return false;
        }
        else
        {
            Console.WriteLine("Invalid Input");
            goto CheckoutChoice;
        }
    }
    private void Inventory()
    {
        currentStore.DisplayStock();
    }
}