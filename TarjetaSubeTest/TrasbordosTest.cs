using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class TrasbordosTest
    {
        [Test]
        public void Tarjeta_PuedeRealizarTrasbordo_DentroDeUnaHora_LineaDiferente()
        {
            Tarjeta tarjeta = new Tarjeta(5000);
            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0); 
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 30, 0); 

            tarjeta.RegistrarViajeConLinea(primerViaje, "132");

            bool puedeTrasbordo = tarjeta.PuedeRealizarTrasbordo(segundoViaje, "133");
            
            Assert.IsTrue(puedeTrasbordo);
        }

        [Test]
        public void Tarjeta_NoPuedeRealizarTrasbordo_MismaLinea()
        {
            Tarjeta tarjeta = new Tarjeta(5000);
            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 30, 0);

            tarjeta.RegistrarViajeConLinea(primerViaje, "132");

            bool puedeTrasbordo = tarjeta.PuedeRealizarTrasbordo(segundoViaje, "132");
            
            Assert.IsFalse(puedeTrasbordo);
        }

        [Test]
        public void Tarjeta_NoPuedeRealizarTrasbordo_FueraDeUnaHora()
        {
            Tarjeta tarjeta = new Tarjeta(5000);
            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 1, 0); 

            tarjeta.RegistrarViajeConLinea(primerViaje, "132");

            bool puedeTrasbordo = tarjeta.PuedeRealizarTrasbordo(segundoViaje, "133");
            
            Assert.IsFalse(puedeTrasbordo);
        }

        [Test]
        public void Tarjeta_NoPuedeRealizarTrasbordo_FueraDeFranjaHoraria()
        {
            Tarjeta tarjeta = new Tarjeta(5000);
            
            DateTime primerViaje = new DateTime(2024, 10, 27, 10, 0, 0); 
            DateTime segundoViaje = new DateTime(2024, 10, 27, 10, 30, 0);

            tarjeta.RegistrarViajeConLinea(primerViaje, "132");

            bool puedeTrasbordo = tarjeta.PuedeRealizarTrasbordo(segundoViaje, "133");
            
            Assert.IsFalse(puedeTrasbordo);
        }

        [Test]
        public void Tarjeta_NoPuedeRealizarTrasbordo_FueraDeHorarioNocturno()
        {
            Tarjeta tarjeta = new Tarjeta(5000);
            
            DateTime primerViaje = new DateTime(2024, 10, 30, 21, 0, 0); 
            DateTime segundoViaje = new DateTime(2024, 10, 30, 22, 0, 0); 

            tarjeta.RegistrarViajeConLinea(primerViaje, "132");

            bool puedeTrasbordo = tarjeta.PuedeRealizarTrasbordo(segundoViaje, "133");
            
            Assert.IsFalse(puedeTrasbordo);
        }

        [Test]
        public void Tarjeta_PuedeRealizarTrasbordo_EnHorarioPermitido()
        {
            Tarjeta tarjeta = new Tarjeta(5000);
            
            DateTime primerViaje = new DateTime(2024, 10, 30, 21, 0, 0); 
            DateTime segundoViaje = new DateTime(2024, 10, 30, 21, 30, 0); 

            tarjeta.RegistrarViajeConLinea(primerViaje, "132");

            bool puedeTrasbordo = tarjeta.PuedeRealizarTrasbordo(segundoViaje, "133");
            
            Assert.IsTrue(puedeTrasbordo);
        }

        [Test]
        public void Colectivo_PagarCon_Trasbordo_Gratuito()
        {
            Colectivo colectivo1 = new Colectivo("132", 1234);
            Colectivo colectivo2 = new Colectivo("133", 5678);
            Tarjeta tarjeta = new Tarjeta(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 30, 0);

            Boleto boleto1 = colectivo1.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(1580, boleto1.Monto);
            Assert.IsFalse(boleto1.EsTrasbordo);
            Assert.AreEqual(5000 - 1580, tarjeta.Saldo);

            Boleto boleto2 = colectivo2.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(0, boleto2.Monto); 
            Assert.IsTrue(boleto2.EsTrasbordo);
            Assert.AreEqual(5000 - 1580, tarjeta.Saldo);
        }

        [Test]
        public void Colectivo_PagarCon_NoEsTrasbordo_PagaNormal()
        {
            Colectivo colectivo1 = new Colectivo("132", 1234);
            Colectivo colectivo2 = new Colectivo("133", 5678);
            Tarjeta tarjeta = new Tarjeta(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 11, 1, 0); 

            Boleto boleto1 = colectivo1.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(1580, boleto1.Monto);

            Boleto boleto2 = colectivo2.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(1580, boleto2.Monto);
            Assert.IsFalse(boleto2.EsTrasbordo);
            Assert.AreEqual(5000 - 1580 - 1580, tarjeta.Saldo);
        }

        [Test]
        public void MedioBoletoEstudiantil_Trasbordo_FuncionaCorrectamente()
        {
            Colectivo colectivo1 = new Colectivo("132", 1234);
            Colectivo colectivo2 = new Colectivo("133", 5678);
            MedioBoletoEstudiantil tarjeta = new MedioBoletoEstudiantil(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 30, 0);

            Boleto boleto1 = colectivo1.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(790, boleto1.Monto); 
            Assert.IsFalse(boleto1.EsTrasbordo);

            Boleto boleto2 = colectivo2.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(0, boleto2.Monto);
            Assert.IsTrue(boleto2.EsTrasbordo);
        }

        [Test]
        public void FranquiciaCompleta_Trasbordo_FuncionaCorrectamente()
        {
            Colectivo colectivo1 = new Colectivo("132", 1234);
            Colectivo colectivo2 = new Colectivo("133", 5678);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 30, 0);

            Boleto boleto1 = colectivo1.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(0, boleto1.Monto);
            Assert.IsFalse(boleto1.EsTrasbordo);

            Boleto boleto2 = colectivo2.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(0, boleto2.Monto);
            Assert.IsTrue(boleto2.EsTrasbordo);
        }

        [Test]
        public void Boleto_ToString_ConTrasbordo_MuestraInformacion()
        {
            Colectivo colectivo = new Colectivo("133", 5678);
            Tarjeta tarjeta = new Tarjeta(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            tarjeta.RegistrarViajeConLinea(primerViaje, "132");

            Boleto boleto = colectivo.PagarConEnFecha(tarjeta, new DateTime(2024, 10, 30, 10, 30, 0));
            
            string boletoString = boleto.ToString();
            Assert.IsTrue(boletoString.Contains("TRASBORDO"));
        }

        [Test]
        public void MultiplesTrasbordos_DentroDeUnaHora()
        {
            Colectivo colectivo1 = new Colectivo("132", 1234);
            Colectivo colectivo2 = new Colectivo("133", 5678);
            Colectivo colectivo3 = new Colectivo("134", 9101);
            Tarjeta tarjeta = new Tarjeta(5000);

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 20, 0);
            DateTime tercerViaje = new DateTime(2024, 10, 30, 10, 40, 0);

            Boleto boleto1 = colectivo1.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(1580, boleto1.Monto);
            Assert.IsFalse(boleto1.EsTrasbordo);

            Boleto boleto2 = colectivo2.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(0, boleto2.Monto);
            Assert.IsTrue(boleto2.EsTrasbordo);

            Boleto boleto3 = colectivo3.PagarConEnFecha(tarjeta, tercerViaje);
            Assert.AreEqual(0, boleto3.Monto);
            Assert.IsTrue(boleto3.EsTrasbordo);

            Assert.AreEqual(5000 - 1580, tarjeta.Saldo);
        }

        [Test]
        public void Trasbordo_DespuesDeFranquiciaCompleta_Gratuito()
        {
            Colectivo colectivo1 = new Colectivo("132", 1234);
            Colectivo colectivo2 = new Colectivo("133", 5678);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();

            DateTime primerViaje = new DateTime(2024, 10, 30, 10, 0, 0);
            DateTime segundoViaje = new DateTime(2024, 10, 30, 10, 30, 0);

            Boleto boleto1 = colectivo1.PagarConEnFecha(tarjeta, primerViaje);
            Assert.AreEqual(0, boleto1.Monto);
            Assert.IsFalse(boleto1.EsTrasbordo);

            Boleto boleto2 = colectivo2.PagarConEnFecha(tarjeta, segundoViaje);
            Assert.AreEqual(0, boleto2.Monto);
            Assert.IsTrue(boleto2.EsTrasbordo);
        }
    }
}