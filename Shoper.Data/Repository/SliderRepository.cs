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
    public class SliderRepository : ISliderRepository
    {
        public Slider Add(Slider entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Sliders.Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public Slider Delete(Slider entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Sliders.Remove(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public IEnumerable<Slider> GetAll()
        {
            using (var context = new ShoperContext())
            {
                var result = context.Sliders.ToList();
                return result;
            }
        }

        public Slider GetbyId(int id)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Sliders.Find(id);
                return result;
            }
        }

        public IEnumerable<Slider> GetExp(Expression<Func<Slider, bool>> predicate)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Sliders.Where(predicate).ToList();
                return result;
            }
        }

        public Slider Update(Slider entity)
        {
            using (var context = new ShoperContext())
            {
                var result = context.Sliders.Update(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }
    }
}
