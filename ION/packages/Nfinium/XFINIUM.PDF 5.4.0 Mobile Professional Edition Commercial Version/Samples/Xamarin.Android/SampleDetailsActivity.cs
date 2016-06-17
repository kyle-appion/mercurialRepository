
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xfinium.Pdf.Samples;
using System.Reflection;
using Xfinium.Pdf.Standards;

namespace Xfinium.Pdf.SamplesExplorer.Xamarin.Android
{
	[Activity (Label = "Xfinium.Pdf Samples Explorer")]			
	public class SampleDetailsActivity : Activity
	{
		private string sampleName;
		private string sampleID;
		private string sampleCsSourceCodeFile;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.SampleDetails);

			sampleName = Intent.GetStringExtra ("SampleName");
			if (sampleName != null) 
			{
				TextView tvSampleName = FindViewById<TextView>(Resource.Id.tvSampleName);
				tvSampleName.Text = sampleName;
				TextView tvSampleDescription = FindViewById<TextView>(Resource.Id.tvSampleDescription);
				tvSampleDescription.Text = Intent.GetStringExtra("SampleDescription");

				sampleCsSourceCodeFile = Intent.GetStringExtra ("SampleCSharpSourceCodeFile");
				sampleID = Intent.GetStringExtra ("SampleID");

				Button btnRunSample = FindViewById<Button>(Resource.Id.btnRunSample);
				btnRunSample.Click += new EventHandler(btnRunSample_Click);
	
				Button btnViewCsSourceCode = FindViewById<Button>(Resource.Id.btnViewCsSourceCode);
				btnViewCsSourceCode.Click += new EventHandler(btnViewCsSourceCode_Click);
			}
		}

		private void btnRunSample_Click (object sender, EventArgs e)
		{
			if (sampleID != null) 
			{
				SampleOutputInfo[] output = null;
                MemoryStream outputStream = null;
				Assembly asm = Assembly.GetExecutingAssembly();

                switch (sampleID)
                {
                    case "actions":
                        output = Xfinium.Pdf.Samples.Actions.Run();
                        break;
                    case "annotations":
                        Stream flashInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.clock.swf");
                        Stream u3dInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.airplane.u3d");
                        output = Xfinium.Pdf.Samples.Annotations.Run(flashInput, u3dInput);
                        flashInput.Close();
                        u3dInput.Close();
                        break;
                    case "barcodes":
                        output = Xfinium.Pdf.Samples.Barcodes.Run();
                        break;
					case "batesnumbers":
						Stream batesNumbersInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
						output = Xfinium.Pdf.Samples.BatesNumbers.Run(batesNumbersInput);
						batesNumbersInput.Close();
						batesNumbersInput = null;
						break;
					case "contentextraction":
                        Stream contentExtractionInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
                        output = Xfinium.Pdf.Samples.ContentExtraction.Run(contentExtractionInput);
                        contentExtractionInput.Close();
                        contentExtractionInput = null;
                        break;
					case "contentstream":
						output = Xfinium.Pdf.Samples.ContentStream.Run();
						break;
                    case "documentappend":
                        Stream file1Input = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
                        Stream file2Input = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.formfill.pdf");
                        output = Xfinium.Pdf.Samples.DocumentAppend.Run(file1Input, file2Input);
                        file1Input.Close();
                        file2Input.Close();
                        break;
                    case "documentincrementalupdate":
                        outputStream = new MemoryStream();
                        Stream sri = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
                        byte[] buffer = new byte[8192];
                        while (true)
                        {
                            int readSize = sri.Read(buffer, 0, buffer.Length);
                            if (readSize <= 0)
                            {
                                break;
                            }
                            outputStream.Write(buffer, 0, readSize);
                        }
                        sri.Close();

                        output = Xfinium.Pdf.Samples.DocumentIncrementalUpdate.Run(outputStream);
                        break;
                    case "documentpagebypagesave":
                        outputStream = new MemoryStream();
                        output = Xfinium.Pdf.Samples.DocumentPageByPageSave.Run(outputStream);
                        break;
                    case "documentproperties":
                        output = Xfinium.Pdf.Samples.DocumentProperties.Run();
                        break;
                    case "documentsplit":
                        Stream splitInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
                        output = Xfinium.Pdf.Samples.DocumentSplit.Run(splitInput);
                        splitInput.Close();
                        break;
                    case "encryption":
                        Stream encryptionInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.encrypted.pdf");
                        output = Xfinium.Pdf.Samples.Encryption.Run(encryptionInput);
                        encryptionInput.Close();
                        break;
                    case "fileattachments":
                        Stream attachmentStream1 = Assets.Open("fileattachments_cs.html");
                        Stream attachmentStream2 = Assets.Open("fileattachments_vb.html");
                        output = Xfinium.Pdf.Samples.FileAttachments.Run(attachmentStream1, attachmentStream2);
                        attachmentStream1.Close();
                        attachmentStream2.Close();
                        break;
                    case "fonts":
                        Stream ttfStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.verdana.ttf");
                        output = Xfinium.Pdf.Samples.Fonts.Run(ttfStream);
                        ttfStream.Close();
                        break;
					case "formattedcontent":
                        output = Xfinium.Pdf.Samples.FormattedContent.Run();
                        break;
					case "formfill":
						Stream formStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.formfill.pdf");
						output = Xfinium.Pdf.Samples.FormFill.Run(formStream);
						formStream.Close();
						break;
					case "formgenerator":
                        output = Xfinium.Pdf.Samples.FormGenerator.Run();
                        break;
					case "grayscaleconversion":
						Stream grayscaleConversionInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
						output = Xfinium.Pdf.Samples.GrayscaleConversion.Run(grayscaleConversionInput);
						grayscaleConversionInput.Close();
						break;
					case "images":
                        Stream imageStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.image.jpg");
                        Stream cmykImageStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.cmyk.tif");
                        Stream softMaskStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.softmask.png");
                        Stream stencilMaskStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.stencilmask.png");
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
						Stream oceInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
						output = Xfinium.Pdf.Samples.OptionalContentExtraction.Run(oceInput);
						oceInput.Close();
						break;
                    case "outlines":
                        output = Xfinium.Pdf.Samples.Outlines.Run();
                        break;
                    case "pageimposition":
                        Stream pageImpositionInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
                        output = Xfinium.Pdf.Samples.PageImposition.Run(pageImpositionInput);
                        pageImpositionInput.Close();
                        break;
					case "pageobjects":
						Stream pageObjectsInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.pageobjects.pdf");
						output = Xfinium.Pdf.Samples.PageObjects.Run(pageObjectsInput);
						pageObjectsInput.Close();
						break;
					case "pdfa":
						Stream pdfaIccInput = 
								asm.GetManifestResourceStream ("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.rgb.icc");
						Stream pdfaTtfInput = 
								asm.GetManifestResourceStream ("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.verdana.ttf");
						output = Xfinium.Pdf.Samples.PDFA.Run (pdfaIccInput, pdfaTtfInput);
						pdfaIccInput.Close ();
						pdfaTtfInput.Close ();
						break;
                    case "portfolios":
                        Stream imagesStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.image.jpg");
                        Stream pdfStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
                        Stream csStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.portfolios_cs.html");
                        Stream vbStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.portfolios_vb.html");
                        output = Xfinium.Pdf.Samples.Portfolios.Run(imagesStream, pdfStream, csStream, vbStream);
                        imagesStream.Close();
                        pdfStream.Close();
                        csStream.Close();
                        vbStream.Close();
                        break;
                    case "redaction":
                        Stream redactionStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
                        output = Xfinium.Pdf.Samples.Redaction.Run(redactionStream);
                        redactionStream.Close();
                        break;
					case "removereplaceimages":
						Stream removeReplaceImagesInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
						output = Xfinium.Pdf.Samples.RemoveReplaceImages.Run(removeReplaceImagesInput);
						removeReplaceImagesInput.Close();
						break;
					case "searchtext":
						Stream searchTextInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf");
						output = Xfinium.Pdf.Samples.SearchText.Run(searchTextInput);
						searchTextInput.Close();
						break;
					case "svgtopdf":
						Stream svgInput = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.tiger.svg");
						output = Xfinium.Pdf.Samples.SvgToPdf.Run(svgInput);
						svgInput.Close();
						break;
					case "text":
                        output = Xfinium.Pdf.Samples.Text.Run();
                        break;
					case "tifftopdf":
						Stream tiffStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.sample.tif");
						output = Xfinium.Pdf.Samples.TiffToPdf.Run(tiffStream);
						tiffStream.Close();
						break;
					case "type3fonts":
                        output = Xfinium.Pdf.Samples.Type3Fonts.Run();
                        break;
					case "vectorgraphics":
                        Stream iccStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.rgb.icc");
                        output = Xfinium.Pdf.Samples.VectorGraphics.Run(iccStream);
                        iccStream.Close();
                        break;
					case "watermarks":
						Stream watermarkStream = 
							asm.GetManifestResourceStream("Xfinium.Pdf.SamplesExplorer.Xamarin.Android.Support.content.pdf"); 
						output = Xfinium.Pdf.Samples.Watermarks.Run(watermarkStream);
						watermarkStream.Close();
						break;
				}

				string pdfPath = "";
				if (output != null)
                {
					if (sampleID == "pdfa") 
					{
						pdfPath = System.IO.Path.Combine(
							System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), output[0].FileName);
						Stream pdfStream = File.Open(pdfPath, FileMode.Create);
						PdfAFormatter.Save(output[0].Document as PdfFixedDocument, pdfStream, PdfAFormat.PdfA1b);
						pdfStream.Flush();
						pdfStream.Close();
					}
					else
					{
						for (int i = 0; i < output.Length; i++)
						{
							pdfPath = System.IO.Path.Combine(
								System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), output[i].FileName);
							Stream pdfStream = File.Open(pdfPath, FileMode.Create);
		                    output[i].Document.Save(pdfStream);
		                    pdfStream.Flush();
		                    pdfStream.Close();
						}
					}
				}

                if (outputStream != null)
                {
					string fileName = "";
                    switch (sampleID)
                    {
                        case "documentincrementalupdate":
                            fileName = "xfinium.pdf.sample.documentincrementalupdate.pdf";
                            break;
                        case "documentpagebypagesave":
                            fileName = "xfinium.pdf.sample.documentpagebypagesave.pdf";
                            break;
                    }

					pdfPath = System.IO.Path.Combine(
						System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
					Stream pdfStream = File.Open(pdfPath, FileMode.Create);
                    outputStream.WriteTo(pdfStream);
                    pdfStream.Flush();
                    pdfStream.Close();
                }

				Java.IO.File file = new Java.IO.File(pdfPath);
				file.SetReadable(true, false);
				global::Android.Net.Uri u = global::Android.Net.Uri.FromFile(file);
				Intent intent = new Intent(Intent.ActionView);
				intent.SetDataAndType(u, "application/pdf");
				intent.SetFlags(ActivityFlags.ClearTop);

				try
				{
					StartActivity(intent);
				}
				catch (ActivityNotFoundException ex)
				{
					Toast.MakeText(this, "No PDF viewer available", ToastLength.Short).Show();
				}
			}
		}

		private void btnViewCsSourceCode_Click (object sender, EventArgs e)
		{
			if (sampleCsSourceCodeFile != null) 
			{
				Intent sampleSourceCodeActivity = new Intent (this, typeof(SampleSourceCodeActivity));
				sampleSourceCodeActivity.PutExtra ("SampleName", sampleName);
				sampleSourceCodeActivity.PutExtra ("SampleCSharpSourceCodeFile", sampleCsSourceCodeFile);
				StartActivity (sampleSourceCodeActivity);
			}
		}
	}
}

