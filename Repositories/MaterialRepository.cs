using System.Collections.Generic;

namespace MinhaBiblioteca.Repositories;
public class MaterialRepository : IMaterialRepository
{
    private readonly Dictionary<int, MaterialBiblioteca> _materiais = new()
    { 
        {1, new Livro {Id = 1, Titulo = "Harry Potter e a Pedra Filosofal"}},
        {2, new Revista {Id = 2, Titulo = "Codgue"}}
    };

    private int _proximoId = 2;
    public IEnumerable<MaterialBiblioteca> ObterTodos()
    {
        return _materiais.Values;
    }

    public MaterialBiblioteca? ObterPorId(int id)
    {
        if (!_materiais.ContainsKey(id))
        {
            return null;
        }

        return _materiais[id];
    }

    public MaterialBiblioteca Adicionar(MaterialBiblioteca material)
    {
        material.Id = _proximoId;
        _proximoId++;

        _materiais.Add(material.Id, material);

        return material;
    }

    public bool Atualizar(int id, MaterialBiblioteca material)
    {
        if (!_materiais.ContainsKey(id))
        {
            return false;
        }

        if (material.Id != id)
        {
            return false;
        }

        _materiais[id] = material;

        return true;
    }

    public bool Remover(int id)
    {
        return _materiais.Remove(id);
    }
}
