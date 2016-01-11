using System;
using System.Drawing;
using System.Collections.Generic;

using Foundation;
using UIKit;
using System.IO;
using Xfinium.Pdf.Samples;
using QuickLook;
using Xfinium.Pdf.Standards;
using CoreGraphics;

namespace Xfinium.Pdf.SamplesExplorer.Xamarin.iOS
{
	public partial class DetailViewController : UIViewController
	{
		private SampleInfo currentSample;
		private string pdfFileName;
		private UILabel sampleDescriptionLabel;
		
		public DetailViewController () : base ("DetailViewController", null)
		{
		}
		
		public void SetDetailItem (SampleInfo newDetailItem)
		{
			if (currentSample != newDetailItem) {
				currentSample = newDetailItem;
				
				// Update the view
				ConfigureView ();
			}
		}
		
		void ConfigureView ()
		{
			// Update the user interface for the detail item
			if ((currentSample != null) && (sampleDescriptionLabel != null))
			{
				sampleDescriptionLabel.Text = currentSample.Description;
				this.Title = currentSample.Name;
			}
		}

		partial void viewSourceCodeClick (NSObject sender)
		{
			var scvc = new SourceCodeViewController ();
			this.NavigationController.PushViewController (scvc, true);
			scvc.SetSample(currentSample);
		}

		partial void runSampleClick (NSObject sender)
		{
			SampleOutputInfo[] output = null;
			string contentPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Support/");
			string outputPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);

			switch (currentSample.ID)
			{
			case "actions":
				output = Xfinium.Pdf.Samples.Actions.Run();
				break;
			case "annotations":
				FileStream flashInput = new FileStream(contentPath + "clock.swf", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream u3dInput = new FileStream(contentPath + "airplane.u3d", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.Annotations.Run(flashInput, u3dInput);
				flashInput.Close();
				u3dInput.Close();
				break;
			case "barcodes":
				output = Xfinium.Pdf.Samples.Barcodes.Run();
				break;
			case "batesnumbers":
				FileStream batesNumbersInput = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.BatesNumbers.Run(batesNumbersInput);
				batesNumbersInput.Close();
				break;
			case "contentextraction":
				FileStream contentExtractionInput = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.ContentExtraction.Run(contentExtractionInput);
				contentExtractionInput.Close();
				break;
			case "contentstream":
				output = Xfinium.Pdf.Samples.ContentStream.Run();
				break;
			case "documentappend":
				FileStream file1Input = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream file2Input = new FileStream(contentPath + "formfill.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.DocumentAppend.Run(file1Input, file2Input);
				file1Input.Close();
				file2Input.Close();
				break;
			case "documentincrementalupdate":
				File.Copy(contentPath + "content.pdf", outputPath + "/xfinium.pdf.sample.documentincrementalupdate.pdf", true);
				FileStream incrementalUpdateInput = new FileStream(outputPath + "/xfinium.pdf.sample.documentincrementalupdate.pdf", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
				output = Xfinium.Pdf.Samples.DocumentIncrementalUpdate.Run(incrementalUpdateInput);
				pdfFileName = "xfinium.pdf.sample.documentincrementalupdate.pdf";
				incrementalUpdateInput.Flush();
				incrementalUpdateInput.Close();
				break;
			case "documentpagebypagesave":
				FileStream documentPageByPageSaveOutput = new FileStream(outputPath + "/xfinium.pdf.sample.documentpagebypagesave.pdf", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				output = Xfinium.Pdf.Samples.DocumentPageByPageSave.Run(documentPageByPageSaveOutput);
				documentPageByPageSaveOutput.Flush();
				documentPageByPageSaveOutput.Close();
				pdfFileName = "xfinium.pdf.sample.documentpagebypagesave.pdf";
				break;
			case "documentproperties":
				output = Xfinium.Pdf.Samples.DocumentProperties.Run();
				break;
			case "documentsplit":
				FileStream splitInput = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.DocumentSplit.Run(splitInput);
				splitInput.Close();
				break;
			case "encryption":
				FileStream encryptionInput = new FileStream(contentPath + "encrypted.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.Encryption.Run(encryptionInput);
				encryptionInput.Close();
				break;
			case "fileattachments":
				FileStream attachmentStream1 = new FileStream(contentPath + "fileattachments_cs.html", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream attachmentStream2 = new FileStream(contentPath + "fileattachments_vb.html", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.FileAttachments.Run(attachmentStream1, attachmentStream2);
				attachmentStream1.Close();
				attachmentStream2.Close();
				break;
			case "fonts":
				FileStream ttfStream = new FileStream(contentPath + "verdana.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.Fonts.Run(ttfStream);
				ttfStream.Close();
				break;
			case "formattedcontent":
				output = Xfinium.Pdf.Samples.FormattedContent.Run();
				break;
			case "formfill":
				FileStream formStream = new FileStream(contentPath + "formfill.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.FormFill.Run(formStream);
				formStream.Close();
				break;
			case "formgenerator":
				output = Xfinium.Pdf.Samples.FormGenerator.Run();
				break;
			case "grayscaleconversion":
				FileStream grayscaleConversionInput = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.GrayscaleConversion.Run(grayscaleConversionInput);
				grayscaleConversionInput.Close();
				break;
			case "images":
				FileStream imageStream = new FileStream(contentPath + "image.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream cmykImageStream = new FileStream(contentPath + "cmyk.tif", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream softMaskStream = new FileStream(contentPath + "softmask.png", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream stencilMaskStream = new FileStream(contentPath + "stencilmask.png", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.Images.Run(imageStream, cmykImageStream, softMaskStream, stencilMaskStream);
				imageStream.Close();
				cmykImageStream.Close();
				softMaskStream.Close();
				stencilMaskStream.Close();
				break;
			case "measurements":
				output = Xfinium.Pdf.Samples.Measurements.Run();
				break;
			case "optionalcontent":
				output = Xfinium.Pdf.Samples.OptionalContent.Run();
				break;
			case "optionalcontentextraction":
				FileStream oceInput = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.OptionalContentExtraction.Run(oceInput);
				oceInput.Close();
				break;
			case "outlines":
				output = Xfinium.Pdf.Samples.Outlines.Run();
				break;
			case "pageimposition":
				FileStream pageImpositionInput = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.PageImposition.Run(pageImpositionInput);
				pageImpositionInput.Close();
				break;
			case "pageobjects":
				FileStream pageObjectsInput = new FileStream(contentPath + "pageobjects.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.PageObjects.Run(pageObjectsInput);
				pageObjectsInput.Close();
				break;
			case "pdfa":
				FileStream pdfaIccInput = new FileStream(contentPath + "rgb.icc", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream pdfaTtfInput = new FileStream(contentPath + "verdana.ttf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.PDFA.Run(pdfaIccInput, pdfaTtfInput);
				pdfaIccInput.Close();
				pdfaTtfInput.Close();
				break;
			case "portfolios":
				FileStream imagesStream = new FileStream(contentPath + "image.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream pdfStream = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream csStream = new FileStream(contentPath + "portfolios_cs.html", FileMode.Open, FileAccess.Read, FileShare.Read);
				FileStream vbStream = new FileStream(contentPath + "portfolios_vb.html", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.Portfolios.Run(imagesStream, pdfStream, csStream, vbStream);
				imagesStream.Close();
				pdfStream.Close();
				csStream.Close();
				vbStream.Close();
				break;
			case "redaction":
				FileStream redactionStream = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.Redaction.Run(redactionStream);
				redactionStream.Close();
				break;
			case "removereplaceimages":
				FileStream removeReplaceImagesInput = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.RemoveReplaceImages.Run(removeReplaceImagesInput);
				removeReplaceImagesInput.Close();
				break;
			case "searchtext":
				FileStream searchTextInput = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.SearchText.Run(searchTextInput);
				searchTextInput.Close();
				break;
			case "svgtopdf":
				FileStream svgInput = new FileStream(contentPath + "tiger.svg", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.SvgToPdf.Run(svgInput);
				svgInput.Close();
				break;
			case "text":
				output = Xfinium.Pdf.Samples.Text.Run();
				break;
			case "tifftopdf":
				FileStream tiffStream = new FileStream(contentPath + "sample.tif", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.TiffToPdf.Run(tiffStream);
				tiffStream.Close();
				break;
			case "type3fonts":
				output = Xfinium.Pdf.Samples.Type3Fonts.Run();
				break;
			case "vectorgraphics":
				FileStream iccStream = new FileStream(contentPath + "rgb.icc", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.VectorGraphics.Run(iccStream);
				iccStream.Close();
				break;
			case "watermarks":
				FileStream watermarksInput = new FileStream(contentPath + "content.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
				output = Xfinium.Pdf.Samples.Watermarks.Run(watermarksInput);
				watermarksInput.Close();
				break;
			}
			
			if (output != null)
			{
				if (currentSample.ID == "pdfa")
				{
				    PdfAFormatter.Save(output[0].Document as PdfFixedDocument, outputPath + "/" + output[0].FileName, PdfAFormat.PdfA1b);
				}
				else
				{
					for (int i = 0; i < output.Length; i++)
					{
					    output[i].Document.Save(outputPath + "/" + output[i].FileName);
					}
				}

				pdfFileName = output[0].FileName;

				UIAlertView messageBox = new UIAlertView("XFINIUM.PDF", "Sample completed with success", null, "Close", "View PDF");
				messageBox.Clicked += HandleClicked;
				messageBox.Show();
			}
		}

		void HandleClicked (object sender, UIButtonEventArgs e)
		{
			if (e.ButtonIndex == 1) 
			{
				QLPreviewController previewController = new QLPreviewController();
				previewController.DataSource = new PDFViewDataSource(pdfFileName);
				this.PresentViewController(previewController, true, null);
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			sampleDescriptionLabel = new UILabel (new CGRect (0, 0, View.Bounds.Width, 350));
			sampleDescriptionLabel.Lines = 0;
			View.AddSubview (sampleDescriptionLabel);
			ConfigureView ();
			// Perform any additional setup after loading the view, typically from a nib.
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
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

