namespace MinhaBiblioteca.Models;

public abstract class MaterialBiblioteca
{
    public int Id { get; set; }

    private string _titulo = string.Empty;

    public string Titulo
    {
        get { return _titulo; }
        set
        {
            _titulo = value ?? string.Empty;
        }
    }

    private int _anoPublicacao;

    public int AnoPublicacao
    {
        get { return _anoPublicacao; }
        set
        {
            _anoPublicacao = value;
        }
    }

    public abstract int CalcularPrazo();

    public virtual string Descricao()
    {
        return $"Título: {Titulo}, Ano de Publicação: {AnoPublicacao}";
    }
}