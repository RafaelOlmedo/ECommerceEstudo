using ECommerce.Domain.Entities;

namespace ECommerce.API.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public static implicit operator CategoriaViewModel(Categoria categoria) =>
            new CategoriaViewModel()
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Ativo = categoria.Ativo
            };

        public static IEnumerable<CategoriaViewModel> ConverteListaDeCategoriaEmListaCategoriaViewModel(IEnumerable<Categoria> categorias)
        {
            if(categorias == null)
                return Enumerable.Empty<CategoriaViewModel>();

            return categorias.Select(c => (CategoriaViewModel)c);
        }
    }
}
