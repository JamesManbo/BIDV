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
   public class NewsImageRepository: IRepository<BIDV.Model.bidv__news_image>   
   {
       readonly BIDVEntities _entities = new BIDVEntities();
       public IEnumerable<bidv__news_image> GetAll()
       {
           return _entities.bidv__news_image;
       }

       public IEnumerable<bidv__news_image> GetWhere(System.Linq.Expressions.Expression<Func<bidv__news_image, bool>> query)
       {
           return _entities.bidv__news_image.Where(query);
       }

       public bidv__news_image GetById(int id)
       {
           return _entities.bidv__news_image.Find(id);
       }

       public void Add(bidv__news_image item)
       {
           _entities.bidv__news_image.Add(item);
           _entities.SaveChanges();
       }

       public void Update(bidv__news_image item)
       {
           _entities.Entry(item).State = EntityState.Modified;
           _entities.SaveChanges();
       }

       public void Delete(bidv__news_image item)
       {
           _entities.bidv__news_image.Remove(item);
           _entities.SaveChanges();
       }
   }
}
