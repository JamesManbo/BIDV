using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class CardRepository: IRepository<bidv__card>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__card> GetAll()
        {
            return _entities.bidv__card;
        }

        public IEnumerable<bidv__card> GetWhere(System.Linq.Expressions.Expression<Func<bidv__card, bool>> query)
        {
            return _entities.bidv__card.Where(query);
        }

        public bidv__card GetById(int id)
        {
            return _entities.bidv__card.Find(id);
        }

        public void Add(bidv__card item)
        {
            _entities.bidv__card.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__card item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__card item)
        {
            _entities.bidv__card.Remove(item);
            _entities.SaveChanges();
        }
    }
}
