using TarjetaSube;
using NUnit.Framework;

namespace TarjetaSubeTest
{
    public class Tests
    {

        Tarjeta t;

        [SetUp]
        public void Setup()
        {
            t = new Tarjeta();
        }

        [Test]
        public void CargaTest()
        {
            t.Cargar(100);
            t.Pagar();
            Assert.AreEqual(t.Saldo, 50);
        }
    }
}