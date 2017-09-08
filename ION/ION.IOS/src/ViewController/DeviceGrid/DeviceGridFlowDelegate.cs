using System;
using Foundation;
using CoreGraphics;
using UIKit;

namespace ION.IOS.ViewController.DeviceGrid {
  public class DeviceGridFlowDelegate : UICollectionViewDelegateFlowLayout {
    SensorStatusPopup passPopup;

		public DeviceGridFlowDelegate(SensorStatusPopup selectedSensor) {
			passPopup = selectedSensor;
		}

    public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath) {
			CGSize cellDimensions = new CGSize(.21 * collectionView.Bounds.Width, .18 * collectionView.Bounds.Height);
			if(indexPath.Section == 1){
        cellDimensions = new CGSize(.21 * collectionView.Bounds.Width, .08 * collectionView.Bounds.Height);
      } 
      return cellDimensions;
    }

    public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath) {
			Console.WriteLine("Selected cell index " + indexPath.Row);
      if(indexPath.Section == 0){
				var cell = collectionView.CellForItem(indexPath) as GridCellConnected;

				if (!cell.BackgroundView.Hidden) {
					if (passPopup.shouldOpen) {
						passPopup.updatePopup(cell.slotSensor);
						passPopup.popupView.Hidden = false;
					} else {
						passPopup.shouldOpen = true;
					}
				}
      } else if (indexPath.Section == 1){
				var cell = collectionView.CellForItem(indexPath) as GridCellDisconnected;

				if (!cell.BackgroundView.Hidden) {
					if (passPopup.shouldOpen) {
						passPopup.updatePopup(cell.slotSensor);
						passPopup.popupView.Hidden = false;
					} else {
						passPopup.shouldOpen = true;
					}
				}

      }


    }

		public override void DraggingStarted(UIScrollView scrollView) {
			Console.WriteLine("Started dragging");
		}

		public override void DraggingEnded(UIScrollView scrollView, bool willDecelerate) {
			Console.WriteLine("Ended dragging");
		}

		public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath) {
			return true;
		}

		public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath) {
      if (indexPath.Section == 0) {
        var cell = (GridCellConnected)collectionView.CellForItem(indexPath);
        cell.ContentView.Alpha = .5f;
      } else if (indexPath.Section == 1){
				var cell = (GridCellDisconnected)collectionView.CellForItem(indexPath);
				cell.ContentView.Alpha = .5f;
      }
		}

		public override void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath) {
			if (indexPath.Section == 0) {
				var cell = (GridCellConnected)collectionView.CellForItem(indexPath);
				cell.ContentView.Alpha = 1f;
			} else if (indexPath.Section == 1) {
				var cell = (GridCellDisconnected)collectionView.CellForItem(indexPath);
				cell.ContentView.Alpha = 1f;
			}

		}

    public override CGSize GetReferenceSizeForHeader(UICollectionView collectionView, UICollectionViewLayout layout, nint section) {
      CGSize headerDimensions = new CGSize(collectionView.Bounds.Width, 50);
      return headerDimensions;
    }

  }
}
