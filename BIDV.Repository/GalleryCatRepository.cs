using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class GalleryCatRepository: IRepository<bidv__gallery_cats>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__gallery_cats> GetAll()
        {
            return _entities.bidv__gallery_cats;
        }

        public IEnumerable<bidv__gallery_cats> GetWhere(System.Linq.Expressions.Expression<Func<bidv__gallery_cats, bool>> query)
        {
            return _entities.bidv__gallery_cats.Where(query);
        }

        public bidv__gallery_cats GetById(int id)
        {
            return _entities.bidv__gallery_cats.Find(id);
        }

        public void Add(bidv__gallery_cats item)
        {
            _entities.bidv__gallery_cats.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__gallery_cats item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__gallery_cats item)
        {
            _entities.bidv__gallery_cats.Remove(item);
            _entities.SaveChanges();
        }
    }
}
