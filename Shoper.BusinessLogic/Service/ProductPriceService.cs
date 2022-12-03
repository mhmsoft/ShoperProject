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
    public class ProductPriceService : IProductPriceService
    {
        private readonly IProductPriceRepository _productPriceRepository;
        public ProductPriceService(IProductPriceRepository productPriceRepository)
        {
            this._productPriceRepository = productPriceRepository;
        }
        public ProductPrice Add(ProductPrice entity)
        {
            return _productPriceRepository.Add(entity);
        }
        public ProductPrice Delete(ProductPrice entity)
        {
            return _productPriceRepository.Delete(entity);
        }
        public ProductPrice Get(int id)
        {
            return _productPriceRepository.GetbyId(id);
        }
        public IEnumerable<ProductPrice> GetAll()
        {
            return _productPriceRepository.GetAll();
        }
        public IEnumerable<ProductPrice> GetExp(Expression<Func<ProductPrice, bool>> predicate)
        {
           return _productPriceRepository.GetExp(predicate);
        }
        public ProductPrice Update(ProductPrice entity)
        {
            return _productPriceRepository.Update(entity);
        }
    }
}
