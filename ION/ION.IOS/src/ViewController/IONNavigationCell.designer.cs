// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ION.IOS
{
    [Register ("IONNavigationCell")]
    partial class IONNavigationCell
    {
        [Outlet]
        UIKit.UIImageView imageIcon { get; set; }


        [Outlet]
        UIKit.UILabel labelTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imageIcon != null) {
                imageIcon.Dispose ();
                imageIcon = null;
            }

            if (labelTitle != null) {
                labelTitle.Dispose ();
                labelTitle = null;
            }
        }
    }
}