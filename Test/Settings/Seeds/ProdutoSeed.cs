using Core.Entities;

namespace Test.Settings.Seeds
{
    public static class ProdutoSeed
    {
        public static List<Produto> Seeds()
        {
            return new List<Produto>
            {
                new Produto
                {
                    Descricao = "Biscoito Recheado de Chocolate",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddMonths(8),
                    Ativo = true,
                    IdFornecedor = 1
                },
                new Produto
                {
                    Descricao = "Cream Cracker",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddMonths(6),
                    Ativo = true,
                    IdFornecedor = 2
                },
                new Produto
                {
                    Descricao = "Biscoito Recheado de Morango",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddMonths(9),
                    Ativo = true,
                    IdFornecedor = 3
                },
                new Produto
                {
                    Descricao = "Biscoito Waffer de Chocolate",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddMonths(3),
                    Ativo = true,
                    IdFornecedor = 4
                },
                new Produto
                {
                    Descricao = "Prestígio",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddMonths(4),
                    Ativo = true,
                    IdFornecedor = 5
                },
                new Produto
                {
                    Descricao = "Baunilha",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddMonths(2),
                    Ativo = true,
                    IdFornecedor = 1
                },
                new Produto
                {
                    Descricao = "Biscoito Água e Sal",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddMonths(18),
                    Ativo = true,
                    IdFornecedor = 2
                }
            };
        }
    }
}
