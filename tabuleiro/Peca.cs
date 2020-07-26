namespace tabuleiro
{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QntMovimentos {get; protected set;}
        public Tabuleiro Tab {get; protected set;}

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Tab = tabuleiro;
            Cor = cor;
            QntMovimentos = 0;
        }

        public void IncrementarQntMovimentos()
        {
            QntMovimentos++;
        }

        public void DecrementarQntMovimentos()
        {
            QntMovimentos--;
        }
        public bool ExistemMovimentosPossiveis()
        {
            bool [,] mat = MovimentosPossiveis();

            for(int i=0;i<mat.Linhas;i++)
            {
                for(int j=0;j<mat.Colunas;j++)
                {
                    if(mat[i,j])
                        return true;
                }
            }
            return false;
        }

        public bool PodeMoverPara(Posicao destino)
        {
            return MovimentosPossiveis()[destino.Linha,destino.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}