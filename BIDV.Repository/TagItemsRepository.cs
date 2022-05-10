using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;
using System.Data.Entity;
namespace BIDV.Repository
{
    public class TagItemsRepository : IRepository<bidv__tag_items>
    {

        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__tag_items> GetAll()
        {
            return _entities.bidv__tag_items;
        }

        public IEnumerable<bidv__tag_items> GetWhere(System.Linq.Expressions.Expression<Func<bidv__tag_items, bool>> query)
        {
            return _entities.bidv__tag_items.Where(query);
        }

        public bidv__tag_items GetById(int id)
        {
            return _entities.bidv__tag_items.Find(id);
        }

        public void Add(bidv__tag_items item)
        {
            _entities.bidv__tag_items.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__tag_items item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__tag_items item)
        {
            _entities.bidv__tag_items.Remove(item);
            _entities.SaveChanges();
        }
    }
}
