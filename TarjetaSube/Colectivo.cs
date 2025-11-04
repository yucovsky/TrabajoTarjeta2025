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

        public string Linea
        {
            get { return linea; }
        }

        public int Interno
        {
            get { return interno; }
        }

        public bool EsInterurbano
        {
            get { return esInterurbano; }
        }

        private int ObtenerTarifa()
        {
            return esInterurbano ? TARIFA_INTERURBANA : TARIFA_URBANA;
        }

        public Boleto PagarCon(Tarjeta tarjeta)
        {
            int tarifaBase = ObtenerTarifa();
            int montoAPagar = tarjeta.CalcularMontoPasaje(tarifaBase);
            int montoTotalAbonado = tarjeta.CalcularMontoTotalAbonado(tarifaBase);

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
            int tarifaBase = ObtenerTarifa();
            int montoAPagar = tarjeta.CalcularMontoPasaje(tarifaBase);

            if (!tarjeta.PuedePagar(montoAPagar))
            {
                return false;
            }

            try
            {
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

        public Boleto PagarConEnFecha(Tarjeta tarjeta, DateTime fechaHora)
{
    int tarifaBase = ObtenerTarifa();
    int montoAPagar;
    int montoTotalAbonado;

    // No capturar excepciones aquí - dejar que se propaguen
    if (tarjeta is MedioBoletoEstudiantil medioBoleto)
    {
        montoAPagar = medioBoleto.CalcularMontoPasajeEnFecha(tarifaBase, fechaHora);
        montoTotalAbonado = medioBoleto.CalcularMontoTotalAbonado(tarifaBase);
        medioBoleto.RegistrarViaje(fechaHora);
    }
    else if (tarjeta is BoletoGratuitoEstudiantil boletoGratuito)
    {
        montoAPagar = boletoGratuito.CalcularMontoPasajeEnFecha(tarifaBase, fechaHora);
        montoTotalAbonado = boletoGratuito.CalcularMontoTotalAbonado(tarifaBase);
        boletoGratuito.RegistrarViajeGratuito(fechaHora);
    }
    else if (tarjeta is FranquiciaCompleta franquiciaCompleta)
    {
        montoAPagar = franquiciaCompleta.CalcularMontoPasajeEnFecha(tarifaBase, fechaHora);
        montoTotalAbonado = franquiciaCompleta.CalcularMontoTotalAbonado(tarifaBase);
        franquiciaCompleta.RegistrarViajeGratuito(fechaHora);
    }
    else
    {
        montoAPagar = tarjeta.CalcularMontoPasajeEnFecha(tarifaBase, fechaHora);
        montoTotalAbonado = tarjeta.CalcularMontoTotalAbonado(tarifaBase);
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
    }
}