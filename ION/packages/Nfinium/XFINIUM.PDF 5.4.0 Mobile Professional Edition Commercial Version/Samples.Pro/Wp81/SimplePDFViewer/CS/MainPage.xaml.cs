using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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

namespace Xfinium.Pdf.Samples.Pro.WP81.SimplePDFViewer
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

        private const double thumbnailDpi = 10.0;
        private Image[] pageThumbnailImages;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((Application.Current as App).Document != null)
            {
                return;
            }

            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;

            Stream pdfStream =
                assembly.GetManifestResourceStream("Xfinium.Pdf.Samples.Pro.WP81.SimplePDFViewer.xfinium.pdf");
            PdfFixedDocument document = new PdfFixedDocument(pdfStream);
            pdfStream.Dispose();
            (Application.Current as App).Document = document;

            int i = 0;

            pageThumbnailImages = new Image[document.Pages.Count];

            for (i = 0; i < document.Pages.Count; i++)
            {
                Border border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.Margin = new Thickness(10, 10, 0, 0);

                Image pageImage = new Image();
                pageImage.Tag = i;
                pageImage.Tapped += pageImage_Tapped;
                border.Child = pageImage;
                pageThumbnailImages[i] = pageImage;

                pageThumbnails.Children.Add(border);
            }

            for (i = 0; i < document.Pages.Count; i++)
            {
                int index = i;

                var t = Task<PdfBgraByteRenderingSurface>.Factory.StartNew(() =>
                {
                    PdfPageRenderer renderer = new PdfPageRenderer(document.Pages[index]);
                    PdfBgraByteRenderingSurface rs = renderer.CreateRenderingSurface<PdfBgraByteRenderingSurface>(thumbnailDpi, thumbnailDpi);
                    PdfRendererSettings settings = new PdfRendererSettings(thumbnailDpi, thumbnailDpi, rs);
                    renderer.ConvertPageToImage(settings);

                    return rs;
                })
                    .ContinueWith(value =>
                    {
                        PdfBgraByteRenderingSurface rs = value.Result;
                        WriteableBitmap pageBitmap = new WriteableBitmap(rs.Width, rs.Height);
                        Stream imageStream = pageBitmap.PixelBuffer.AsStream();
                        imageStream.Write(rs.Bitmap, 0, rs.Bitmap.Length);

                        pageThumbnailImages[index].Width = pageBitmap.PixelWidth;
                        pageThumbnailImages[index].Height = pageBitmap.PixelHeight;
                        pageThumbnailImages[index].Source = pageBitmap;

                    }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        void pageImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image pageImage = sender as Image;
            int pageNumber = (int)pageImage.Tag;

            Frame.Navigate(typeof(ViewPDFPage), pageNumber);
        }
    }
}
