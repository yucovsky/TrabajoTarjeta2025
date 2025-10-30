using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class SaldoNegativoTest
    {
        private const int TARIFA = 1580;
        private const int SALDO_NEGATIVO_MAXIMO = -1200;

        [Test]
        public void Tarjeta_PermitirSaldoNegativo_DentroDelLimite()
        {
            Tarjeta tarjeta = new Tarjeta(500);

            bool primerViaje = tarjeta.IntentarPagar(TARIFA);
            Assert.IsTrue(primerViaje);
            Assert.AreEqual(500 - TARIFA, tarjeta.Saldo);

            bool segundoViaje = tarjeta.IntentarPagar(TARIFA);
            Assert.IsFalse(segundoViaje);
            Assert.AreEqual(500 - TARIFA, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_NoPermitirSaldoMenos1200()
        {
            Tarjeta tarjeta = new Tarjeta(0);

            Assert.IsFalse(tarjeta.IntentarPagar(1300));
            Assert.AreEqual(0, tarjeta.Saldo);

            Assert.IsTrue(tarjeta.IntentarPagar(1200));
            Assert.AreEqual(-1200, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_CargaConSaldoNegativo_DescuentaDeuda()
        {
            Tarjeta tarjeta = new Tarjeta(-800);
            tarjeta.Cargar(2000);
            Assert.AreEqual(1200, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_CargaPagaDeudaCompleta()
        {
            Tarjeta tarjeta = new Tarjeta(-1200);
            tarjeta.Cargar(2000);
            Assert.AreEqual(800, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_ViajesPlus_MultiplesViajesHastaLimite()
        {
            Tarjeta tarjeta = new Tarjeta(0);
            Colectivo colectivo = new Colectivo("132", 1234);

            bool primerViaje = colectivo.PagarConBoolean(tarjeta);
            Assert.IsTrue(primerViaje);
            Assert.AreEqual(-TARIFA, tarjeta.Saldo);

            bool segundoViaje = colectivo.PagarConBoolean(tarjeta);
            Assert.IsFalse(segundoViaje);
            Assert.AreEqual(-TARIFA, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_Constructor_SaldoInicialNoMenorLimite()
        {
            Assert.Throws<ArgumentException>(() => new Tarjeta(-1300));
            
            Tarjeta tarjetaEnLimite = new Tarjeta(-1200);
            Assert.AreEqual(-1200, tarjetaEnLimite.Saldo);
        }

        [Test]
        public void Colectivo_PagarCon_SaldoNegativoPermitido_GeneraBoleto()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(500);

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(500 - TARIFA, tarjeta.Saldo);
        }

        [Test]
        public void Colectivo_PagarCon_SaldoInsuficienteInclusoConNegativo_LanzaExcepcion()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(-1100);

            Assert.Throws<InvalidOperationException>(() => colectivo.PagarCon(tarjeta));
            Assert.AreEqual(-1100, tarjeta.Saldo);
        }

        [Test]
        public void Tarjeta_RecuperacionDeuda_CargaYViajesSubsiguientes()
        {
            Tarjeta tarjeta = new Tarjeta(-1000);
            Colectivo colectivo = new Colectivo("132", 1234);

            tarjeta.Cargar(3000);
            Assert.AreEqual(2000, tarjeta.Saldo);

            bool viaje = colectivo.PagarConBoolean(tarjeta);
            Assert.IsTrue(viaje);
            Assert.AreEqual(2000 - TARIFA, tarjeta.Saldo);
        }
    }
}