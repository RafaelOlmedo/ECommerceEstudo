using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Tests
{
    [TestClass]
    public class ProdutoTests
    {
        const string NomeProduto = "Nome produto",
                     DescricaoProduto = "Descrição produto";

        const decimal PrecoProduto = 15.45m;

        private Guid GeraGuidTime() =>
            Guid.NewGuid();

        private Produto RecuperaProduto(Guid idCategoria) =>
            new Produto(nome: NomeProduto, descricao: DescricaoProduto, preco: PrecoProduto, idCategoria: idCategoria);

        [TestMethod]
        public void Produto_DeveSerCriadoComPropriedadesValidas()
        {
            // Arrange            
            Guid idCategoria = GeraGuidTime();

            // Act
            var produto = RecuperaProduto(idCategoria);

            // Assert
            Assert.IsNotNull(produto);
            Assert.AreEqual(produto.Nome, NomeProduto);
            Assert.AreEqual(produto.Descricao, DescricaoProduto);
            Assert.AreEqual(produto.Preco, PrecoProduto);
            Assert.AreEqual(produto.IdCategoria, idCategoria);
            Assert.AreEqual(0, produto.Notifications.Count);            
        }

        [TestMethod]
        public void Produto_ValidaPreenchimentoDataCriacao()
        {
            // Arrange
            var produto = RecuperaProduto(GeraGuidTime());

            // Act
            produto.AdicionaDataDeCriacao();

            // Arrange
            Assert.AreNotEqual(produto.DataCriacao, DateTime.MinValue);            
        }
    }
}
