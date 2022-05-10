using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class CategoryRepository : IRepository<bidv__category>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__category> GetAll()
        {
            return _entities.bidv__category;
        }

        public IEnumerable<bidv__category> GetWhere(System.Linq.Expressions.Expression<Func<bidv__category, bool>> query)
        {
            return _entities.bidv__category.Where(query);
        }

        public bidv__category GetById(int id)
        {
            return _entities.bidv__category.Find(id);
        }

        public void Add(bidv__category item)
        {
            _entities.bidv__category.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__category item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__category item)
        {
            _entities.bidv__category.Remove(item);
            _entities.SaveChanges();
        }
    }
}
