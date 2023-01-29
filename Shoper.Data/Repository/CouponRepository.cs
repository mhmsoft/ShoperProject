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
    public class CouponRepository : ICouponRepository
    {
        public Coupon Add(Coupon entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Coupons.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public Coupon Delete(Coupon entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Coupons.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<Coupon> GetAll()
        {
         
            using (var context = new ShoperContext())
            {
                var result = context.Coupons.ToList();
                return result;
            }
        }

        public Coupon GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Coupons.Find(id);
                return result;
            }
        }

        public IEnumerable<Coupon> GetExp(Expression<Func<Coupon, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Coupons.Where(predicate).ToList();
                return result;
            }
        }

        public Coupon Update(Coupon entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Coupons.Update(entity);
                context.SaveChanges();
                return result.Entity;

            }
        }
    }
}
