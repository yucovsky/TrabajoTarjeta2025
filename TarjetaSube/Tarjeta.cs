﻿using System;

namespace TarjetaSube
{
    public class Tarjeta
    {
        protected int saldo;
        private const int SALDO_MAXIMO = 40000;
        private const int SALDO_NEGATIVO_MAXIMO = -1200;
        private static readonly int[] CARGAS_PERMITIDAS = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
        private static int nextId = 1;
        private int id;

        public Tarjeta(int saldo = 0)
        {
            if (saldo < SALDO_NEGATIVO_MAXIMO)
                throw new ArgumentException($"El saldo inicial no puede ser menor a ${SALDO_NEGATIVO_MAXIMO}");
            
            this.saldo = saldo;
            this.id = nextId++;
        }

        public int Id
        {
            get { return id; }
        }

        public int Saldo
        {
            get { return saldo; }
        }

        public virtual string TipoTarjeta
        {
            get { return "Normal"; }
        }
        
        public void Cargar(int importe)
        {
            if (!EsCargaValida(importe))
                throw new ArgumentException($"La carga de ${importe} no está permitida. Cargas permitidas: {string.Join(", ", CARGAS_PERMITIDAS)}");
            
            if (saldo + importe > SALDO_MAXIMO)
                throw new InvalidOperationException($"No se puede superar el saldo máximo de ${SALDO_MAXIMO}");
            
            saldo += importe;
        }

        public void Pagar(int monto)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto a pagar debe ser positivo");
            
            if (!PuedePagar(monto))
                throw new InvalidOperationException("Saldo insuficiente para realizar el pago");
            
            saldo -= monto;
        }

        public bool IntentarPagar(int monto)
        {
            if (monto <= 0)
                return false;
            
            if (!PuedePagar(monto))
                return false;
            
            saldo -= monto;
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
    }
}