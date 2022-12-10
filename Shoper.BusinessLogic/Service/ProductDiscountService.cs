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
    public class ProductDiscountService : IProductDiscountService
    {
        private readonly IProductDiscountRepository _productDiscountRepository;
        public ProductDiscountService(IProductDiscountRepository productDiscountRepository)
        {
            this._productDiscountRepository = productDiscountRepository;
        }
        public ProductDiscount Add(ProductDiscount entity)
        {
            return _productDiscountRepository.Add(entity);
        }

        public ProductDiscount Delete(ProductDiscount entity)
        {
            return _productDiscountRepository.Delete(entity);
        }

        public ProductDiscount Get(int id)
        {
            return _productDiscountRepository.GetbyId(id);
        }

        public IEnumerable<ProductDiscount> GetAll()
        {
            return _productDiscountRepository.GetAll();
        }

        public IEnumerable<ProductDiscount> GetExp(Expression<Func<ProductDiscount, bool>> predicate)
        {
            return _productDiscountRepository.GetExp(predicate);
        }

        public ProductDiscount Update(ProductDiscount entity)
        {
            return _productDiscountRepository.Update(entity);
        }
    }
}
