using tabuleiro;

namespace xadrez
{
    public class Rei : Peca
    {
        private PartidaDeXadrez partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab,cor)
        {
            this.partida = partida;
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

        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.Value.Peca(pos);
            return p != null && p is Torre && p.Cor.Value == Cor.Value && p.QteMovimentos == 0;
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

            // #jogadaespecial roque
            if (QteMovimentos == 0  && !partida.xeque)
            {
                // #jogaespecial roque pequeno
                Posicao posT1 = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna + 3);
                if (TesteTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna + 2);
                    if (Tab.Value.Peca(p1) == null && Tab.Value.Peca(p2) == null)
                    {
                        mat[Posicao.Value.Linha, Posicao.Value.Coluna + 2] = true;
                    }
                }
                // #jogaespecial roque grande
                Posicao posT2 = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna - 4);
                if (TesteTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna - 3);
                    if (Tab.Value.Peca(p1) == null && Tab.Value.Peca(p2) == null && Tab.Value.Peca(p3) == null)
                    {
                        mat[Posicao.Value.Linha, Posicao.Value.Coluna - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
