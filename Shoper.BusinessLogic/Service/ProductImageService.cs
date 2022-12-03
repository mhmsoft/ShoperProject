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
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImageService(IProductImageRepository productImageRepository)
        {
            this._productImageRepository = productImageRepository;
        }
        public ProductImage Add(ProductImage entity)
        {
            return _productImageRepository.Add(entity);
        }
        public ProductImage Delete(ProductImage entity)
        {
            return _productImageRepository.Delete(entity);
        }
        public ProductImage Get(int id)
        {
            return _productImageRepository.GetbyId(id);
        }
        public IEnumerable<ProductImage> GetAll()
        {
            return _productImageRepository.GetAll();
        }
        public IEnumerable<ProductImage> GetExp(Expression<Func<ProductImage, bool>> predicate)
        {
            return _productImageRepository.GetExp(predicate);
        }
        public ProductImage Update(ProductImage entity)
        {
           return _productImageRepository.Update(entity);   
        }
    }
}
