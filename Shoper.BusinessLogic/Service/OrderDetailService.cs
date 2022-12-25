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
    public class OrderDetailService:IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public OrderDetail Add(OrderDetail entity)
        {
            return _orderDetailRepository.Add(entity);
        }

        public OrderDetail Delete(OrderDetail entity)
        {
            return _orderDetailRepository.Delete(entity);
        }

        public OrderDetail Get(int id)
        {
            return _orderDetailRepository.GetbyId(id);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();
        }

        public IEnumerable<OrderDetail> GetExp(Expression<Func<OrderDetail, bool>> predicate)
        {
            return _orderDetailRepository.GetExp(predicate);
        }

        public OrderDetail Update(OrderDetail entity)
        {
            return _orderDetailRepository.Update(entity);
        }
    }
}
