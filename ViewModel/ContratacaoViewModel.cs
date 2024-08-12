using ContrRendaFixa.Models;

namespace ContrRendaFixa.ViewModel
{
    public class ContratacaoViewModel
    {
        public int Id { get; set; }
        public int ContratanteId { get; set; }
        public ContratanteViewModel Contratante { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
        public DateTime DataContratacao { get; set; }
        public bool Pago { get; set; }

        public static ContratacaoViewModel FromModel(ContratacaoModel model)
        {
            return new ContratacaoViewModel
            {
                Id = model.Id,
                ContratanteId = model.ContratanteId,
                Contratante = ContratanteViewModel.FromModel(model.Contratante),
                ProdutoId = model.ProdutoId,
                Produto = ProdutoViewModel.FromModel(model.Produto),
                Quantidade = model.Quantidade,
                PrecoUnitario = model.PrecoUnitario,
                Desconto = model.Desconto,
                DataContratacao = model.DataContratacao,
                Pago = model.Pago
            };
        }
    }
    public class ContratacaoPostViewModel
    {
        public int ContratanteId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
        public DateTime DataContratacao { get; set; }
        public bool Pago { get; set; }
    }

    public static class MensagensErrosContratacao
    {
        public const string SegmentoInvalido = "Produto não é válido para o segmento do contratante.";
        public const string ProdutoBloqueado = "Este Produto está bloqueado.";
        public const string ContratanteBloqueado = "Este Contratante está bloqueado.";
        public const string ForaDeHorario = "Operações podem ser realizadas apenas entre 10:30 e 16:00.";
        public const string ContratacaoNotFound = "Contratação não encontrada.";
        public const string CancelarContratacaoPaga = "Não é possível cancelar uma contratação que já foi paga.";
        public const string DescontoMaior = "O desconto não pode ser maior que o valor total.";
        public const string ValorUnitario = "O valor unitário não pode ser negativo.";
        public const string QuantidadeNegativa = "A quantidade não pode ser negativa.";
        public const string ContratacaoPaga = "Esta contratação já foi paga.";
    }
}
