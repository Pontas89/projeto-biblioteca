namespace MinhaBiblioteca.Models;

public class Livro : MaterialBiblioteca, IEmprestimo
{
    private string _autor = string.Empty;
    private int _paginas;

    public int Paginas
    {
        get { return _paginas; }
        set
        {
            _paginas = value;
        }
    }

    public string Autor
    {
        get { return _autor; }
        set
        {
            _autor = value;
        }
    }

    public override int CalcularPrazo()
    {
        return 14;
    }

    public override string Descricao()
    {
        return $"Título: {Titulo}, Autor: {Autor}, Ano de Publicação: {AnoPublicacao}, Páginas: {Paginas}";
    }

    public void Emprestar()
    {
        Console.WriteLine($"Empréstimo do livro '{Titulo}' realizado. Prazo: {CalcularPrazo()} dias.");
    }
}