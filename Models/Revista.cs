using Microsoft.Net.Http.Headers;
public class Revista : MaterialBiblioteca
{
    public int Edcao {get;init;}
    public int Id{get;init;}
    public int _mespubli;
    public int MesPublicacao
    {
        get
        {
            return _mespubli;
        }
        set
        {
            if(value <=1 && value >= 12)
            {
                Console.Write("MÊS VALIDO");
            }
            else
            {
                Console.WriteLine("ERRO! Mês invalido");
            }
        }
    }

    public int QuantDias{get;set;}
    public override int CalcularPrazo()
    {
       
        {
              if(_quantdias <= 7)
               {
                    
                   Console.WriteLine("Prazo em dia!");
                }
              else
                {
                    Console.WriteLine("FORA DO PRAZO DE ENTREGA!!!");
                }
        }   
    }

    public override string Descricao()
        {
            return $"REVISTA ====> ID: {Id} | Título: {Titulo} | Edição: {Edicao} | Publicação: {MesPublicacao} | Prazo: {CalcularPrazo()} dias";
        }

}