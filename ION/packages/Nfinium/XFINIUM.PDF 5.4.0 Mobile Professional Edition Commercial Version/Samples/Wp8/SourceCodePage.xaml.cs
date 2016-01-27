using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.IO;

namespace Xfinium.Pdf.SamplesExplorer.Wp8
{
    public partial class SourceCodePage : PhoneApplicationPage
    {
        public SourceCodePage()
        {
            InitializeComponent();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                DataContext = App.ViewModel.Items[index];
            }

            string sourceCodeFile = "";
            if (NavigationContext.QueryString.TryGetValue("file", out sourceCodeFile))
            {
                wbSourceCode.Navigate(new Uri("Support/" + sourceCodeFile, UriKind.Relative));
            }
        }
    }
}