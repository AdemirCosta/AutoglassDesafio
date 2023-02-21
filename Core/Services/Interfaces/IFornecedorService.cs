using Core.Entities;

namespace Core.Services.Interfaces
{
    public interface IFornecedorService
    {
        IList<Fornecedor> GetAll();
    }
}
