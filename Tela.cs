using System;
using tabuleiro;
using xadrez;
using System.Collections.Generic;

namespace C__and_.Net
{
    public class Tela
    {

        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);
            System.Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            System.Console.WriteLine();
            System.Console.WriteLine("Turno: " + partida.Turno);
            System.Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
            if(partida.Xeque)
                System.Console.WriteLine("XEQUE!");
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            System.Console.WriteLine("Peças capturadas:");
            System.Console.Write("Brancas: ");
            ImprimirConjuntos(partida.Capturadas(Cor.Branca));
            System.Console.WriteLine();
            System.Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjuntos(partida.Capturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            System.Console.WriteLine();
        }

        public static void ImprimirConjuntos(HashSet<Peca> hs)
        {
            System.Console.Write("[");
            foreach(Peca x in hs)
            {
                System.Console.Write(x + " ");
            }
            System.Console.Write("]");
        } 

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for(int i=0;i<tab.Linhas;i++)
            {
                System.Console.Write(8-i+" ");
                for(int j=0;j<tab.Colunas;j++)
                {
                    ImprimirPeca(tab.PecaEspecifica(i,j));
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool [,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for(int i=0;i<tab.Linhas;i++)
            {
                System.Console.Write(8-i+" ");
                for(int j=0;j<tab.Colunas;j++)
                {
                    if(posicoesPossiveis[i,j])
                        Console.BackgroundColor = fundoAlterado;
                    else
                        Console.BackgroundColor = fundoOriginal;

                    ImprimirPeca(tab.PecaEspecifica(i,j));
                    Console.BackgroundColor = fundoOriginal;
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
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
            if(p == null)
            {
                 System.Console.Write("- ");
            }
            else{

                if(p.Cor == Cor.Branca)
                    System.Console.Write(p);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(p);
                    Console.ForegroundColor = aux;
                }   
                Console.Write(" ");
            }     
        }
    }
}