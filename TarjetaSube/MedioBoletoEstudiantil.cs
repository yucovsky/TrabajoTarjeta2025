using System;


namespace TarjetaSube
{
    public class MedioBoletoEstudiantil : Tarjeta
    {
        public MedioBoletoEstudiantil(int saldo = 0) : base(saldo)
        {
        }

        public override string TipoTarjeta
        {
            get { return "Medio Boleto Estudiantil"; }
        }

        public override int CalcularMontoPasaje(int tarifaBase)
        {
            return tarifaBase / 2;
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
    }
}