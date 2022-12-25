using Shoper.Data.Interface;
using Shoper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Data.Repository
{
    public class OrderDetailRepository:IOrderDetailRepository
    {
        public OrderDetail Add(OrderDetail entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.OrderDetails.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public OrderDetail Delete(OrderDetail entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.OrderDetails.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.OrderDetails.ToList();
                return result;
            }
        }

        public OrderDetail GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.OrderDetails.Find(id);
                return result;
            }
        }

        public IEnumerable<OrderDetail> GetExp(Expression<Func<OrderDetail, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.OrderDetails.Where(predicate).ToList();
                return result;
            }
        }

        public OrderDetail Update(OrderDetail entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.OrderDetails.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
