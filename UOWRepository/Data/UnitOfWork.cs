using Microsoft.Extensions.Configuration;
using UOWRepository.Data.Repositories;

namespace UOWRepository.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private IDepartamentoRepository _departamentoRepository;

        public IDepartamentoRepository DepartamentoRepository => _departamentoRepository ?? new DepartamentoRepository(_configuration);

        private IFuncionarioRepository _funcionarioRepository;

        public IFuncionarioRepository FuncionarioRepository => _funcionarioRepository ?? new FuncionarioRepository(_configuration);
    }
}
