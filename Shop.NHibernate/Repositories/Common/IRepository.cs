using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.NHibernate.Repositories
{
    public interface IRepository<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T GetById(object id);
        IQueryable<T> GetAll();
        //T GetById(int id);
        //bool Add(IEnumerable<T> items);
        //bool Update(IEnumerable<T> items);
        //bool Delete(IEnumerable<T> items);
        //T GetBy(Expression<Func<T, bool>> expression);
        //IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);
    }
}
