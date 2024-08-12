using ContrRendaFixa.Models;
using System.ComponentModel.DataAnnotations;

namespace ContrRendaFixa.ViewModel
{
    public class PagamentoPostViewModel
    {
        [Required]
        public int ContratacaoId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do pagamento deve ser positivo e maior que zero.")]
        public decimal Valor { get; set; }

        public DateTime DataPagamento { get; set; } = DateTime.UtcNow;
    }

    public static class MensagensErrosPagamento
    {
        public const string ValorPositivo = "O valor do pagamento deve ser positivo e maior que zero.";
    }
}
