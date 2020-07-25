using System;
using tabuleiro;

namespace xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro Tab {get; private set;}
        private int Turno;
        private Cor JogadorAtual;

        public bool Terminada {get; private set;}

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8,8);
            Turno = 1;
            Terminada = false;
            JogadorAtual = Cor.Branca;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao inicio, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(inicio);
            p.IncrementarQntMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p,destino);
        }

        public void ColocarPecas()
        {

        }
    }
}