namespace Api.Models.Dtos
{
    public class AddProdutoDto
    {
        public string Descricao { get; set; }

        public DateTime DataFabricacao { get; set; }

        public DateTime DataValidade { get; set; }

        public int IdFornecedor { get; set; }
    }
}
