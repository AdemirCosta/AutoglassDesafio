using Core.Entities;
using Core.Parametros;
using Core.Repositories;
using Core.Services.Interfaces;
using Core.Utils;
using Core.Validators;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;

        public ProdutoService(IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
        }

        public Produto? GetById(int id)
        {
            return _produtoRepository.Find(id, n => n.Fornecedor);
        }

        public ListaPaginada<Produto> GetPaged(ProdutoParametros parametros)
        {
            int totalItens = GetProdutosPorParametro(parametros).Count();

            var listaProdutos = GetProdutosPorParametro(parametros)
                .Skip((parametros.NumeroPagina - 1) * parametros.TamanhoPagina)
                .Take(parametros.TamanhoPagina)
                .ToList();

            return new ListaPaginada<Produto>(listaProdutos, totalItens, parametros.NumeroPagina, parametros.TamanhoPagina);
        }

        private IQueryable<Produto> GetProdutosPorParametro(ProdutoParametros parametros)
        {
            return _produtoRepository.FindAllAsQueryable()
                .Include(n => n.Fornecedor)
                .Where(n => n.Ativo
                    && (parametros.Descricao == null || n.Descricao.ToLower().Contains(parametros.Descricao.ToLower()))
                    && (!parametros.DataFabricacao.HasValue || n.DataFabricacao.Date == parametros.DataFabricacao.Value.Date)
                    && (!parametros.DataValidade.HasValue || n.DataValidade.Date == parametros.DataValidade.Value.Date)
                    && (!parametros.IdFornecedor.HasValue || n.IdFornecedor == parametros.IdFornecedor))
                .OrderBy(n => n.Descricao);
        }

        public IReadOnlyList<string> CanAdd(Produto produto)
        {
            var erros = new List<string>();

            if (produto.Id > 0)
                erros.Add("Id inválido.");

            produto.Fornecedor = _fornecedorRepository.Find(produto.IdFornecedor);

            var validacao = new ProdutoValidator().Validate(produto);
            erros.AddRange(validacao.Errors.Select(n => n.ErrorMessage));

            return erros;
        }

        public Produto? Add(Produto produto)
        {
            if (CanAdd(produto).Any())
                throw new InvalidOperationException();

            produto.Ativo = true;

            var novoProduto = _produtoRepository.Add(produto);
            _produtoRepository.Save();
            return novoProduto;
        }

        public IReadOnlyList<string> CanUpdate(Produto produto)
        {
            var erros = new List<string>();

            if (_produtoRepository.Find(produto.Id) == null)
            {
                erros.Add("Produto inexistente.");
                return erros;
            }

            produto.Fornecedor = _fornecedorRepository.Find(produto.IdFornecedor);

            var validacao = new ProdutoValidator().Validate(produto);
            erros.AddRange(validacao.Errors.Select(n => n.ErrorMessage));

            return erros;
        }

        public Produto? Update(Produto produto)
        {
            if (CanUpdate(produto).Any())
                throw new InvalidOperationException();

            var produtoExistente = _produtoRepository.Find(produto.Id);
            produto.Ativo = produtoExistente.Ativo;

            var produtoAtualizado = _produtoRepository.Update(produto);
            _produtoRepository.Save();
            return produtoAtualizado;
        }

        public void Delete(int id)
        {
            var produto = _produtoRepository.Find(id);
            if (produto != null)
            {
                produto.Ativo = false;
                _produtoRepository.Update(produto);
                _produtoRepository.Save();
            }
        }
    }
}
