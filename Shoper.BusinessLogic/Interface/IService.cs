using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.BusinessLogic.Interface
{
    public interface IService<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetExp(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
