
using Microsoft.AspNetCore.Mvc;
using MinhaBiblioteca.Models;
using MinhaBiblioteca.Repositories;

namespace MinhaBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriaisController : ControllerBase
{
    private readonly IMaterialRepository _repository;

    public MateriaisController(IMaterialRepository repository)
    {
        _repository = repository;
    }

    // GET: api/Materiais
    [HttpGet]
    public IActionResult ObterTodos()
    {
        return Ok(_repository.ObterTodos());
    }

    // GET: api/Materiais/5
    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var material = _repository.ObterPorId(id);

        if (material == null)
            return NotFound("Material não encontrado.");

        return Ok(material);
    }

    // POST: api/Materiais/livro
    [HttpPost("livro")]
    public IActionResult AdicionarLivro([FromBody] Livro livro)
    {
        if (livro == null ||
            string.IsNullOrWhiteSpace(livro.Titulo) ||
            livro.AnoPublicacao < 0 ||
            string.IsNullOrWhiteSpace(livro.Autor) ||
            livro.Paginas <= 0)
        {
            return BadRequest("Dados do livro inválidos.");
        }

        _repository.Adicionar(livro);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = livro.Id },
            livro
        );
    }

    // POST: api/Materiais/revista
    [HttpPost("revista")]
    public IActionResult AdicionarRevista([FromBody] Revista revista)
    {
        if (revista == null ||
            string.IsNullOrWhiteSpace(revista.Titulo) ||
            revista.AnoPublicacao < 0 ||
            revista.Edicao <= 0 ||
            revista.MesPublicacao < 1 ||
            revista.MesPublicacao > 12)
        {
            return BadRequest("Dados da revista inválidos.");
        }

        _repository.Adicionar(revista);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = revista.Id },
            revista
        );
    }

    // PUT: api/Materiais/livro/5
    [HttpPut("livro/{id}")]
    public IActionResult AtualizarLivro(int id, [FromBody] Livro livro)
    {
        if (livro == null ||
            string.IsNullOrWhiteSpace(livro.Titulo) ||
            livro.AnoPublicacao < 0 ||
            string.IsNullOrWhiteSpace(livro.Autor) ||
            livro.Paginas <= 0)
        {
            return BadRequest("Dados do livro inválidos.");
        }

        var materialExistente = _repository.ObterPorId(id);

        if (materialExistente == null)
            return NotFound("Material não encontrado.");

        if (materialExistente is not Livro)
            return BadRequest("O material informado não é um livro.");

        var livroAtualizado = new Livro
        {
            Id = id,
            Titulo = livro.Titulo,
            AnoPublicacao = livro.AnoPublicacao,
            Autor = livro.Autor,
            Paginas = livro.Paginas
        };

        if (!_repository.Atualizar(id, livroAtualizado))
            return NotFound("Não foi possível atualizar o livro.");

        return Ok(livroAtualizado);
    }

    // PUT: api/Materiais/revista/5
    [HttpPut("revista/{id}")]
    public IActionResult AtualizarRevista(int id, [FromBody] Revista revista)
    {
        if (revista == null ||
            string.IsNullOrWhiteSpace(revista.Titulo) ||
            revista.AnoPublicacao < 0 ||
            revista.Edicao <= 0 ||
            revista.MesPublicacao < 1 ||
            revista.MesPublicacao > 12)
        {
            return BadRequest("Dados da revista inválidos.");
        }

        var materialExistente = _repository.ObterPorId(id);

        if (materialExistente == null)
            return NotFound("Material não encontrado.");

        if (materialExistente is not Revista)
            return BadRequest("O material informado não é uma revista.");

        var revistaAtualizada = new Revista
        {
            Id = id,
            Titulo = revista.Titulo,
            AnoPublicacao = revista.AnoPublicacao,
            Edicao = revista.Edicao,
            MesPublicacao = revista.MesPublicacao
        };

        if (!_repository.Atualizar(id, revistaAtualizada))
            return NotFound("Não foi possível atualizar a revista.");

        return Ok(revistaAtualizada);
    }

    // DELETE: api/Materiais/5
    [HttpDelete("{id}")]
    public IActionResult Remover(int id)
    {
        var material = _repository.ObterPorId(id);

        if (material == null)
            return NotFound("Material não encontrado.");

        _repository.Remover(id);

        return Ok("Material removido com sucesso.");
    }

    // GET: api/Materiais/5/descricao
    [HttpGet("{id}/descricao")]
    public IActionResult ObterDescricao(int id)
    {
        var material = _repository.ObterPorId(id);

        if (material == null)
            return NotFound("Material não encontrado.");

        return Ok(new
        {
            Descricao = material.Descricao(),
            PrazoEmprestimo = material.CalcularPrazo()
        });
    }

    // POST: api/Materiais/5/emprestar
    [HttpPost("{id}/emprestar")]
    public IActionResult Emprestar(int id)
    {
        var material = _repository.ObterPorId(id);

        if (material == null)
            return NotFound("Material não encontrado.");

        if (material is not IEmprestimo emprestimo)
            return BadRequest("Este material não permite empréstimo.");

        emprestimo.Emprestar();

        return Ok("Empréstimo realizado com sucesso.");
    }
}
