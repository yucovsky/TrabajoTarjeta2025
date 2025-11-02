using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class SaldoPendienteTest
    {
        [Test]
        public void Tarjeta_Cargar_SuperaLimiteMaximo_AcreditaParcialmenteYGuardaPendiente()
        {
            Tarjeta tarjeta = new Tarjeta(54000);
            
            tarjeta.Cargar(3000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(1000, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Tarjeta_Cargar_SinEspacioDisponible_GuardaTodoComoPendiente()
        {
            Tarjeta tarjeta = new Tarjeta(56000);
            
            tarjeta.Cargar(5000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(5000, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Tarjeta_Pagar_ConSaldoPendiente_AcreditaAutomaticamente()
        {
            Tarjeta tarjeta = new Tarjeta(56000);
            tarjeta.Cargar(5000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(5000, tarjeta.SaldoPendiente);
            
            tarjeta.Pagar(1580);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(5000 - 1580, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Tarjeta_IntentarPagar_ConSaldoPendiente_AcreditaAutomaticamente()
        {
            Tarjeta tarjeta = new Tarjeta(56000);
            tarjeta.Cargar(3000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(3000, tarjeta.SaldoPendiente);
            
            bool resultado = tarjeta.IntentarPagar(2000);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(3000 - 2000, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Tarjeta_AcreditarCarga_Manual_AcreditaHastaMaximo()
        {
            Tarjeta tarjeta = new Tarjeta(55000);
            tarjeta.Cargar(2000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(1000, tarjeta.SaldoPendiente);
            
            // Primero pagar sin acreditar automáticamente (simulamos)
            tarjeta.Pagar(3000);
            
            // Después llamar manualmente a AcreditarCarga
            tarjeta.AcreditarCarga();
            
            Assert.AreEqual(56000 - 3000 + 1000, tarjeta.Saldo);
            Assert.AreEqual(0, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Tarjeta_Cargar_MultiplesVeces_AcumulaSaldoPendienteCorrectamente()
        {
            Tarjeta tarjeta = new Tarjeta(55000);
            
            tarjeta.Cargar(2000);
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(1000, tarjeta.SaldoPendiente);
            
            tarjeta.Cargar(3000);
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(4000, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Tarjeta_Pagar_MultiplesVeces_AcreditaPendienteProgresivamente()
        {
            Tarjeta tarjeta = new Tarjeta(56000);
            tarjeta.Cargar(10000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(10000, tarjeta.SaldoPendiente);
            
            tarjeta.Pagar(1580);
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(10000 - 1580, tarjeta.SaldoPendiente);
            
            tarjeta.Pagar(1580);
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(10000 - 1580 - 1580, tarjeta.SaldoPendiente);
            
            tarjeta.Pagar(1580);
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(10000 - 1580 - 1580 - 1580, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Colectivo_PagarCon_ConSaldoPendiente_AcreditaAutomaticamente()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(56000);
            tarjeta.Cargar(5000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(5000, tarjeta.SaldoPendiente);
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto);
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(5000 - 1580, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Colectivo_PagarConBoolean_ConSaldoPendiente_AcreditaAutomaticamente()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(56000);
            tarjeta.Cargar(3000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(3000, tarjeta.SaldoPendiente);
            
            bool resultado = colectivo.PagarConBoolean(tarjeta);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(3000 - 1580, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Tarjeta_Cargar_ExactamenteAlLimite_NoGeneraPendiente()
        {
            Tarjeta tarjeta = new Tarjeta(54000);
            
            tarjeta.Cargar(2000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(0, tarjeta.SaldoPendiente);
        }

        [Test]
        public void Tarjeta_AcreditarCarga_Parcialmente_CuandoNoHayEspacioSuficiente()
        {
            Tarjeta tarjeta = new Tarjeta(55500);
            tarjeta.Cargar(2000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(1500, tarjeta.SaldoPendiente);
            
            tarjeta.Pagar(1000);
            
            Assert.AreEqual(56000, tarjeta.Saldo);
            Assert.AreEqual(1500 - 1000, tarjeta.SaldoPendiente);
        }
    }
}