using Core.Entities;
using Core.Parametros;
using Core.Repositories;
using Core.Services;
using Moq;
using Test.Settings.Seeds;

namespace Test.Services
{
    public class ProdutoServiceTest
    {
        public Mock<IProdutoRepository> _mockProdutoRepository;
        public Mock<IFornecedorRepository> _mockFornecedorRepository;

        public ProdutoService _produtoService;

        [SetUp]
        public void Setup()
        {
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _mockFornecedorRepository = new Mock<IFornecedorRepository>();

            _produtoService = new ProdutoService(_mockProdutoRepository.Object, _mockFornecedorRepository.Object);
        }

        [Test]
        public void DeveObterProdutoPorId()
        {
            _mockProdutoRepository.Setup(n => n.Find(It.IsAny<int>(), n => n.Fornecedor)).Returns(ProdutoSeed.Seeds().FirstOrDefault());

            var result = _produtoService.GetById(It.IsAny<int>());

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(ProdutoSeed.Seeds().FirstOrDefault().Id));
        }



        [Test]
        public void DeveObterProdutosPaginados()
        {
            _mockProdutoRepository.Setup(n => n.FindAllAsQueryable()).Returns(ProdutoSeed.Seeds().AsQueryable);
            var parametros = new ProdutoParametros()
            {
                NumeroPagina = 1,
                TamanhoPagina = 4
            };

            var result = _produtoService.GetPaged(parametros);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Itens.Count, Is.EqualTo(4));
                Assert.That(result.TotalItens, Is.EqualTo(ProdutoSeed.Seeds().Count));
            });
        }

        [Test]
        public void DeveObterProdutosPaginadosFiltradosPorDescricao()
        {
            _mockProdutoRepository.Setup(n => n.FindAllAsQueryable()).Returns(ProdutoSeed.Seeds().AsQueryable);
            var parametros = new ProdutoParametros()
            {
                NumeroPagina = 1,
                TamanhoPagina = 4,
                Descricao = ProdutoSeed.Seeds().FirstOrDefault().Descricao
            };

            var result = _produtoService.GetPaged(parametros);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Itens.Count, Is.EqualTo(1));
                Assert.That(result.TotalItens, Is.EqualTo(1));
                Assert.That(result.Itens.FirstOrDefault().Descricao, Is.EqualTo(ProdutoSeed.Seeds().FirstOrDefault().Descricao));
            });
        }

        [Test]
        public void DeveObterProdutosPaginadosFiltradosPorDataFabricacao()
        {
            _mockProdutoRepository.Setup(n => n.FindAllAsQueryable()).Returns(ProdutoSeed.Seeds().AsQueryable);
            var parametros = new ProdutoParametros()
            {
                NumeroPagina = 1,
                TamanhoPagina = 4,
                DataFabricacao = ProdutoSeed.Seeds().FirstOrDefault().DataFabricacao.AddDays(1)
            };

            var result = _produtoService.GetPaged(parametros);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Itens.Count, Is.EqualTo(0));
                Assert.That(result.TotalItens, Is.EqualTo(0));
            });
        }

        [Test]
        public void DeveObterProdutosPaginadosFiltradosPorDataValidade()
        {
            _mockProdutoRepository.Setup(n => n.FindAllAsQueryable()).Returns(ProdutoSeed.Seeds().AsQueryable);
            var parametros = new ProdutoParametros()
            {
                NumeroPagina = 1,
                TamanhoPagina = 4,
                DataValidade = ProdutoSeed.Seeds().FirstOrDefault().DataValidade
            };

            var result = _produtoService.GetPaged(parametros);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Itens.Count, Is.EqualTo(1));
                Assert.That(result.TotalItens, Is.EqualTo(1));
            });
        }

        [Test]
        public void DeveObterProdutosPaginadosFiltradosPorFornecedor()
        {
            _mockProdutoRepository.Setup(n => n.FindAllAsQueryable()).Returns(ProdutoSeed.Seeds().AsQueryable);
            var parametros = new ProdutoParametros()
            {
                NumeroPagina = 1,
                TamanhoPagina = 1,
                IdFornecedor = 1
            };

            var result = _produtoService.GetPaged(parametros);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Itens.Count, Is.EqualTo(1));
                Assert.That(result.TotalItens, Is.EqualTo(2));
            });
        }

        [Test]
        public void DeveValidarCamposProdutoAoAdicionar()
        {
            _mockFornecedorRepository.Setup(n => n.Find(It.IsAny<int>(), null)).Returns((Fornecedor)null);
            var produto = new Produto();
            produto.Id = 1;

            var result = _produtoService.CanAdd(produto);

            Assert.That(result.Count, Is.EqualTo(7));
        }

        [Test]
        public void DeveValidarCamposProdutoAoAtualizarComProdutoExistente()
        {
            _mockProdutoRepository.Setup(n => n.Find(It.IsAny<int>(), null)).Returns(ProdutoSeed.Seeds().FirstOrDefault());
            var produto = new Produto();

            var result = _produtoService.CanUpdate(produto);

            Assert.That(result.Count, Is.EqualTo(6));
        }

        [Test]
        public void DeveValidarCamposProdutoAoAtualizarComProdutoInexistente()
        {
            _mockProdutoRepository.Setup(n => n.Find(It.IsAny<int>(), null)).Returns((Produto)null);
            var produto = new Produto();

            var result = _produtoService.CanUpdate(produto);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void DeveAdicionarProduto()
        {
            _mockFornecedorRepository.Setup(n => n.Find(It.IsAny<int>(), null)).Returns(FornecedorSeed.Seeds().FirstOrDefault());
            var produto = ProdutoSeed.Seeds().FirstOrDefault();

            _produtoService.Add(produto);

            _mockProdutoRepository.Verify(n => n.Add(It.IsAny<Produto>()), Times.Once());
            _mockProdutoRepository.Verify(n => n.Save(), Times.Once());
        }

        [Test]
        public void DeveAtualizarProduto()
        {
            _mockProdutoRepository.Setup(n => n.Find(It.IsAny<int>(), null)).Returns(ProdutoSeed.Seeds().FirstOrDefault());
            _mockFornecedorRepository.Setup(n => n.Find(It.IsAny<int>(), null)).Returns(FornecedorSeed.Seeds().FirstOrDefault());
            var produto = ProdutoSeed.Seeds().FirstOrDefault();

            _produtoService.Update(produto);

            _mockProdutoRepository.Verify(n => n.Update(It.IsAny<Produto>()), Times.Once());
            _mockProdutoRepository.Verify(n => n.Save(), Times.Once());
        }

        [Test]
        public void DeveRemoverProduto()
        {
            _mockProdutoRepository.Setup(n => n.Find(It.IsAny<int>(), null)).Returns(ProdutoSeed.Seeds().FirstOrDefault());

            _produtoService.Delete(It.IsAny<int>());
            
            _mockProdutoRepository.Verify(n => n.Update(It.Is<Produto>(n => !n.Ativo)), Times.Once());
            _mockProdutoRepository.Verify(n => n.Save(), Times.Once());
        }
    }
}
