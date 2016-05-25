namespace ION.Core.Internal.Testing {

  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using ION.Core.Devices;
  using ION.Core.Internal;
  using ION.Core.Measure;
  using ION.Core.Sensors;
  using ION.Core.Util;

  public class Av760ProductionTest {

    public delegate void OnTargetPointMet(TestProcedure.TargetPoint tp);

    /// <summary>
    /// The event handler that is notified when the test yields a target point that was met by the test rig.
    /// </summary>
    public event OnTargetPointMet onTargetPointMet;

    /// <summary>
    /// </summary>
    public List<GaugeDeviceSensor> sensors {
      get {
        return new List<GaugeDeviceSensor>(__sensors);
      }
      private set {
        __sensors = new List<GaugeDeviceSensor>(value);
      }
    } List<GaugeDeviceSensor> __sensors = new List<GaugeDeviceSensor>();
    /// <summary>
    /// The test rig device.
    /// </summary>
    public BluefruitDevice bluefruit { get; private set; }
    /// <summary>
    /// The procedure that is used for the test.
    /// </summary>
    public TestProcedure procedure { get; private set; }

    public Av760ProductionTest(TestProcedure procedure, BluefruitDevice bf, List<GaugeDeviceSensor> gaugeSensors) {
      if (ESensorType.Vacuum != procedure.sensorType) {
        throw new Exception("Cannot create Av760ProductionTest with " + procedure.sensorType + " sensor type test");
      }

      this.procedure = procedure;
      bluefruit = bf;
      sensors = gaugeSensors;
    }

    /// <summary>
    /// Runs the test.
    /// </summary>
    public async void Run() {
      var targetMicron = 760000;

      var conn = bluefruit.connection;
      var protocol = bluefruit.protocol as BluefruitProtocol;

      foreach (var tp in procedure.targetPoints) {
        for (;;) {
          if (tp.target.AssertEquals(bluefruit.vrcMeasurement, 0.01)) { // If the vrc is reading a target point
            if (onTargetPointMet != null) {
              onTargetPointMet(tp);
            }
          } else {
            if (tp.target > bluefruit.vrcMeasurement) { // If we over shot the target, move back a little.
              if (!bluefruit.controlStepper.isMoving) {
                conn.Write(protocol.CreateModifyTargetDegreeAngle(0.5f, BluefruitProtocol.EDirection.Close)); // Rotate slightly
              }
            } else { // Otherwise, we need to inch our way in.
              if (!bluefruit.controlStepper.isMoving) {
                conn.Write(protocol.CreateModifyTargetDegreeAngle(0.5f, BluefruitProtocol.EDirection.Open)); // Rotate slightly
              }
            }
          }
        }

        await Task.Delay(TimeSpan.FromMilliseconds(500));

      }

      // Cleanup
    }
  }
}

