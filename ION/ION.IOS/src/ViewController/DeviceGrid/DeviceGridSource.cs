using System;
using Foundation;
using CoreGraphics;
using UIKit;
using System.Collections.Generic;
using ION.Core.Devices;

namespace ION.IOS.ViewController.DeviceGrid {
  public class DeviceGridSource : UICollectionViewDataSource {
		public List<GaugeDeviceSensor> rowsConnected { get; set; }
		public List<GaugeDeviceSensor> rowsDisconnected { get; set; }

		public DeviceGridSource(List<GaugeDeviceSensor> connected, List<GaugeDeviceSensor> disconnected) {
			rowsConnected = connected;
			rowsDisconnected = disconnected;
		}

		public override nint NumberOfSections(UICollectionView collectionView) {
			return 2;
		}

		public override nint GetItemsCount(UICollectionView collectionView, nint section) {
      if(section == 0){
				return rowsConnected.Count;
			} else {
				return rowsDisconnected.Count;
			}
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath) {
      if(indexPath.Section == 0){
				var cell = (GridCellConnected)collectionView.DequeueReusableCell(GridCellConnected.CellID, indexPath);
				
				cell.UpdateCell(rowsConnected[indexPath.Row]);
        return cell;
			} else {
				var cell = (GridCellDisconnected)collectionView.DequeueReusableCell(GridCellDisconnected.CellID, indexPath);
				
				cell.UpdateCell(rowsDisconnected[indexPath.Row]);
				return cell;
			}

		}

		public override bool CanMoveItem(UICollectionView collectionView, NSIndexPath indexPath) {
			return true;
		}

    public override UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath) {
			var headerView = (Header)collectionView.DequeueReusableSupplementaryView(elementKind, "sectionHeader", indexPath);

      if (indexPath.Section == 0) {
        headerView.Text = "Available Devices";
        headerView.BackgroundColor = UIColor.Clear;
        headerView.label.TextColor = UIColor.FromRGB(57, 181, 74);
			} else {
				headerView.Text = "Disconnected Devices";
				headerView.BackgroundColor = UIColor.Clear;
        headerView.label.TextColor = UIColor.Red;
      }
			return headerView;
    }
  }

	public class Header : UICollectionReusableView {
		public UILabel label;
    public UIImageView headerImage;
		public string Text {
			get {
				return label.Text;
			}
			set {
				label.Text = value;
				SetNeedsDisplay();
			}
		}
		[Export("initWithFrame:")]
		public Header(System.Drawing.RectangleF frame) : base(frame) {
      headerImage = new UIImageView(new CGRect(0,0,50,50));
      headerImage.Image = UIImage.FromBundle("ic_checkbox");
      label = new UILabel(new CGRect(55, 0, 300, 50));

      label.Font = UIFont.BoldSystemFontOfSize(28f);
			AddSubview(headerImage);
			AddSubview(label);
		}
	}
}
