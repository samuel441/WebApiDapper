using System.Collections.Generic;

namespace UOWRepository.Data.Repositories.Base
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Atualizar(T entity);
        T BuscarPorId(int id);
        IEnumerable<T> BuscarTodos();
    }
}
