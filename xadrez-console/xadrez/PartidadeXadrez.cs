﻿using System.Collections.Generic;
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
        public bool xeque { get; private set; }
        public Peca VuneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            VuneravelEnPassant = null;
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // #jogadaespecia roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = tab.RetirarPeca(origemT);
                t.IncrementarQtdMovimentos();
                tab.ColocarPeca(t, destinoT);
            }

            // #jogadaespecia roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca t = tab.RetirarPeca(origemT);
                t.IncrementarQtdMovimentos();
                tab.ColocarPeca(t, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor.Value == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.RetirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }
        
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);

            // #jogadaespecia roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = tab.RetirarPeca(destinoT);
                t.DecrementarQtdMovimentos();
                tab.ColocarPeca(t, origemT);
            }

            // #jogadaespecia roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca t = tab.RetirarPeca(destinoT);
                t.DecrementarQtdMovimentos();
                tab.ColocarPeca(t, origemT);
            }

            // #jogadaespecail en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VuneravelEnPassant)
                {
                    Peca peao = tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor.Value == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    tab.ColocarPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmCheque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voce nao pode se colocar em xeque!");
            }

            Peca p = tab.Peca(destino);

            //jogada especial promocao
            if (p is Peao)
            {
                if ((p.Cor.Value == Cor.Branca && destino.Linha == 0) || (p.Cor.Value == Cor.Preta && destino.Linha == 7))
                {
                    p = tab.RetirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.Cor.Value);
                    tab.ColocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (EstaEmCheque(Adversaria(JogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            // #jogadaespecia en passant
            if (p is Peao && (destino.Linha == origem.Linha -2 || destino.Linha == origem.Linha +2))
            {
                VuneravelEnPassant = p;
            }
            else
            {
                VuneravelEnPassant = null;
            }
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
            if (!tab.Peca(origem).MovimentoPossivel(destino))
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

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmCheque(Cor cor)
        {
            Peca r = Rei(cor);
            if (r == null)
            {
                throw new TabuleiroException($"Nao tem rei da cor {cor} no tabuleiro");
            }

            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[r.Posicao.Value.Linha, r.Posicao.Value.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmCheque(cor))
            {
                return false;
            }

            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < tab.Linhas; i++)
                {
                    for (int j = 0; j < tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao.Value;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, new Posicao(i, j));
                            bool testeXeque = EstaEmCheque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ColacarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColacarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            ColacarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            ColacarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            ColacarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            ColacarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            ColacarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            ColacarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            ColacarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            ColacarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            ColacarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            ColacarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            ColacarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            ColacarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            ColacarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            ColacarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            ColacarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            ColacarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            ColacarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            ColacarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            ColacarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            ColacarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            ColacarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            ColacarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            ColacarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            ColacarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            ColacarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            ColacarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            ColacarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            ColacarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            ColacarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            ColacarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            ColacarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }
    }
}
