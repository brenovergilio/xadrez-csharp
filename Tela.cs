using System;
using tabuleiro;
using xadrez;

namespace C__and_.Net
{
    public class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for(int i=0;i<tab.Linhas;i++)
            {
                System.Console.Write(8-i+" ");
                for(int j=0;j<tab.Colunas;j++)
                {
                    if(tab.PecaEspecifica(i,j) == null)
                        System.Console.Write("- ");
                    else
                    {
                        ImprimirPeca(PecaEspecifica(i,j));
                        System.Console.Write(" ");
                    }
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  a b c d e f g h");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();

            char coluna = s[0];
            int linha = int.Parse(s[1]+ "");
            return new PosicaoXadrez(coluna,linha);
        }

        public static void ImprimirPeca(Peca p)
        {
            if(p.Cor == Cor.Branca)
                System.Console.Write(p);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(p);
                Console.ForegroundColor = aux;
            }    
        }
    }
}