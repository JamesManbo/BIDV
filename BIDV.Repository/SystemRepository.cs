using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class SystemRepository: IRepository<bidv__system>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__system> GetAll()
        {
            return _entities.bidv__system;
        }

        public IEnumerable<bidv__system> GetWhere(System.Linq.Expressions.Expression<Func<bidv__system, bool>> query)
        {
            return _entities.bidv__system.Where(query);
        }

        public bidv__system GetById(int id)
        {
            return _entities.bidv__system.Find(id);
        }

        public void Add(bidv__system item)
        {
            _entities.bidv__system.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__system item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }


        public void Delete(bidv__system item)
        {
            throw new NotImplementedException();
        }
    }
}
