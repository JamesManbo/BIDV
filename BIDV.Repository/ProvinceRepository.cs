using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using BIDV.Model;

namespace BIDV.Repository
{
    public class ProvinceRepository : IRepository<BIDV.Model.bidv___province>
    {
        readonly BIDVEntities _bidvEntities = new BIDVEntities();
        public IEnumerable<Model.bidv___province> GetAll()
        {
            return _bidvEntities.bidv___province;
        }

        public IEnumerable<Model.bidv___province> GetWhere(System.Linq.Expressions.Expression<Func<Model.bidv___province, bool>> query)
        {
            return _bidvEntities.bidv___province.Where(query);
        }

        public Model.bidv___province GetById(int id)
        {
            return _bidvEntities.bidv___province.Find(id);
        }

        public void Add(Model.bidv___province item)
        {
            try
            {
                _bidvEntities.bidv___province.Add(item);
                _bidvEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw e;
            }

        }

        public void Update(Model.bidv___province item)
        {
            _bidvEntities.Entry(item).State = EntityState.Modified;
            _bidvEntities.SaveChanges();
        }

        public void Delete(Model.bidv___province item)
        {
            _bidvEntities.bidv___province.Remove(item);
            _bidvEntities.Entry(item).State = EntityState.Deleted;
            _bidvEntities.SaveChanges();
        }
    }
}
