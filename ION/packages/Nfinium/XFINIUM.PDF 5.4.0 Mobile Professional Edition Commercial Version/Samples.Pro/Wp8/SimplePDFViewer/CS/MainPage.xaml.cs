using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Xfinium.Pdf.Samples.Pro.WP8.SimplePDFViewer.Resources;
using Xfinium.Pdf.Rendering.RenderingSurfaces;
using System.Windows.Resources;
using System.Windows.Media;
using Xfinium.Pdf.Rendering;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace Xfinium.Pdf.Samples.Pro.WP8.SimplePDFViewer
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private const double thumbnailDpi = 10.0;
        private Image[] pageThumbnailImages;

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            StreamResourceInfo xfiniumPdf = Application.GetResourceStream(new Uri("xfinium.pdf", UriKind.Relative));
            PdfFixedDocument document = new PdfFixedDocument(xfiniumPdf.Stream);
            xfiniumPdf.Stream.Close();
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
                pageImage.Tap += pageImage_Tap;
                border.Child = pageImage;
                pageThumbnailImages[i] = pageImage;

                pageThumbnails.Children.Add(border);
            }

            for (i = 0; i < document.Pages.Count; i++)
            {
                int index = i;

                var t = Task<PdfArgbIntRenderingSurface>.Factory.StartNew(() =>
                {
                    PdfPageRenderer renderer = new PdfPageRenderer(document.Pages[index]);
                    PdfArgbIntRenderingSurface rs = renderer.CreateRenderingSurface<PdfArgbIntRenderingSurface>(thumbnailDpi, thumbnailDpi);
                    PdfRendererSettings settings = new PdfRendererSettings(thumbnailDpi, thumbnailDpi, rs);
                    renderer.ConvertPageToImage(settings);

                    return rs;
                })
                    .ContinueWith(value =>
                    {
                        PdfArgbIntRenderingSurface rs = value.Result;
                        WriteableBitmap wb = new WriteableBitmap(rs.Width, rs.Height);
                        Array.Copy(rs.Bitmap, wb.Pixels, rs.Bitmap.Length);
                        pageThumbnailImages[index].Width = wb.PixelWidth;
                        pageThumbnailImages[index].Height = wb.PixelHeight;
                        pageThumbnailImages[index].Source = wb;

                    }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        void pageImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image pageImage = sender as Image;
            int pageNumber = (int)pageImage.Tag;

            NavigationService.Navigate(new Uri("/ViewPDFPage.xaml?pagenumber=" + pageNumber, UriKind.Relative));
        }
    }
}