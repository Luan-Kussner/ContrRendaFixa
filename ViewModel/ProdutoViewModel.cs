using ContrRendaFixa.Models;

namespace ContrRendaFixa.ViewModel
{
    public class ProdutoGetViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoProdutoModel Tipo { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public bool Bloqueado { get; set; }
    }

    public class ProdutoPostViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int TipoId { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public bool Bloqueado { get; set; }
    }

    public class ProdutoPatchViewModel
    {
        public string Descricao { get; set; }
        public int TipoId { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public bool Bloqueado { get; set; }
    }

    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int TipoId { get; set; }
        public TipoProdutoViewModel Tipo { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public bool Bloqueado { get; set; }

        public static ProdutoViewModel FromModel(ProdutoModel model)
        {
            return new ProdutoViewModel
            {
                Id = model.Id,
                Descricao = model.Descricao,
                TipoId = model.TipoId,
                Tipo = model.Tipo != null ? TipoProdutoViewModel.FromModel(model.Tipo) : null,
                Preco = model.Preco,
                Quantidade = model.Quantidade,
                Bloqueado = model.Bloqueado
            };
        }
    }

    public static class MensagensErros
    {
        public const string DescricaoInvalida = "Descrição do Produto deve ter entre 20 e 50 caracteres.";
        public const string ProdutoExistente = "Produto já existente.";
        public const string ProdutoNotFound = "Produto não encontrado.";
        public const string PatchInvalido= "O documento de patch não pode ser vazio.";
        public const string ConflitoAtualizacao = "Conflito de atualização.";
    }
}
