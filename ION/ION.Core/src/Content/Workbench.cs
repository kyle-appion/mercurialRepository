using System;
using System.Collections.Generic;

using ION.Core.Sensors;

namespace ION.Core.Content {
  /// <summary>
  /// A workbench is a simple container that will hold a collection of manifolds.
  /// </summary>
  public class Workbench {
    public delegate void OnManifoldAdded(Workbench workbench, Manifold manifold);
    public delegate void OnManifoldRemoved(Workbench workbench, Manifold manifold);

    public event OnManifoldAdded onManifoldAdded;
    public event OnManifoldRemoved onManifoldRemoved;

    /// <summary>
    /// The number of manifolds that are present in the workbench.
    /// </summary>
    /// <value>The count.</value>
    public int count { get { return __manifolds.Count; } }
    /// <summary>
    /// The indexer that will poll a manifold from the workbench.
    /// </summary>
    /// <param name="index">Index.</param>
    public Manifold this[int index] {
      get {
        return __manifolds[index];
      }
    }

    /// <summary>
    /// The backing list of manifolds for the workbench.
    /// </summary>
    private List<Manifold> __manifolds = new List<Manifold>();

    public Workbench() {
    }

    /// <summary>
    /// Adds the given manifold to the workbench. A given manifold may only exist
    /// once in the workbench.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    /// <returns>True if the manifold was added, false otherwise.</returns>
    public bool Add(Manifold manifold) {
      if (__manifolds.Contains(manifold)) {
        return false;
      } else {
        __manifolds.Add(manifold);
        NotifyManifoldAdded(manifold);
        return true;
      } 
    }

    /// <summary>
    /// Adds the given sensor to the workbench. If the sensor is already present
    /// within the workbench, the sensor will not be added.
    /// </summary>
    /// <returns><c>true</c>, if sensor was added, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool AddSensor(Sensor sensor) {
      if (ContainsSensor(sensor)) {
        return false;
      } else {
        var add = new Manifold(sensor);
        __manifolds.Add(add);
        NotifyManifoldAdded(add);
        return true;
      }
    }

    /// <summary>
    /// Removes the manifold from the workbench.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    public void Remove(Manifold manifold) {
      __manifolds.Remove(manifold);
      NotifyManifoldRemoved(manifold);
    }

    /// <summary>
    /// Clears the workbench of all manifolds.
    /// </summary>
    public void Clear() {
      foreach (Manifold m in __manifolds) {
        Remove(m);
      }
    }

    /// <summary>
    /// Queries whether or not the workbench contains the given sensor as a primary
    /// sensor to a manifold that exists within the workbench.
    /// </summary>
    /// <returns><c>true</c>, if sensor was containsed, <c>false</c> otherwise.</returns>
    /// <param name="sensor">Sensor.</param>
    public bool ContainsSensor(Sensor sensor) {
      foreach (Manifold manifold in __manifolds) {
        if (manifold.primarySensor.Equals(sensor)) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Notifies the OnManifoldAdded event handler that a manifold was added to
    /// the workbench.
    /// </summary>
    /// <param name="manifold">Manifold.</param>
    private void NotifyManifoldAdded(Manifold manifold) {
      if (onManifoldAdded != null) {
        onManifoldAdded(this, manifold);
      }
    }

    /// <summary>
    /// Notified the OnManifoldRemoved event handler that a manifold was removed
    /// from the workbench.
    /// </summary>
    /// <param name="removed">Removed.</param>
    private void NotifyManifoldRemoved(Manifold manifold) {
      if (onManifoldRemoved != null) {
        onManifoldRemoved(this, manifold);
      }
    }
  }
}

