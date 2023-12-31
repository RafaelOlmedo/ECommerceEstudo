﻿using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Infra.Data.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infra.Data.EntityFramework.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ECommerceDataContext _context;
        private readonly DbSet<Produto> _dbSetProdutos;

        public ProdutoRepository(ECommerceDataContext dataContext)
        {
            _context = dataContext;
            _dbSetProdutos = _context.Set<Produto>();
        }

        public IEnumerable<Produto> RecuperaTodos()
        {
            return _dbSetProdutos.Include(c => c.Categoria).AsNoTracking();
        }

        public Produto Adiciona(Produto produto)
        {
            produto.AdicionaDataDeCriacao();

            _dbSetProdutos.Add(produto);
            _context.SaveChanges();

            return produto;
        }

        public Produto RecuperaPeloId(Guid id)
        {
            return _dbSetProdutos.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public Produto Atualiza(Produto produto)
        {
            _dbSetProdutos.Update(produto);
            _context.SaveChanges();

            return produto;
        }

        public void Remove(Produto produto)
        {
            _dbSetProdutos.Remove(produto);
            _context.SaveChanges();
        }
    }
}
