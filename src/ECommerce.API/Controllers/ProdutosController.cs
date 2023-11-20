using ECommerce.API.InputModels;
using ECommerce.API.ViewModels;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoRepository produtoRepository,
                                  IProdutoService produtoService)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
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
                var produto = _produtoService.Adiciona(produtoInputModel);

                if (produto.Invalid)
                    return BadRequest(new { sucesso = false, error = produto.Notifications });

                return CreatedAtAction(nameof(Get), new { id = produto.Id }, (ProdutoViewModel)produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno ao realizar o 'Post'. Retorno: {ex.Message}.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, ProdutoInputModel produtoInputModel)
        {
            try
            {
                var produtoAtualizar = (Produto)produtoInputModel;
                produtoAtualizar.AtribuiId(id);

                var produto = _produtoService.Atualiza(produtoAtualizar);

                if (produto.Invalid)
                    return BadRequest(new { sucesso = false, error = produto.Notifications });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno ao realizar o 'Put'. Retorno: {ex.Message}.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                (string mensagemErro, bool sucesso) = _produtoService.Remove(id);

                if (!sucesso)
                    return BadRequest(new { sucesso = false, error = mensagemErro });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno ao realizar o 'Delete'. Retorno: {ex.Message}.");
            }
        }
    }
}
