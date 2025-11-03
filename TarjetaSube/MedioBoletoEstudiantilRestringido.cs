using System;
using System.Collections.Generic;
using System.Linq;

namespace TarjetaSube
{
    public class MedioBoletoEstudiantilRestringido : Tarjeta
    {
        private List<DateTime> viajesHoy;
        private const int MINUTOS_ENTRE_VIAJES = 5;
        private const int MAX_VIAJES_POR_DIA = 2;

        public MedioBoletoEstudiantilRestringido(int saldo = 0) : base(saldo)
        {
            viajesHoy = new List<DateTime>();
        }

        public override string TipoTarjeta
        {
            get { return "Medio Boleto Estudiantil Restringido"; }
        }

        public override int CalcularMontoPasaje(int tarifaBase)
        {
            return CalcularMontoPasajeEnFecha(tarifaBase, DateTime.Now);
        }

        public override int CalcularMontoPasajeEnFecha(int tarifaBase, DateTime fechaReferencia)
        {
            if (!ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(fechaReferencia))
            {
                return tarifaBase;
            }

            LimpiarViajesAntiguos(fechaReferencia);
            return viajesHoy.Count < MAX_VIAJES_POR_DIA ? tarifaBase / 2 : tarifaBase;
        }

        public override void RegistrarViaje(DateTime fechaHoraViaje)
        {
            if (!ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(fechaHoraViaje))
            {
                throw new InvalidOperationException("No se puede utilizar el medio boleto fuera de la franja horaria permitida");
            }

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

        public override bool PuedePagar(int monto)
        {
            if (!ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(DateTime.Now))
            {
                return base.PuedePagar(monto);
            }
            return base.PuedePagar(monto);
        }
    }
}