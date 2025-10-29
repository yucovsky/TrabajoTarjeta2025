using System;

namespace TarjetaSube
{
    public class Boleto
    {
        private string linea;
        private int interno;
        private int monto;
        private DateTime fechaHora;

        public Boleto(string linea, int interno, int monto, DateTime fechaHora)
        {
            this.linea = linea;
            this.interno = interno;
            this.monto = monto;
            this.fechaHora = fechaHora;
        }

        public string Linea
        {
            get { return linea; }
        }

        public int Interno
        {
            get { return interno; }
        }

        public int Monto
        {
            get { return monto; }
        }

        public DateTime FechaHora
        {
            get { return fechaHora; }
        }
    }
}