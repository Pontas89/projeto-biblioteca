using MinhaBiblioteca.Models;
using System.Collections.Generic;
using System.Linq;

namespace MinhaBiblioteca.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly Dictionary<int, MaterialBiblioteca> _materiais = new()
    {
        {
            1,
            new Livro
            {
                Id = 1,
                Titulo = "Harry Potter e a Pedra Filosofal",
                Autor = "J. K. Rowling",
                Paginas = 264,
                AnoPublicacao = 1997
            }
        },
        {
            2,
            new Revista
            {
                Id = 2,
                Titulo = "Código",
                Edicao = 1,
                MesPublicacao = 11,
                AnoPublicacao = 2026
            }
        }
    };

    public IEnumerable<MaterialBiblioteca> ObterTodos()
    {
        return _materiais.Values;
    }

    public MaterialBiblioteca? ObterPorId(int id)
    {
        if (!_materiais.ContainsKey(id))
            return null;

        return _materiais[id];
    }

    public MaterialBiblioteca Adicionar(MaterialBiblioteca material)
    {
        int novoId = GerarNovoId();

        material.Id = novoId;

        _materiais.Add(novoId, material);

        return material;
    }

    public bool Atualizar(int id, MaterialBiblioteca material)
    {
        if (!_materiais.ContainsKey(id))
            return false;

        material.Id = id;

        _materiais[id] = material;

        return true;
    }

    public bool Remover(int id)
    {
        return _materiais.Remove(id);
    }

    private int GerarNovoId()
    {
        if (_materiais.Count == 0)
            return 1;

        return _materiais.Keys.Max() + 1;
    }
}