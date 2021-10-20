using MyCustomTestBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Repository
{
    public abstract class MockRepositoryBase<TModel> : IRepository<TModel> where TModel : class, IModel, new()
    {
        private static List<TModel> _models = new List<TModel>();

        public void Create(TModel item)
        {
            int id = _models.LastOrDefault()?.Id + 1 ?? 1;
            item.Id = id;
            FillModelForCreate(item);

            _models.Add(item);
        }

        public void Delete(int Id)
        {
            _models.Remove(_models.Where(x => x.Id == Id).FirstOrDefault());
        }

        public TModel GetItem(int Id)
        {
            return _models.Where(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<TModel> GetItems()
        {
            return _models;
        }

        protected abstract void FillModelForCreate(TModel modelToFill);
    }
}
