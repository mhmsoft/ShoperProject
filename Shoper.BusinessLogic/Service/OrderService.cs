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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Order Add(Order entity)
        {
           return _orderRepository.Add(entity);
        }

        public Order Delete(Order entity)
        {
            return _orderRepository.Delete(entity);
        }

        public Order Get(int id)
        {
            return _orderRepository.GetbyId(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public IEnumerable<Order> GetExp(Expression<Func<Order, bool>> predicate)
        {
            return _orderRepository.GetExp(predicate);
        }

        public Order Update(Order entity)
        {
            return _orderRepository.Update(entity);
        }
    }
}
