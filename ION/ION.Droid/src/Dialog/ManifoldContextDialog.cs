namespace ION.Droid.Dialog {

  using System;
  using System.Collections.Generic;

  using Android.App;
  using Android.Content;

  using ION.Core.App;
  using ION.Core.Content;
  
	public class ManifoldContextDialog {


    private Context context; 

    public ManifoldContextDialog(Context context, Manifold manifold) {
    }


    /// <summary>
    /// The builder class that will allow customization of a ManifoldContextDialog.
    /// </summary>
    public class Builder {
      private IION ion;
      private Manifold manifold;

      private bool showConnectOptions;
      private bool showSubviewOptions;
      private bool showAlarms;
      private List<IOption> customOptions = new List<IOption>();

      public Builder(IION ion, Manifold manifold) {
        this.ion = ion;
        this.manifold = manifold;  
      }

      /// <summary>
      /// Sets whether or not the dialog will show connect / disconnect (based on the primary device's current
      /// connection status) within the dialog.
      /// </summary>
      /// <returns>The connect options.</returns>
      /// <param name="showConnectOptions">If set to <c>true</c> show connect options.</param>
      public Builder SetConnectOptions(bool showConnectOptions) {
        this.showConnectOptions = showConnectOptions;
        return this;
      }

      /// <summary>
      /// Sets whether or not the dialog will show an option for to select 
      /// </summary>
      /// <returns>The subview options.</returns>
      /// <param name="showSubviewOptions">If set to <c>true</c> show subview options.</param>
      public Builder ShowSubviewOptions(bool showSubviewOptions) {
        this.showSubviewOptions = showSubviewOptions;
        return this;
      }
    }

    internal interface IOption {
      int stringRes { get; }
      Action action { get; }
    }
  }
}
