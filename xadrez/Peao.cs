using tabuleiro;

namespace xadrez
{
    public class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab,cor)
        {}

        public override string ToString()
        {
            return "P";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.PecaEspecifica(pos);
            return p == null || p.Cor!=this.Cor;

        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.PecaEspecifica(pos);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao pos)
        {
            return Tab.PecaEspecifica(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas,Tab.Colunas];

            Posicao pos = new Posicao(0,0);

            if(Cor == Cor.Branca)
            {   //acima 
                    pos.DefinirValores(Posicao.Linha-1,Posicao.Coluna);

                    if(Tab.PosicaoValida(pos) && PodeMover(pos))
                    {
                        mat[pos.Linha, pos.Coluna] = true;
                    }

                    pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                    if(Tab.PosicaoValida(pos) && Livre(pos) && QntMovimentos == 0)
                    {
                        mat[pos.Linha,pos.Coluna] == true;
                    }
                    
                    pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna -1);

                    if(Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    {
                        mat[pos.Linha,pos.Coluna] == true;
                    }

                    pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna +1);

                    if(Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    {
                        mat[pos.Linha,pos.Coluna] == true;
                    }
            }

            else
            {
                    pos.DefinirValores(Posicao.Linha+1,Posicao.Coluna);

                    if(Tab.PosicaoValida(pos) && PodeMover(pos))
                    {
                        mat[pos.Linha, pos.Coluna] = true;
                    }

                    pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                    if(Tab.PosicaoValida(pos) && Livre(pos) && QntMovimentos == 0)
                    {
                        mat[pos.Linha,pos.Coluna] == true;
                    }
                    
                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna -1);

                    if(Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    {
                        mat[pos.Linha,pos.Coluna] == true;
                    }

                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna +1);

                    if(Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    {
                        mat[pos.Linha,pos.Coluna] == true;
                    }                
            }
            return mat;

        }
    }
}