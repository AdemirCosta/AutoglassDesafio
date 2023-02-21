using Core.Entities;
using Core.Parametros;
using Core.Utils;

namespace Core.Services.Interfaces
{
    public interface IProdutoService
    {
        Produto? GetById(int id);
        ListaPaginada<Produto> GetPaged(ProdutoParametros parametros);
        IReadOnlyList<string> CanAdd(Produto produto);
        Produto? Add(Produto produto);
        IReadOnlyList<string> CanUpdate(Produto produto);
        Produto? Update(Produto produto);
        void Delete(int id);
    }
}
