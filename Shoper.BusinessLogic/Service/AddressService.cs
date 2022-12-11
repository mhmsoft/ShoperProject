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
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            this._addressRepository = addressRepository;
        }
        public Address Add(Address entity)
        {
           return this._addressRepository.Add(entity);
        }

        public Address Delete(Address entity)
        {
            return this._addressRepository.Delete(entity);
        }

        public Address Get(int id)
        {
            return this._addressRepository.GetbyId(id);
        }

        public IEnumerable<Address> GetAll()
        {
            return this._addressRepository.GetAll();
        }

        public IEnumerable<Address> GetExp(Expression<Func<Address, bool>> predicate)
        {
            return this._addressRepository.GetExp(predicate);
        }

        public Address Update(Address entity)
        {
            return this._addressRepository.Update(entity);
        }
    }
}
