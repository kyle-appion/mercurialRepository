using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Reflection;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Rendering;
using Android.Graphics;

namespace PDFToImage
{
	[Activity (Label = "PDFToImage", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private PdfFixedDocument document;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			Assembly asm = Assembly.GetExecutingAssembly(); 
			Stream pdfStream = asm.GetManifestResourceStream("PDFToImage.xfinium.pdf");
			document = new PdfFixedDocument (pdfStream);
			pdfStream.Close(); 

			Spinner pageNumbers = FindViewById<Spinner>(Resource.Id.pageNumberSpinner);
			ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem);
			pageNumbers.Adapter = adapter;
			adapter.Add("Please select a page number");
			for (int i = 1; i <= document.Pages.Count; i++) 
			{
				adapter.Add(i.ToString());
			}
			Button button = FindViewById<Button> (Resource.Id.btnConvertToImage);
			
			button.Click += delegate {
				if ((pageNumbers.SelectedItemPosition >= 1) && (pageNumbers.SelectedItemPosition <= document.Pages.Count))
				{
					PdfPageRenderer renderer = new PdfPageRenderer (document.Pages [pageNumbers.SelectedItemPosition - 1]);
					Bitmap pageImage = renderer.ConvertPageToImage (96);

					ImageView pageImageView = FindViewById<ImageView> (Resource.Id.pageImageView);
					pageImageView.SetImageBitmap(pageImage);
				}
			};
		}
	}
}


