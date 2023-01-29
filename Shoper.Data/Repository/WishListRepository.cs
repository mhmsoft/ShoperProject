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
    public class WishListRepository : IWishListRepository
    {
        public WishList Add(WishList entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.WishLists.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public WishList Delete(WishList entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.WishLists.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<WishList> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.WishLists.ToList();
                return result;
            }
        }

        public WishList GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.WishLists.Find(id);
                return result;
            }
        }

        public IEnumerable<WishList> GetExp(Expression<Func<WishList, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.WishLists
                    .Include(x=>x.product)
                    .ThenInclude(x=>x.ProductImage)
                    .Include(x=>x.product.ProductPrice)
                    .Where(predicate).ToList();
                return result;
            }
        }

        public WishList Update(WishList entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.WishLists.Update(entity);
                context.SaveChanges();
                return result.Entity;

            }
        }
    }
}
