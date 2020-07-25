namespace tabuleiro
{
    public class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QntMovimentos {get; protected set;}
        public Tabuleiro Tab {get; protected set;}

        public Peca(Posicao posicao, Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = posicao;
            Tab = tabuleiro;
            Cor = cor;
            QntMovimentos = 0;
        }
    }
}