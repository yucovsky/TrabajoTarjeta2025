using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class TarjetaTest
    {
        private Tarjeta tarjeta;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta();
        }

        [Test]
        public void CargaTest()
        {
            tarjeta.Cargar(10000);
            tarjeta.Pagar(1580);
            Assert.AreEqual(8420, tarjeta.Saldo);
        }

        [Test]
        public void Cargar_MontosPermitidos_ActualizaSaldo()
        {
            int[] montosPermitidos = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
            
            foreach (int monto in montosPermitidos)
            {
                var tarjetaTest = new Tarjeta();
                tarjetaTest.Cargar(monto);
                Assert.AreEqual(monto, tarjetaTest.Saldo);
            }
        }

        [Test]
        public void Cargar_MontoNoPermitido_LanzaExcepcion()
        {
            Assert.Throws<ArgumentException>(() => tarjeta.Cargar(1000));
        }

        [Test]
        public void Cargar_SuperaSaldoMaximo_LanzaExcepcion()
        {
            tarjeta.Cargar(30000);
            Assert.Throws<InvalidOperationException>(() => tarjeta.Cargar(20000));
        }

        [Test]
        public void Pagar_SaldoSuficiente_ActualizaSaldo()
        {
            tarjeta.Cargar(5000);
            tarjeta.Pagar(1580);
            Assert.AreEqual(3420, tarjeta.Saldo);
        }

        [Test]
        public void Pagar_SaldoInsuficiente_LanzaExcepcion()
        {
            Tarjeta tarjetaConSaldoBajo = new Tarjeta(0);
            Assert.Throws<InvalidOperationException>(() => tarjetaConSaldoBajo.Pagar(1580));
        }

        [Test]
        public void Constructor_SaldoNegativo_LanzaExcepcion()
        {
            Assert.Throws<ArgumentException>(() => new Tarjeta(-1300));
        }

        [Test]
        public void Constructor_SaldoInicialPermitido()
        {
            Tarjeta tarjetaConSaldo = new Tarjeta(5000);
            Assert.AreEqual(5000, tarjetaConSaldo.Saldo);
        }

        [Test]
        public void Pagar_SuperaLimiteNegativo_LanzaExcepcion()
        {
            Tarjeta tarjeta = new Tarjeta(-1100);
            Assert.Throws<InvalidOperationException>(() => tarjeta.Pagar(200));
            Assert.AreEqual(-1100, tarjeta.Saldo);
        }

        [Test]
        public void PuedePagar_VerificaLimiteNegativo()
        {
            Tarjeta tarjeta = new Tarjeta(-1100);
            Assert.IsTrue(tarjeta.PuedePagar(100));
            Assert.IsFalse(tarjeta.PuedePagar(101));
        }

        [Test]
        public void Pagar_MontoCero_LanzaExcepcion()
        {
            Tarjeta tarjeta = new Tarjeta(1000);
            Assert.Throws<ArgumentException>(() => tarjeta.Pagar(0));
        }

        [Test]
        public void Pagar_MontoNegativo_LanzaExcepcion()
        {
            Tarjeta tarjeta = new Tarjeta(1000);
            Assert.Throws<ArgumentException>(() => tarjeta.Pagar(-100));
        }
    }
}