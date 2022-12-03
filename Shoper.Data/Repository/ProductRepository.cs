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
    public class ProductRepository : IProductRepository
    {
        public Product Add(Product entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Products.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public Product Delete(Product entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Products.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.Products.ToList();                
                return result;
            }
        }

        public Product GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Products.Find(id);               
                return result;
            }
        }

        public IEnumerable<Product> GetExp(Expression<Func<Product, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Products.Where(predicate).ToList();                
                return result;
            }
        }

        public Product Update(Product entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Products.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
