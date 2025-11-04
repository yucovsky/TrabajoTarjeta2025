using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class FranjaHorariaTest
    {
        private const int TARIFA_BASICA = 1580;

        [Test]
        public void MedioBoletoEstudiantil_FueraFranjaHoraria_NoPermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);

            DateTime finDeSemana = new DateTime(2024, 10, 26, 10, 0, 0);
            Boleto boleto = colectivo.PagarConEnFecha(tarjeta, finDeSemana);
            
            Assert.IsNotNull(boleto);
        }

        [Test]
        public void TarjetaNormal_FueraFranjaHoraria_PermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(5000);

            DateTime finDeSemana = new DateTime(2024, 10, 26, 10, 0, 0); 
            Boleto boleto = colectivo.PagarConEnFecha(tarjeta, finDeSemana);
            
            Assert.AreEqual(TARIFA_BASICA, boleto.Monto);
        }
    }
}