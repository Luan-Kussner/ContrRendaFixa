using ContrRendaFixa.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContrRendaFixa.ViewModel
{
    public class ContratantePostViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        [Required]
        [AllowedValues("V", "A", "E")]
        public string Segmento { get; set; }

        [JsonIgnore]
        public SegmentoEnum SegmentoEnumValue
        {
            get
            {
                if (Segmento.Length == 1 && Enum.IsDefined(typeof(SegmentoEnum), (SegmentoEnum)Segmento[0]))
                {
                    return (SegmentoEnum)Segmento[0];
                }
                throw new ArgumentException($"Valor inválido para Segmento: {Segmento}");
            }
        }
    }
    public class ContratanteGetViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Segmento { get; set; }
        public bool Bloqueado { get; set; }

        public ContratanteGetViewModel(ContratanteModel contratante)
        {
            Id = contratante.Id;
            Nome = contratante.Nome;
            Sobrenome = contratante.Sobrenome;
            Segmento = contratante.Segmento.ToString();
            Bloqueado = contratante.Bloqueado;
        }
    }

    public class ContratanteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Segmento { get; set; }
        public bool Bloqueado { get; set; }

        public static ContratanteViewModel FromModel(ContratanteModel model)
        {
            return new ContratanteViewModel
            {
                Id = model.Id,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Segmento = model.Segmento.ToString(),
                Bloqueado = model.Bloqueado
            };
        }
    }

    public class ContratantePatchViewModel
    {
        public bool Bloqueado { get; set; }
    }

    public static class MensagensErrosViewModel
    {
        public const string NomeInvalido = "Nome do contratante deve ter entre 10 e 150 caracteres.";
        public const string SobrenomeInvalido = "Sobrenome do contratante deve ter entre 10 e 250 caracteres.";
        public const string SegmentoInvalido = "Segmento do contratante deve ser 'V', 'A' ou 'E'.";
        public const string ContratanteExistente = "Já existe um contratante com o mesmo nome.";
        public const string ContratanteNotFound = "Contratante não encontrado.";
        public const string PatchErro = "Erro ao aplicar o patch.";
        public const string PatchSegmentoInvalido = "Segmento do contratante deve ser 'V', 'A' ou 'E'.";
        public const string ConflitoAtualizacao = "Conflito de atualização.";
    }
}
