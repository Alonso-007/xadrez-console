using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    public class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "B";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Value.Peca(pos);
            return p == null || p.Cor.Value != Cor.Value;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Value.Linhas, Tab.Value.Linhas];

            Posicao pos = new Posicao(0, 0);

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
