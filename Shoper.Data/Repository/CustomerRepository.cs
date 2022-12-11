using Microsoft.EntityFrameworkCore;
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
    public class CustomerRepository : ICustomerRepository
    {
        public Customer Add(Customer entity)
        {
            using (var context= new ShoperContext())
            {
                var result = context.Customers.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public Customer Delete(Customer entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Customers.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.Customers.Include(x=>x.Addresses).ToList();                
                return result;
            }
        }

        public Customer GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Customers.Include(x => x.Addresses).FirstOrDefault(x=>x.CustomerId==id);
                return result;
            }
        }

        public IEnumerable<Customer> GetExp(Expression<Func<Customer, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Customers.Include(x => x.Addresses).Where(predicate).ToList();
                return result;
            }
        }
        public Customer Update(Customer entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Customers.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
