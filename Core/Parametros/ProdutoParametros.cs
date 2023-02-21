namespace Core.Parametros
{
    public class ProdutoParametros : PaginacaoParametros
    {
        public string? Descricao { get; set; }
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public int? IdFornecedor { get; set; }
    }
}
