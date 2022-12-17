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
    public class ProductItemValueService:IProductItemValueService
    {
        private readonly IProductItemValueRepository _productItemValueRepository;
        public ProductItemValueService(IProductItemValueRepository productItemValueRepository)
        {
            this._productItemValueRepository = productItemValueRepository;
        }
        public ProductItemValue Add(ProductItemValue entity)
        {
            return _productItemValueRepository.Add(entity);
        }
        public ProductItemValue Delete(ProductItemValue entity)
        {
            return _productItemValueRepository.Delete(entity);
        }
        public ProductItemValue Get(int id)
        {
            return _productItemValueRepository.GetbyId(id);
        }
        public IEnumerable<ProductItemValue> GetAll()
        {
            return this._productItemValueRepository.GetAll();
        }
        public IEnumerable<ProductItemValue> GetExp(Expression<Func<ProductItemValue, bool>> predicate)
        {
            return _productItemValueRepository.GetExp(predicate);
        }
        public ProductItemValue Update(ProductItemValue entity)
        {
            return _productItemValueRepository.Update(entity);
        }
    }
}
