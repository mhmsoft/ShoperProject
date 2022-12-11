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
    public class ProductCommentService : IProductCommentService
    {
        private readonly IProductCommentRepository _productCommentRepository;
        public ProductCommentService(IProductCommentRepository productCommentRepository)
        {
            this._productCommentRepository = productCommentRepository;
        }
        public ProductComment Add(ProductComment entity)
        {
            return _productCommentRepository.Add(entity);
        }

        public ProductComment Delete(ProductComment entity)
        {
            return _productCommentRepository.Delete(entity);
        }

        public ProductComment Get(int id)
        {
            return _productCommentRepository.GetbyId(id);
        }

        public IEnumerable<ProductComment> GetAll()
        {
            return _productCommentRepository.GetAll();

        }

        public IEnumerable<ProductComment> GetExp(Expression<Func<ProductComment, bool>> predicate)
        {
            return _productCommentRepository.GetExp(predicate);
        }

        public ProductComment Update(ProductComment entity)
        {
            return _productCommentRepository.Update(entity);    
        }
    }
}
