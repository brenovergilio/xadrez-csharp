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

        public Peca PecaVulneravelEnPassant {get; private set;}

        public bool Xeque {get; private set;}

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8,8);
            Turno = 1;
            Terminada = false;
            Xeque = false;
            JogadorAtual = Cor.Branca;
            PecaVulneravelEnPassant = null;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao inicio, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(inicio);
            p.IncrementarQntMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p,destino);
            if(pecaCapturada!=null)
                PecasCapturadas.Add(pecaCapturada);

            // #Jogada Especial: Roque pequeno

            if(p is Rei && destino.Coluna == inicio.Coluna +2)
            {
                Posicao origemTorre = new Posicao(inicio.Linha, inicio.Coluna + 3);
                Posicao destinoTorre = new Posicao(inicio.Linha, inicio.Coluna +1);

                Peca T = Tab.RetirarPeca(origemTorre);
                T.IncrementarQntMovimentos();
                Tab.ColocarPeca(T,destinoTorre);
            }
            // #Jogada Especial: Roque grande

            if(p is Rei && destino.Coluna == inicio.Coluna -2)
            {
                Posicao origemTorre = new Posicao(inicio.Linha, inicio.Coluna -4);
                Posicao destinoTorre = new Posicao(inicio.Linha, inicio.Coluna -1);

                Peca T = Tab.RetirarPeca(origemTorre);
                T.IncrementarQntMovimentos();
                Tab.ColocarPeca(T,destinoTorre);
            }

            // #Jogada Especial: En Passant

            if(p is Peao)
            {
                if(inicio.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if(p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha+1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha-1, destino.Coluna);
                    }
                    pecaCapturada = Tab.RetirarPeca(posP);
                    PecasCapturadas.Add(pecaCapturada);
                }
            }


            return pecaCapturada;    
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

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);

            if(R == null)
                throw new TabuleiroException("Não tem rei dessa cor no tabuleiro!");

            foreach(Peca x in EmJogo(Adversaria(cor)))
            {
                bool [,] mat = x.MovimentosPossiveis();

                if(mat[R.Posicao.Linha,R.Posicao.Coluna])
                    return true;
            }   

            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if(!EstaEmXeque(cor))
                return false;

            foreach(Peca x in EmJogo(cor))
            {
                bool [,] mat = x.MovimentosPossiveis();

                for(int i=0;i<Tab.Linhas;i++)
                {
                    for(int j=0; j<Tab.Colunas;j++)
                    {
                        Posicao origem = x.Posicao;
                        Posicao destino = new Posicao(i,j);
                        Peca pecaCapturada = ExecutaMovimento(origem, destino);
                        bool testaXeque = EstaEmXeque(cor);
                        DesfazMovimento(origem,destino, pecaCapturada);
                        if(!testaXeque)
                            return false;
                    }
                }
            }  
            return true;  
        }
        private Peca Rei(Cor cor)
        {
            foreach(Peca x in EmJogo(cor))
            {
                if (x is Rei)
                    return x;
            }
            return null;
        }

        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
                return Cor.Preta;
            return Cor.Branca;    
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

        public void DesfazMovimento(Posicao inicio, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQntMovimentos();
            if(pecaCapturada!=null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p,inicio);

            // #Jogada Especial: Roque pequeno

            if(p is Rei && destino.Coluna == inicio.Coluna +2)
            {
                Posicao origemTorre = new Posicao(inicio.Linha, inicio.Coluna + 3);
                Posicao destinoTorre = new Posicao(inicio.Linha, inicio.Coluna +1);

                Peca T = Tab.RetirarPeca(destinoTorre);
                T.DecrementarQntMovimentos();
                Tab.ColocarPeca(T,origemTorre);
            }

            // #Jogada Especial: Roque grande

            if(p is Rei && destino.Coluna == inicio.Coluna -2)
            {
                Posicao origemTorre = new Posicao(inicio.Linha, inicio.Coluna -4);
                Posicao destinoTorre = new Posicao(inicio.Linha, inicio.Coluna -1);

                Peca T = Tab.RetirarPeca(destinoTorre);
                T.DecrementarQntMovimentos();
                Tab.ColocarPeca(T, origemTorre);
            }

            // #Jogada Especial: En passant

            if(p is Peao)
            {
                if(inicio.Coluna != destino.Coluna && pecaCapturada == PecaVulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posP;
                    if(p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }

                    Tab.ColocarPeca(peao, posP);
                }
            }

        }

        public void RealizaJogada(Posicao inicio, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(inicio, destino);

            if(EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(inicio,destino, pecaCapturada);
            }

            Peca p = Tab.PecaEspecifica(destino);

            // #Jogada Especial: Promoção

            if(p is Peao)
            {
                if((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tab.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(Tab,p.Cor);
                    Tab.ColocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
            }            

            if(EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }

            else
            {
                Xeque = false;
            }

            if(TesteXequeMate(Adversaria(JogadorAtual)))
                Terminada = true;

            else
            {       
                Turno++;
                MudaJogador();
            } 

            // #Jogada Especial: En Passant

            if(p is Peao && (destino.Linha == inicio.Linha-2 || destino.Linha == inicio.Linha+2))
            {
                PecaVulneravelEnPassant = p;
            }
            else
            {
                PecaVulneravelEnPassant = null;
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao origem)
        {
            if(Tab.PecaEspecifica(origem) == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            if(JogadorAtual != Tab.PecaEspecifica(origem).Cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            if(!Tab.PecaEspecifica(origem).ExistemMovimentosPossiveis())        
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
            ColocarNovaPeca('a',1,new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('b',1,new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('c',1,new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('d',1,new Dama(Tab, Cor.Branca));
            ColocarNovaPeca('e',1,new Rei(Tab, Cor.Branca,this));
            ColocarNovaPeca('f',1,new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('g',1,new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('h',1,new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('a',2,new Peao(Tab, Cor.Branca,this));
            ColocarNovaPeca('b',2,new Peao(Tab, Cor.Branca,this));
            ColocarNovaPeca('c',2,new Peao(Tab, Cor.Branca,this));
            ColocarNovaPeca('d',2,new Peao(Tab, Cor.Branca,this));
            ColocarNovaPeca('e',2,new Peao(Tab, Cor.Branca,this));
            ColocarNovaPeca('f',2,new Peao(Tab, Cor.Branca,this));
            ColocarNovaPeca('g',2,new Peao(Tab, Cor.Branca,this));
            ColocarNovaPeca('h',2,new Peao(Tab, Cor.Branca,this));

            ColocarNovaPeca('a',8,new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('b',8,new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('c',8,new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('d',8,new Dama(Tab, Cor.Preta));
            ColocarNovaPeca('e',8,new Rei(Tab, Cor.Preta,this));
            ColocarNovaPeca('f',8,new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('g',8,new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('h',8,new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('a',7,new Peao(Tab, Cor.Preta,this));
            ColocarNovaPeca('b',7,new Peao(Tab, Cor.Preta,this));
            ColocarNovaPeca('c',7,new Peao(Tab, Cor.Preta,this));
            ColocarNovaPeca('d',7,new Peao(Tab, Cor.Preta,this));
            ColocarNovaPeca('e',7,new Peao(Tab, Cor.Preta,this));
            ColocarNovaPeca('f',7,new Peao(Tab, Cor.Preta,this));
            ColocarNovaPeca('g',7,new Peao(Tab, Cor.Preta,this));
            ColocarNovaPeca('h',7,new Peao(Tab, Cor.Preta,this));
        }
    }
}