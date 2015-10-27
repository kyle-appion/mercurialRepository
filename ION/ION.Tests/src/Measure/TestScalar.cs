namespace ION.TestFixtures.Measure {

  using System;
  using System.IO;
  using System.Runtime.Serialization;
  using System.Xml;

  using NUnit.Framework;

  using ION.Core.Measure;

  // TODO ahodder@appioninc.com: Finish implementing the tests
  [TestFixture]
  public class TestScalar {
    private static readonly Unit KEL = Units.Temperature.KELVIN;
    private static readonly Unit FAH = Units.Temperature.FAHRENHEIT;

    private static readonly double EPSILON = 0.01;
    private void AssertEquals(double expected, double received) {
      Assert.AreEqual(expected, received, EPSILON);
    }

    [Test]
    public void TestScalarTransitionalAddition() {
      AssertEquals(54.33, (FAH.OfScalar(10) + KEL.OfScalar(280)).amount);
    }

    [Test]
    public void TestScalarTransitionalSubtraction() {
      AssertEquals(39.072, (KEL.OfScalar(300) - FAH.OfScalar(10)).amount);
    }

    [Test]
    public void TestScalarTransitionalMultiplication() {
      AssertEquals(2581.5, (KEL.OfScalar(10) * FAH.OfScalar(5)).amount);
    }

    [Test]
    public void TestScalarSerialization() {
      Scalar scalar = KEL.OfScalar(1337);

      var ds = new DataContractSerializer(typeof(Scalar));
      var stream = new MemoryStream();
      using (XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter(stream))
        ds.WriteObject(w, scalar);
    

      Scalar inflated = null;
      using (XmlDictionaryReader r = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max))
        inflated = (Scalar)ds.ReadObject(r);
    }
  }
}
