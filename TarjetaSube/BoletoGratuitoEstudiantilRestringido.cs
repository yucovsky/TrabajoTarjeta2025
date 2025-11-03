using System;
using System.Collections.Generic;
using System.Linq;

namespace TarjetaSube
{
    public class BoletoGratuitoEstudiantilRestringido : Tarjeta
    {
        private List<DateTime> viajesGratuitosHoy;
        private const int MAX_VIAJES_GRATUITOS_POR_DIA = 2;

        public BoletoGratuitoEstudiantilRestringido(int saldo = 0) : base(saldo)
        {
            viajesGratuitosHoy = new List<DateTime>();
        }

        public override string TipoTarjeta
        {
            get { return "Boleto Gratuito Estudiantil Restringido"; }
        }

        public override int CalcularMontoPasaje(int tarifaBase)
        {
            if (!ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(DateTime.Now))
            {
                throw new InvalidOperationException("No se puede utilizar el boleto gratuito fuera de la franja horaria permitida");
            }

            LimpiarViajesAntiguos(DateTime.Now);
            return viajesGratuitosHoy.Count < MAX_VIAJES_GRATUITOS_POR_DIA ? 0 : tarifaBase;
        }

        public void RegistrarViajeGratuito(DateTime fechaHoraViaje)
        {
            if (!ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(fechaHoraViaje))
            {
                throw new InvalidOperationException("No se puede utilizar el boleto gratuito fuera de la franja horaria permitida");
            }
            
            LimpiarViajesAntiguos(fechaHoraViaje);
            
            if (viajesGratuitosHoy.Count >= MAX_VIAJES_GRATUITOS_POR_DIA)
            {
                throw new InvalidOperationException($"Límite de {MAX_VIAJES_GRATUITOS_POR_DIA} viajes gratuitos por día alcanzado");
            }
        
            viajesGratuitosHoy.Add(fechaHoraViaje);
        }

        public override bool PuedePagar(int monto)
        {
            return ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(DateTime.Now);
        }

        public bool PuedePagarEnFecha(int monto, DateTime fechaHora)
        {
            return ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(fechaHora);
        }

        public override bool EsFranquiciaGratuita()
        {
            return true;
        }

        public override int CalcularMontoTotalAbonado(int tarifaBase)
        {
            return CalcularMontoPasaje(tarifaBase);
        }

        private void LimpiarViajesAntiguos(DateTime fechaReferencia)
        {
            DateTime hoy = fechaReferencia.Date;
            viajesGratuitosHoy.RemoveAll(viaje => viaje.Date < hoy);
        }

        public int CalcularMontoPasajeEnFecha(int tarifaBase, DateTime fechaReferencia)
        {
            if (!ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(fechaReferencia))
            {
                throw new InvalidOperationException("No se puede utilizar el boleto gratuito fuera de la franja horaria permitida");
            }

            LimpiarViajesAntiguos(fechaReferencia);
            return viajesGratuitosHoy.Count < MAX_VIAJES_GRATUITOS_POR_DIA ? 0 : tarifaBase;
        }
    }
}