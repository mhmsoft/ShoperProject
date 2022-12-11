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
    public class AddressRepository : IAddressRepository
    {
        public Address Add(Address entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Addresses.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
        public Address Delete(Address entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Addresses.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
        public IEnumerable<Address> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.Addresses.ToList();
                return result;
            }
        }

        public Address GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Addresses.Find(id);
                return result;
            }
        }
        public IEnumerable<Address> GetExp(Expression<Func<Address, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Addresses.Where(predicate).ToList();
                return result;
            }
        }
        public Address Update(Address entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Addresses.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
