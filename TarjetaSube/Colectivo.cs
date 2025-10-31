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
            int montoAPagar = tarjeta.CalcularMontoPasaje(TARIFA_BASICA);
            int montoTotalAbonado = tarjeta.CalcularMontoTotalAbonado(TARIFA_BASICA);
            
            if (!tarjeta.PuedePagar(montoAPagar))
            {
                throw new InvalidOperationException("No se puede realizar el viaje. Saldo insuficiente.");
            }

            int saldoAnterior = tarjeta.Saldo;
            
            if (tarjeta is MedioBoletoEstudiantil medioBoleto)
            {
                medioBoleto.RegistrarViaje(DateTime.Now);
            }
            
            if (!tarjeta.EsFranquiciaGratuita())
            {
                tarjeta.Pagar(montoAPagar);
            }
            
            return new Boleto(linea, interno, montoAPagar, DateTime.Now, 
                            tarjeta.TipoTarjeta, tarjeta.Saldo, tarjeta.Id, montoTotalAbonado);
        }

        public bool PagarConBoolean(Tarjeta tarjeta)
        {
            int montoAPagar = tarjeta.CalcularMontoPasaje(TARIFA_BASICA);
            
            if (!tarjeta.PuedePagar(montoAPagar))
            {
                return false;
            }

            if (tarjeta is MedioBoletoEstudiantil medioBoleto)
            {
                try
                {
                    medioBoleto.RegistrarViaje(DateTime.Now);
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }

            if (!tarjeta.EsFranquiciaGratuita())
            {
                return tarjeta.IntentarPagar(montoAPagar);
            }
            
            return true;
        }

        public Boleto PagarConEnFecha(Tarjeta tarjeta, DateTime fechaHora)
        {
            if (tarjeta is MedioBoletoEstudiantil medioBoleto)
            {
                medioBoleto.RegistrarViaje(fechaHora); 
            }

            int montoAPagar = tarjeta.CalcularMontoPasaje(TARIFA_BASICA);
            int montoTotalAbonado = tarjeta.CalcularMontoTotalAbonado(TARIFA_BASICA);

            if (!tarjeta.PuedePagar(montoAPagar))
            {
                throw new InvalidOperationException("No se puede realizar el viaje. Saldo insuficiente.");
            }

            if (!tarjeta.EsFranquiciaGratuita())
            {
                tarjeta.Pagar(montoAPagar);
            }

            return new Boleto(linea, interno, montoAPagar, fechaHora, 
                            tarjeta.TipoTarjeta, tarjeta.Saldo, tarjeta.Id, montoTotalAbonado);
        }
    }
}