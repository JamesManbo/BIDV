using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class GalleryRepository: IRepository<bidv__gallery>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__gallery> GetAll()
        {
            return _entities.bidv__gallery;
        }

        public IEnumerable<bidv__gallery> GetWhere(System.Linq.Expressions.Expression<Func<bidv__gallery, bool>> query)
        {
            return _entities.bidv__gallery.Where(query);
        }

        public bidv__gallery GetById(int id)
        {
            return _entities.bidv__gallery.Find(id);
        }

        public void Add(bidv__gallery item)
        {
            _entities.bidv__gallery.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__gallery item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__gallery item)
        {
            _entities.bidv__gallery.Remove(item);
            _entities.SaveChanges();
        }
    }
}
