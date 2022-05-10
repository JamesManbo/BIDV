using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class PromotionRepository: IRepository<bidv__promotion>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__promotion> GetAll()
        {
            return _entities.bidv__promotion;
        }

        public IEnumerable<bidv__promotion> GetWhere(System.Linq.Expressions.Expression<Func<bidv__promotion, bool>> query)
        {
            return _entities.bidv__promotion.Where(query);
        }

        public bidv__promotion GetById(int id)
        {
            return _entities.bidv__promotion.Find(id);
        }

        public void Add(bidv__promotion item)
        {
            _entities.bidv__promotion.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__promotion item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__promotion item)
        {
            _entities.bidv__promotion.Remove(item);
            _entities.SaveChanges();
        }
    }
}
