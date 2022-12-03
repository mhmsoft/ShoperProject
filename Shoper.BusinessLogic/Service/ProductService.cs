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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository )
        {
            this._productRepository = productRepository;
        }
        public Product Add(Product entity)
        {
            return _productRepository.Add(entity);
        }
        public Product Delete(Product entity)
        {
            return _productRepository.Delete(entity);
        }
        public Product Get(int id)
        {
            return _productRepository.GetbyId(id);
        }
        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
        public IEnumerable<Product> GetExp(Expression<Func<Product, bool>> predicate)
        {
            return _productRepository.GetExp(predicate);
        }
        public Product Update(Product entity)
        {
            return _productRepository.Update(entity);
        }
    }
}
