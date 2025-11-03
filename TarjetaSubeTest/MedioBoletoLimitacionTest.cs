using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class MedioBoletoLimitacionTest
    {
        private const int TARIFA_BASICA = 1580;

        [Test]
        public void MedioBoleto_NoPermiteSegundoViajeAntesDe5Minutos()
        {
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);
        
            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 4, 0); 
        
            tarjeta.RegistrarViaje(primerViaje);
        
            Assert.Throws<InvalidOperationException>(() => tarjeta.RegistrarViaje(segundoViaje));
        }
        
        [Test]
        public void MedioBoleto_NoPermiteMasDeDosViajesPorDia()
        {
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);
        
            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 0, 0);
            DateTime tercerViaje = new DateTime(2024, 10, 30, 12, 0, 0);
        
            tarjeta.RegistrarViaje(primerViaje);
        
            tarjeta.RegistrarViaje(segundoViaje);
        
            Assert.Throws<InvalidOperationException>(() => tarjeta.RegistrarViaje(tercerViaje));
        }

        [Test]
        public void MedioBoleto_CalcularMontoPasaje_TercerViajeCompleto()
        {
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);

            DateTime hoy = DateTime.Today;

            tarjeta.RegistrarViaje(hoy.AddHours(10));
            tarjeta.RegistrarViaje(hoy.AddHours(11));

            int monto = tarjeta.CalcularMontoPasaje(TARIFA_BASICA);
            Assert.AreEqual(TARIFA_BASICA, monto);
        }

        [Test]
        public void MedioBoleto_ViajesHoy_ContadorCorrecto()
        {
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);

            DateTime hoy = DateTime.Today;

            Assert.AreEqual(0, tarjeta.ViajesHoy);

            tarjeta.RegistrarViaje(hoy.AddHours(10));
            Assert.AreEqual(1, tarjeta.ViajesHoy);

            tarjeta.RegistrarViaje(hoy.AddHours(11));
            Assert.AreEqual(2, tarjeta.ViajesHoy);

            Assert.Throws<InvalidOperationException>(() => 
                tarjeta.RegistrarViaje(hoy.AddHours(12)));
        }

        [Test]
        public void MedioBoleto_PermiteSegundoViajeDespuesDe5Minutos()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 6, 0);

            Boleto boleto1 = colectivo.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto1.Monto);

            Boleto boleto2 = colectivo.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto2.Monto);
        }

        [Test]
        public void MedioBoleto_ReiniciaContadorAlDiaSiguiente()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);
        
            // Cambiar fechas a dentro de franja horaria
            DateTime dia1Viaje1 = new DateTime(2024, 10, 25, 10, 0, 0); // Viernes 10 AM
            DateTime dia1Viaje2 = new DateTime(2024, 10, 25, 10, 6, 0); // Viernes 10:06 AM
            DateTime dia2Viaje1 = new DateTime(2024, 10, 28, 10, 0, 0); // Lunes 10 AM
        
            Boleto boleto1 = colectivo.PagarConEnFecha(tarjeta, dia1Viaje1);
            Boleto boleto2 = colectivo.PagarConEnFecha(tarjeta, dia1Viaje2);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto1.Monto);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto2.Monto);
        
            Boleto boleto3 = colectivo.PagarConEnFecha(tarjeta, dia2Viaje1);
            Assert.AreEqual(TARIFA_BASICA / 2, boleto3.Monto);
        }

        [Test]
        public void MedioBoleto_PagarConBoolean_RespetaLimitacion5Minutos()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);

            bool primerViaje = colectivo.PagarConBoolean(tarjeta);
            Assert.IsTrue(primerViaje);

            bool segundoViaje = colectivo.PagarConBoolean(tarjeta);
            Assert.IsFalse(segundoViaje);
        }

        
    }
}