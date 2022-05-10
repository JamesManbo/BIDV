using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;
using System.Data.Entity;
namespace BIDV.Repository
{
  public  class MenuRepository: IRepository<bidv__menu>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__menu> GetAll()
        {
            return _entities.bidv__menu;
        }

        public IEnumerable<bidv__menu> GetWhere(System.Linq.Expressions.Expression<Func<bidv__menu, bool>> query)
        {
            return _entities.bidv__menu.Where(query);
        }

        public bidv__menu GetById(int id)
        {
            return _entities.bidv__menu.Find(id);
        }

        public void Add(bidv__menu item)
        {
            _entities.bidv__menu.Add(item);
            _entities.SaveChanges();
        }

        public void Update(bidv__menu item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__menu item)
        {
            _entities.Entry(item).State = EntityState.Deleted;
            _entities.bidv__menu.Remove(item);
            _entities.SaveChanges();
        }
    }
}
