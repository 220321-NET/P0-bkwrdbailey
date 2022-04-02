namespace Models;

public static class AccountHandling
{

    public static bool Login()
    {


        return false;
    }

    public static bool Register()
    {

    Register:
        Console.WriteLine("Enter a username: ");
        string? userName = Console.ReadLine() ?? "";

        Console.WriteLine("Enter a password: ");
        string? password = Console.ReadLine() ?? "";
        goto Register;

        return false;
    }
}