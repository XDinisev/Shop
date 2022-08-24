using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Shop.NHibernate.Repositories;

namespace Shop.NHibernate.Repositories
{
    //NHibernateContext да видам зашо е
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly UnitOfWork _unitOfWork;
        protected ISession _session =>_unitOfWork.Session;
        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        public bool Add(T entity)
        {
            _session.Save(entity);
            return true;
        }
        public bool Update(T entity)
        {
            _session.Update(entity);
            return true;
        }

        public bool Delete(T entity)
        {
            _session.Delete(entity);
            return true;
        }
        public T GetById(object id)
        {
            return _session.Get<T>(id);
        }

        public IQueryable<T> GetAll()
        {
            return _session.Query<T>();
        }

    }
}
