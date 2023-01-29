using Shoper.BusinessLogic.Interface;
using Shoper.Data.Interface;
using Shoper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.BusinessLogic.Service
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public Coupon Add(Coupon entity)
        {
            return _couponRepository.Add(entity);
        }

        public Coupon Delete(Coupon entity)
        {
            return _couponRepository.Delete(entity);
        }
        public Coupon Get(int id)
        {
            return _couponRepository.GetbyId(id);

        }
        public IEnumerable<Coupon> GetAll()
        {
            return _couponRepository.GetAll();
        }
        public IEnumerable<Coupon> GetExp(Expression<Func<Coupon, bool>> predicate)
        {
            return _couponRepository.GetExp(predicate);
        }
        public Coupon Update(Coupon entity)
        {
            return _couponRepository.Update(entity);
        }
    }
}
