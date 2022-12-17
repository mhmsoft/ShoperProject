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
    public class ProductItemService : IProductItemService
    {
        private readonly IProductItemRepository _productItemRepository;
        public ProductItemService(IProductItemRepository productItemRepository)
        {
            this._productItemRepository = productItemRepository;
        }
        public ProductItem Add(ProductItem entity)
        {
           return _productItemRepository.Add(entity);
        }
        public ProductItem Delete(ProductItem entity)
        {
            return _productItemRepository.Delete(entity);
        }
        public ProductItem Get(int id)
        {
           return _productItemRepository.GetbyId(id);
        }
        public IEnumerable<ProductItem> GetAll()
        {
            return this._productItemRepository.GetAll();
        }
        public IEnumerable<ProductItem> GetExp(Expression<Func<ProductItem, bool>> predicate)
        {
            return _productItemRepository.GetExp(predicate);
        }
        public ProductItem Update(ProductItem entity)
        {
            return _productItemRepository.Update(entity);
        }
    }
}
