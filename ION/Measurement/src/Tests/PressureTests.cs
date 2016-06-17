namespace Measurement {

  using System;

  using NUnit.Framework;

  [TestFixture]
  public class PressureTests {
    
    private static readonly double EPSILON = 0.01;
    private void AssertEquals(double expected, double received) {
      Assert.AreEqual(expected, received, EPSILON);
    }

    [Test]
    public void TestKPaToPascal() {
//      AssertEquals();
    }
  }
}

