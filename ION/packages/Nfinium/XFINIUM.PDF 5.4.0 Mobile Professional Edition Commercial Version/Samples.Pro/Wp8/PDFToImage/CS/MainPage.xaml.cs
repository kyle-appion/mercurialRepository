using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Xfinium.Pdf.Samples.Pro.WP8.PDFToImage.Resources;
using System.Windows.Resources;
using Xfinium.Pdf.Rendering;
using Xfinium.Pdf.Rendering.RenderingSurfaces;
using System.Windows.Media.Imaging;

namespace Xfinium.Pdf.Samples.Pro.WP8.PDFToImage
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private PdfFixedDocument document;
        private int selectedPageNumber = -1;

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            StreamResourceInfo pdfStream = Application.GetResourceStream(new Uri("xfinium.pdf", UriKind.Relative));
            document = new PdfFixedDocument(pdfStream.Stream);
            pdfStream.Stream.Close();

            for (int i = 0; i < document.Pages.Count; i++)
            {
                RadioButton rbtn = new RadioButton();
                rbtn.Content = (i + 1).ToString();
                rbtn.Tag = i;
                rbtn.Click += rbtn_Click;
                pageList.Children.Add(rbtn);
            }
        }

        void rbtn_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;
            selectedPageNumber = (int)rbtn.Tag;
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPageNumber != -1)
            {
                PdfPageRenderer renderer = new PdfPageRenderer(document.Pages[selectedPageNumber]);
                WriteableBitmap pageBitmap = renderer.ConvertPageToImage(96);

                pageImage.Source = pageBitmap;
            }
        }
    }
}