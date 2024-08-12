using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ContrRendaFixa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposProdutosController : ControllerBase
    {
        private readonly ITipoProdutoService _tipoProdutoService;

        public TiposProdutosController(ITipoProdutoService tipoProdutoService)
        {
            _tipoProdutoService = tipoProdutoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProdutoViewModel>>> GetTiposProdutos()
        {
            var tiposProdutos = await _tipoProdutoService.GetTiposProdutosAsync();
            if (tiposProdutos == null || !tiposProdutos.Any())
            {
                return NoContent();
            }

            return Ok(tiposProdutos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProdutoViewModel>> GetTipoProduto(int id)
        {
            var tipoProduto = await _tipoProdutoService.GetTipoProdutoByIdAsync(id);
            if (tipoProduto == null)
            {
                return NotFound(new { error = new { code = "NOT_FOUND", message = TipoProdutoViewModel.MensagensErros.TipoProdutoNotFound } });
            }

            return Ok(tipoProduto);
        }

        [HttpPost]
        public async Task<ActionResult<TipoProdutoViewModel>> PostTipoProduto([FromBody] TipoProdutoViewModel tipoProdutoViewModel)
        {
            try
            {
                var result = await _tipoProdutoService.CreateTipoProdutoAsync(tipoProdutoViewModel);
                return CreatedAtAction(nameof(GetTipoProduto), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return UnprocessableEntity(new { error = new { code = "INVALID_NAME", message = ex.Message } });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = new { code = "CONFLICT", message = ex.Message } });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoProduto(int id, [FromBody] TipoProdutoViewModel tipoProdutoViewModel)
        {
            try
            {
                if (id != tipoProdutoViewModel.Id)
                {
                    return BadRequest(new { error = new { code = "BAD_REQUEST", message = TipoProdutoViewModel.MensagensErros.IdIncompativel } });
                }

                var result = await _tipoProdutoService.UpdateTipoProdutoAsync(id, tipoProdutoViewModel);
                if (!result)
                {
                    return NotFound(new { error = new { code = "NOT_FOUND", message = TipoProdutoViewModel.MensagensErros.TipoProdutoNotFound } });
                }

                return Ok("Tipo Produto alterado com Sucesso!");
            }
            catch (ArgumentException ex)
            {
                return UnprocessableEntity(new { error = new { code = "INVALID_NAME", message = ex.Message } });
            }
        }
    }
}
