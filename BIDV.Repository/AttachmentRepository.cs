using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class AttachmentRepository: IRepository<bidv__attach>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__attach> GetAll()
        {
            return _entities.bidv__attach;
        }

        public IEnumerable<bidv__attach> GetWhere(System.Linq.Expressions.Expression<Func<bidv__attach, bool>> query)
        {
            return _entities.bidv__attach.Where(query);
        }

        public bidv__attach GetById(int id)
        {
            return _entities.bidv__attach.Find(id);
        }

        public void Add(bidv__attach item)
        {
            _entities.bidv__attach.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__attach item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__attach item)
        {
            _entities.bidv__attach.Remove(item);
            _entities.SaveChanges();
        }
    }
}
