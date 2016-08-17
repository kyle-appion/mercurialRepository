using System;

using ION.Core.Sensors;

namespace ION.Core.App {
  /// <summary>
  /// A Viewer is the backing context for a graphical representation of a sensor.
  /// However, a viewer is more than a simple graphical representation of a
  /// sensor, it is also an aggragation of various content to create a single
  /// reference point. For example, a Pressure/Temperature chart is a pressure
  /// sensor and a temperature sensor in the same context using their measurements
  /// to extract a HVAC system state delta/target. A viewer can be the
  /// representation of this context.
  /// </summary>
  public class Viewer {
    /// <summary>
    /// A Delegate that is notified when the viewer changes. A change can be
    /// anything from a sensor reading change to a more subviewer change.
    /// </summary>
    /// <param name="viewer"></param>
    public delegate void OnViewerUpdated(Viewer viewer);

    /// <summary>
    /// The event handler that will be notified when the viewer's state changes.
    /// </summary>
    public event OnViewerUpdated onViewerUpdated;
    /// <summary>
    /// The ION context for the viewer.
    /// </summary>
    public IION ion { get; private set; }
    /// <summary>
    /// The primary sensor for the viewer. 
    /// </summary>
    public Sensor primarySensor {
      get {
        return __sensor;
      }
      set {
        __sensor = value;
        if (onViewerUpdated != null) {
          onViewerUpdated(this);
        }
      }
    } Sensor __sensor;

    

    public Viewer() {
    }
  }
}

