using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class SubscribeRepository:IRepository<bidv__subscribe>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__subscribe> GetAll()
        {
            return _entities.bidv__subscribe;
        }

        public IEnumerable<bidv__subscribe> GetWhere(System.Linq.Expressions.Expression<Func<bidv__subscribe, bool>> query)
        {
            return _entities.bidv__subscribe.Where(query);
        }

        public bidv__subscribe GetById(int id)
        {
            return _entities.bidv__subscribe.Find(id);
        }

        public void Add(bidv__subscribe item)
        {
            _entities.bidv__subscribe.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__subscribe item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__subscribe item)
        {
            _entities.bidv__subscribe.Remove(item);
            _entities.SaveChanges();
        }
    }
}
