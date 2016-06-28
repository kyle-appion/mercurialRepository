namespace ION.Core.Internal.Testing {

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Xml.Serialization;

  using ION.Core.Sensors;

  public class XmlTestProcedureParser {

    public TestProcedure Parse(Stream stream) {
      var serializer = new XmlSerializer(typeof(XmlTestProcedure));
      XmlTestProcedure procedure = null;

      using (var reader = new StreamReader(stream)) {
        procedure = (XmlTestProcedure)serializer.Deserialize(reader);        
      }

      var sensorType = (ESensorType)Enum.Parse(typeof(ESensorType), procedure.sensorType);

      var targetPoints = new List<TestProcedure.TargetPoint>();
      foreach (var xtp in procedure.targetPoints.targetPoints) {
        var unit = UnitLookup.GetUnit(sensorType, xtp.unitName);
        targetPoints.Add(new TestProcedure.TargetPoint(unit.OfScalar(xtp.value), xtp.weight));
      }

      var grades = new List<TestProcedure.Grade>();
      foreach (var xg in procedure.gradeScale.grades) {
        grades.Add(new TestProcedure.Grade(xg.errorBand, xg.value, xg.passable));
      }

      return new TestProcedure(sensorType, targetPoints, grades);
    }

    public static TestProcedure DoParse(Stream stream) {
      var parser = new XmlTestProcedureParser();
      return parser.Parse(stream);
    }

    [XmlRoot("testProcedure")]
    public class XmlTestProcedure {
      [XmlAttribute("sensorType")]
      public string sensorType;
      [XmlElement("targetPoints")]
      public XmlTargetPoints targetPoints;
      [XmlElement("gradeScale")]
      public XmlGradeScale gradeScale;
    }

    [XmlRoot("targetPoints")]
    public class XmlTargetPoints {
      [XmlElement("point")]
      public List<XmlTargetPoint> targetPoints;
    }

    [XmlRoot("targetPoint")]
    public class XmlTargetPoint {
      [XmlAttribute("unit")]
      public string unitName;
      [XmlAttribute("value")]
      public double value;
      [XmlAttribute("gradeWeight")]
      public float weight;
    }

    [XmlRoot("gradeScale")]
    public class XmlGradeScale {
      [XmlElement("grade")]
      public List<XmlGrade> grades;
    }

    [XmlRoot("gradeScale")]
    public class XmlGrade {
      [XmlAttribute("errorBand")]
      public float errorBand;
      [XmlAttribute("value")]
      public string value;
      [XmlAttribute("passable")]
      public bool passable;
    }
  }
}

