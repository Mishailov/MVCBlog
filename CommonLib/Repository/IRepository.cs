using System.Collections.Generic;

namespace CommonLib.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetItems();
        T GetItem(int Id);
        void Create(T item);
        void Delete(int Id);
    }
}
