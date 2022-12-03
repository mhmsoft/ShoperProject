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
    public class ProductPriceRepository : IProductPriceRepository
    {
        public ProductPrice Add(ProductPrice entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductPrices.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
        public ProductPrice Delete(ProductPrice entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductPrices.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
        public IEnumerable<ProductPrice> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductPrices;
                return result;
            }
        }

        public ProductPrice GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductPrices.Find(id);
                return result;
            }
        }

        public IEnumerable<ProductPrice> GetExp(Expression<Func<ProductPrice, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductPrices.Where(predicate);
                return result;
            }
        }
        public ProductPrice Update(ProductPrice entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductPrices.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
