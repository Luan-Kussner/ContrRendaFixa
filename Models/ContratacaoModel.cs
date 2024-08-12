using static ContrRendaFixa.Models.ContratanteModel;
using static ContrRendaFixa.Models.ProdutoModel;

namespace ContrRendaFixa.Models
{
    public class ContratacaoModel
    {
        public int Id { get; set; }
        public int ContratanteId { get; set; }
        public ContratanteModel Contratante { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
        public DateTime DataContratacao { get; set; }
        public bool Pago { get; set; }
    }
}
