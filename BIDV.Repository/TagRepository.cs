using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class TagRepository: IRepository<bidv__tags>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<bidv__tags> GetAll()
        {
            return _entities.bidv__tags;
        }

        public IEnumerable<bidv__tags> GetWhere(System.Linq.Expressions.Expression<Func<bidv__tags, bool>> query)
        {
            return _entities.bidv__tags.Where(query);
        }

        public bidv__tags GetById(int id)
        {
            return _entities.bidv__tags.Find(id);
        }

        public void Add(bidv__tags item)
        {
            _entities.bidv__tags.Add(item);
            _entities.SaveChanges();
        }
        public int AddAndGetId(bidv__tags item)
        {
            _entities.bidv__tags.Add(item);
            _entities.SaveChanges();
            int insertId = item.id;
            return insertId;
        }
        public void Update(bidv__tags item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(bidv__tags item)
        {
            _entities.bidv__tags.Remove(item);
            _entities.SaveChanges();
        }
    }
}
