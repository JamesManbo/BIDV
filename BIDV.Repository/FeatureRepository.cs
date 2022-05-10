using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class FeatureRepository: IRepository<bidv__feature>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__feature> GetAll()
        {
            return _entities.bidv__feature;
        }

        public IEnumerable<bidv__feature> GetWhere(System.Linq.Expressions.Expression<Func<bidv__feature, bool>> query)
        {
            return _entities.bidv__feature.Where(query);
        }

        public bidv__feature GetById(int id)
        {
            return _entities.bidv__feature.Find(id);
        }

        public void Add(bidv__feature item)
        {
            _entities.bidv__feature.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__feature item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__feature item)
        {
            _entities.bidv__feature.Remove(item);
            _entities.SaveChanges();
        }
    }
}
