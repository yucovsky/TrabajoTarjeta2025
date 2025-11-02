using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class FranquiciaCompletaLimitacionTest
    {
        private const int TARIFA_BASICA = 1580;

        [Test]
        public void BoletoGratuitoEstudiantil_NoPermiteMasDeDosViajesGratuitosPorDia()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 0, 0);
            DateTime tercerViaje = new DateTime(2024, 10, 30, 12, 0, 0);

            Boleto boleto1 = colectivo.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(0, boleto1.Monto);

            Boleto boleto2 = colectivo.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(0, boleto2.Monto);
        
            Assert.Throws<InvalidOperationException>(() => 
                colectivo.PagarConEnFecha(tarjeta, tercerViaje));
        }

        [Test]
        public void BoletoGratuitoEstudiantil_TercerViajeCobraTarifaCompleta()
        {
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil(5000);

            DateTime fechaReferencia = new DateTime(2024, 10, 30, 12, 0, 0);
            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 0, 0);
        
            tarjeta.RegistrarViajeGratuito(primerViaje);
            tarjeta.RegistrarViajeGratuito(segundoViaje);

            int montoTercerViaje = tarjeta.CalcularMontoPasajeEnFecha(TARIFA_BASICA, fechaReferencia);
            Assert.AreEqual(TARIFA_BASICA, montoTercerViaje);
        }

        [Test]
        public void BoletoGratuitoEstudiantil_ReiniciaContadorAlDiaSiguiente()
        {
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil(5000);

            DateTime dia1Viaje1 = new DateTime(2024, 10, 30, 23, 0, 0);
            DateTime dia1Viaje2 = new DateTime(2024, 10, 30, 23, 30, 0);
            DateTime dia2Viaje1 = new DateTime(2024, 10, 31, 1, 0, 0);

            tarjeta.RegistrarViajeGratuito(dia1Viaje1);
            tarjeta.RegistrarViajeGratuito(dia1Viaje2);

            Assert.AreEqual(2, tarjeta.ViajesGratuitosHoyEnFecha(dia1Viaje2));

            tarjeta.RegistrarViajeGratuito(dia2Viaje1);
            Assert.AreEqual(1, tarjeta.ViajesGratuitosHoyEnFecha(dia2Viaje1));
        }

        [Test]
        public void FranquiciaCompleta_NoPermiteMasDeDosViajesGratuitosPorDia()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 0, 0);
            DateTime tercerViaje = new DateTime(2024, 10, 30, 12, 0, 0);
        
            Boleto boleto1 = colectivo.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(0, boleto1.Monto);

            Boleto boleto2 = colectivo.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(0, boleto2.Monto);

            Assert.Throws<InvalidOperationException>(() => 
                colectivo.PagarConEnFecha(tarjeta, tercerViaje));
        }

        [Test]
        public void FranquiciaCompleta_TercerViajeCobraTarifaCompleta()
        {
            FranquiciaCompleta tarjeta = new FranquiciaCompleta(5000);

            DateTime fechaReferencia = new DateTime(2024, 10, 30, 12, 0, 0);
            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 0, 0);

            tarjeta.RegistrarViajeGratuito(primerViaje);
            tarjeta.RegistrarViajeGratuito(segundoViaje);

            int montoTercerViaje = tarjeta.CalcularMontoPasajeEnFecha(TARIFA_BASICA, fechaReferencia);
            Assert.AreEqual(TARIFA_BASICA, montoTercerViaje);
        }

        [Test]
        public void FranquiciaCompleta_ReiniciaContadorAlDiaSiguiente()
        {
            FranquiciaCompleta tarjeta = new FranquiciaCompleta(5000);

            DateTime dia1Viaje1 = new DateTime(2024, 10, 30, 23, 0, 0);
            DateTime dia1Viaje2 = new DateTime(2024, 10, 30, 23, 30, 0);
            DateTime dia2Viaje1 = new DateTime(2024, 10, 31, 1, 0, 0); 

            tarjeta.RegistrarViajeGratuito(dia1Viaje1);
            tarjeta.RegistrarViajeGratuito(dia1Viaje2);

            Assert.AreEqual(2, tarjeta.ViajesGratuitosHoyEnFecha(dia1Viaje2));

            tarjeta.RegistrarViajeGratuito(dia2Viaje1);
            Assert.AreEqual(1, tarjeta.ViajesGratuitosHoyEnFecha(dia2Viaje1));
        }

        [Test]
        public void FranquiciaCompleta_ViajesGratuitosHoy_ContadorCorrecto()
        {
            FranquiciaCompleta tarjeta = new FranquiciaCompleta(5000);

            DateTime hoy = DateTime.Today;

            Assert.AreEqual(0, tarjeta.ViajesGratuitosHoyEnFecha(hoy));

            tarjeta.RegistrarViajeGratuito(hoy.AddHours(10));
            Assert.AreEqual(1, tarjeta.ViajesGratuitosHoyEnFecha(hoy));

            tarjeta.RegistrarViajeGratuito(hoy.AddHours(11));
            Assert.AreEqual(2, tarjeta.ViajesGratuitosHoyEnFecha(hoy));

            Assert.Throws<InvalidOperationException>(() => 
                tarjeta.RegistrarViajeGratuito(hoy.AddHours(12)));
        }

        [Test]
        public void BoletoGratuitoEstudiantil_TercerViajeConSaldo_CobraCorrectamente()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 0, 0);
            DateTime tercerViaje = new DateTime(2024, 10, 30, 12, 0, 0);

            Boleto boleto1 = colectivo.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(0, boleto1.Monto);

            Boleto boleto2 = colectivo.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(0, boleto2.Monto);
        
            int montoCalculado = tarjeta.CalcularMontoPasajeEnFecha(TARIFA_BASICA, tercerViaje);
            Assert.AreEqual(TARIFA_BASICA, montoCalculado);
        }

        [Test]
        public void BoletoGratuitoEstudiantil_TercerViajeConTarifaCompleta_Funciona()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            BoletoGratuitoEstudiantil tarjeta = new BoletoGratuitoEstudiantil(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 0, 0);

            tarjeta.RegistrarViajeGratuito(primerViaje);
            tarjeta.RegistrarViajeGratuito(segundoViaje);

            int montoTercerViaje = tarjeta.CalcularMontoPasajeEnFecha(TARIFA_BASICA, new DateTime(2024, 10, 30, 12, 0, 0));
            Assert.AreEqual(TARIFA_BASICA, montoTercerViaje);
        }

        [Test]
        public void FranquiciaCompleta_TercerViajeConTarifaCompleta_Funciona()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 0, 0);

            tarjeta.RegistrarViajeGratuito(primerViaje);
            tarjeta.RegistrarViajeGratuito(segundoViaje);

            int montoTercerViaje = tarjeta.CalcularMontoPasajeEnFecha(TARIFA_BASICA, new DateTime(2024, 10, 30, 12, 0, 0));
            Assert.AreEqual(TARIFA_BASICA, montoTercerViaje);
        }
    }
}