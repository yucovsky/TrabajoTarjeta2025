using System;
using System.Collections.Generic;
using System.Linq;

namespace TarjetaSube
{
    public class MedioBoletoEstudiantil : Tarjeta
    {
        private List<DateTime> viajesHoy;
        private const int MINUTOS_ENTRE_VIAJES = 5;
        private const int MAX_VIAJES_POR_DIA = 2;

        public MedioBoletoEstudiantil(int saldo = 0) : base(saldo)
        {
            viajesHoy = new List<DateTime>();
        }

        public override string TipoTarjeta
        {
            get { return "Medio Boleto Estudiantil"; }
        }

        public override int CalcularMontoPasaje(int tarifaBase)
        {
            LimpiarViajesAntiguos(DateTime.Now);
            return viajesHoy.Count < MAX_VIAJES_POR_DIA ? tarifaBase / 2 : tarifaBase;
        }

        public void RegistrarViaje(DateTime fechaHoraViaje)
        {
            LimpiarViajesAntiguos(fechaHoraViaje);

            if (viajesHoy.Count >= MAX_VIAJES_POR_DIA)
            {
                throw new InvalidOperationException($"Límite de {MAX_VIAJES_POR_DIA} viajes por día alcanzado con medio boleto");
            }

            if (viajesHoy.Count > 0)
            {
                DateTime ultimoViaje = viajesHoy.Last();
                TimeSpan tiempoDesdeUltimoViaje = fechaHoraViaje - ultimoViaje;
                if (tiempoDesdeUltimoViaje.TotalMinutes < MINUTOS_ENTRE_VIAJES)
                {
                    throw new InvalidOperationException($"Deben pasar al menos {MINUTOS_ENTRE_VIAJES} minutos entre viajes con medio boleto");
                }
            }

            viajesHoy.Add(fechaHoraViaje);
        }

        public int ViajesHoy
        {
            get 
            { 
                LimpiarViajesAntiguos(DateTime.Now);
                return viajesHoy.Count; 
            }
        }

        public DateTime? UltimoViaje
        {
            get 
            { 
                LimpiarViajesAntiguos(DateTime.Now);
                return viajesHoy.Count > 0 ? viajesHoy.Last() : (DateTime?)null; 
            }
        }

        private void LimpiarViajesAntiguos(DateTime fechaReferencia)
        {
            DateTime hoy = fechaReferencia.Date; 
            viajesHoy.RemoveAll(viaje => viaje.Date < hoy);
        }

        public override bool EsFranquiciaGratuita()
        {
            return false;
        }

        public override int CalcularMontoTotalAbonado(int tarifaBase)
        {
            int montoPasaje = CalcularMontoPasaje(tarifaBase);
            if (saldo < 0)
            {
                return montoPasaje + Math.Abs(saldo);
            }
            return montoPasaje;
        }

        public int CalcularMontoPasajeEnFecha(int tarifaBase, DateTime fechaReferencia)
        {
            LimpiarViajesAntiguos(fechaReferencia);
            return viajesHoy.Count < MAX_VIAJES_POR_DIA ? tarifaBase / 2 : tarifaBase;
        }
    }
}