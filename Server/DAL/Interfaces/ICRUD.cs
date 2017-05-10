using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICrud<T>
    {
        void Insert(T item);
        T ReadItem(string id);
        ICollection<T> GetElementsByClient(string userName);
        void Delete(string id);
    }
}
