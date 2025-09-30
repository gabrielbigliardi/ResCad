using Microsoft.AspNetCore.Mvc;
using ResCad.Application.Interfaces;
using ResCad.Dominio.Dtos;

namespace ResCad.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResidentesController(
            ILogger<ResidentesController> logger,

            IResidentesAplService residentesAplService
        ) : ControllerBase
    {
        private readonly IResidentesAplService _residentesAplService = residentesAplService;
        private readonly ILogger<ResidentesController> _logger = logger;



        [HttpGet]
        public async Task<ActionResult<ICollection<ResidentesDto>>> GetAll()
        {
            var residentes = await _residentesAplService.ObtemTodosResidentes();
            return Ok(residentes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResidentesDto>> GetOne(int id)
        {
            var residente = await _residentesAplService.ObtemUmResidente(id);
            return Ok(residente);
        }

        [HttpPost]
        public async Task<ActionResult<ResidentesDto>> Create([FromBody] ResidentesDto residente)
        {
            var res = await _residentesAplService.CriaResidente(residente);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResidentesDto>> Update(int id, [FromBody] ResidentesDto residente)
        {
            if (id != residente.Id)
                return BadRequest("ID não corresponde");

            var res = await _residentesAplService.AtualizaResidente(residente);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var sucesso = await _residentesAplService.DeletaResidente(id);

            if (!sucesso)
                return NotFound($"Residente com ID {id} não encontrado");

            return NoContent();
        }
    }
}
