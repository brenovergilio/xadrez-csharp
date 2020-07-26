using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro Tab {get; private set;}
        public int Turno {get; private set;}
        public Cor JogadorAtual {get; private set;}
        private HashSet<Peca> Pecas;
        private HashSet<Peca> PecasCapturadas; 
        public bool Terminada {get; private set;}

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8,8);
            Turno = 1;
            Terminada = false;
            JogadorAtual = Cor.Branca;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao inicio, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(inicio);
            p.IncrementarQntMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p,destino);
            if(pecaCapturada!=null)
                PecasCapturadas.Add(pecaCapturada);
        }

        public HashSet<Peca> Capturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in PecasCapturadas)
            {
                if(x.Cor == cor)
                    aux.Add(x);
            }
            return aux;
        }
        public HashSet<Peca> EmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Pecas)
            {
                if(x.Cor == cor)
                    aux.Add(x);
            }
            aux.ExceptWith(Capturadas(cor));
            return aux;
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

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!Tab.PecaEspecifica(origem).PodeMoverPara(destino))
                throw new TabuleiroException("Posição de destino inválida!");
        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;    
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca p)
        {
            Tab.ColocarPeca(p, new PosicaoXadrez(coluna,linha).ToPosicao());
            Pecas.Add(p);
        }
        public void ColocarPecas()
        {

        }
    }
}