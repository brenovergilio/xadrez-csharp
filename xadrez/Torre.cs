using tabuleiro;

namespace xadrez
{
    public class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab,cor)
        {}

        public override string ToString()
        {
            return "T";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.PecaEspecifica(pos);
            return p == null || p.Cor!=this.Cor;

        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas,Tab.Colunas];

            Posicao pos = new Posicao(0,0);

            //acima 
            pos.DefinirValores(Posicao.Linha-1,Posicao.Coluna);

            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(Tab.PecaEspecifica(pos) != null && Tab.PecaEspecifica(pos).Cor != this.Cor)
                    break;
                pos.Linha--;
            }

             //direita 
            pos.DefinirValores(Posicao.Linha,Posicao.Coluna+1);

            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(Tab.PecaEspecifica(pos) != null && Tab.PecaEspecifica(pos).Cor != this.Cor)
                    break;
                pos.Coluna++;
            }
            
            //abaixo 
            pos.DefinirValores(Posicao.Linha+1,Posicao.Coluna);

            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(Tab.PecaEspecifica(pos) != null && Tab.PecaEspecifica(pos).Cor != this.Cor)
                    break;
                pos.Linha++;
            }

            //esquerda 
            pos.DefinirValores(Posicao.Linha,Posicao.Coluna-1);

            while(Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(Tab.PecaEspecifica(pos) != null && Tab.PecaEspecifica(pos).Cor != this.Cor)
                    break;
                pos.Coluna--;
            }

            return mat;

        }
 
    }
}