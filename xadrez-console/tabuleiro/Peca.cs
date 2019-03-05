using System;

namespace tabuleiro
{
    public class Peca
    {
        public Lazy<Posicao> Posicao { get; set; }
        public Lazy<Cor> Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Lazy<Tabuleiro> Tab { get; set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tab)
        {
            this.Posicao = new Lazy<Posicao>(posicao);
            this.Cor = new Lazy<Cor>(cor);
            this.Tab = new Lazy<Tabuleiro>(tab);
            this.QteMovimentos = 0;
        }
    }
}
