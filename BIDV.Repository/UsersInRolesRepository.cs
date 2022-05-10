using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIDV.Model;

namespace BIDV.Repository
{
    public class UsersInRolesRepository
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<webpages_UsersInRoles> GetAll()
        {
            return _entities.webpages_UsersInRoles;
        }

        public IEnumerable<view_UserRoles> GetViewUserInRoles()
        {
            return _entities.view_UserRoles;
        }
        public IEnumerable<webpages_UsersInRoles> GetWhere(System.Linq.Expressions.Expression<Func<webpages_UsersInRoles, bool>> query)
        {
            return _entities.webpages_UsersInRoles.Where(query);
        }

        public webpages_UsersInRoles GetById(int id)
        {
            return _entities.webpages_UsersInRoles.Find(id);
        }

        public void Add(webpages_UsersInRoles item)
        {
            _entities.webpages_UsersInRoles.Add(item);
            _entities.SaveChanges();
        }

        public void Add(List<webpages_UsersInRoles> items)
        {
            foreach (var item in items)
            {
                _entities.webpages_UsersInRoles.Add(item);
            }
            _entities.SaveChanges();
        }

        public void Update(webpages_UsersInRoles item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(int RoleId, int UserId)
        {
            var objUsersInRoles = _entities.webpages_UsersInRoles.FirstOrDefault(g => g.RoleId == RoleId && g.UserId == UserId);
            _entities.webpages_UsersInRoles.Remove(objUsersInRoles);
            _entities.SaveChanges();
        }

        public void Delete(int RoleId)
        {
            var items = _entities.webpages_UsersInRoles.Where(g => g.RoleId == RoleId);
            foreach (var item in items)
            {
                _entities.webpages_UsersInRoles.Remove(item);
            }
            _entities.SaveChanges();
        }
    }
}
