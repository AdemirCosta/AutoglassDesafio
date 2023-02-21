using Core.Entities;
using Core.Repositories;

namespace Data.Repositories
{
    public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AutoglassContext context) : base(context) { }
    }
}
