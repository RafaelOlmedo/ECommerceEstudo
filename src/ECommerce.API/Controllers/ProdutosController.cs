using ECommerce.API.InputModels;
using ECommerce.API.ViewModels;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
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
            try
            {
                var todosProdutos = _produtoRepository.RecuperaTodos();

                if (!todosProdutos.Any())
                    return NotFound();

                var produtosViewModel = ProdutoViewModel.ConverteListaDeProdutoEmListaProdutoViewModel(todosProdutos);

                return Ok(produtosViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno ao realizar o 'Get'. Retorno: {ex.Message}.");
            }            
        }

        [HttpPost]
        public IActionResult Post(ProdutoInputModel produtoInputModel) 
        {
            try
            {
                var produto = _produtoRepository.Adiciona(produtoInputModel);

                return CreatedAtAction(nameof(Get), new { id = produto.Id }, (ProdutoViewModel)produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno ao realizar o 'Post'. Retorno: {ex.Message}.");
            }
       
        }
    }
}
