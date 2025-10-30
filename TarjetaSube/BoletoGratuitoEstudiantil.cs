namespace TarjetaSube
{
    public class BoletoGratuitoEstudiantil : Tarjeta
    {
        public BoletoGratuitoEstudiantil(int saldo = 0) : base(saldo)
        {
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
    }
}