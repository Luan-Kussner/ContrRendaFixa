using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ContrRendaFixa.Models
{
    public class ContratanteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public SegmentoEnum Segmento { get; set; }
        public bool Bloqueado { get; set; }
    }

    public enum SegmentoEnum
    {
        [EnumMember(Value = "V")]
        Varejo = 'V',
        [EnumMember(Value = "A")]
        Atacado = 'A',
        [EnumMember(Value = "E")]
        Especial = 'E'
    }

}