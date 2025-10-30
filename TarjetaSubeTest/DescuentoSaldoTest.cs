using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class DescuentoSaldoTest
    {
        [Test]
        public void Colectivo_PagarConBoolean_SaldoSuficiente_DevuelveTrue()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(2000);

            bool resultado = colectivo.PagarConBoolean(tarjeta);

            Assert.IsTrue(resultado);
            Assert.AreEqual(420, tarjeta.Saldo);
        }

        [Test]
        public void Colectivo_PagarConBoolean_SaldoInsuficiente_DevuelveFalse()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(1000);

            bool resultado = colectivo.PagarConBoolean(tarjeta);

            Assert.IsFalse(resultado);
            Assert.AreEqual(1000, tarjeta.Saldo);
        }

        [Test]
        public void Colectivo_PagarConBoolean_SaldoExacto_DevuelveTrue()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(1580);

            bool resultado = colectivo.PagarConBoolean(tarjeta);

            Assert.IsTrue(resultado);
            Assert.AreEqual(0, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_IntentarPagar_SaldoSuficiente_DevuelveTrueYDescuenta()
        {
            Tarjeta tarjeta = new Tarjeta(3000);
            bool resultado = tarjeta.IntentarPagar(1580);

            Assert.IsTrue(resultado);
            Assert.AreEqual(1420, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_IntentarPagar_SaldoInsuficiente_DevuelveFalseYNoDescuenta()
        {
            Tarjeta tarjeta = new Tarjeta(1000);
            bool resultado = tarjeta.IntentarPagar(1580);

            Assert.IsFalse(resultado);
            Assert.AreEqual(1000, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_IntentarPagar_MontoCero_DevuelveFalse()
        {
            Tarjeta tarjeta = new Tarjeta(2000);
            bool resultado = tarjeta.IntentarPagar(0);

            Assert.IsFalse(resultado);
            Assert.AreEqual(2000, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_IntentarPagar_MontoNegativo_DevuelveFalse()
        {
            Tarjeta tarjeta = new Tarjeta(2000);
            bool resultado = tarjeta.IntentarPagar(-100);

            Assert.IsFalse(resultado);
            Assert.AreEqual(2000, tarjeta.Saldo);
        }

        [Test]
        public void Colectivo_PagarConBoolean_MultiplesViajes_DescuentaCorrectamente()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(5000);

            bool primerViaje = colectivo.PagarConBoolean(tarjeta);
            Assert.IsTrue(primerViaje);
            Assert.AreEqual(3420, tarjeta.Saldo);

            bool segundoViaje = colectivo.PagarConBoolean(tarjeta);
            Assert.IsTrue(segundoViaje);
            Assert.AreEqual(1840, tarjeta.Saldo);

            bool tercerViaje = colectivo.PagarConBoolean(tarjeta);
            Assert.IsTrue(tercerViaje);
            Assert.AreEqual(260, tarjeta.Saldo);

            bool cuartoViaje = colectivo.PagarConBoolean(tarjeta);
            Assert.IsFalse(cuartoViaje);
            Assert.AreEqual(260, tarjeta.Saldo);
        }
    }
}