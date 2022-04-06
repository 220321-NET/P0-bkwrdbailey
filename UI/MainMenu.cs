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

    public void LoginMenu()
    {
        Console.WriteLine("=========================================\n====== Just Another Computer Store ======\n=========================================\n");

    LoginRegister:
        Console.WriteLine("[1] Login");
        Console.WriteLine("[2] Register");
        Console.WriteLine("[x] Exit");

        string? answer = Console.ReadLine().Trim() ?? "";
        User user = new User();

        if (answer == "1")
        {
            user = Login();
        }
        else if (answer == "2")
        {
            user = Register();

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

        if (user == null)
        {
            return;
        }
        else
        {
            if (user.IsEmployed == true)
            {
                Employee employee = (Employee)user;
                new EmployeeMenu(_bl, employee).Menu();
                Console.WriteLine("==================================================================");
            }
            else
            {
                User customer = user;
                new CustomerMenu(_bl, customer).StoreMenu();
                Console.WriteLine("==================================================================");
            }
        }
        Console.WriteLine("Logging out...");
    }

    private User Login()
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
                    User signedIn = user;
                    return signedIn;
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
            User customer = Register();
            return customer;
        }

        return null;

    }

    public User Register()
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
                    return null;
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

        return newUser;
    }
}