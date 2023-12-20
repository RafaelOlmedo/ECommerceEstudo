using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Domain.Services;
using Moq;

namespace ECommerce.Domain.Tests
{
    [TestClass]
    public class ProdutoServiceTests
    {
        private Mock<IProdutoRepository> _produtoRepositoryMock;
        private Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private ProdutoService _produtoService;

        [TestInitialize]
        public void Setup()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object, _categoriaRepositoryMock.Object);
        }

        [TestMethod]
        public void Adiciona_DeveRetornarProdutosSeCategoriaNaoExiste()
        {
            // Arrange
            var produto = new Produto("Produto teste", "Descrição teste", 15.6m, Guid.NewGuid());
            _categoriaRepositoryMock.Setup(repo => repo.RecuperaPeloId(It.IsAny<Guid>())).Returns((Categoria)null);

            // Act
            var resultado = _produtoService.Adiciona(produto);

            // Assert
            Assert.AreSame(produto, resultado);
            _categoriaRepositoryMock.Verify(repo => repo.RecuperaPeloId(It.IsAny<Guid>()), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.Adiciona(It.IsAny<Produto>()), Times.Never);
        }

        [TestMethod]
        public void Adiciona_DeveChamarValidacoesEAdicionarAoRepositorioSeCategoriaExiste()
        {
            // Arrange
            var produto = new Produto("Produto Teste", "Descrição Teste", 19.99m, Guid.NewGuid());
            _categoriaRepositoryMock.Setup(repo => repo.RecuperaPeloId(It.IsAny<Guid>())).Returns(new Categoria("NomeCategoria", "DescriçãoCategoria"));
            _produtoRepositoryMock.Setup(repo => repo.Adiciona(It.IsAny<Produto>())).Returns(produto);

            // Act
            var resultado = _produtoService.Adiciona(produto);

            // Assert
            Assert.AreSame(produto, resultado);
            _produtoRepositoryMock.Verify(repo => repo.Adiciona(It.IsAny<Produto>()), Times.Once);
            Assert.AreEqual(0, produto.Notifications.Count());
        }
    }
}
