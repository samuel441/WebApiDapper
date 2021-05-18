using System.Collections.Generic;
using UOWRepository.Data.Repositories.Base;
using UOWRepository.Model;

namespace UOWRepository.Data.Repositories
{
    public interface IFuncionarioRepository :IRepository<Funcionario>
    {
        List<Funcionario> FindByDepartamentoId(int id);
    }
}
