using LiteDB;
namespace Pokedex_MAUI.Services;

public class RestService<T>
{
    protected LiteCollection<T> _collection;
    
    public RestService()
    {
        _collection = (LiteCollection<T>)App.Database.GetCollection<T>();
    }

    public IEnumerable<T> FindAll()
    {
        return _collection.FindAll();
    }

    public T FindById(int id)
    {
        return _collection.FindById(id);
    }

    public bool UpsertItem(T item)
    {
        return _collection.Upsert(item);
    }

    public bool DeleteAll()
    {
        return _collection.DeleteAll() > 0;
    }
}