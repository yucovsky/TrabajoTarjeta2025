using System;

namespace TarjetaSube
{
    public class Boleto
    {
        public string Linea { get; }
        public int Interno { get; }
        public int Monto { get; }
        public DateTime FechaHora { get; }
        public string TipoTarjeta { get; }
        public int SaldoRestante { get; }
        public int IdTarjeta { get; }
        public int MontoTotalAbonado { get; }
        public bool EsTrasbordo { get; }

        public Boleto(string linea, int interno, int monto, DateTime fechaHora, 
                    string tipoTarjeta, int saldoRestante, int idTarjeta, 
                    int montoTotalAbonado, bool esTrasbordo = false)
        {
            Linea = linea;
            Interno = interno;
            Monto = monto;
            FechaHora = fechaHora;
            TipoTarjeta = tipoTarjeta;
            SaldoRestante = saldoRestante;
            IdTarjeta = idTarjeta;
            MontoTotalAbonado = montoTotalAbonado;
            EsTrasbordo = esTrasbordo;
        }

        public string Fecha => FechaHora.ToString("dd/MM/yyyy");
        public string Hora => FechaHora.ToString("HH:mm:ss");

        public override string ToString()
        {
            string trasbordoInfo = EsTrasbordo ? " (TRASBORDO)" : "";
            return $"Línea: {Linea}, Interno: {Interno}, Tipo: {TipoTarjeta}, " +
                    $"Monto: ${Monto}{trasbordoInfo}, Saldo Restante: ${SaldoRestante}";
        }
    }
}