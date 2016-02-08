using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Xfinium.Pdf.SamplesExplorer.Wp8
{
    public class SampleViewModel
    {
        /// <summary>
        /// Initializes a new <see cref="SampleViewModel"/> object.
        /// </summary>
        /// <param name="sampleInfo"></param>
        public SampleViewModel(SampleInfo sampleInfo)
        {
            this.sampleInfo = sampleInfo;
        }

        private SampleInfo sampleInfo;

        /// <summary>
        /// Gets the sample name.
        /// </summary>
        public string Name
        {
            get { return sampleInfo.Name; }
        }

        /// <summary>
        /// Gets the sample description.
        /// </summary>
        public string Description
        {
            get { return sampleInfo.Description; }
        }

        /// <summary>
        /// Gets the C# source code file.
        /// </summary>
        public string CSharpSourceCodeFile
        {
            get { return sampleInfo.CSharpSourceCodeFile; }
        }

        /// <summary>
        /// Gets the VB.NET source code file.
        /// </summary>
        public string VbNetSourceCodeFile
        {
            get { return sampleInfo.VbNetSourceCodeFile; }
        }

        /// <summary>
        /// Gets the sample id.
        /// </summary>
        public string ID
        {
            get { return sampleInfo.ID; }
        }
    }
}