using Models;
using BL;

namespace UI;

public class EmployeeMenu
{
    private readonly StoreBL _bl;
    private Employee _user = new Employee();

    public EmployeeMenu(StoreBL bl, Employee user)
    {
        _bl = bl;
        _user = user;
    }

    public void Menu()
    {

    }
}