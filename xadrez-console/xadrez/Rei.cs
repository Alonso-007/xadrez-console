using tabuleiro;

namespace xadrez
{
    public class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab,cor)
        {

        }

        public override string ToString()
        {
            return "R";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Value.Peca(pos);
            return p == null || p.Cor.Value != this.Cor.Value;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Value.Linhas, Tab.Value.Colunas];

            Posicao pos = new Posicao(0, 0);

            //acima
            pos.DefinirValores(Posicao.Value.Linha - 1, Posicao.Value.Coluna);
            if (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //nordeste
            pos.DefinirValores(Posicao.Value.Linha - 1, Posicao.Value.Coluna + 1);
            if (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //direita
            pos.DefinirValores(Posicao.Value.Linha, Posicao.Value.Coluna + 1);
            if (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //sudeste
            pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna + 1);
            if (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //abaixo
            pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna);
            if (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //sudoeste
            pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna - 1);
            if (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //esquerda
            pos.DefinirValores(Posicao.Value.Linha, Posicao.Value.Coluna - 1);
            if (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //noroeste
            pos.DefinirValores(Posicao.Value.Linha - 1, Posicao.Value.Coluna - 1);
            if (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }
    }
}
