using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UOWRepository.Data;
using UOWRepository.Model;

namespace UOWRepository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartamentoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Departamento), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var departamento = _unitOfWork.DepartamentoRepository.BuscarPorId(id);

            if (departamento is null)
                return NotFound();

            return Ok(departamento);
        }

        [HttpPost]

        public IActionResult NovoDepartamento(Departamento departamento)
        {
            _unitOfWork.DepartamentoRepository.Add(departamento);

            return Ok();
        }
    }
}
