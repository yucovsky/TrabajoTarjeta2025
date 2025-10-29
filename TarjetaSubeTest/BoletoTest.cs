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
            
            Boleto boleto = new Boleto("132", 1234, 1580, fechaHora);

            Assert.AreEqual("132", boleto.Linea);
            Assert.AreEqual(1234, boleto.Interno);
            Assert.AreEqual(1580, boleto.Monto);
            Assert.AreEqual(fechaHora, boleto.FechaHora);
        }
    }
}