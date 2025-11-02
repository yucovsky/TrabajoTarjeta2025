using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class FranquiciaTest
    {
        private const int TARIFA_BASICA = 1580;

        [Test]
        public void FranquiciaCompleta_SiemprePuedePagar()
        {
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            Colectivo colectivo = new Colectivo("132", 1234);

            bool resultado = colectivo.PagarConBoolean(tarjeta);

            Assert.IsTrue(resultado);
            Assert.AreEqual(0, tarjeta.Saldo);
        }

        [Test]
        public void FranquiciaCompleta_MultiplesViajes_SiempreGratis()
        {
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            Colectivo colectivo = new Colectivo("132", 1234);
        
            bool primerResultado = colectivo.PagarConBoolean(tarjeta);
            Assert.IsTrue(primerResultado);
            Assert.AreEqual(0, tarjeta.Saldo);
        
            bool segundoResultado = colectivo.PagarConBoolean(tarjeta);
            Assert.IsTrue(segundoResultado);
            Assert.AreEqual(0, tarjeta.Saldo);
            
            bool tercerResultado = colectivo.PagarConBoolean(tarjeta);
        }

        [Test]
        public void MedioBoletoEstudiantil_PagaMitad()
        {
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(1000);
            Colectivo colectivo = new Colectivo("132", 1234);

            int montoEsperado = TARIFA_BASICA / 2;
            bool resultado = colectivo.PagarConBoolean(tarjeta);

            Assert.IsTrue(resultado);
            Assert.AreEqual(1000 - montoEsperado, tarjeta.Saldo);
        }

        [Test]
        public void MedioBoletoEstudiantil_MultiplesViajes_SiempreMitad()
        {
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);
            Colectivo colectivo = new Colectivo("132", 1234);
            
            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 6, 0); 

            Boleto boleto1 = colectivo.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto1.Monto);

            Boleto boleto2 = colectivo.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto2.Monto);
        }

        [Test]
        public void BoletoGratuitoEstudiantil_SiempreGratis()
        {
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil(1000);
            Colectivo colectivo = new Colectivo("132", 1234);

            bool resultado = colectivo.PagarConBoolean(tarjeta);

            Assert.IsTrue(resultado);
            Assert.AreEqual(1000, tarjeta.Saldo);
        }

        [Test]
        public void TarjetaNormal_PagaMontoCompleto()
        {
            Tarjeta tarjeta = new Tarjeta(2000);
            Colectivo colectivo = new Colectivo("132", 1234);

            bool resultado = colectivo.PagarConBoolean(tarjeta);

            Assert.IsTrue(resultado);
            Assert.AreEqual(2000 - TARIFA_BASICA, tarjeta.Saldo);
        }

        [Test]
        public void MedioBoletoEstudiantil_CalculaMontoCorrecto()
        {
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil();
            int montoCalculado = tarjeta.CalcularMontoPasaje(TARIFA_BASICA);

            Assert.AreEqual(TARIFA_BASICA / 2, montoCalculado);
        }

        [Test]
        public void FranquiciaCompleta_CalculaMontoCero()
        {
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            int montoCalculado = tarjeta.CalcularMontoPasaje(TARIFA_BASICA);

            Assert.AreEqual(0, montoCalculado);
        }

        [Test]
        public void BoletoGratuitoEstudiantil_CalculaMontoCero()
        {
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil();
            int montoCalculado = tarjeta.CalcularMontoPasaje(TARIFA_BASICA);

            Assert.AreEqual(0, montoCalculado);
        }

        [Test]
        public void FranquiciaCompleta_SaldoNegativo_PuedePagar()
        {
            FranquiciaCompleta tarjeta = new FranquiciaCompleta(-1200);
            bool puedePagar = tarjeta.PuedePagar(TARIFA_BASICA);

            Assert.IsTrue(puedePagar);
        }

        [Test]
        public void BoletoGratuitoEstudiantil_SaldoNegativo_PuedePagar()
        {
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil(-1200);
            bool puedePagar = tarjeta.PuedePagar(TARIFA_BASICA);

            Assert.IsTrue(puedePagar);
        }
    }
}