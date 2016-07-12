namespace ION.Core.Internal.Testing {

  using System;
  using System.Collections.Generic;

  using ION.Core.Measure;
  using ION.Core.Sensors;
  /// <summary>
  /// THIS CLASS IS FOR INTERNAL APPION USE ONLY!
  /// A Test procedure is a procedure that is used to test the performance of an ION gauge. In most instances, this
  /// class is used to NIST certify Appion gauge devices.
  /// </summary>
  public class TestProcedure {

    /// <summary>
    /// The type of sensor that is test by this procedure.
    /// </summary>
    public ESensorType sensorType;
		/// <summary>
		/// Returns the number of target points that are present in the test procedure.
		/// </summary>
		/// <value>The count.</value>
		public int count { 
			get {
				return targetPoints.Count;
			}
		}

		/// <summary>
		/// Gets the <see cref="T:ION.Core.Internal.Testing.TestProcedure"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public TargetPoint this[int index] {
			get {
				return targetPoints[index];
			}
		}
    /// <summary>
    /// The points that all tested gauges are tested against.
    /// </summary>
    public List<TargetPoint> targetPoints;
    /// <summary>
    /// The grades that are available to the test procedure.
    /// </summary>
    public List<Grade> grades;


    public TestProcedure(ESensorType sensorType, List<TargetPoint> targetPoints, List<Grade> grades) {
      this.sensorType = sensorType;
      this.targetPoints = targetPoints;
      this.grades = grades;
    }

		/// <summary>
		/// Gets the expected grade for the given percent error.
		/// </summary>
		/// <returns>The grade for error.</returns>
		/// <param name="error">Error.</param>
		public Grade GetGradeForError(float error) {
			foreach (var g in grades) {
				if (error <= g.errorBand) {
					return g;
				}
			}

			return new Grade(1, "F", false);
		}

    /// <summary>
    /// The representation of a target point that a device is to hit within a test procedure.
    /// </summary>
    public class TargetPoint {
      /// <summary>
      /// Gets or sets the target.
      /// </summary>
      /// <value>The target.</value>
      public Scalar target { get; private set; }
      /// <summary>
      /// The weight of the target point.
      /// </summary>
      /// <value>The weight.</value>
      public float weight { get; private set; }

      public TargetPoint(Scalar target, float weight) {
        this.target = target;
        this.weight = weight;
      }
    }

    /// <summary>
    /// A grade is how well a device performed with its test. This is calculated by averaging the differences of the
    /// device's actual to target points with respect to the target point's weight.
    /// </summary>
    public class Grade {
      /// <summary>
      /// The percent error difference that the grade expects to be considered to be graded.
      /// </summary>
      /// <value>The error band.</value>
      public float errorBand { get; private set; }
      /// <summary>
      /// The string/letter value of the grade.
      /// </summary>
      /// <value>The grade.</value>
      public string grade { get; private set; }
      /// <summary>
      /// Whether or not this grade is a passable grade.
      /// </summary>
      /// <value><c>true</c> if passable; otherwise, <c>false</c>.</value>
      public bool passable { get; private set; }

      public Grade(float errorBand, string grade, bool passable) {
        this.errorBand = errorBand;
        this.grade = grade;
        this.passable = passable;
      }
    }
  }
}

