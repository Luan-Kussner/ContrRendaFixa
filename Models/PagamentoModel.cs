using static ContrRendaFixa.Models.ContratacaoModel;

namespace ContrRendaFixa.Models
{
    public class PagamentoModel
    {
        public int Id { get; set; }
        public int ContratacaoId { get; set; }
        public ContratacaoModel Contratacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
