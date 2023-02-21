using Core.Entities;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Core.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public IList<Fornecedor> GetAll()
        {
            return _fornecedorRepository.FindAll();
        }
    }
}
