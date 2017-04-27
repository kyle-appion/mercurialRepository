using System;
namespace ION.Core.Content {

  public class IonState {
      /// <summary>
    /// The type of the event.
    /// </summary>
    /// <value>The type.</value>
    public EType type { get; private set; }  
    /// <summary>
    /// The enumeration of the type of events that a workbench will throw.
    /// </summary>
    public enum EType {
      /// <summary>
      /// The event type used when a datalogging has started or stopped.
      /// </summary>
      DataLog,
    }
  
    public IonState() {
    }
    
  }
  
}
