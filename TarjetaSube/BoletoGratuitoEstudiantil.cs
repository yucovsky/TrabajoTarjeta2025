using System;
using System.Collections.Generic;
using System.Linq;

namespace TarjetaSube
{
    public class BoletoGratuitoEstudiantil : Tarjeta
    {
        private List<DateTime> viajesGratuitosHoy;
        private const int MAX_VIAJES_GRATUITOS_POR_DIA = 2;

        public BoletoGratuitoEstudiantil(int saldo = 0) : base(saldo)
        {
            viajesGratuitosHoy = new List<DateTime>();
        }

        public override string TipoTarjeta
        {
            get { return "Boleto Gratuito Estudiantil"; }
        }

        public override int CalcularMontoPasaje(int tarifaBase)
        {
            return CalcularMontoPasajeEnFecha(tarifaBase, DateTime.Now);
        }

        public override int CalcularMontoPasajeEnFecha(int tarifaBase, DateTime fechaReferencia)
        {
            LimpiarViajesAntiguos(fechaReferencia);
            return viajesGratuitosHoy.Count < MAX_VIAJES_GRATUITOS_POR_DIA ? 0 : tarifaBase;
        }

        public void RegistrarViajeGratuito(DateTime fechaHoraViaje)
        {
            LimpiarViajesAntiguos(fechaHoraViaje);
            
            if (viajesGratuitosHoy.Count >= MAX_VIAJES_GRATUITOS_POR_DIA)
            {
                throw new InvalidOperationException($"Límite de {MAX_VIAJES_GRATUITOS_POR_DIA} viajes gratuitos por día alcanzado");
            }

            viajesGratuitosHoy.Add(fechaHoraViaje);
        }

        public int ViajesGratuitosHoyEnFecha(DateTime fechaReferencia)
        {
            LimpiarViajesAntiguos(fechaReferencia);
            return viajesGratuitosHoy.Count;
        }

        public int ViajesGratuitosHoy
        {
            get 
            { 
                return ViajesGratuitosHoyEnFecha(DateTime.Now);
            }
        }

        private void LimpiarViajesAntiguos(DateTime fechaReferencia)
        {
            DateTime hoy = fechaReferencia.Date;
            viajesGratuitosHoy.RemoveAll(viaje => viaje.Date < hoy);
        }

        public override bool PuedePagar(int monto)
        {
            return true;
        }

        public override bool EsFranquiciaGratuita()
        {
            return true;
        }

        public override int CalcularMontoTotalAbonado(int tarifaBase)
        {
            return CalcularMontoPasaje(tarifaBase);
        }
    }
}