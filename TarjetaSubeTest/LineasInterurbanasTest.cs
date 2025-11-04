using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class LineasInterurbanasTest
    {
        [Test]
        public void Colectivo_Interurbano_TarifaCorrecta()
        {
            Colectivo colectivoInterurbano = new Colectivo("33", 1234, true);
            Tarjeta tarjeta = new Tarjeta(5000);

            Boleto boleto = colectivoInterurbano.PagarCon(tarjeta);

            Assert.AreEqual(3000, boleto.Monto);
            Assert.AreEqual(2000, tarjeta.Saldo);
        }

        [Test]
        public void Colectivo_Urbano_TarifaCorrecta()
        {
            Colectivo colectivoUrbano = new Colectivo("132", 1234, false);
            Tarjeta tarjeta = new Tarjeta(5000);

            Boleto boleto = colectivoUrbano.PagarCon(tarjeta);

            Assert.AreEqual(1580, boleto.Monto);
            Assert.AreEqual(3420, tarjeta.Saldo);
        }

        [Test]
        public void Colectivo_ConstructorPorDefecto_EsUrbano()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            
            Assert.IsFalse(colectivo.EsInterurbano);
        }

        [Test]
        public void MedioBoletoEstudiantil_Interurbano_MitadDeTarifaInterurbana()
        {
            Colectivo colectivoInterurbano = new Colectivo("33", 1234, true);
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);

            Boleto boleto = colectivoInterurbano.PagarCon(tarjeta);

            Assert.AreEqual(1500, boleto.Monto); // 3000 / 2 = 1500
            Assert.AreEqual(3500, tarjeta.Saldo);
        }

        [Test]
        public void Colectivo_Interurbano_PropiedadesCorrectas()
        {
            Colectivo colectivo = new Colectivo("33", 1234, true);
            
            Assert.AreEqual("33", colectivo.Linea);
            Assert.AreEqual(1234, colectivo.Interno);
            Assert.IsTrue(colectivo.EsInterurbano);
        }

        [Test]
        public void Colectivo_Urbano_PropiedadesCorrectas()
        {
            Colectivo colectivo = new Colectivo("132", 5678, false);
            
            Assert.AreEqual("132", colectivo.Linea);
            Assert.AreEqual(5678, colectivo.Interno);
            Assert.IsFalse(colectivo.EsInterurbano);
        }
    }
}