using DL;
namespace BL;
public class StoreBL
{
    private readonly DBRepository _repo;
    public StoreBL(DBRepository repo)
    {
        _repo = repo;
    }

}
