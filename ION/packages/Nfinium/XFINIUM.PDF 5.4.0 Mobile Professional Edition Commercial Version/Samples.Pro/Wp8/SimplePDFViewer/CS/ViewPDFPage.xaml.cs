using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Xfinium.Pdf.Rendering;
using Xfinium.Pdf.Rendering.RenderingSurfaces;
using System.Windows.Media.Imaging;

namespace Xfinium.Pdf.Samples.Pro.WP8.SimplePDFViewer
{
    public partial class ViewPDFPage : PhoneApplicationPage
    {
        public ViewPDFPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string pageNumber = "";
            if (NavigationContext.QueryString.TryGetValue("pagenumber", out pageNumber))
            {
                PdfFixedDocument document = (Application.Current as App).Document;

                var t = Task<PdfArgbIntRenderingSurface>.Factory.StartNew(() =>
                {
                    PdfPageRenderer renderer = new PdfPageRenderer(document.Pages[int.Parse(pageNumber)]);
                    PdfArgbIntRenderingSurface rs = renderer.CreateRenderingSurface<PdfArgbIntRenderingSurface>(96, 96);
                    PdfRendererSettings settings = new PdfRendererSettings(96, 96, rs);
                    renderer.ConvertPageToImage(settings);

                    return rs;
                })
                    .ContinueWith(value =>
                    {
                        PdfArgbIntRenderingSurface rs = value.Result;
                        WriteableBitmap wb = new WriteableBitmap(rs.Width, rs.Height);
                        Array.Copy(rs.Bitmap, wb.Pixels, rs.Bitmap.Length);
                        pageImage.Width = wb.PixelWidth;
                        pageImage.Height = wb.PixelHeight;
                        pageImage.Source = wb;

                    }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}