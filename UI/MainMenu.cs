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

        int answer = Convert.ToInt32(Console.ReadLine());

        if (answer == 1)
        {
            AccountHandling.Login();
        }
        else if (answer == 2)
        {
            AccountHandling.Register();

        }
        else
        {
            Console.WriteLine("Invalid Input");
            goto LoginRegister;
        }

    StoreLocation:
        Console.WriteLine("We have two stores currently that you can shop from\nWhich one would you like to shop at?"); // Maybe change later if I want to add more than two store locations
        Console.WriteLine("[1] First Location");
        Console.WriteLine("[2] Second Location");

        answer = Convert.ToInt32(Console.ReadLine());

        if (answer == 1)
        {
            // Store firstStore = ; // Add first store instance
        }
        else if (answer == 2)
        {
            // Store secondStore = ;   // Add second store instance
        }
        else
        {
            Console.WriteLine("Invalid Input");
            goto StoreLocation;
        }

        string result = Menu(/*Store Instance*/);

        if (result == "6")
        {
            goto StoreLocation;
        }
    }

    private static string Menu(/*Store Instance*/)
    {
    MenuChoices:
        Console.WriteLine("[1] See inventory");
        Console.WriteLine("[2] Add product to cart");
        Console.WriteLine("[3] Remove product from cart");
        Console.WriteLine("[4] Show cart's contents");
        Console.WriteLine("[5] Checkout");
        Console.WriteLine("[6] Change store location");
        Console.WriteLine("[7] Logout");

        string? answer = Console.ReadLine().Trim() ?? "";

        switch (answer)
        {
            case "1":
                // Loop through inventory of the specified store and display products
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
                return answer;

            case "x":
                Console.WriteLine("Logging out...");
                return answer;

            default:
                Console.WriteLine("Invalid Input");
                goto MenuChoices;
        }

        goto MenuChoices;


    }

    private static void Checkout()
    {

    }

    private static void DisplayCart()
    {

    }

}