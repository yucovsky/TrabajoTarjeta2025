using System;

namespace TarjetaSube
{
    public class Colectivo
    {
        private const int TARIFA_URBANA = 1580;
        private const int TARIFA_INTERURBANA = 3000;
        private string linea;
        private int interno;
        private bool esInterurbano;

        public Colectivo(string linea, int interno, bool esInterurbano = false)
        {
            this.linea = linea;
            this.interno = interno;
            this.esInterurbano = esInterurbano;
        }

        public Colectivo(string linea, int interno) : this(linea, interno, false)
        {
        }

        public string Linea => linea;
        public int Interno => interno;
        public bool EsInterurbano => esInterurbano;

        private int ObtenerTarifa()
        {
            return esInterurbano ? TARIFA_INTERURBANA : TARIFA_URBANA;
        }

        public Boleto PagarCon(Tarjeta tarjeta)
        {
            return PagarConEnFecha(tarjeta, DateTime.Now);
        }

        public Boleto PagarConEnFecha(Tarjeta tarjeta, DateTime fechaHora)
        {
            int tarifaBase = ObtenerTarifa();
            bool esTrasbordo = false;
            int montoAPagar;

            if (tarjeta.PuedeRealizarTrasbordo(fechaHora, this.linea))
            {
                esTrasbordo = true;
                montoAPagar = 0; 
            }
            else
            {
                if (tarjeta is MedioBoletoEstudiantil medioBoleto)
                {
                    montoAPagar = medioBoleto.CalcularMontoPasajeEnFecha(tarifaBase, fechaHora);
                }
                else if (tarjeta is BoletoGratuitoEstudiantil boletoGratuito)
                {
                    montoAPagar = boletoGratuito.CalcularMontoPasajeEnFecha(tarifaBase, fechaHora);
                }
                else if (tarjeta is FranquiciaCompleta franquiciaCompleta)
                {
                    montoAPagar = franquiciaCompleta.CalcularMontoPasajeEnFecha(tarifaBase, fechaHora);
                }
                else
                {
                    montoAPagar = tarjeta.CalcularMontoPasajeEnFecha(tarifaBase, fechaHora);
                }
            }

            int montoTotalAbonado = tarjeta.CalcularMontoTotalAbonado(tarifaBase);

            if (montoAPagar > 0 && !tarjeta.PuedePagar(montoAPagar))
            {
                throw new InvalidOperationException("No se puede realizar el viaje. Saldo insuficiente.");
            }

            tarjeta.RegistrarViajeConLinea(fechaHora, this.linea);

            if (tarjeta is MedioBoletoEstudiantil medioBoletoTarjeta)
            {
                medioBoletoTarjeta.RegistrarViaje(fechaHora);
            }
            else if (tarjeta is BoletoGratuitoEstudiantil boletoGratuitoTarjeta)
            {
                boletoGratuitoTarjeta.RegistrarViajeGratuito(fechaHora);
            }
            else if (tarjeta is FranquiciaCompleta franquiciaCompletaTarjeta)
            {
                franquiciaCompletaTarjeta.RegistrarViajeGratuito(fechaHora);
            }
            else
            {
                tarjeta.RegistrarViaje(fechaHora);
            }

            if (montoAPagar > 0)
            {
                tarjeta.Pagar(montoAPagar);
            }

            return new Boleto(linea, interno, montoAPagar, fechaHora, 
                            tarjeta.TipoTarjeta, tarjeta.Saldo, tarjeta.Id, 
                            montoTotalAbonado, esTrasbordo);
        }

        public bool PagarConBoolean(Tarjeta tarjeta)
        {
            try
            {
                var boleto = PagarCon(tarjeta);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}