using System.Collections.Generic;

namespace FastGTD.Domain
{
    public interface IItemPersistence<T>
    {
        T GetById(int id);
        IList<T> GetAll();
        void DeleteAll();
        void Delete(T item);
        void Save(T item);
    }
}