using tabuleiro;

namespace xadrez
{
    public class Dama : Peca
    {
        public Dama(Tabuleiro tab, Cor cor) : base (tab, cor)
        {

        }

        public override string ToString()
        {
            return "D";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Value.Peca(pos);
            return p == null || p.Cor.Value != Cor.Value;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Value.Linhas, Tab.Value.Colunas];

            Posicao pos = new Posicao(0, 0);

            //equerda
            pos.DefinirValores(Posicao.Value.Linha, Posicao.Value.Coluna - 1);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor.Value != Cor.Value)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha, pos.Coluna - 1);
            }

            //direita
            pos.DefinirValores(Posicao.Value.Linha, Posicao.Value.Coluna + 1);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor.Value != Cor.Value)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha, pos.Coluna + 1);
            }

            //acima
            pos.DefinirValores(Posicao.Value.Linha - 1, Posicao.Value.Coluna);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor.Value != Cor.Value)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna);
            }

            //abaixo
            pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor.Value != Cor.Value)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna);
            }

            //
            pos.DefinirValores(Posicao.Value.Linha - 1, Posicao.Value.Coluna - 1);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor.Value != Cor.Value)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            }
            //
            pos.DefinirValores(Posicao.Value.Linha - 1, Posicao.Value.Coluna + 1);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor.Value != Cor.Value)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
            }
            //
            pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna + 1);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor.Value != Cor.Value)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
            }
            //
            pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna - 1);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor.Value != Cor.Value)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
            }

            return mat;
        }
    }
}
