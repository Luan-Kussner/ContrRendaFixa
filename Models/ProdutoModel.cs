using System.Text.Json.Serialization;
using static ContrRendaFixa.Models.TipoProdutoModel;

namespace ContrRendaFixa.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int TipoId { get; set; }
        [JsonIgnore]
        public TipoProdutoModel? Tipo { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public bool Bloqueado { get; set; }
    }
}
