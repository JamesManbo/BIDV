using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class PromoCardCityRepository: IRepository<bidv__promotion_city>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__promotion_city> GetAll()
        {
            return _entities.bidv__promotion_city;
        }

        public IEnumerable<bidv__promotion_city> GetWhere(System.Linq.Expressions.Expression<Func<bidv__promotion_city, bool>> query)
        {
            return _entities.bidv__promotion_city.Where(query);
        }

        public bidv__promotion_city GetById(int id)
        {
            return _entities.bidv__promotion_city.Find(id);
        }

        public void Add(bidv__promotion_city item)
        {
            _entities.bidv__promotion_city.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__promotion_city item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__promotion_city item)
        {
            _entities.bidv__promotion_city.Remove(item);
            _entities.SaveChanges();
        }
    }
}
