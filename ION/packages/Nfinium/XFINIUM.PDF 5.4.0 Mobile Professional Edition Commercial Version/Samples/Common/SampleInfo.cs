using System;
using System.Collections.Generic;
using System.Text;

namespace Xfinium.Pdf.SamplesExplorer
{
    /// <summary>
    /// Stores information about a sample.
    /// </summary>
    public class SampleInfo
    {
        /// <summary>
        /// Gets or sets the sample name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sample description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the C# source code file.
        /// </summary>
        public string CSharpSourceCodeFile { get; set; }

        /// <summary>
        /// Gets or sets the VB.NET source code file.
        /// </summary>
        public string VbNetSourceCodeFile { get; set; }

        /// <summary>
        /// Gets or sets the sample id.
        /// </summary>
        public string ID { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
