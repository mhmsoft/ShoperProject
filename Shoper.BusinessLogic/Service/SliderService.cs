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
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        public Slider Add(Slider entity)
        {
            return _sliderRepository.Add(entity);
        }

        public Slider Delete(Slider entity)
        {
            return _sliderRepository.Delete(entity);
        }

        public Slider Get(int id)
        {
            return _sliderRepository.GetbyId(id);
        }

        public IEnumerable<Slider> GetAll()
        {
            return _sliderRepository.GetAll();
        }

        public IEnumerable<Slider> GetExp(Expression<Func<Slider, bool>> predicate)
        {
           return _sliderRepository.GetExp(predicate);
        }

        public Slider Update(Slider entity)
        {
            return _sliderRepository.Update(entity);
        }
    }
}
