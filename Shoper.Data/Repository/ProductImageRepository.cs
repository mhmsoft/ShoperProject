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
    public class ProductImageRepository : IProductImageRepository
    {
        public ProductImage Add(ProductImage entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductImages.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public ProductImage Delete(ProductImage entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductImages.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<ProductImage> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductImages.ToList();               
                return result;
            }
        }

        public ProductImage GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductImages.Find(id);
                return result;
            }
        }

        public IEnumerable<ProductImage> GetExp(Expression<Func<ProductImage, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductImages.Where(predicate).ToList();
                return result;
            }
        }
        public ProductImage Update(ProductImage entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductImages.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
