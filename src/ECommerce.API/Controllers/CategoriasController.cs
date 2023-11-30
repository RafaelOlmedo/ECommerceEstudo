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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogger<CategoriasController> _logger;

        private const string PrefixoLog = "[Categorias] - ";

        public CategoriasController(ICategoriaRepository categoriaRepository,
                                    ICategoriaService categoriaService,
                                    ILogger<CategoriasController> logger)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var todasCategorias = _categoriaRepository.RecuperaTodos();

                if (!todasCategorias.Any())
                    return NotFound();

                var categoriasViewModel = CategoriaViewModel.ConverteListaDeCategoriaEmListaCategoriaViewModel(todasCategorias);

                return Ok(categoriasViewModel);
            }
            catch (Exception ex)
            {
                string mensagemErro = $"Ocorreu um erro interno ao realizar o 'Get'. Retorno: {ex.Message}.";
                GravaLogTextoParaErro(mensagemErro);
                return StatusCode(500, mensagemErro);
            }
        }

        [HttpPost]
        public IActionResult Post(CategoriaInputModel categoriaInputModel)
        {
            try
            {
                var categoria = _categoriaService.Adiciona(categoriaInputModel);

                if (categoria.Invalido)
                    return BadRequest(new { sucesso = false, error = categoria.Notifications });

                return CreatedAtAction(nameof(Get), new { id = categoria.Id }, (CategoriaViewModel)categoria);
            }
            catch (Exception ex)
            {
                string mensagemErro = $"Ocorreu um erro interno ao realizar o 'Post'. Retorno: {ex.Message}.";
                GravaLogTextoParaErro(mensagemErro);
                return StatusCode(500, mensagemErro);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, CategoriaInputModel categoriaInputModel)
        {
            try
            {
                var categoriaAtualizar = (Categoria)categoriaInputModel;
                categoriaAtualizar.AtribuiId(id);

                var categoria = _categoriaService.Atualiza(categoriaAtualizar);

                if (categoria.Invalido)
                    return BadRequest(new { sucesso = false, error = categoria.Notifications });

                return NoContent();
            }
            catch (Exception ex)
            {
                string mensagemErro = $"Ocorreu um erro interno ao realizar o 'Put'. Retorno: {ex.Message}.";
                GravaLogTextoParaErro(mensagemErro);
                return StatusCode(500, mensagemErro);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                (string mensagemErro, bool sucesso) = _categoriaService.Remove(id);

                if (!sucesso)
                    return BadRequest(new { sucesso = false, error = mensagemErro });

                return NoContent();
            }
            catch (Exception ex)
            {
                string mensagemErro = $"Ocorreu um erro interno ao realizar o 'Delete'. Retorno: {ex.Message}.";
                GravaLogTextoParaErro(mensagemErro);
                return StatusCode(500, mensagemErro);
            }
        }

        private void GravaLogTextoParaErro(string mensagem) =>
                _logger.LogError($"{PrefixoLog}{mensagem}");
    }
}
