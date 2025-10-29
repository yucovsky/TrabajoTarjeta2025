using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class ColectivoTest
    {
        private Colectivo colectivo;

        [SetUp]
        public void Setup()
        {
            colectivo = new Colectivo("132", 1234);
        }

        [Test]
        public void PagarCon_SaldoSuficiente_GeneraBoleto()
        {
            Tarjeta tarjeta = new Tarjeta(2000);

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual("132", boleto.Linea);
            Assert.AreEqual(1234, boleto.Interno);
            Assert.AreEqual(1580, boleto.Monto);
            Assert.AreEqual(420, tarjeta.Saldo);
        }

        [Test]
        public void PagarCon_SaldoInsuficiente_LanzaExcepcion()
        {
            Tarjeta tarjeta = new Tarjeta(1000);

            Assert.Throws<InvalidOperationException>(() => colectivo.PagarCon(tarjeta));
        }
    }
}