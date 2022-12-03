using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Data.Interface
{
    public interface IRepository<T> where T : class
    {
        T GetbyId(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetExp(Expression<Func<T,bool>> predicate);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
