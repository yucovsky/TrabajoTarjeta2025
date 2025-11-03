using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class BoletoFrecuenteTest
    {
        private const int TARIFA_BASICA = 1580;

        [Test]
        public void TarjetaNormal_RegistrarViaje_Funciona()
        {
            Tarjeta tarjeta = new Tarjeta(50000);
            DateTime fechaViaje = new DateTime(2024, 10, 15);
            
            tarjeta.RegistrarViaje(fechaViaje);
            
            int viajesEnMes = tarjeta.ViajesEnMes(new DateTime(2024, 10, 15));
            Assert.AreEqual(1, viajesEnMes, "Debería tener 1 viaje en octubre 2024");
        }

        [Test]
        public void TarjetaNormal_RegistrarMultiplesViajes()
        {
            Tarjeta tarjeta = new Tarjeta(50000);
            
            tarjeta.RegistrarViaje(new DateTime(2024, 10, 1));
            tarjeta.RegistrarViaje(new DateTime(2024, 10, 2));
            tarjeta.RegistrarViaje(new DateTime(2024, 10, 3));
            
            int viajesEnMes = tarjeta.ViajesEnMes(new DateTime(2024, 10, 15));
            Assert.AreEqual(3, viajesEnMes, "Debería tener 3 viajes en octubre 2024");
        }

        [Test]
        public void TarjetaNormal_DescuentoBasico()
        {
            Tarjeta tarjeta = new Tarjeta(50000);
            
            for (int i = 1; i <= 30; i++)
            {
                tarjeta.RegistrarViaje(new DateTime(2024, 10, i));
            }
            
            int monto = tarjeta.CalcularMontoPasajeEnFecha(TARIFA_BASICA, new DateTime(2024, 10, 31));
            int montoEsperado = (int)(TARIFA_BASICA * 0.8);
            
            Assert.AreEqual(montoEsperado, monto, "Debería aplicar 20% de descuento");
        }

        [Test]
        public void TarjetaNormal_Viaje30a59_20PorcientoDescuento()
        {
            Tarjeta tarjeta = new Tarjeta(50000);
            
            for (int i = 1; i <= 30; i++)
            {
                tarjeta.RegistrarViaje(new DateTime(2024, 10, i));
            }

            int montoViaje30 = tarjeta.CalcularMontoPasajeEnFecha(TARIFA_BASICA, new DateTime(2024, 10, 30));
            int montoEsperado = (int)(TARIFA_BASICA * 0.8);
            Assert.AreEqual(montoEsperado, montoViaje30, "Viaje 30 debería tener 20% de descuento");
        }

        [Test]
        public void Colectivo_PagarCon_TarjetaNormal_AplicaDescuentoPorUsoFrecuente()
        {
            Colectivo colectivo = new Colectivo("132", 1234);
            Tarjeta tarjeta = new Tarjeta(50000);
            
            for (int i = 1; i <= 29; i++)
            {
                DateTime fechaViaje = new DateTime(2024, 10, i);
                Boleto boleto = colectivo.PagarConEnFecha(tarjeta, fechaViaje);
            }

            DateTime fechaViaje30 = new DateTime(2024, 10, 30);
            int viajesAntes = tarjeta.ViajesEnMes(fechaViaje30);

            Boleto boleto30 = colectivo.PagarConEnFecha(tarjeta, fechaViaje30);
            int montoEsperado = (int)(TARIFA_BASICA * 0.8);

            Assert.AreEqual(montoEsperado, boleto30.Monto, "Viaje 30 debería tener 20% de descuento");
        }

        [Test]
        public void Tarjeta_ViajesMesEspecifico()
        {
            Tarjeta tarjeta = new Tarjeta(50000);
            
            tarjeta.RegistrarViaje(new DateTime(2024, 10, 15));
            tarjeta.RegistrarViaje(new DateTime(2024, 10, 16));
            
            int viajesOctubre = tarjeta.ViajesEnMes(new DateTime(2024, 10, 20));
            Assert.AreEqual(2, viajesOctubre, "Debería tener 2 viajes en octubre 2024");
        }
    }
}