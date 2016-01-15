using System;
using System.Drawing;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

using UIKit;
using Foundation;

namespace Xfinium.Pdf.SamplesExplorer.Xamarin.iOS
{
	public partial class RootViewController : UITableViewController
	{
		public RootViewController () : base ("RootViewController", null)
		{
			this.Title = "XFINIUM.PDF Samples";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			
			TableView.Source = new DataSource (this);

		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		class DataSource : UITableViewSource
		{
			static NSString cellIdentifier = new NSString ("CellId");
			RootViewController controller;

			private SampleInfo[] samples;

			public DataSource (RootViewController controller)
			{
				this.controller = controller;

				XDocument samplesDoc = XDocument.Load("Samples/samples.xml");
				IEnumerable<SampleInfo> q = from file in samplesDoc.Descendants("sample")
					select new SampleInfo()
				{
					Name = file.Element("name").Value,
					Description = file.Element("description").Value,
					CSharpSourceCodeFile = file.Element("csharpsourcecode").Value,
					VbNetSourceCodeFile = file.Element("vbnetsourcecode").Value,
					ID = file.Element("id").Value,
				};
				samples = q.ToArray();
			}
			
			// Customize the number of sections in the table view.
			public override nint NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return samples.Length;
			}
			
			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (cellIdentifier);
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}
				
				// Configure the cell.
				cell.TextLabel.Text = samples[indexPath.Row].Name; 
				
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var dvc = new DetailViewController ();
				// Pass the selected object to the new view controller.
				controller.NavigationController.PushViewController (dvc, true);
				dvc.SetDetailItem(samples[indexPath.Row]);
			}
		}
	}
}
