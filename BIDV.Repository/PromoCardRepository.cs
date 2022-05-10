using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class PromoCardRepository: IRepository<bidv__promotion_card>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__promotion_card> GetAll()
        {
            return _entities.bidv__promotion_card;
        }

        public IEnumerable<bidv__promotion_card> GetWhere(System.Linq.Expressions.Expression<Func<bidv__promotion_card, bool>> query)
        {
            return _entities.bidv__promotion_card.Where(query);
        }

        public bidv__promotion_card GetById(int id)
        {
            return _entities.bidv__promotion_card.Find(id);
        }

        public void Add(bidv__promotion_card item)
        {
            _entities.bidv__promotion_card.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__promotion_card item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__promotion_card item)
        {
            _entities.bidv__promotion_card.Remove(item);
            _entities.SaveChanges();
        }
    }
}
