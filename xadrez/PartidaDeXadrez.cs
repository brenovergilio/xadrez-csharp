using System;
using tabuleiro;

namespace xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro Tab {get; private set;}
        public int Turno {get; private set;}
        public Cor JogadorAtual {get; private set;}

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
        
        public void RealizaJogada(Posicao inicio, Posicao destino)
        {
            ExecutaMovimento(inicio, destino);
            turno++;
        }

        public void ValidarPosicaoDeOrigem(Posicao origem)
        {
            if(Tab.Peca(origem) == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            if(JogadorAtual != Tab.PecaEspecifica(origem).Cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            if(!Tab.PecaEspecifica(pos).ExistemMovimentosPossiveis())        
                throw new TabuleiroException("Não há movimentos possíveis para a peça escolhida!");
        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;    
        }
        public void ColocarPecas()
        {

        }
    }
}