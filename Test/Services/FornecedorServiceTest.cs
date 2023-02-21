using Core.Repositories;
using Core.Services;
using Moq;
using Test.Settings.Seeds;

namespace Test.Services
{
    public class FornecedorServiceTest
    {
        public Mock<IFornecedorRepository> _mockFornecedorRepository;

        public FornecedorService _fornecedorService;

        [SetUp]
        public void Setup()
        {
            _mockFornecedorRepository = new Mock<IFornecedorRepository>();

            _fornecedorService = new FornecedorService(_mockFornecedorRepository.Object);
        }

        [Test]
        public void DeveObterTodosOsFornecedores()
        {
            _mockFornecedorRepository.Setup(f => f.FindAll()).Returns(FornecedorSeed.Seeds());

            var result = _fornecedorService.GetAll();

            Assert.That(result.Count, Is.EqualTo(FornecedorSeed.Seeds().Count));
        }

    }
}
