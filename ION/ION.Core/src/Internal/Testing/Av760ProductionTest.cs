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
    private TestProcedure procedure;

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

      Log.D(this, "Writing to the remote terminus...");

      conn.Write(protocol.CreateInitializeCommand());

      while (!bluefruit.isInitialized) {
        Log.D(this, "bluefruit is not initialized");
        await Task.Delay(500);
      }

      Log.D(this, "Bluefruit initialized! Moving to run loop");

      int dir = 0;
      int degrees = 45;
      for (;;) {
        try {
          if (!conn.isConnected) {
            break;
          }
          
          var target = Units.Vacuum.MICRON.OfScalar(targetMicron);
          targetMicron -= 50000;
          if (targetMicron <= 0) {
            targetMicron = 760000;
          }
//          conn.Write(protocol.CreateSetTargetMicronCommand(target));
          Log.D(this, "Moving motor to target: " + degrees);

          conn.Write(protocol.CreateModifyTargetDegreeAngle(degrees));

          await Task.Delay(TimeSpan.FromSeconds(10));
        } catch (Exception e) {
          Log.E(this, "ERROR!!!!!", e);
          break;
        }
      }

      // Cleanup
    }
  }
}

