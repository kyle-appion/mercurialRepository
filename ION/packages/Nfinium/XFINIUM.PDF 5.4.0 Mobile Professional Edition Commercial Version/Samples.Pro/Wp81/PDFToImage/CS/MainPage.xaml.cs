using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Xfinium.Pdf.Rendering;
using Xfinium.Pdf.Rendering.RenderingSurfaces;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Xfinium.Pdf.Samples.Pro.WP81.PDFToImage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private PdfFixedDocument document;
        private int selectedPageNumber = -1;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;

            Stream pdfStream =
                assembly.GetManifestResourceStream("Xfinium.Pdf.Samples.Pro.WP81.PDFToImage.xfinium.pdf");
            document = new PdfFixedDocument(pdfStream);
            pdfStream.Dispose();

            for (int i = 0; i < document.Pages.Count; i++)
            {
                cbxPageNumber.Items.Add(i + 1);
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (cbxPageNumber.SelectedIndex != -1)
            {
                PdfPageRenderer renderer = new PdfPageRenderer(document.Pages[cbxPageNumber.SelectedIndex]);
                PdfBgraByteRenderingSurface rs = renderer.CreateRenderingSurface<PdfBgraByteRenderingSurface>(96, 96);
                PdfRendererSettings settings = new PdfRendererSettings(96, 96, rs);
                renderer.ConvertPageToImage(settings);

                WriteableBitmap pageBitmap = new WriteableBitmap(rs.Width, rs.Height);
                Stream imageStream = pageBitmap.PixelBuffer.AsStream();
                imageStream.Write(rs.Bitmap, 0, rs.Bitmap.Length);

                //pageImage.Width = pageBitmap.PixelWidth;
                //pageImage.Height = pageBitmap.PixelHeight;
                pageImage.Source = pageBitmap;
            }
        }
    }
}
