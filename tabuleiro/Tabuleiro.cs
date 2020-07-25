namespace tabuleiro
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }

        private Peca[,] pecas;

        public Tabuleiro (int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            pecas = new Peca[linhas,colunas];
        }

        public bool ExistePeca(Posicao pos)
        {
           ValidarPosicao(pos);

           return PecaEspecifica(pos) != null;  
        }

        public Peca PecaEspecifica(int linha, int coluna)
        {
            return pecas[linha,coluna];
        }

        public Peca PecaEspecifica(Posicao pos)
        {
            return pecas[pos.Linha,pos.Coluna];
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            if(ExistePeca(pos))
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            pecas[pos.Linha,pos.Coluna] = p;
            p.Posicao = pos;
        }

        public bool PosicaoValida(Posicao pos)
        {
            if(pos.Linha<0 || pos.Linha >= Linhas || pos.Coluna<0 || pos.Coluna >=Colunas)
                return false;
            return true;    
        }

        public void ValidarPosicao(Posicao pos)
        {
            if(!PosicaoValida(pos))
                throw new TabuleiroException("Posição inválida!");
        }
    }
}