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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public Customer Add(Customer entity)
        {
            return _customerRepository.Add(entity);
        }

        public Customer Delete(Customer entity)
        {
            return _customerRepository.Delete(entity);
        }

        public Customer Get(int id)
        {
            return _customerRepository.GetbyId(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public IEnumerable<Customer> GetExp(Expression<Func<Customer, bool>> predicate)
        {
            return _customerRepository.GetExp(predicate);           
        }

        public Customer Update(Customer entity)
        {
            return _customerRepository.Update(entity);
        }
    }
}
