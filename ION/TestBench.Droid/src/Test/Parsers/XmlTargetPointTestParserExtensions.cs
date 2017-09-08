namespace TestBench.Droid.Tests.Parsers {

	using System;
	using System.Xml.Linq;

	using Appion.Commons.Measure;
	using Appion.Commons.Util;

	using ION.Core.Devices;
	using ION.Core.Sensors;

	public static class XmlTargetPointTestParserExtensions {
		private static readonly string TAG = typeof(XmlTargetPointTestParserExtensions).Name;
		/// <summary>
		/// The tag that is used to identify an element comprised of target points and grade scale elements.
		/// </summary>
		public const string TAG_TARGET_POINT_TEST = "targetPointTest";
		/// <summary>
		/// The tag that is used to identify a collection of target point elements that comprise the test.
		/// </summary>
		public const string TAG_TARGET_POINTS = "targetPoints";
		/// <summary>
		/// The tag that is used to identify a single target point element.
		/// </summary>
		public const string TAG_TARGET_POINT = "targetPoint";
		/// <summary>
		/// The tag that is used to identify a collection of grade elements that are attached to the test.
		/// </summary>
		public const string TAG_GRADE_SCALE = "gradeScale";
		/// <summary>
		/// The tag that is used to identify a grade element.
		/// </summary>
		public const string TAG_GRADE = "grade";

		public const string ATT_DEVICE_MODEL = "deviceModel";
		public const string ATT_SENSOR_TYPE = "sensorType";
		public const string ATT_AMOUNT = "amount";
		public const string ATT_UNIT = "unit";
		public const string ATT_RELATIVE_ERROR = "relativeError";
		public const string ATT_NAME = "name";
		public const string ATT_PASSABLE = "passable";

		/// <summary>
		/// Parses a TargetPointTest from the given 
		/// </summary>
		/// <returns>The target point test.</returns>
		/// <param name="xl">Xl.</param>
		/// <exception cref="Exception">Element failed to resolve into a target point test.</exception>
		public static TargetPointTest ParseTargetPointTest(XElement xl) {
			if (!TAG_TARGET_POINT_TEST.Equals(xl.Name)) {
				ThrowInvalidTagException(xl.Name.ToString(), TAG_TARGET_POINT_TEST);
			}

			var usdm = xl.Attribute(ATT_DEVICE_MODEL).ToString();
			var usst = xl.Attribute(ATT_SENSOR_TYPE).ToString();

			EDeviceModel dm;
			ESensorType st;
			if (!Enum.TryParse(usdm, out dm)) {
				throw new Exception("Cannot parse TargetPointTest: unknown or unexpected device mode: " + usdm);
			}
			if (!Enum.TryParse(usst, out st)) {
				throw new Exception("Cannot parse TargetPointTest: unknown or unexpected sensor type: " + usst);
			}

			var b = new TargetPointTest.Builder(st.GetDefaultUnit().quantity);

			foreach (var child in xl.Elements()) {
				if (TAG_TARGET_POINTS.Equals(child.Name)) {
					ParseTargetPoints(child, b, st);
				} else if (TAG_GRADE_SCALE.Equals(child.Name)) {
					ParseGradeScale(child, b);
				} else {
					Log.E(TAG, "Unxpected tag {" + child.Name + "} in test");
				}
			}

			return b.Build();
		}

		/// <summary>
		/// Parses the target points from the given xml element.
		/// </summary>
		/// <param name="el">El.</param>
		/// <param name="b">The blue component.</param>
		private static void ParseTargetPoints(XElement el, TargetPointTest.Builder b, ESensorType st) {
			foreach (var child in el.Elements()) {
				if (TAG_TARGET_POINT.Equals(child.Name)) {
					var usamount = child.Attribute(ATT_AMOUNT).ToString();
					var usunit = child.Attribute(ATT_UNIT).ToString();

					double amount;
					Unit unit = UnitLookup.GetUnit(st, usunit);

					if (!double.TryParse(usamount, out amount)) {
						throw new Exception("Cannot parse TargetPointTest: malformed amount: " + usamount);
					}

					b.AddTargetPoint(unit.OfScalar(amount));
				} else {
					Log.E(TAG, "Unexpected tag {" + child.Name + "} in test");
				}
			}
		}

		private static void ParseGradeScale(XElement el, TargetPointTest.Builder b) {
			foreach (var child in el.Elements()) {
				if (TAG_GRADE.Equals(child.Name)) {
					var userr = child.Attribute(ATT_RELATIVE_ERROR).ToString();
					var usname = child.Attribute(ATT_NAME).ToString();
					var uspassable = child.Attribute(ATT_PASSABLE).ToString();

					double err;
					bool passable;
					if (!double.TryParse(userr, out err)) {
						throw new Exception("Cannot parse TargetPointTest: malformed error: " + userr);
					}
					if (!bool.TryParse(uspassable, out passable)) {
						throw new Exception("Cannot parse TargetPointtest: malformed bool: " + uspassable);
					}

					b.AddGrade(err, usname, passable);
				} else {
					Log.E(TAG, "Unexpected tag {" + child.Name + "} in test");
				}
			}
		}

		private static void ThrowInvalidTagException(string elementName, string expectedElementName) {
			throw new Exception("Cannot parse TargetPointTest: received element {" + elementName + "} expected element {" + expectedElementName + "}");
		}
	}
}
