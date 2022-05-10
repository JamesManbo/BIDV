using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class PromoDesRepository: IRepository<bidv__promotion_des>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__promotion_des> GetAll()
        {
            return _entities.bidv__promotion_des;
        }

        public IEnumerable<bidv__promotion_des> GetWhere(System.Linq.Expressions.Expression<Func<bidv__promotion_des, bool>> query)
        {
            return _entities.bidv__promotion_des.Where(query);
        }

        public bidv__promotion_des GetById(int id)
        {
            return _entities.bidv__promotion_des.Find(id);
        }

        public void Add(bidv__promotion_des item)
        {
            _entities.bidv__promotion_des.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__promotion_des item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__promotion_des item)
        {
            _entities.bidv__promotion_des.Remove(item);
            _entities.SaveChanges();
        }
    }
}
