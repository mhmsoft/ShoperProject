using Shoper.BusinessLogic.Interface;
using Shoper.Data.Interface;
using Shoper.Data.Repository;
using Shoper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.BusinessLogic.Service
{
    public class ManifactureService:IManifactureService
    {
         private readonly IManifactureRepository _manifactureRepository;
        public ManifactureService(IManifactureRepository manifactureRepository)
        {
            this._manifactureRepository = manifactureRepository;
        }
           
            public Manifacture Add(Manifacture entity)
            {
                return _manifactureRepository.Add(entity);
            }

            public Manifacture Delete(Manifacture entity)
            {
                return _manifactureRepository.Delete(entity);
            }

            public Manifacture Get(int id)
            {
                return _manifactureRepository.GetbyId(id);
            }

            public IEnumerable<Manifacture> GetAll()
            {
                return _manifactureRepository.GetAll();
            }

            public IEnumerable<Manifacture> GetExp(Expression<Func<Manifacture, bool>> predicate)
            {
                return _manifactureRepository.GetExp(predicate);
            }

            public Manifacture Update(Manifacture entity)
            {
                return _manifactureRepository.Update(entity);
            }
        }
}
