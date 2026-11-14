namespace MinhaBiblioteca.Models;

public class Revista : MaterialBiblioteca
{
    public int Edicao { get; set; }

    private int _mespubli;

    public int MesPublicacao
    {
        get { return _mespubli; }
        set
        {
            _mespubli = value;
        }
    }

    public override int CalcularPrazo()
    {
        return 7;
    }

    public override string Descricao()
    {
        return $"REVISTA ====> ID: {Id} | Título: {Titulo} | Edição: {Edicao} | Publicação: {MesPublicacao} | Prazo: {CalcularPrazo()} dias";
    }
}