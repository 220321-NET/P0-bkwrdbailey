using DL;
using Models;

namespace BL;
public class StoreBL
{
    private readonly DBRepository _repo;
    public StoreBL(DBRepository repo)
    {
        _repo = repo;
    }

    public List<User> GetAllUsers()
    {
        return _repo.GetAllUsers();
    }

    public User AddUser(User userToAdd)
    {
        return _repo.CreateUser(userToAdd);
    }

    public void AddOrder(Order order) {
        _repo.CreateOrder(order);
    }

    public List<Store> GetAllStores()
    {
        return _repo.GetAllStores();
    }

    public Store GetStoreInventory(Store currentStore)
    {
        return _repo.GetStoreInventory(currentStore);
    }

}
