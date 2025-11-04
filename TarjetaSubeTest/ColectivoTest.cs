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
            Tarjeta tarjeta = new Tarjeta(500);

            Tarjeta tarjetaSinSaldo = new Tarjeta(0);
            Assert.Throws<InvalidOperationException>(() => colectivo.PagarCon(tarjetaSinSaldo));
        }

        [Test]
        public void Colectivo_LineaEInterno_GetterFuncionan()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            
            Assert.AreEqual("132", colectivo.Linea);
            Assert.AreEqual(1234, colectivo.Interno);
        }

        [Test]
        public void Colectivo_PagarConEnFecha_TarjetaNormal_FuncionaCorrectamente()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(2000);
            DateTime fechaEspecifica = new DateTime(2024, 10, 30, 14, 30, 0);

            Boleto boleto = colectivo.PagarConEnFecha(tarjeta, fechaEspecifica);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(fechaEspecifica, boleto.FechaHora);
            Assert.AreEqual(420, tarjeta.Saldo);
        }

        [Test]
        public void Colectivo_Interurbano_Constructor_Y_Propiedades()
        {
            Colectivo colectivo = new Colectivo("33", 1234, true);
            
            Assert.AreEqual("33", colectivo.Linea);
            Assert.AreEqual(1234, colectivo.Interno);
            Assert.IsTrue(colectivo.EsInterurbano);
        }
        
        [Test]
        public void Colectivo_Urbano_Constructor_Y_Propiedades()
        {
            Colectivo colectivo = new Colectivo("132", 5678, false);
            
            Assert.AreEqual("132", colectivo.Linea);
            Assert.AreEqual(5678, colectivo.Interno);
            Assert.IsFalse(colectivo.EsInterurbano);
        }
    }
}