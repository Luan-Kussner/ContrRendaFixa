using ContrRendaFixa.Models;
using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ContrRendaFixa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly IPagamentoService _pagamentoService;

        public PagamentosController(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        [HttpPost]
        public async Task<ActionResult<PagamentoModel>> PostPagamento([FromBody] PagamentoPostViewModel pagamentoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdPagamento = await _pagamentoService.CreatePagamentoAsync(pagamentoViewModel);
                return CreatedAtAction(nameof(GetPagamento), new { id = createdPagamento.Id }, createdPagamento);
            }
            catch (ArgumentException ex)
            {
                return UnprocessableEntity(new { error = new { code = "INVALID_DATA", message = ex.Message } });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PagamentoModel>> GetPagamento(int id)
        {
            var pagamento = await _pagamentoService.GetPagamentoByIdAsync(id);
            if (pagamento == null)
            {
                return NoContent();
            }
            return Ok(pagamento);
        }
    }
}
