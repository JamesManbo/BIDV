using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class DetailAttachmentRepository: IRepository<bidv__attach_detail>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__attach_detail> GetAll()
        {
            return _entities.bidv__attach_detail;
        }

        public IEnumerable<bidv__attach_detail> GetWhere(System.Linq.Expressions.Expression<Func<bidv__attach_detail, bool>> query)
        {
            return _entities.bidv__attach_detail.Where(query);
        }

        public bidv__attach_detail GetById(int id)
        {
            return _entities.bidv__attach_detail.Find(id);
        }

        public void Add(bidv__attach_detail item)
        {
            _entities.bidv__attach_detail.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__attach_detail item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__attach_detail item)
        {
            _entities.bidv__attach_detail.Remove(item);
            _entities.SaveChanges();
        }
    }
}
