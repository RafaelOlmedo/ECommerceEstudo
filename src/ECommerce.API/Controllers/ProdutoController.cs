using ECommerce.API.InputModels;
using ECommerce.API.ViewModels;
using ECommerce.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
           _produtoRepository = produtoRepository;
        }

        /// <summary>
        /// Obtem informações dos produtos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var todosProdutos = _produtoRepository.RecuperaTodos();

            if (todosProdutos.Count() == 0)
                return NoContent();

            List<ProdutoViewModel> produtosViewModel = new List<ProdutoViewModel>();

            foreach (var produto in todosProdutos)
                produtosViewModel.Add(produto);

            return Ok(produtosViewModel);
        }

        [HttpPost]
        public IActionResult Post(ProdutoInputModel produtoInputModel) 
        {
           var produto = _produtoRepository.Adiciona(produtoInputModel);

            return Ok((ProdutoViewModel)produto);
        }
    }
}
