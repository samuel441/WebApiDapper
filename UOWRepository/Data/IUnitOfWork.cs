using System;
using UOWRepository.Data.Repositories;

namespace UOWRepository.Data
{
    public interface IUnitOfWork
    {
        IDepartamentoRepository DepartamentoRepository { get; }
        IFuncionarioRepository FuncionarioRepository { get; }
    }
}
