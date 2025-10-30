namespace TarjetaSube
{
    public class MedioBoletoEstudiantil : Tarjeta
    {
        public MedioBoletoEstudiantil(int saldo = 0) : base(saldo)
        {
        }

        public override int CalcularMontoPasaje(int tarifaBase)
        {
            return tarifaBase / 2;
        }

        public override bool EsFranquiciaGratuita()
        {
            return false;
        }
    }
}