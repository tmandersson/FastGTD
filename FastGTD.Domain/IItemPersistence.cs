using System.Collections.Generic;

namespace FastGTD.Domain
{
    public interface IItemPersistence<T>
    {
        IList<T> GetAll();
        void DeleteAll();
        void Delete(T item);
        void Save(T item);
    }
}