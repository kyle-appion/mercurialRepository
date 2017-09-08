using System.Linq;
namespace TestBench {

  using System;
  using System.Collections.Generic;

	using Appion.Commons.Measure;

	using ION.Core.Devices;
	using ION.Core.Sensors;

  public class TestParameters {

		/// <summary>
		/// The device model that the test parameters were built for.
		/// </summary>
		/// <value>The device model.</value>
		public EDeviceModel deviceModel { get; private set; }
    /// <summary>
    /// The type of sensor that the test applies to.
    /// </summary>
    public ESensorType sensorType { get; private set; }
    /// <summary>
    /// The list of target points for the test. This list is sorted from highest to lowest.
    /// </summary>
    public List<TargetPoint> targetPoints { get; private set; }
    /// <summary>
    /// The list of the grades that a gauge can receive when tested against these parameters.
    /// </summary>
    public  HashSet<Grade> grades { get; private set; }

		public double errorBand { get; private set; }

    public TestParameters(EDeviceModel deviceModel, ESensorType sensorType, List<TargetPoint> targetPoints, HashSet<Grade> grades) {
			this.deviceModel = deviceModel;
      this.sensorType = sensorType;
      this.targetPoints = new List<TargetPoint>();
      this.grades = grades;

      this.targetPoints.AddRange(targetPoints);

      targetPoints.Sort();
      targetPoints.Reverse();

      if (grades == null || grades.Count <= 0) {
        this.grades = new HashSet<Grade>();
        this.grades.Add(new Grade(1.00, "F", false));
      }

			errorBand = 1;
			foreach (var grade in grades) {
				if (grade.errorBand < errorBand) {
					errorBand =	grade.errorBand;
				}
			}
    }

    /// <summary>
    /// Find the grade of the measurement when compared to the given target point. If the target point is not in the
    /// test or the scalars are not compatible, we will return false and the grade will be set to null. Otherwise, we
    /// will set the grade to the appropriate grade and return true.
    /// </summary>
    /// <returns><c>true</c>, if measurement pass was doesed, <c>false</c> otherwise.</returns>
    /// <param name="targetPoint">Target point.</param>
    /// <param name="measurement">Measurement.</param>
    public bool FindGradeForMeasurement(TargetPoint targetPoint, Scalar measurement, out Grade grade) {
      if (!targetPoints.Contains(targetPoint) || !targetPoint.measurement.CompatibleWith(measurement.unit.quantity)) {
        grade = null;
        return false;
      }

      measurement = measurement.ConvertTo(targetPoint.measurement.unit);

      var errorBand = Math.Abs(targetPoint.measurement.amount - measurement.amount) / targetPoint.measurement.amount;

      grade = FindGradeForBand(errorBand);
      return true;
    }

    /// <summary>
    /// Finds the appropriate grade for the given error band.
    /// </summary>
    /// <returns>The grade for band.</returns>
    /// <param name="band">Band.</param>
    private Grade FindGradeForBand(double band) {
      Grade ret = null;

      var list = grades.ToList();
      list.Sort();
      list.Reverse();

      foreach (var g in list) {
        if (ret == null || band <= g.errorBand) {
          ret = g;
        }
      }

      return ret;
    }

    /// <summary>
    /// A class that represents the details of a target point.
    /// </summary>
    public class TargetPoint : IComparable<TargetPoint> {
      /// <summary>
      /// The target measurement for the target point.
      /// </summary>
      public Scalar measurement;

      public TargetPoint(Scalar measurement) {
        this.measurement = measurement;
      }

      public string ToRowString() {
        return measurement.amount.ToString("###,##0.##") + " " + measurement.unit;
      }

      public int CompareTo(TargetPoint tp) {
        if (!measurement.CompatibleWith(tp.measurement.unit.quantity)) {
          return int.MinValue;
        } else {
          return measurement.amount.CompareTo(tp.measurement.ConvertTo(measurement.unit).amount);
        }
      }

      public override string ToString() {
        return "[TargetPoint{measurement: " + measurement.amount.ToString("N") + " " + measurement.unit + "}]";
      }

      public override bool Equals(object obj) {
        var tp = obj as TargetPoint;
        if (tp != null) {
          return measurement.Equals(tp.measurement);
        } else {
          return false;
        }
      }

      public override int GetHashCode() {
        return measurement.GetHashCode();
      }
      
    }

    /// <summary>
    /// The grade that a gauge can receive from a test.
    /// </summary>
    public class Grade : IComparable<Grade> {
      /// <summary>
      /// The maximum delta that a gauge may drift from a target point.
      /// </summary>
      public double errorBand;
      /// <summary>
      /// The string or character symbol that represents the grade.
      /// </summary>
      public string grade;
      /// <summary>
      /// Whether or not this grade is a passing grade.
      /// </summary>
      public bool passable;

      public Grade(double errorBand, string grade, bool passable) {
        this.errorBand = errorBand;
        this.grade = grade;
        this.passable = passable;
      }

      public int CompareTo(Grade g) {
        return (int)(errorBand * 100) - (int)(g.errorBand * 100);
      }

      public override string ToString() {
        return "[Grade{errorBand: " + errorBand + ", grade: " + grade + ", passable: " + passable + "}]";
      }

      public override bool Equals(object obj) {
        var g = obj as Grade;
        if (g != null) {
          return errorBand == g.errorBand && grade.Equals(g.grade) && passable == g.passable;
        } else {
          return false;
        }
      }

      public override int GetHashCode() {
        return 37 * errorBand.GetHashCode() +  23 * grade.GetHashCode() + 43 * passable.GetHashCode();
      }
    }
  }
}

