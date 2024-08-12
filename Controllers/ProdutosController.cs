using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ContrRendaFixa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoGetViewModel>>> GetProdutos([FromQuery] string nome = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var produtos = await _produtoService.GetProdutosAsync();

            if (!string.IsNullOrEmpty(nome))
            {
                produtos = produtos.Where(p => p.Descricao.Contains(nome)).ToList();
            }

            var totalItems = produtos.Count();
            var paginatedProdutos = produtos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (!paginatedProdutos.Any())
            {
                return NoContent();
            }

            return Ok(new
            {
                totalItems,
                page,
                pageSize,
                items = paginatedProdutos
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoGetViewModel>> GetProduto(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null)
            {
                return NoContent();
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoGetViewModel>> PostProduto([FromBody] ProdutoPostViewModel produtoViewModel)
        {
            try
            {
                var result = await _produtoService.CreateProdutoAsync(produtoViewModel);
                return CreatedAtAction(nameof(GetProduto), new { id = result.Id }, result);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduto(int id, [FromBody] ProdutoPatchViewModel produtoViewModel)
        {
            try
            {
                var result = await _produtoService.UpdateProdutoPatchAsync(id, produtoViewModel);
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

    }
}
