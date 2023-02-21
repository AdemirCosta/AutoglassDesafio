namespace Core.Entities
{
    public class Fornecedor : BaseEntity
    {
        public string Descricao { get; set; }

        public long Cnpj { get; set; }

        public Fornecedor(string Descricao, long Cnpj)
        {
            this.Descricao = Descricao;
            this.Cnpj = Cnpj;
        }
    }
}
