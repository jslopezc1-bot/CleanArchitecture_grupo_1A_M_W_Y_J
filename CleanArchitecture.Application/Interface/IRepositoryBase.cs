using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interface
{
    // Interfaz genérica para repositorios, define las operaciones CRUD básicas
    public interface IRepositoryBase<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
