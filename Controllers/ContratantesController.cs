using ContrRendaFixa.Models;
using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ContrRendaFixa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratantesController : ControllerBase
    {
        private readonly IContratanteService _contratanteService;

        public ContratantesController(IContratanteService contratanteService)
        {
            _contratanteService = contratanteService;
        }

        [HttpPost]
        public async Task<ActionResult<ContratanteModel>> PostContratante([FromBody] ContratantePostViewModel contratante)
        {
            try
            {
                var createdContratante = await _contratanteService.CreateContratanteAsync(contratante);
                return CreatedAtAction(nameof(GetContratanteById), new { id = createdContratante.Id }, createdContratante);
            }
            catch (ArgumentException ex)
            {
                return UnprocessableEntity(new { error = new { code = "INVALID_DATA", message = ex.Message } });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchContratante(int id, [FromBody] ContratantePatchViewModel contratantePath)
        {
            try
            {
                var result = await _contratanteService.UpdateContratanteAsync(id, contratantePath);
                if (!result)
                {
                    return Conflict(new { error = new { code = "CONFLICT", message = MensagensErrosViewModel.ConflitoAtualizacao } });
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = new { code = "INVALID_PATCH", message = ex.Message } });
            }
            catch (KeyNotFoundException ex)
            {
                return UnprocessableEntity(new { error = new { code = "NOT_FOUND", message = ex.Message } });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContratanteGetViewModel>> GetContratanteById(int id)
        {
            var contratante = await _contratanteService.GetContratanteByIdAsync(id);
            if (contratante == null)
            {
                return NoContent();
            }

            return Ok(contratante);
        }
    }
}
