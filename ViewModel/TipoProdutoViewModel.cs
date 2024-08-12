using ContrRendaFixa.Models;

namespace ContrRendaFixa.ViewModel
{
    public class TipoProdutoViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public static TipoProdutoViewModel FromModel(TipoProdutoModel model)
        {
            return new TipoProdutoViewModel
            {
                Id = model.Id,
                Descricao = model.Descricao
            };
        }

        public static class MensagensErros
        {
            public const string DescricaoInvalida = "Descrição do Tipo do Produto deve ter entre 5 e 50 caracteres.";
            public const string TipoProdutoExistente = "Tipo de Produto já existente.";
            public const string TipoProdutoNotFound = "Tipo de Produto não encontrado.";
            public const string IdIncompativel = "Registro não encontrado.";
        }
    }
}
