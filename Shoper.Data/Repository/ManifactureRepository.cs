using Shoper.Data.Interface;
using Shoper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Data.Repository
{
    public class ManifactureRepository:IManifactureRepository
    {
        public Manifacture Add(Manifacture entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Manifactures.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public Manifacture Delete(Manifacture entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Manifactures.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<Manifacture> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.Manifactures.ToList();
                return result;
            }
        }
        public Manifacture GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Manifactures.Find(id);
                return result;
            }
        }

        public IEnumerable<Manifacture> GetExp(Expression<Func<Manifacture, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Manifactures.Where(predicate).ToList();
                return result;
            }
        }

        public Manifacture Update(Manifacture entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Manifactures.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

    }
}
