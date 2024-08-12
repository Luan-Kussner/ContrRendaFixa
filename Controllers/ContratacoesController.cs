using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ContrRendaFixa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratacoesController : ControllerBase
    {
        private readonly IContratacaoService _contratacaoService;

        public ContratacoesController(IContratacaoService contratacaoService)
        {
            _contratacaoService = contratacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contratacoes = await _contratacaoService.GetContratacoesAsync();
            return Ok(contratacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contratacao = await _contratacaoService.GetContratacaoByIdAsync(id);
            if (contratacao == null)
            {
                return NoContent();
            }
            return Ok(contratacao);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContratacaoPostViewModel contratacaoViewModel)
        {
            try
            {
                var contratacao = await _contratacaoService.CreateContratacaoAsync(contratacaoViewModel);
                return CreatedAtAction(nameof(Get), new { id = contratacao.Id }, contratacao);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sucesso = await _contratacaoService.CancelarContratacaoAsync(id);
                if (!sucesso)
                {
                    return BadRequest("Erro ao cancelar contratação.");
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("limpeza-nao-pagas")]
        public async Task<IActionResult> LimpezaNaoPagas()
        {
            try
            {
                await _contratacaoService.LimpezaContratacaoNaoPaga();
                return StatusCode(201);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desistir(int id)
        {
            try
            {
                var contratacao = await _contratacaoService.DesistirContratacaoAsync(id);
                return Ok(contratacao);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
