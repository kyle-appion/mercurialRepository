using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;


namespace Xfinium.Pdf.SamplesExplorer.Wp7
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<SampleViewModel>();
        }

        /// <summary>
        /// A collection for SampleViewModel objects.
        /// </summary>
        public ObservableCollection<SampleViewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            XDocument samplesDoc = XDocument.Load("Samples/samples.xml");
            XName n = XName.Get("sample");

            var q = from file in samplesDoc.Descendants("sample")
                    select new SampleInfo()
                    {
                        Name = file.Element("name").Value,
                        Description = file.Element("description").Value,
                        CSharpSourceCodeFile = file.Element("csharpsourcecode").Value,
                        VbNetSourceCodeFile = file.Element("vbnetsourcecode").Value,
                        ID = file.Element("id").Value,
                    };

            foreach (SampleInfo si in q)
            {
                SampleViewModel svm = new SampleViewModel(si);
                this.Items.Add(svm);
            }

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}