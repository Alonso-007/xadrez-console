using System;

namespace tabuleiro
{
    public abstract class Peca
    {
        public Lazy<Posicao> Posicao { get; set; }
        public Lazy<Cor> Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Lazy<Tabuleiro> Tab { get; set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.Posicao = null;
            this.Cor = new Lazy<Cor>(cor);
            this.Tab = new Lazy<Tabuleiro>(tab);
            this.QteMovimentos = 0;
        }

        public void IncrementarQtdMovimentos()
        {
            QteMovimentos++;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i = 0; i < Tab.Value.Linhas; i++)
            {
                for (int j = 0; j < Tab.Value.Colunas; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
