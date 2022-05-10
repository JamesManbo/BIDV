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
    public class NewsRepository: IRepository<BIDV.Model.bidv__news>
    {
        readonly BIDVEntities _bidvEntities = new BIDVEntities();
        public IEnumerable<bidv__news> GetAll()
        {
            return _bidvEntities.bidv__news;
        }

        public IEnumerable<bidv__news> GetWhere(Expression<Func<bidv__news, bool>> query)
        {
            return _bidvEntities.bidv__news.Where(query);
        }

        public bidv__news GetById(int id)
        {
          return  _bidvEntities.bidv__news.FirstOrDefault(a => a.id.Equals(id));
        }

        public void Add(bidv__news item)
        {
            _bidvEntities.bidv__news.Add(item);
            _bidvEntities.SaveChanges();
        }

        public void Update(bidv__news item)
        {
            _bidvEntities.Entry(item).State = EntityState.Modified;
            _bidvEntities.SaveChanges();
        }

        public void Delete(bidv__news item)
        {
            _bidvEntities.bidv__news.Remove(item);
            _bidvEntities.SaveChanges();
        }
    }
}
