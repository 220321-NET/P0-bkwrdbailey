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

    public void AddOrder(Order order)
    {
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

    public List<OrderHistory> GetOrderHistoryByStore(Store _store)
    {
        List<OrderHistory> storeOrderHistory = _repo.GetOrderHistoryByStore(_store);
        return storeOrderHistory;
    }

    public List<OrderHistory> GetOrderHistoryByUser(User user)
    {
        List<OrderHistory> userOrderHistory = _repo.GetOrderHistoryByUser(user);
        return userOrderHistory;
    }
}
