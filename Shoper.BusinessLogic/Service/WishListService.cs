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
    public class WishListService:IWishListService
    {
        private readonly IWishListRepository _wishListRepository;
        public WishListService(IWishListRepository wishListRepository)
        {
            _wishListRepository= wishListRepository;
        }

        public WishList Add(WishList entity)
        {
           return _wishListRepository.Add(entity);
        }

        public WishList Delete(WishList entity)
        {
            return _wishListRepository.Delete(entity);
        }

        public WishList Get(int id)
        {
            return _wishListRepository.GetbyId(id);
        }

        public IEnumerable<WishList> GetAll()
        {
            return  _wishListRepository.GetAll();
        }

        public IEnumerable<WishList> GetExp(Expression<Func<WishList, bool>> predicate)
        {
            return _wishListRepository.GetExp(predicate);
        }

        public WishList Update(WishList entity)
        {
            return _wishListRepository.Update(entity);
        }
    }
}
