namespace TestBench {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Xml.Serialization;

	using ION.Core.Devices;
	using ION.Core.Sensors;

  public class XmlTestParser {

    public TestParameters Parse(Stream stream) {
      var serializer = new XmlSerializer(typeof(XmlTest));
      XmlTest test = null;

      using (var reader = new StreamReader(stream)) {
        test = (XmlTest)serializer.Deserialize(reader);        
      }

			var deviceModel = (EDeviceModel)Enum.Parse(typeof(EDeviceModel), test.deviceModel);
      var sensorType = (ESensorType)Enum.Parse(typeof(ESensorType), test.sensorType);

      var targetPoints = new List<TestParameters.TargetPoint>();
      foreach (var xtp in test.targetPoints.targetPoints) {
        var unit = UnitLookup.GetUnit(sensorType, xtp.unit);
        targetPoints.Add(new TestParameters.TargetPoint(unit.OfScalar(xtp.amount)));
      }

      var grades = new HashSet<TestParameters.Grade>();
      foreach (var xg in test.gradeScale.grades) {
        grades.Add(new TestParameters.Grade(xg.errorBand, xg.grade, xg.passable));
      }

      return new TestParameters(deviceModel, sensorType, targetPoints, grades);
    }

    [XmlRoot("test")]
    public class XmlTest {
			[XmlAttribute("deviceModel")]
			public string deviceModel;
      [XmlAttribute("sensorType")]
      public string sensorType;
      [XmlElement("targetPoints")]
      public XmlTargetPoints targetPoints;
      [XmlElement("gradeScale")]
      public XmlGradeScale gradeScale;
    }

    [XmlRoot("targetPoints")]
    public class XmlTargetPoints {
      [XmlElement("targetPoint")]
      public List<XmlTargetPoint> targetPoints;
    }

    [XmlRoot("targetPoint")]
    public class XmlTargetPoint {
      [XmlAttribute("amount")]
      public double amount;
      [XmlAttribute("unit")]
      public string unit;
    }

    [XmlRoot("gradeScale")]
    public class XmlGradeScale {
      [XmlElement("grade")]
      public List<XmlGrade> grades;
    }

    [XmlRoot("grade")]
    public class XmlGrade {
      [XmlAttribute("errorBand")]
      public double errorBand;
      [XmlAttribute("value")]
      public string grade;
      [XmlAttribute("passable")]
      public bool passable;
    }
  }
}

