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

        public CategoriasController(ICategoriaRepository categoriaRepository, ICategoriaService categoriaService)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
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
                return StatusCode(500, $"Ocorreu um erro interno ao realizar o 'Get'. Retorno: {ex.Message}.");
            }
        }

        [HttpPost]
        public IActionResult Post(CategoriaInputModel categoriaInputModel)
        {
            try
            {
                var categoria = _categoriaService.Adiciona(categoriaInputModel);

                if (categoria.Invalid)
                    return BadRequest(new { sucesso = false, error = categoria.Notifications });

                return CreatedAtAction(nameof(Get), new { id = categoria.Id }, (CategoriaViewModel)categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno ao realizar o 'Post'. Retorno: {ex.Message}.");
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

                if (categoria.Invalid)
                    return BadRequest(new { sucesso = false, error = categoria.Notifications });

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
                (string mensagemErro, bool sucesso) = _categoriaService.Remove(id);

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
