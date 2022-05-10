using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class MemberShipRepository: IRepository<webpages_Membership>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<webpages_Membership> GetAll()
        {
            return _entities.webpages_Membership;
        }

        public IEnumerable<webpages_Membership> GetWhere(System.Linq.Expressions.Expression<Func<webpages_Membership, bool>> query)
        {
            return _entities.webpages_Membership.Where(query);
        }

        public webpages_Membership GetById(int id)
        {
            return _entities.webpages_Membership.Find(id);
        }

        public void Add(webpages_Membership item)
        {
            _entities.webpages_Membership.Add(item);
            _entities.SaveChanges();
        }

        public void Update(webpages_Membership item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(webpages_Membership item)
        {
            _entities.webpages_Membership.Remove(item);
            _entities.SaveChanges();
        }
    }
}
