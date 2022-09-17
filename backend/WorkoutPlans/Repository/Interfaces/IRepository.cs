using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Models;

namespace WorkoutPlans.Repository.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FetchAll();
        T FetchById(Guid? id);
        T Insert(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
