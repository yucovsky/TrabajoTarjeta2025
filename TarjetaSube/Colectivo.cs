using System;

namespace TarjetaSube
{
    public class Colectivo
    {
        private const int TARIFA_BASICA = 1580;
        private string linea;
        private int interno;

        public Colectivo(string linea, int interno)
        {
            this.linea = linea;
            this.interno = interno;
        }

        public string Linea
        {
            get { return linea; }
        }

        public int Interno
        {
            get { return interno; }
        }

        public Boleto PagarCon(Tarjeta tarjeta)
        {
            if (tarjeta.Saldo < TARIFA_BASICA)
            {
                throw new InvalidOperationException("Saldo insuficiente para realizar el viaje");
            }

            tarjeta.Pagar(TARIFA_BASICA);
            return new Boleto(linea, interno, TARIFA_BASICA, DateTime.Now);
        }
    }
}