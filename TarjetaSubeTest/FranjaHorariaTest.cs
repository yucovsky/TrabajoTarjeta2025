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

            DateTime finDeSemana = new DateTime(2024, 10, 26, 10, 0, 0); // Sábado
            DateTime fueraHorario = new DateTime(2024, 10, 25, 5, 0, 0); // Viernes 5 AM
            DateTime noche = new DateTime(2024, 10, 25, 22, 0, 0); // Viernes 10 PM

            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, finDeSemana));
            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, fueraHorario));
            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, noche));
        }

        [Test]
        public void MedioBoletoEstudiantil_DentroFranjaHoraria_PermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);

            DateTime horarioValido1 = new DateTime(2024, 10, 25, 6, 0, 0); // Viernes 6 AM
            DateTime horarioValido2 = new DateTime(2024, 10, 25, 14, 0, 0); // Viernes 2 PM
            DateTime horarioValido3 = new DateTime(2024, 10, 25, 21, 59, 0); // Viernes 9:59 PM

            Boleto boleto1 = colectivo.PagarConEnFecha(tarjeta, horarioValido1);
            Boleto boleto2 = colectivo.PagarConEnFecha(tarjeta, horarioValido2);
            
            Assert.AreEqual(TARIFA_BASICA / 2, boleto1.Monto);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto2.Monto);
        }

        [Test]
        public void BoletoGratuitoEstudiantil_FueraFranjaHoraria_NoPermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil(5000);

            DateTime finDeSemana = new DateTime(2024, 10, 26, 10, 0, 0); // Sábado
            DateTime fueraHorario = new DateTime(2024, 10, 25, 5, 0, 0); // Viernes 5 AM
            DateTime noche = new DateTime(2024, 10, 25, 22, 0, 0); // Viernes 10 PM

            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, finDeSemana));
            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, fueraHorario));
            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, noche));
        }

        [Test]
        public void FranquiciaCompleta_FueraFranjaHoraria_NoPermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta(5000);

            DateTime finDeSemana = new DateTime(2024, 10, 26, 10, 0, 0); // Sábado
            DateTime fueraHorario = new DateTime(2024, 10, 25, 5, 0, 0); // Viernes 5 AM
            DateTime noche = new DateTime(2024, 10, 25, 22, 0, 0); // Viernes 10 PM

            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, finDeSemana));
            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, fueraHorario));
            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, noche));
        }

        [Test]
        public void TarjetaNormal_FueraFranjaHoraria_PermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(5000);

            DateTime finDeSemana = new DateTime(2024, 10, 26, 10, 0, 0); // Sábado
            DateTime fueraHorario = new DateTime(2024, 10, 25, 5, 0, 0); // Viernes 5 AM
            DateTime noche = new DateTime(2024, 10, 25, 22, 0, 0); // Viernes 10 PM

            Boleto boleto1 = colectivo.PagarConEnFecha(tarjeta, finDeSemana);
            Boleto boleto2 = colectivo.PagarConEnFecha(tarjeta, fueraHorario);
            Boleto boleto3 = colectivo.PagarConEnFecha(tarjeta, noche);
            
            Assert.AreEqual(TARIFA_BASICA, boleto1.Monto);
            Assert.AreEqual(TARIFA_BASICA, boleto2.Monto);
            Assert.AreEqual(TARIFA_BASICA, boleto3.Monto);
        }
    }
}