using tabuleiro;

namespace xadrez
{
    public class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "T";
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
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha -= 1;
            }
            //abaixo
            pos.DefinirValores(Posicao.Value.Linha + 1, Posicao.Value.Coluna);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha += 1;
            }
            //direita
            pos.DefinirValores(Posicao.Value.Linha, Posicao.Value.Coluna + 1);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna += 1;
            }
            //esquerda
            pos.DefinirValores(Posicao.Value.Linha, Posicao.Value.Coluna - 1);
            while (Tab.Value.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Value.Peca(pos) != null && Tab.Value.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna -= 1;
            }

            return mat;
        }
    }
}
