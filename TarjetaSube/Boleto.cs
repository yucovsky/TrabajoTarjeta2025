using System;

namespace TarjetaSube
{
    public class Boleto
    {
        private string linea;
        private int interno;
        private int monto;
        private DateTime fechaHora;
        private string tipoTarjeta;
        private int saldoRestante;
        private int idTarjeta;
        private int montoTotalAbonado;

        public Boleto(string linea, int interno, int monto, DateTime fechaHora, string tipoTarjeta, int saldoRestante, int idTarjeta, int montoTotalAbonado)
        {
            this.linea = linea;
            this.interno = interno;
            this.monto = monto;
            this.fechaHora = fechaHora;
            this.tipoTarjeta = tipoTarjeta;
            this.saldoRestante = saldoRestante;
            this.idTarjeta = idTarjeta;
            this.montoTotalAbonado = montoTotalAbonado;
        }

        public string Linea
        {
            get { return linea; }
        }

        public int Interno
        {
            get { return interno; }
        }

        public int Monto
        {
            get { return monto; }
        }

        public DateTime FechaHora
        {
            get { return fechaHora; }
        }

        public string TipoTarjeta
        {
            get { return tipoTarjeta; }
        }

        public int SaldoRestante
        {
            get { return saldoRestante; }
        }

        public int IdTarjeta
        {
            get { return idTarjeta; }
        }

        public int MontoTotalAbonado
        {
            get { return montoTotalAbonado; }
        }

        public string Fecha
        {
            get { return fechaHora.ToString("dd/MM/yyyy"); }
        }

        public string Hora
        {
            get { return fechaHora.ToString("HH:mm:ss"); }
        }

        public override string ToString()
        {
            return $"Boleto - Línea: {Linea}, Interno: {Interno}, Fecha: {Fecha}, Hora: {Hora}, " +
                    $"Tipo: {TipoTarjeta}, Monto: ${Monto}, Saldo Restante: ${SaldoRestante}, " +
                    $"ID Tarjeta: {IdTarjeta}, Total Abonado: ${MontoTotalAbonado}";
        }
    }
}