using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class SloganRepository : IRepository<bidv__slogan>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__slogan> GetAll()
        {
            return _entities.bidv__slogan;
        }

        public IEnumerable<bidv__slogan> GetWhere(System.Linq.Expressions.Expression<Func<bidv__slogan, bool>> query)
        {
            return _entities.bidv__slogan.Where(query);
        }

        public bidv__slogan GetById(int id)
        {
            return _entities.bidv__slogan.Find(id);
        }

        public void Add(bidv__slogan item)
        {
            _entities.bidv__slogan.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__slogan item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__slogan item)
        {
            _entities.bidv__slogan.Remove(item);
            _entities.SaveChanges();
        }
    }
}
