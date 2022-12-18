using Microsoft.EntityFrameworkCore;
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
    public class ProductItemValueRepository : IProductItemValueRepository
    {
        public ProductItemValue Add(ProductItemValue entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItemValues.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }

        }
        public ProductItemValue Delete(ProductItemValue entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItemValues.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
        public IEnumerable<ProductItemValue> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItemValues.ToList();
                return result;
            }
        }
        public ProductItemValue GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItemValues.Find(id);
                return result;
            }
        }
        public IEnumerable<ProductItemValue> GetExp(Expression<Func<ProductItemValue, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItemValues.Include(x=>x.ProductItem).Where(predicate).ToList();
                return result;
            }
        }
        public ProductItemValue Update(ProductItemValue entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItemValues.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
