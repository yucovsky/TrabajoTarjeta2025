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
            else if (tarjeta is BoletoGratuitoEstudiantil boletoGratuito)
            {
                boletoGratuito.RegistrarViajeGratuito(DateTime.Now);
            }
            else if (tarjeta is FranquiciaCompleta franquiciaCompleta)
            {
                franquiciaCompleta.RegistrarViajeGratuito(DateTime.Now);
            }
            else
            {
                tarjeta.RegistrarViaje(DateTime.Now);
            }

            if (!tarjeta.EsFranquiciaGratuita() || montoAPagar > 0)
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
            else if (tarjeta is BoletoGratuitoEstudiantil boletoGratuito)
            {
                try
                {
                    boletoGratuito.RegistrarViajeGratuito(DateTime.Now);
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
            else if (tarjeta is FranquiciaCompleta franquiciaCompleta)
            {
                try
                {
                    franquiciaCompleta.RegistrarViajeGratuito(DateTime.Now);
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
            else
            {
                tarjeta.RegistrarViaje(DateTime.Now);
            }

            if (!tarjeta.EsFranquiciaGratuita() || montoAPagar > 0)
            {
                return tarjeta.IntentarPagar(montoAPagar);
            }

            return true;
        }

        public Boleto PagarConEnFecha(Tarjeta tarjeta, DateTime fechaHora)
        {
            int montoAPagar;
            int montoTotalAbonado;

            if (tarjeta is MedioBoletoEstudiantil medioBoleto)
            {
                medioBoleto.RegistrarViaje(fechaHora);
                montoAPagar = medioBoleto.CalcularMontoPasaje(TARIFA_BASICA);
                montoTotalAbonado = medioBoleto.CalcularMontoTotalAbonado(TARIFA_BASICA);
            }
            else if (tarjeta is BoletoGratuitoEstudiantil boletoGratuito)
            {
                boletoGratuito.RegistrarViajeGratuito(fechaHora);
                montoAPagar = 0;
                montoTotalAbonado = 0;
            }
            else if (tarjeta is FranquiciaCompleta franquiciaCompleta)
            {
                franquiciaCompleta.RegistrarViajeGratuito(fechaHora);
                montoAPagar = 0;
                montoTotalAbonado = 0;
            }
            else
            {
                montoAPagar = tarjeta.CalcularMontoPasajeEnFecha(TARIFA_BASICA, fechaHora);
                montoTotalAbonado = tarjeta.CalcularMontoTotalAbonado(TARIFA_BASICA);
                tarjeta.RegistrarViaje(fechaHora);
            }

            if (!tarjeta.PuedePagar(montoAPagar))
            {
                throw new InvalidOperationException("No se puede realizar el viaje. Saldo insuficiente.");
            }

            if (!tarjeta.EsFranquiciaGratuita() || montoAPagar > 0)
            {
                tarjeta.Pagar(montoAPagar);
            }

            return new Boleto(linea, interno, montoAPagar, fechaHora, 
                            tarjeta.TipoTarjeta, tarjeta.Saldo, tarjeta.Id, montoTotalAbonado);
        }

        public bool PagarConBooleanEnFecha(Tarjeta tarjeta, DateTime fechaSimulada)
        {
            int montoAPagar = tarjeta.CalcularMontoPasaje(TARIFA_BASICA);
        
            if (!tarjeta.PuedePagarEnFecha(montoAPagar, fechaSimulada))
            {
                return false;
            }
        
            try
            {
                if (tarjeta is MedioBoletoEstudiantil medioBoleto)
                {
                    medioBoleto.RegistrarViaje(fechaSimulada);
                }
                else if (tarjeta is BoletoGratuitoEstudiantil boletoGratuito)
                {
                    boletoGratuito.RegistrarViajeGratuito(fechaSimulada);
                }
                else if (tarjeta is FranquiciaCompleta franquiciaCompleta)
                {
                    franquiciaCompleta.RegistrarViajeGratuito(fechaSimulada);
                }
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        
            if (!tarjeta.EsFranquiciaGratuita() || montoAPagar > 0)
            {
                return tarjeta.IntentarPagar(montoAPagar);
            }
        
            return true;
        }
    }
}