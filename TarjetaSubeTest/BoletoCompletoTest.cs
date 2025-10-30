using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class BoletoCompletoTest
    {
        private const int TARIFA_BASICA = 1580;

        [Test]
        public void Boleto_TarjetaNormal_TodosLosDatosCompletos()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(2000);

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.AreEqual("132", boleto.Linea);
            Assert.AreEqual(1234, boleto.Interno);
            Assert.AreEqual("Normal", boleto.TipoTarjeta);
            Assert.AreEqual(1580, boleto.Monto);
            Assert.AreEqual(420, boleto.SaldoRestante);
            Assert.IsTrue(boleto.IdTarjeta > 0);
            Assert.AreEqual(1580, boleto.MontoTotalAbonado);
            Assert.IsNotNull(boleto.Fecha);
            Assert.IsNotNull(boleto.Hora);
        }

        [Test]
        public void Boleto_MedioBoletoEstudiantil_DatosCorrectos()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(2000);

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.AreEqual("Medio Boleto Estudiantil", boleto.TipoTarjeta);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto.Monto);
            Assert.AreEqual(2000 - (TARIFA_BASICA / 2), boleto.SaldoRestante);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto.MontoTotalAbonado);
        }

        [Test]
        public void Boleto_FranquiciaCompleta_DatosCorrectos()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta(1000);

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.AreEqual("Franquicia Completa", boleto.TipoTarjeta);
            Assert.AreEqual(0, boleto.Monto);
            Assert.AreEqual(1000, boleto.SaldoRestante); 
            Assert.AreEqual(0, boleto.MontoTotalAbonado);
        }

        [Test]
        public void Boleto_BoletoGratuitoEstudiantil_DatosCorrectos()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil(1000);

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.AreEqual("Boleto Gratuito Estudiantil", boleto.TipoTarjeta);
            Assert.AreEqual(0, boleto.Monto);
            Assert.AreEqual(1000, boleto.SaldoRestante);
            Assert.AreEqual(0, boleto.MontoTotalAbonado);
        }

        [Test]
        public void Boleto_TarjetaConSaldoNegativo_MontoTotalAbonadoCorrecto()
        {
            Colectivo colectivo = new Colectivo("132", 1234);

            Tarjeta tarjeta = new Tarjeta(500);
            tarjeta.Pagar(1000); 

            Tarjeta tarjetaValida = new Tarjeta(800); 

            Boleto boleto = colectivo.PagarCon(tarjetaValida);

            Assert.AreEqual("Normal", boleto.TipoTarjeta);
            Assert.AreEqual(1580, boleto.Monto);
            Assert.AreEqual(800 - 1580, boleto.SaldoRestante);
            Assert.AreEqual(1580, boleto.MontoTotalAbonado);
        }

        [Test]
        public void Boleto_TarjetaConSaldoNegativoRecuperaDeuda_MontoTotalAbonadoCorrecto()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            
            Tarjeta tarjeta = new Tarjeta(-500);

            tarjeta.Cargar(2000); 

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.AreEqual("Normal", boleto.TipoTarjeta);
            Assert.AreEqual(1580, boleto.Monto);
            Assert.AreEqual(1500 - 1580, boleto.SaldoRestante);
            Assert.AreEqual(1580, boleto.MontoTotalAbonado);
        }

        [Test]
        public void Boleto_TarjetaEnLimiteNegativo_NoPermiteViaje()
        {
            Colectivo colectivo = new Colectivo("132", 1234);

            Tarjeta tarjeta = new Tarjeta(-1200);

            Assert.Throws<InvalidOperationException>(() => colectivo.PagarCon(tarjeta));
        }

        [Test]
        public void Boleto_MultiplesTarjetas_IdsDiferentes()
        {
            Tarjeta tarjeta1 = new Tarjeta(2000);
            Tarjeta tarjeta2 = new Tarjeta(2000);
            Tarjeta tarjeta3 = new MedioBoletoEstudiantil(2000);

            Assert.AreNotEqual(tarjeta1.Id, tarjeta2.Id);
            Assert.AreNotEqual(tarjeta1.Id, tarjeta3.Id);
            Assert.AreNotEqual(tarjeta2.Id, tarjeta3.Id);
        }

        [Test]
        public void Boleto_ToString_FormatoCorrecto()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(2000);

            Boleto boleto = colectivo.PagarCon(tarjeta);
            string boletoString = boleto.ToString();

            Assert.IsTrue(boletoString.Contains("Línea: 132"));
            Assert.IsTrue(boletoString.Contains("Interno: 1234"));
            Assert.IsTrue(boletoString.Contains("Tipo: Normal"));
            Assert.IsTrue(boletoString.Contains("Monto: $1580"));
            Assert.IsTrue(boletoString.Contains("Saldo Restante: $420"));
        }

        [Test]
        public void Boleto_FechaHora_FormatoCorrecto()
        {
            DateTime fechaHora = new DateTime(2024, 10, 30, 14, 30, 25);
            Boleto boleto = new Boleto("132", 1234, 1580, fechaHora, "Normal", 420, 1, 1580);

            Assert.AreEqual("30/10/2024", boleto.Fecha);
            Assert.AreEqual("14:30:25", boleto.Hora);
        }

        [Test]
        public void Boleto_TiposDiferentes_PropiedadesEspecificas()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            
            Tarjeta normal = new Tarjeta(2000);
            Boleto boletoNormal = colectivo.PagarCon(normal);
            Assert.AreEqual("Normal", boletoNormal.TipoTarjeta);
            Assert.AreEqual(1580, boletoNormal.MontoTotalAbonado);

            MedioBoletoEstudiantil medio = new MedioBoletoEstudiantil(2000);
            Boleto boletoMedio = colectivo.PagarCon(medio);
            Assert.AreEqual("Medio Boleto Estudiantil", boletoMedio.TipoTarjeta);
            Assert.AreEqual(790, boletoMedio.MontoTotalAbonado);

            FranquiciaCompleta completa = new FranquiciaCompleta(2000);
            Boleto boletoCompleta = colectivo.PagarCon(completa);
            Assert.AreEqual("Franquicia Completa", boletoCompleta.TipoTarjeta);
            Assert.AreEqual(0, boletoCompleta.MontoTotalAbonado);
        }
    }
}