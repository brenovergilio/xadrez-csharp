using System;
using tabuleiro;

namespace C__and_.Net
{
    public class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for(int i=0;i<tab.Linhas;i++)
            {
                for(int j=0;j<tab.Colunas;j++)
                {
                    if(tab.PecaEspecifica(i,j) == null)
                        System.Console.Write("- ");
                    else
                        System.Console.Write(tab.PecaEspecifica(i,j) + " ");
                }
                System.Console.WriteLine();
            }
        }
    }
}