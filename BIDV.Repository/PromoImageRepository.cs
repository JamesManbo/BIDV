using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class PromoImageRepository: IRepository<bidv__promotion_img>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__promotion_img> GetAll()
        {
            return _entities.bidv__promotion_img;
        }

        public IEnumerable<bidv__promotion_img> GetWhere(System.Linq.Expressions.Expression<Func<bidv__promotion_img, bool>> query)
        {
            return _entities.bidv__promotion_img.Where(query);
        }

        public bidv__promotion_img GetById(int id)
        {
            return _entities.bidv__promotion_img.Find(id);
        }

        public void Add(bidv__promotion_img item)
        {
            _entities.bidv__promotion_img.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__promotion_img item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__promotion_img item)
        {
            _entities.bidv__promotion_img.Remove(item);
            _entities.SaveChanges();
        }
    }
}
