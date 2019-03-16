using tabuleiro;

namespace xadrez
{
    public class Peao : Peca
    {
        private PartidaDeXadrez partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base (tab, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.Value.Peca(pos);
            return p != null && p.Cor.Value != Cor.Value;
        }

        private bool livre(Posicao pos)
        {
            return Tab.Value.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Value.Linhas, Tab.Value.Colunas];

            Posicao pos = new Posicao(0,0);

            if (Cor.Value == tabuleiro.Cor.Branca)
            {
                pos.DefinirValores(Posicao.Value.Linha - 1, Posicao.Value.Coluna);
                if (Tab.Value.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Value.Linha - 2, Posicao.Value.Coluna);
                if (Tab.Value.PosicaoValida(pos) && livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Value.Linha - 1, Posicao.Value.Coluna - 1);
                if (Tab.Value.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Value.Linha - 1, Posicao.Value.Coluna + 1);
                if (Tab.Value.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //#jogadaespecial en passant
                if (Posicao.Value.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna - 1);
                    if (Tab.Value.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Value.Peca(esquerda) == partida.VuneravelEnPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna + 1);
                    if (Tab.Value.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Value.Peca(direita) == partida.VuneravelEnPassant)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna);
                if (Tab.Value.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Value.Linha + 2, Posicao.Value.Coluna);
                if (Tab.Value.PosicaoValida(pos) && livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna - 1);
                if (Tab.Value.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna + 1);
                if (Tab.Value.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //#jogadaespecial en passant
                if (Posicao.Value.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna - 1);
                    if (Tab.Value.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Value.Peca(esquerda) == partida.VuneravelEnPassant)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Value.Linha, Posicao.Value.Coluna + 1);
                    if (Tab.Value.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Value.Peca(direita) == partida.VuneravelEnPassant)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}
