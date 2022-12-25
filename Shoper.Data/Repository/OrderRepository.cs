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
    public class OrderRepository : IOrderRepository
    {
        public Order Add(Order entity)
        {
            using (var context=new ShoperContext())
            {
                var result = context.Orders.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public Order Delete(Order entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Orders.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<Order> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.Orders.ToList();               
                return result;
            }
        }

        public Order GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Orders.Find(id);
                return result;
            }
        }

        public IEnumerable<Order> GetExp(Expression<Func<Order, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Orders.Where(predicate).ToList();
                return result;
            }
        }

        public Order Update(Order entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Orders.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
