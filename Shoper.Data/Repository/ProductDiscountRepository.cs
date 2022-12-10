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
    public class ProductDiscountRepository : IProductDiscountRepository
    {
        public ProductDiscount Add(ProductDiscount entity)
        {
            using (var context=new ShoperContext())
            {
                var result= context.ProductDiscounts.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public ProductDiscount Delete(ProductDiscount entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductDiscounts.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<ProductDiscount> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductDiscounts.ToList();
                return result;
            }
        }

        public ProductDiscount GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductDiscounts.Find(id);              
                return result;
            }
        }

        public IEnumerable<ProductDiscount> GetExp(Expression<Func<ProductDiscount, bool>> predicate)
        {
            using(var context = new ShoperContext())
            {
                var result = context.ProductDiscounts.Where(predicate).ToList();                
                return result;
            }
        }
        public ProductDiscount Update(ProductDiscount entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductDiscounts.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
