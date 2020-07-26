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

        public abstract bool[,] MovimentosPossiveis();
    }
}