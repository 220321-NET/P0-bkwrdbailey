using Models;
using BL;

namespace UI;

public class MainMenu
{

    private readonly StoreBL _bl;
    public MainMenu(StoreBL bl)
    {
        _bl = bl;

    }

    public void StoreMenu()
    {
        Console.WriteLine("=========================================\n====== Just Another Computer Store ======\n=========================================\n");

    LoginRegister:
        Console.WriteLine("[1] Login");
        Console.WriteLine("[2] Register");
        Console.WriteLine("[x] Exit");

        string? answer = Console.ReadLine().Trim() ?? "";
        bool isLoggedIn = false;

        if (answer == "1")
        {
            isLoggedIn = Login();
        }
        else if (answer == "2")
        {
            isLoggedIn = Register();

        }
        else if (answer.ToLower() == "x")
        {
            return;
        }
        else
        {
            Console.WriteLine("Invalid Input");
            goto LoginRegister;
        }

        if (!isLoggedIn)
        {
            return;
        }

        Store currentStore = new Store();
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

        string result = Menu(currentStore);

        if (result == "6")
        {
            goto StoreLocation;
        }
    }

    private string Menu(Store currentStore)
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

        switch (choice)
        {
            case "1":
                Inventory(currentStore);
                break;

            case "2":
                // Add a product to the customer's cart
                break;

            case "3":
                // Remove a product from the customer's cart
                break;

            case "4":
                // Loop through and display cart's contents
                break;

            case "5":
                // Checkout customer's cart
                break;

            case "6":
                return choice;

            case "x":
                Console.WriteLine("Logging out...");
                return choice;

            default:
                Console.WriteLine("Invalid Input");
                goto MenuChoices;
        }

        goto MenuChoices;


    }

    private bool Login()
    {

    Login:
        Console.WriteLine("Enter your Username: ");
        string? userName = Console.ReadLine().Trim() ?? "";

        List<User> users = _bl.GetAllUsers();

        foreach (User user in users)
        {
            if (user.UserName == userName)
            {
            Password:
                Console.WriteLine("Enter you Password: ");
                string? password = Console.ReadLine().Trim() ?? "";

                if (user.Password == password)
                {
                    Console.WriteLine("Logging in...");
                    return true;
                }
                else
                {
                    Console.WriteLine("Incorrect password...Try again? [Y/N]");
                    string? innerResponse = Console.ReadLine().Trim().ToUpper() ?? "";

                    if (innerResponse == "Y")
                    {
                        goto Password;
                    }
                }
            }
        }

        Console.WriteLine("Could not find an account with that username.\nWould you like to try again or make an account?\n[1] Try Again\n[2] Register");
        string? outerResponse = Console.ReadLine().Trim() ?? "";

        if (outerResponse == "1")
        {
            goto Login;
        }
        else if (outerResponse == "2")
        {
            bool isRegistered = Register();
            return isRegistered;
        }

        return false;

    }

    public bool Register()
    {
    Register:
        Console.WriteLine("Enter a Username: ");
        string? userName = Console.ReadLine().Trim() ?? "";

        List<User> users = _bl.GetAllUsers();

        foreach (User user in users)
        {
            if (user.UserName == userName)
            {
                Console.WriteLine("That username is already taken!\nTry Again?[Y/N]");
                string? response = Console.ReadLine().Trim().ToUpper() ?? "";

                if (response == "N")
                {
                    return false;
                }
                goto Register;
            }
        }

        Console.WriteLine("Enter a Password");
        string? password = Console.ReadLine().Trim() ?? "";

        User newUser = new User();

        newUser.UserName = userName;
        newUser.Password = password;

        _bl.AddUser(newUser);

        return true;

    }

    private void Checkout()
    {

    }

    private void DisplayCart()
    {

    }

    private void Inventory(Store currentStore)
    {
        currentStore = _bl.GetStoreInventory(currentStore);
        int i = 1;

        foreach (Product item in currentStore.Inventory)
        {
            Console.WriteLine($"[{i}] ${item.Price} | {item.Name} | {item.Quantity} QTY.\n{item.Description}");
            i++;
        }
    }

}