namespace TarjetaSube
{
    public class Tarjeta
    {
        protected int saldo;
        private const int SALDO_MAXIMO = 40000;
        private const int SALDO_NEGATIVO_MAXIMO = -1200;
        private static readonly int[] CARGAS_PERMITIDAS = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };

        public Tarjeta(int saldo = 0)
        {
            if (saldo < SALDO_NEGATIVO_MAXIMO)
                throw new System.ArgumentException($"El saldo inicial no puede ser menor a ${SALDO_NEGATIVO_MAXIMO}");
            
            this.saldo = saldo;
        }

        public int Saldo
        {
            get { return saldo; }
        }
        
        public void Cargar(int importe)
        {
            if (!EsCargaValida(importe))
                throw new System.ArgumentException($"La carga de ${importe} no está permitida. Cargas permitidas: {string.Join(", ", CARGAS_PERMITIDAS)}");
            
            if (saldo + importe > SALDO_MAXIMO)
                throw new System.InvalidOperationException($"No se puede superar el saldo máximo de ${SALDO_MAXIMO}");
            
            saldo += importe;
        }

        public void Pagar(int monto)
        {
            if (monto <= 0)
                throw new System.ArgumentException("El monto a pagar debe ser positivo");
            
            if (saldo - monto < SALDO_NEGATIVO_MAXIMO)
                throw new System.InvalidOperationException($"No se puede tener un saldo menor a ${SALDO_NEGATIVO_MAXIMO}");
            
            saldo -= monto;
        }

        public bool IntentarPagar(int monto)
        {
            if (monto <= 0)
                return false;
            
            if (saldo - monto < SALDO_NEGATIVO_MAXIMO)
                return false;
            
            saldo -= monto;
            return true;
        }

        public virtual bool PuedePagar(int monto)
        {
            return saldo - monto >= SALDO_NEGATIVO_MAXIMO;
        }

        public virtual int CalcularMontoPasaje(int tarifaBase)
        {
            return tarifaBase;
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