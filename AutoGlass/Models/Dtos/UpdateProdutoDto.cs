namespace Api.Models.Dtos
{
    public class UpdateProdutoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public DateTime DataFabricacao { get; set; }

        public DateTime DataValidade { get; set; }

        public int IdFornecedor { get; set; }
    }
}
