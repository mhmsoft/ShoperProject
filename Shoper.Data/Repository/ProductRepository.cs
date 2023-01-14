using Microsoft.EntityFrameworkCore;
using Shoper.Data.Interface;
using Shoper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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
                var result = context.Products
                    .Include(p=>p.ProductCategory)
                    .Include(price=>price.ProductPrice)
                    .Include(img=>img.ProductImage)
                    .Include(Item=>Item.ProductItemValue)
                    .ToList();                
                return result;
            }
        }

        public Product GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Products
                    .Include(x=>x.ProductCategory)
                    .ThenInclude(x=>x.ProductItem)
                    .ThenInclude(x => x.ProductItemValue)
                    .Include(x=>x.ProductPrice)
                    .Include(x=>x.ProductImage)
                    .Include(x=>x.ProductComment)
                    .Include(x=>x.ProductDiscount)
                    .Include(x=>x.Manifacture)                    
                    .FirstOrDefault(x=>x.ProductId==id);               
                return result;
            }
        }

        public IEnumerable<Product> GetExp(Expression<Func<Product, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Products.Include(x=>x.ProductCategory).Where(predicate).ToList();                
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
