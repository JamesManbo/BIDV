using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class UserRepository:IRepository<UserProfile>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<UserProfile> GetAll()
        {
            return _entities.UserProfiles;
        }

        public IEnumerable<view_UserProfile> GetAllViewUserProfiles()
        {
            return _entities.view_UserProfile;
        }
        public IEnumerable<UserProfile> GetWhere(System.Linq.Expressions.Expression<Func<UserProfile, bool>> query)
        {
            return _entities.UserProfiles.Where(query);
        }

        public UserProfile GetById(int id)
        {
            return _entities.UserProfiles.Find(id);
        }

        public void Add(UserProfile item)
        {
            _entities.UserProfiles.Add(item);
            _entities.SaveChanges();
        }

        public void Update(UserProfile item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(UserProfile item)
        {
            _entities.UserProfiles.Remove(item);
            _entities.SaveChanges();
        }
    }
}
