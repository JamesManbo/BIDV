using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class QaRepository: IRepository<bidv__faqs>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__faqs> GetAll()
        {
            return _entities.bidv__faqs;
        }

        public IEnumerable<bidv__faqs> GetWhere(System.Linq.Expressions.Expression<Func<bidv__faqs, bool>> query)
        {
            return _entities.bidv__faqs.Where(query);
        }

        public bidv__faqs GetById(int id)
        {
            return _entities.bidv__faqs.Find(id);
        }

        public void Add(bidv__faqs item)
        {
            _entities.bidv__faqs.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__faqs item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__faqs item)
        {
            _entities.bidv__faqs.Remove(item);
            _entities.SaveChanges();
        }
    }
}
