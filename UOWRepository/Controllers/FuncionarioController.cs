using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UOWRepository.Data;
using UOWRepository.Model;

namespace UOWRepository.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FuncionarioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Funcionario), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> ObterPorId(int id)
        {
            var funcioanario = _unitOfWork.FuncionarioRepository.BuscarPorId(id);

            if (funcioanario is null)
                return NotFound();

            return Ok(funcioanario);
        }

        [HttpGet("ObterPorDepartamentoId/{id:int}")]
        [ProducesResponseType(typeof(Funcionario), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> ObterPorDepartamentoId(int id)
        {
            var funcioanario = _unitOfWork.FuncionarioRepository.FindByDepartamentoId(id);

            if (funcioanario is null)
                return NotFound();

            return Ok(funcioanario);
        }

        [HttpPost]
        public IActionResult NovoFuncionario(Funcionario funcioanario)
        {
            _unitOfWork.FuncionarioRepository.Add(funcioanario);

            return Ok();
        }
    }
}
