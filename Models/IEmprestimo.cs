using Microsoft.Net.Http.Headers;
public class IEmprestimo : MaterialBiblioteca, Livro
{
    public int QuantDias{get;set;}
    public override double CalcularPrazo()
    {
        if (QuantDias >= 14)
        {
            Console.WriteLine("Prazo em dia!");
        }
        else
        {
            Console.WriteLine("FORA DO PRAZO DE ENTREGA!!!");
        }
    }

    
}

