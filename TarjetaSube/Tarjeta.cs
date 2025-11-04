using System;
using System.Collections.Generic;
using System.Linq;

namespace TarjetaSube
{
    public class Tarjeta
    {
        protected int saldo;
        private int saldoPendiente;
        private const int SALDO_MAXIMO = 56000;
        private const int SALDO_NEGATIVO_MAXIMO = -1200;
        private static readonly int[] CARGAS_PERMITIDAS = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
        private static int nextId = 1;
        private int id;
        private List<DateTime> viajesMes;
        private List<Viaje> historialViajes;

        public Tarjeta(int saldo = 0)
        {
            if (saldo < SALDO_NEGATIVO_MAXIMO)
                throw new ArgumentException($"El saldo inicial no puede ser menor a ${SALDO_NEGATIVO_MAXIMO}");
            
            this.saldo = saldo;
            this.saldoPendiente = 0;
            this.id = nextId++;
            this.viajesMes = new List<DateTime>();
            this.historialViajes = new List<Viaje>();
        }

        public int Id => id;
        public int Saldo => saldo;
        public int SaldoPendiente => saldoPendiente;

        public virtual string TipoTarjeta => "Normal";
        
        public void Cargar(int importe)
        {
            if (!EsCargaValida(importe))
                throw new ArgumentException($"La carga de ${importe} no está permitida. Cargas permitidas: {string.Join(", ", CARGAS_PERMITIDAS)}");
            
            int espacioDisponible = SALDO_MAXIMO - saldo;
            
            if (espacioDisponible > 0)
            {
                int montoAAcreditar = Math.Min(importe, espacioDisponible);
                saldo += montoAAcreditar;
                saldoPendiente += (importe - montoAAcreditar);
            }
            else
            {
                saldoPendiente += importe;
            }
        }

        public void AcreditarCarga()
        {
            if (saldoPendiente > 0 && saldo < SALDO_MAXIMO)
            {
                int espacioDisponible = SALDO_MAXIMO - saldo;
                int montoAAcreditar = Math.Min(saldoPendiente, espacioDisponible);
                saldo += montoAAcreditar;
                saldoPendiente -= montoAAcreditar;
            }
        }

        public void Pagar(int monto)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto a pagar debe ser positivo");
            
            if (!PuedePagar(monto))
                throw new InvalidOperationException("Saldo insuficiente para realizar el pago");
            
            saldo -= monto;
            AcreditarCarga();
        }

        public bool IntentarPagar(int monto)
        {
            if (monto <= 0)
                return false;
            
            if (!PuedePagar(monto))
                return false;
            
            saldo -= monto;
            AcreditarCarga();
            return true;
        }

        public virtual bool PuedePagar(int monto)
        {
            int saldoDespuesDePago = saldo - monto;
            return saldoDespuesDePago >= SALDO_NEGATIVO_MAXIMO;
        }

        public virtual int CalcularMontoPasaje(int tarifaBase)
        {
            return tarifaBase;
        }

        public virtual int CalcularMontoPasajeEnFecha(int tarifaBase, DateTime fechaReferencia)
        {
            return tarifaBase;
        }

        public virtual bool EsFranquiciaGratuita()
        {
            return false;
        }

        public virtual int CalcularMontoTotalAbonado(int tarifaBase)
        {
            int montoPasaje = CalcularMontoPasaje(tarifaBase);
            
            if (saldo < 0)
            {
                return montoPasaje + Math.Abs(saldo);
            }

            return montoPasaje;
        }

        public virtual void RegistrarViaje(DateTime fechaViaje)
        {
            viajesMes.Add(fechaViaje);
        }

        public virtual int ViajesEnMes(DateTime fechaReferencia)
        {
            return viajesMes.Count(v => v.Month == fechaReferencia.Month && v.Year == fechaReferencia.Year);
        }

        public void RegistrarViajeConLinea(DateTime fechaViaje, string linea)
        {
            historialViajes.Add(new Viaje(fechaViaje, linea));
            viajesMes.Add(fechaViaje);
        }

        public bool PuedeRealizarTrasbordo(DateTime fechaHora, string nuevaLinea)
        {
            LimpiarViajesAntiguos(fechaHora);

            if (!EsDiaYHoraValidoParaTrasbordo(fechaHora))
                return false;

            var viajesRecientes = historialViajes
                .Where(v => (fechaHora - v.FechaHora).TotalHours <= 1)
                .OrderByDescending(v => v.FechaHora)
                .ToList();

            if (!viajesRecientes.Any())
                return false;

            var ultimoViaje = viajesRecientes.First();
            return ultimoViaje.Linea != nuevaLinea;
        }

        private void LimpiarViajesAntiguos(DateTime fechaReferencia)
        {
            historialViajes.RemoveAll(v => (fechaReferencia - v.FechaHora).TotalHours > 1);
        }

        private bool EsDiaYHoraValidoParaTrasbordo(DateTime fechaHora)
        {
            DayOfWeek dia = fechaHora.DayOfWeek;
            int hora = fechaHora.Hour;

            if (dia == DayOfWeek.Sunday)
                return false;

            return hora >= 7 && hora < 22;
        }

        private bool EsCargaValida(int importe)
        {
            foreach (int carga in CARGAS_PERMITIDAS)
            {
                if (carga == importe)
                    return true;
            }
            return false;
        }

        private class Viaje
        {
            public DateTime FechaHora { get; }
            public string Linea { get; }

            public Viaje(DateTime fechaHora, string linea)
            {
                FechaHora = fechaHora;
                Linea = linea;
            }
        }
    }
}