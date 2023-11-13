using ECommerce.Domain.Entities;

namespace ECommerce.API.InputModels
{
    public class CategoriaInputModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public static implicit operator Categoria(CategoriaInputModel model) =>
            new (nome: model.Nome, descricao: model.Descricao );
    }
}
