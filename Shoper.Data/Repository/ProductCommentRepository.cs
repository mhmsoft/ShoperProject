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
    public class ProductCommentRepository : IProductCommentRepository
    {
        public ProductComment Add(ProductComment entity)
        {
            using (var context=new ShoperContext())
            {
                var result = context.ProductComments.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
        public ProductComment Delete(ProductComment entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductComments.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
        public IEnumerable<ProductComment> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductComments.ToList();
                return result;
            }
        }

        public ProductComment GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductComments.Find(id);
                return result;
            }
        }
        public IEnumerable<ProductComment> GetExp(Expression<Func<ProductComment, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductComments.Where(predicate).ToList();
                return result;
            }
        }
        public ProductComment Update(ProductComment entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.ProductComments.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
