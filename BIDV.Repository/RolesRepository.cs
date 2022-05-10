using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BIDV.Model;

namespace BIDV.Repository
{
    public class RolesRepository : IRepository<Model.webpages_Roles>
    {
        readonly BIDVEntities _entities = new BIDVEntities();
        public IEnumerable<webpages_Roles> GetAll()
        {
            return _entities.webpages_Roles;
        }

        public IEnumerable<webpages_Roles> GetWhere(Expression<Func<webpages_Roles, bool>> query)
        {
            return _entities.webpages_Roles.Where(query);
        }

        public webpages_Roles GetById(int id)
        {
            return _entities.webpages_Roles.Find(id);
        }

        public void Add(webpages_Roles item)
        {
            _entities.webpages_Roles.Add(item);
            _entities.SaveChanges();
        }

        public void Update(webpages_Roles item)
        {
            _entities.Entry(item).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        public void Delete(webpages_Roles item)
        {
            _entities.webpages_Roles.Remove(item);
            _entities.SaveChanges();
        }

    }
}
