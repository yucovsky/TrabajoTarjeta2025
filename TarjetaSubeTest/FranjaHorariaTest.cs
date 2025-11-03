using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class FranjaHorariaTest
    {
        private const int TARIFA_BASICA = 1580;

        [Test]
        public void MedioBoletoEstudiantilRestringido_FueraFranjaHoraria_NoPermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            MedioBoletoEstudiantilRestringido tarjeta = new MedioBoletoEstudiantilRestringido(5000);

            DateTime fueraFranja = new DateTime(2024, 11, 2, 23, 0, 0);

            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, fueraFranja));
        }

        [Test]
        public void MedioBoletoEstudiantilRestringido_FueraFranjaHoraria_CobraTarifaCompleta()
        {
            MedioBoletoEstudiantilRestringido tarjeta = new MedioBoletoEstudiantilRestringido(5000);

            DateTime fueraFranja = new DateTime(2024, 11, 3, 5, 0, 0);

            int monto = tarjeta.CalcularMontoPasaje(TARIFA_BASICA);
            Assert.AreEqual(TARIFA_BASICA, monto);
        }

        [Test]
        public void MedioBoletoEstudiantilRestringido_DentroFranjaHoraria_PermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            MedioBoletoEstudiantilRestringido tarjeta = new MedioBoletoEstudiantilRestringido(5000);

            DateTime dentroFranja = new DateTime(2024, 10, 28, 10, 0, 0);

            Boleto boleto = colectivo.PagarConEnFecha(tarjeta, dentroFranja);
            Assert.IsNotNull(boleto);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto.Monto);
        }

        [Test]
        public void BoletoGratuitoEstudiantilRestringido_FueraFranjaHoraria_NoPermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            BoletoGratuitoEstudiantilRestringido tarjeta = new BoletoGratuitoEstudiantilRestringido(5000);

            DateTime fueraFranja = new DateTime(2024, 11, 1, 23, 0, 0);

            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, fueraFranja));
        }

        [Test]
        public void FranquiciaCompletaRestringida_FueraFranjaHoraria_NoPermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            FranquiciaCompletaRestringida tarjeta = new FranquiciaCompletaRestringida(5000);

            DateTime fueraFranja = new DateTime(2024, 11, 3, 5, 59, 0);

            Assert.Throws<InvalidOperationException>(() => colectivo.PagarConEnFecha(tarjeta, fueraFranja));
        }

        [Test]
        public void TodasFranquiciasRestringidas_DentroFranjaHoraria_PermitenViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);

            DateTime dentroFranja = new DateTime(2024, 10, 30, 15, 0, 0);

            MedioBoletoEstudiantilRestringido medioBoleto = new MedioBoletoEstudiantilRestringido(5000);
            Boleto boletoMedio = colectivo.PagarConEnFecha(medioBoleto, dentroFranja);
            Assert.IsNotNull(boletoMedio);

            BoletoGratuitoEstudiantilRestringido gratuito = new BoletoGratuitoEstudiantilRestringido(5000);
            Boleto boletoGratuito = colectivo.PagarConEnFecha(gratuito, dentroFranja);
            Assert.IsNotNull(boletoGratuito);

            FranquiciaCompletaRestringida completa = new FranquiciaCompletaRestringida(5000);
            Boleto boletoCompleta = colectivo.PagarConEnFecha(completa, dentroFranja);
            Assert.IsNotNull(boletoCompleta);
        }

        [Test]
        public void ValidadorFranjaHoraria_CasosBorde()
        {
            Assert.IsTrue(ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(new DateTime(2024, 10, 28, 6, 0, 0)));

            Assert.IsTrue(ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(new DateTime(2024, 11, 1, 21, 59, 0)));

            Assert.IsFalse(ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(new DateTime(2024, 11, 1, 22, 0, 0)));

            Assert.IsFalse(ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(new DateTime(2024, 11, 2, 10, 0, 0)));

            Assert.IsFalse(ValidadorFranjaHoraria.EstaEnFranjaHorariaPermitida(new DateTime(2024, 11, 3, 18, 0, 0)));
        }

        [Test]
        public void TarjetaNormal_FueraFranjaHoraria_FuncionaNormalmente()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjetaNormal = new Tarjeta(5000);

            DateTime fueraFranja = new DateTime(2024, 11, 3, 2, 0, 0);

            Boleto boleto = colectivo.PagarConEnFecha(tarjetaNormal, fueraFranja);
            Assert.IsNotNull(boleto);
            Assert.AreEqual(TARIFA_BASICA, boleto.Monto);
        }
    }
}