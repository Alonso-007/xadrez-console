using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peca na posicao de origem escolhida!");
            }
            if (JogadorAtual != tab.Peca(pos).Cor.Value)
            {
                throw new TabuleiroException("A peca de origem escolhida nao e sua");
            }
            if (!tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Nao ha movimentos possiveis para a peca de origem escolhida!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posicao de destino invalida!");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var x in capturadas)
            {
                if (x.Cor.Value == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor.Value == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColacarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColacarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            ColacarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            ColacarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            ColacarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            ColacarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            ColacarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            ColacarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            ColacarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            ColacarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            ColacarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            ColacarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            ColacarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
        }
    }
}
