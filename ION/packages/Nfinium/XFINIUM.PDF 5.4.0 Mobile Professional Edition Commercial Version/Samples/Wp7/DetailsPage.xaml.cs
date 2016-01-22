using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Resources;
using Xfinium.Pdf.Samples;
using Xfinium.Pdf.Standards;

namespace Xfinium.Pdf.SamplesExplorer.Wp7
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();
        }

        private int sampleIndex = -1;

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                sampleIndex = int.Parse(selectedIndex);
                DataContext = App.ViewModel.Items[sampleIndex];
            }
        }

        private void ViewCSharpSourceCodeMenuItem_Click(object sender, EventArgs e)
        {
            if (sampleIndex != -1)
            {
                SampleViewModel svm = (SampleViewModel)DataContext;
                // Navigate to the new page
                NavigationService.Navigate(new Uri("/SourceCodePage.xaml?selectedItem=" + sampleIndex + "&file=" + svm.CSharpSourceCodeFile, UriKind.Relative));
            }
        }

        private void ViewVbNetSourceCodeMenuItem_Click(object sender, EventArgs e)
        {
            if (sampleIndex != -1)
            {
                SampleViewModel svm = (SampleViewModel)DataContext;
                // Navigate to the new page
                NavigationService.Navigate(new Uri("/SourceCodePage.xaml?selectedItem=" + sampleIndex + "&file=" + svm.VbNetSourceCodeFile, UriKind.Relative));
            }
        }

        private void RunSample_Click(object sender, RoutedEventArgs e)
        {
            SampleViewModel svm = DataContext as SampleViewModel;
            if (svm != null)
            {
                SampleOutputInfo[] output = null;
                MemoryStream outputStream = null;

                switch (svm.ID)
                {
                    case "actions":
                        output = Xfinium.Pdf.Samples.Actions.Run();
                        break;
                    case "annotations":
                        StreamResourceInfo flashInput = Application.GetResourceStream(new Uri("support/clock.swf", UriKind.Relative));
                        StreamResourceInfo u3dInput = Application.GetResourceStream(new Uri("support/airplane.u3d", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.Annotations.Run(flashInput.Stream, u3dInput.Stream);
                        flashInput.Stream.Close();
                        u3dInput.Stream.Close();
                        break;
                    case "barcodes":
                        output = Xfinium.Pdf.Samples.Barcodes.Run();
                        break;
                    case "batesnumbers":
                        StreamResourceInfo batesNumbersInput = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.BatesNumbers.Run(batesNumbersInput.Stream);
                        batesNumbersInput.Stream.Close();
                        batesNumbersInput = null;
                        break;
                    case "contentextraction":
                        StreamResourceInfo contentExtractionInput = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.ContentExtraction.Run(contentExtractionInput.Stream);
                        contentExtractionInput.Stream.Close();
                        contentExtractionInput = null;
                        break;
                    case "contentstream":
                        output = Xfinium.Pdf.Samples.ContentStream.Run();
                        break;
                    case "documentappend":
                        StreamResourceInfo file1Input = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        StreamResourceInfo file2Input = Application.GetResourceStream(new Uri("support/formfill.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.DocumentAppend.Run(file1Input.Stream, file2Input.Stream);
                        file1Input.Stream.Close();
                        file2Input.Stream.Close();
                        break;
                    case "documentincrementalupdate":
                        outputStream = new MemoryStream();

                        StreamResourceInfo sri = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        byte[] buffer = new byte[8192];
                        while (true)
                        {
                            int readSize = sri.Stream.Read(buffer, 0, buffer.Length);
                            if (readSize <= 0)
                            {
                                break;
                            }
                            outputStream.Write(buffer, 0, readSize);
                        }
                        sri.Stream.Close();

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
                        StreamResourceInfo splitInput = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.DocumentSplit.Run(splitInput.Stream);
                        splitInput.Stream.Close();
                        break;
                    case "encryption":
                        StreamResourceInfo encryptionInput = Application.GetResourceStream(new Uri("support/encrypted.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.Encryption.Run(encryptionInput.Stream);
                        encryptionInput.Stream.Close();
                        break;
                    case "fileattachments":
                        StreamResourceInfo attachmentStream1 = Application.GetResourceStream(new Uri("support/fileattachments.cs.html", UriKind.Relative));
                        StreamResourceInfo attachmentStream2 = Application.GetResourceStream(new Uri("support/fileattachments.vb.html", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.FileAttachments.Run(attachmentStream1.Stream, attachmentStream2.Stream);
                        attachmentStream1.Stream.Close();
                        attachmentStream2.Stream.Close();
                        break;
                    case "fonts":
                        StreamResourceInfo ttfResource = Application.GetResourceStream(new Uri("support/verdana.ttf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.Fonts.Run(ttfResource.Stream);
                        ttfResource.Stream.Close();
                        break;
                    case "formattedcontent":
                        output = Xfinium.Pdf.Samples.FormattedContent.Run();
                        break;
                    case "formfill":
                        StreamResourceInfo formStream = Application.GetResourceStream(new Uri("support/formfill.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.FormFill.Run(formStream.Stream);
                        formStream.Stream.Close();
                        break;
                    case "formgenerator":
                        output = Xfinium.Pdf.Samples.FormGenerator.Run();
                        break;
                    case "grayscaleconversion":
                        StreamResourceInfo grayscaleConversionInput = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.GrayscaleConversion.Run(grayscaleConversionInput.Stream);
                        grayscaleConversionInput.Stream.Close();
                        break;
                    case "images":
                        StreamResourceInfo imageStream = Application.GetResourceStream(new Uri("support/image.jpg", UriKind.Relative));
                        StreamResourceInfo cmykImageStream = Application.GetResourceStream(new Uri("support/cmyk.tif", UriKind.Relative));
                        StreamResourceInfo softMaskStream = Application.GetResourceStream(new Uri("support/softmask.png", UriKind.Relative));
                        StreamResourceInfo stencilMaskStream = Application.GetResourceStream(new Uri("support/stencilmask.png", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.Images.Run(imageStream.Stream, cmykImageStream.Stream, softMaskStream.Stream, stencilMaskStream.Stream);
                        imageStream.Stream.Close();
                        cmykImageStream.Stream.Close();
                        softMaskStream.Stream.Close();
                        stencilMaskStream.Stream.Close();
                        break;
                    case "measurements":
                        output = Xfinium.Pdf.Samples.Measurements.Run();
                        break;
                    case "optionalcontent":
                        output = Xfinium.Pdf.Samples.OptionalContent.Run();
                        break;
                    case "optionalcontentextraction":
                        StreamResourceInfo oceInput = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.OptionalContentExtraction.Run(oceInput.Stream);
                        oceInput.Stream.Close();
                        break;
                    case "outlines":
                        output = Xfinium.Pdf.Samples.Outlines.Run();
                        break;
                    case "pageimposition":
                        StreamResourceInfo pageImpositionInput = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.PageImposition.Run(pageImpositionInput.Stream);
                        pageImpositionInput.Stream.Close();
                        break;
                    case "pageobjects":
                        StreamResourceInfo pageObjectsInput = Application.GetResourceStream(new Uri("support/pageobjects.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.PageObjects.Run(pageObjectsInput.Stream);
                        pageObjectsInput.Stream.Close();
                        break;
                    case "pdfa":
                        StreamResourceInfo iccInput = Application.GetResourceStream(new Uri("support/rgb.icc", UriKind.Relative));
                        StreamResourceInfo ttfInput = Application.GetResourceStream(new Uri("support/verdana.ttf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.PDFA.Run(iccInput.Stream, ttfInput.Stream);
                        iccInput.Stream.Close();
                        ttfInput.Stream.Close();
                        break;
                    case "portfolios":
                        StreamResourceInfo imagesStream = Application.GetResourceStream(new Uri("support/image.jpg", UriKind.Relative));
                        StreamResourceInfo pdfStream = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        StreamResourceInfo csStream = Application.GetResourceStream(new Uri("support/portfolios.cs.html", UriKind.Relative));
                        StreamResourceInfo vbStream = Application.GetResourceStream(new Uri("support/portfolios.vb.html", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.Portfolios.Run(imagesStream.Stream, pdfStream.Stream, csStream.Stream, vbStream.Stream);
                        imagesStream.Stream.Close();
                        pdfStream.Stream.Close();
                        csStream.Stream.Close();
                        vbStream.Stream.Close();
                        break;
                    case "redaction":
                        StreamResourceInfo redactionStream = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.Redaction.Run(redactionStream.Stream);
                        redactionStream.Stream.Close();
                        break;
                    case "removereplaceimages":
                        StreamResourceInfo removeReplaceImagesInput = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.RemoveReplaceImages.Run(removeReplaceImagesInput.Stream);
                        removeReplaceImagesInput.Stream.Close();
                        break;
                    case "searchtext":
                        StreamResourceInfo searchTextInput = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.SearchText.Run(searchTextInput.Stream);
                        searchTextInput.Stream.Close();
                        break;
                    case "svgtopdf":
                        StreamResourceInfo svgInput = Application.GetResourceStream(new Uri("support/tiger.svg", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.SvgToPdf.Run(svgInput.Stream);
                        svgInput.Stream.Close();
                        break;
                    case "text":
                        output = Xfinium.Pdf.Samples.Text.Run();
                        break;
                    case "tifftopdf":
                        StreamResourceInfo tiffStream = Application.GetResourceStream(new Uri("support/sample.tif", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.TiffToPdf.Run(tiffStream.Stream);
                        tiffStream.Stream.Close();
                        break;
                    case "type3fonts":
                        output = Xfinium.Pdf.Samples.Type3Fonts.Run();
                        break;
                    case "vectorgraphics":
                        StreamResourceInfo iccStream = Application.GetResourceStream(new Uri("support/rgb.icc", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.VectorGraphics.Run(iccStream.Stream);
                        iccStream.Stream.Close();
                        break;
                    case "watermarks":
                        StreamResourceInfo watermarksInput = Application.GetResourceStream(new Uri("support/content.pdf", UriKind.Relative));
                        output = Xfinium.Pdf.Samples.Watermarks.Run(watermarksInput.Stream);
                        watermarksInput.Stream.Close();
                        break;
                }

                if (output != null)
                {
                    IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
                    IsolatedStorageFileStream pdfStream = new IsolatedStorageFileStream(output[0].FileName, FileMode.Create, fileStorage);
                    if (svm.ID == "pdfa")
                    {
                        PdfAFormatter.Save(output[0].Document as PdfFixedDocument, pdfStream, PdfAFormat.PdfA1b);
                    }
                    else
                    {
                        output[0].Document.Save(pdfStream, output[0].SecurityHandler);
                    }
                    pdfStream.Flush();
                    pdfStream.Close();

                    MessageBox.Show("Sample completed with success. File " + output[0].FileName + " saved to isolated storage.", "Xfinium.Pdf Samples Explorer", MessageBoxButton.OK);
                }
                if (outputStream != null)
                {
                    string fileName = "";
                    switch (svm.ID)
                    {
                        case "documentincrementalupdate":
                            fileName = "xfinium.pdf.sample.documentincrementalupdate.pdf";
                            break;
                        case "documentpagebypagesave":
                            fileName = "xfinium.pdf.sample.documentpagebypagesave.pdf";
                            break;
                    }

                    IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
                    IsolatedStorageFileStream pdfStream = new IsolatedStorageFileStream(fileName, FileMode.Create, fileStorage);
                    outputStream.WriteTo(pdfStream);
                    pdfStream.Flush();
                    pdfStream.Close();

                    MessageBox.Show("Sample completed with success. File " + fileName + " saved to isolated storage.", "Xfinium.Pdf Samples Explorer", MessageBoxButton.OK);
                }
            }
        }
    }
}