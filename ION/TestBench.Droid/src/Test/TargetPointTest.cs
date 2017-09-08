namespace TestBench.Droid {

	using System.Collections.Generic;

	using Appion.Commons.Measure;

	/// <summary>
	/// A TargetPointTest is the most common type of test suite that exists in the test bench. This test is used for
	/// calibration, nist tracing and general accuracy tracking for all Appion products.
	/// </summary>
	public class TargetPointTest {

		/// <summary>
		/// Quantity that the test was constructed for.
		/// </summary>
		/// <value>The quantity.</value>
		public Quantity quantity { get; private set; }
		/// <summary>
		/// The current index within the target points that the test is located.
		/// </summary>
		public int currentPointIndex { get; private set; }

		/// <summary>
		/// The current target point that the test has selected.
		/// </summary>
		/// <value>The current target point.</value>
		public Scalar currentTargetPoint { get { return targetPoints[currentPointIndex]; } } 
		/// <summary>
		/// The number of target points that are contained in the test.
		/// </summary>
		/// <value>The length.</value>
		public int length { get { return targetPoints.Count; } }

		/// <summary>
		/// The grades that are applied to the results of the test.
		/// </summary>
		private List<Grade> grades;
		/// <summary>
		/// The target points that the test is testing over.
		/// </summary>
		private List<Scalar> targetPoints;

		public TargetPointTest(Quantity quantity, List<Grade> grades, List<Scalar> targetPoints) {
			this.quantity = quantity;
			this.grades = grades;
			this.targetPoints = targetPoints;
		}

		public void BeginTest() {
		}

		/// <summary>
		/// Advances the targetpoint index within the test. If the advance passes the targetpoint count, then we will return
		/// true indicating that the test has completed its advance, and the currentPointIndex will be set to 0.
		/// </summary>
		public bool Advance() {
			if (++currentPointIndex >= targetPoints.Count) {
				currentPointIndex = 0;
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Resets the test.
		/// </summary>
		public void Reset() {
			currentPointIndex = 0;
		}

		/// <summary>
		/// Performs a deep copy of the test.
		/// </summary>
		public TargetPointTest Copy() {
			return new TargetPointTest(quantity, new List<Grade>(grades), new List<Scalar>(targetPoints));
		}

		/// <summary>
		/// A Grade is a metric used to determine how valid a result was with regards to a test.
		/// </summary>
		public class Grade {
			/// <summary>
			/// The relative error that a measurement should be less than to achieve this grade.
			/// </summary>
			public double relativeError { get; private set; }
			/// <summary>
			/// The test specific name for the grade. (ie. A, B, C or cat1, cat2, ca3 etc...)
			/// </summary>
			public string name { get; private set; }
			/// <summary>
			/// Whether or not this grade is allowed to pass the test.
			/// </summary>
			public bool passable { get; private set; }

			public Grade(double relativeError, string name, bool passable) {
				this.relativeError = relativeError;
				this.name = name;
				this.passable = passable;
			}
		}

		/// <summary>
		/// A simple object that is used to construct TargetPointTests.
		/// </summary>
		public class Builder {
			private Quantity quantity;
			private List<Grade> grades;
			private List<Scalar> targetPoints;

			public Builder(Quantity quantity) {
				this.quantity = quantity;
				this.grades = new List<Grade>();
				this.targetPoints = new List<Scalar>();
			}

			/// <summary>
			/// Adds a new target point to the test. If the target point is not compatible with the quantity of the test, we
			/// will return false. Note: target points are added in a linear and sequantial manner. Meaning, the target points
			/// are not sorted in any way. If you want a sorted test, you must sort it before adding the points to the test.
			/// </summary>
			/// <returns><c>true</c>, if target point was added, <c>false</c> otherwise.</returns>
			/// <param name="targetPoint">Target point.</param>
			public bool AddTargetPoint(Scalar targetPoint) {
				if (targetPoint.CompatibleWith(quantity)) {
					targetPoints.Add(targetPoint);
					return true;
				} else {
					return false;
				}
			}

			public Builder AddGrade(double relativeError, string name, bool passable) {
				grades.Add(new Grade(relativeError, name, passable));
				return this;
			}

			public TargetPointTest Build() {
				return new TargetPointTest(quantity, grades, targetPoints);
			}
		}
	}
}
