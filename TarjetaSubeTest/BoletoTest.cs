using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class BoletoTest
    {
        [Test]
        public void Boleto_Creacion_PropiedadesCorrectas()
        {
            DateTime fechaHora = DateTime.Now;
            
            Boleto boleto = new Boleto("132", 1234, 1580, fechaHora, "Normal", 420, 1, 1580);

            Assert.AreEqual("132", boleto.Linea);
            Assert.AreEqual(1234, boleto.Interno);
            Assert.AreEqual(1580, boleto.Monto);
            Assert.AreEqual(fechaHora, boleto.FechaHora);
            Assert.AreEqual("Normal", boleto.TipoTarjeta);
            Assert.AreEqual(420, boleto.SaldoRestante);
            Assert.AreEqual(1, boleto.IdTarjeta);
            Assert.AreEqual(1580, boleto.MontoTotalAbonado);
        }
    }
}