using System;

namespace TarjetaSube
{
    public class Tarjeta
    {
        private int saldo;
        public Tarjeta(int saldo = 0)
        {
            this.saldo = saldo;
        }

        public int Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }
        
        public void Cargar(int importe)
        {
            saldo += importe;
        }
        public void Pagar()
        {
            saldo -= 50;
        }
    }
}
