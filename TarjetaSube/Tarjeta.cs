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
        protected List<DateTime> viajes;

        public Tarjeta(int saldo = 0)
        {
            if (saldo < SALDO_NEGATIVO_MAXIMO)
                throw new ArgumentException($"El saldo inicial no puede ser menor a ${SALDO_NEGATIVO_MAXIMO}");
            
            this.saldo = saldo;
            this.saldoPendiente = 0;
            this.id = nextId++;
            this.viajes = new List<DateTime>();
        }

        public int Id
        {
            get { return id; }
        }

        public int Saldo
        {
            get { return saldo; }
        }

        public int SaldoPendiente
        {
            get { return saldoPendiente; }
        }

        public int ViajesEsteMes
        {
            get 
            { 
                return ObtenerViajesDelMes(DateTime.Now).Count; 
            }
        }

        public int ViajesEnMes(DateTime fecha)
        {
            return ObtenerViajesDelMes(fecha).Count;
        }

        public virtual string TipoTarjeta
        {
            get { return "Normal"; }
        }
        
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
            return CalcularMontoPasajeEnFecha(tarifaBase, DateTime.Now);
        }

        public virtual int CalcularMontoPasajeEnFecha(int tarifaBase, DateTime fechaReferencia)
        {
            if (!EsFranquiciaGratuita())
            {
                List<DateTime> viajesDelMes = ObtenerViajesDelMes(fechaReferencia);
                int monto = CalcularMontoConDescuento(tarifaBase, viajesDelMes.Count);
                return monto;
            }
            return tarifaBase;
        }

        private int CalcularMontoConDescuento(int tarifaBase, int cantidadViajesPrevios)
        {

            if (cantidadViajesPrevios <= 28)
            {
                return tarifaBase;
            }

            if (cantidadViajesPrevios >= 29 && cantidadViajesPrevios <= 58)
            {
                int montoConDescuento = (int)(tarifaBase * 0.8);
                return montoConDescuento;
            }

            if (cantidadViajesPrevios >= 59 && cantidadViajesPrevios <= 79)
            {
                int montoConDescuento = (int)(tarifaBase * 0.75);
                return montoConDescuento;
            }
            
            return tarifaBase;
        }

        public virtual void RegistrarViaje(DateTime fechaViaje)
        {
            viajes.Add(fechaViaje);
        }

        private List<DateTime> ObtenerViajesDelMes(DateTime fechaReferencia)
        {
            DateTime primerDiaDelMes = new DateTime(fechaReferencia.Year, fechaReferencia.Month, 1);
            DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);
            
            return viajes.Where(v => v >= primerDiaDelMes && v <= ultimoDiaDelMes).ToList();
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

        private bool EsCargaValida(int importe)
        {
            foreach (int carga in CARGAS_PERMITIDAS)
            {
                if (carga == importe)
                    return true;
            }
            return false;
        }

        public virtual bool PuedePagarEnFecha(int monto, DateTime fechaSimulada)
        {
            return PuedePagar(monto);
        }
    }
}