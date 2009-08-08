using System.Collections.Generic;

namespace FastGTD.Domain
{
    public interface IGTDItemPersistence<T>
    {
        IList<T> GetAll();
        void DeleteAll();
        void Delete(T item);
        void Save(T item);
    }
}