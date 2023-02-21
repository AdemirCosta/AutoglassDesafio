using Core.Entities;
using Core.Repositories;

namespace Data.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AutoglassContext context) : base(context) { }
    }
}
