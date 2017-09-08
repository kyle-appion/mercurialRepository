namespace Measurement {

  using NUnit.Framework;

  public class BaseTest {
    private const double EPSILON = 0.002;

    public void AssertEquals(double expected, double received, double epsilon = EPSILON) {
      Assert.AreEqual(expected, received, epsilon);
    }

    public void AssertEquals(string err, double expected, double received, double epsilon = EPSILON) {
      Assert.AreEqual(expected, received, epsilon, err);
    }
  }
}

