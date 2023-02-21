using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Produto : BaseEntity
    {
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataFabricacao { get; set; }

        public DateTime DataValidade { get; set; }

        [ForeignKey("Fornecedor")]
        public int IdFornecedor { get; set; }

        public Fornecedor? Fornecedor { get; set; }
    }
}
