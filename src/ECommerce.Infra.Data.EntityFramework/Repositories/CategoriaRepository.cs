using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Infra.Data.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infra.Data.EntityFramework.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ECommerceDataContext _context;
        private readonly DbSet<Categoria> _dbSetCategoria;

        public CategoriaRepository(ECommerceDataContext dataContext)
        {
            _context = dataContext;
            _dbSetCategoria = _context.Set<Categoria>();
        }

        public IEnumerable<Categoria> RecuperaTodos()
        {
            return _dbSetCategoria.AsNoTracking();
        }
        public Categoria Adiciona(Categoria categoria)
        {
            categoria.AdicionaDataCriacao();

            _dbSetCategoria.Add(categoria);
            _context.SaveChanges();

            return categoria;
        }

        public Categoria RecuperaPeloId(Guid id)
        {
            return _dbSetCategoria.Find(id);
        }
    }
}
