namespace TarjetaSube
{
    public class FranquiciaCompleta : Tarjeta
    {
        public FranquiciaCompleta(int saldo = 0) : base(saldo)
        {
        }

        public override string TipoTarjeta
        {
            get { return "Franquicia Completa"; }
        }

        public override int CalcularMontoPasaje(int tarifaBase)
        {
            return 0;
        }

        public override bool PuedePagar(int monto)
        {
            return true;
        }

        public override bool EsFranquiciaGratuita()
        {
            return true;
        }

        public override int CalcularMontoTotalAbonado(int tarifaBase)
        {
            return 0;
        }
    }
}