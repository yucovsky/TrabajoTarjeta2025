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
            if (!tarjeta.PuedePagar(TARIFA_BASICA))
            {
                throw new InvalidOperationException("No se puede realizar el viaje. Saldo insuficiente incluso con saldo negativo permitido.");
            }

            tarjeta.Pagar(TARIFA_BASICA);
            return new Boleto(linea, interno, TARIFA_BASICA, DateTime.Now);
        }

        public bool PagarConBoolean(Tarjeta tarjeta)
        {
            if (!tarjeta.PuedePagar(TARIFA_BASICA))
            {
                return false;
            }

            return tarjeta.IntentarPagar(TARIFA_BASICA);
        }
    }
}