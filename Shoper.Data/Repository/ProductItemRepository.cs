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
    public class ProductItemRepository : IProductItemRepository
    {
        public ProductItem Add(ProductItem entity)
        {
            using (var context=new ShoperContext())
            {
                var result = context.ProductItems.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
            
        }
        public ProductItem Delete(ProductItem entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItems.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
        public IEnumerable<ProductItem> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItems.ToList();               
                return result;
            }
        }
        public ProductItem GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItems.Find(id);
                return result;
            }
        }
        public IEnumerable<ProductItem> GetExp(Expression<Func<ProductItem, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItems.Include(x=>x.Category).Where(predicate).ToList();
                return result;
            }
        }
        public ProductItem Update(ProductItem entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductItems.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
