using System;

namespace TarjetaSube
{
    public class Tarjeta
    {
        private int saldo;
        private const int SALDO_MAXIMO = 40000;
        private static readonly int[] CARGAS_PERMITIDAS = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };

        public Tarjeta(int saldo = 0)
        {
            if (saldo < 0)
                throw new ArgumentException("El saldo inicial no puede ser negativo");
            
            this.saldo = saldo;
        }

        public int Saldo
        {
            get { return saldo; }
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
            
            if (saldo < monto)
                throw new InvalidOperationException("Saldo insuficiente para realizar el pago");
            
            saldo -= monto;
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