using System;
using System.IO;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Standards;

namespace Xfinium.Pdf.Samples
{
    /// <summary>
    /// PDF/A sample.
    /// </summary>
    public class PDFA
    {
        /// <summary>
        /// Main method for running the sample.
        /// </summary>
        public static SampleOutputInfo[] Run(Stream iccInput, Stream ttfInput)
        {
            PdfFixedDocument document = new PdfFixedDocument();

            // Setup the document by creating a PDF/A output intent, based on a RGB ICC profile, 
            // and document information and metadata
            PdfIccColorSpace icc = new PdfIccColorSpace();
            byte[] profilePayload = new byte[iccInput.Length];
            iccInput.Read(profilePayload, 0, profilePayload.Length);
            icc.IccProfile = profilePayload;
            PdfOutputIntent oi = new PdfOutputIntent();
            oi.Type = PdfOutputIntentType.PdfA1;
            oi.Info = "sRGB IEC61966-2.1";
            oi.OutputConditionIdentifier = "Custom";
            oi.DestinationOutputProfile = icc;
            document.OutputIntents = new PdfOutputIntentCollection();
            document.OutputIntents.Add(oi);

            document.DocumentInformation = new PdfDocumentInformation();
            document.DocumentInformation.Author = "XFINIUM Software";
            document.DocumentInformation.Title = "XFINIUM.PDF PDF/A-1B Demo";
            document.DocumentInformation.Creator = "XFINIUM.PDF PDF/A-1B Demo";
            document.DocumentInformation.Producer = "XFINIUM.PDF";
            document.DocumentInformation.Keywords = "pdf/a";
            document.DocumentInformation.Subject = "PDF/A-1B Sample produced by XFINIUM.PDF";
            document.XmpMetadata = new PdfXmpMetadata();

            PdfPage page = document.Pages.Add();
            page.Rotation = 90;

            // All fonts must be embedded in a PDF/A document.
            PdfStringAppearanceOptions sao = new PdfStringAppearanceOptions();
            sao.Font = new PdfAnsiTrueTypeFont(ttfInput, 24, true);
            sao.Brush = new PdfBrush(new PdfRgbColor(0, 0, 128));

            PdfStringLayoutOptions slo = new PdfStringLayoutOptions();
            slo.HorizontalAlign = PdfStringHorizontalAlign.Center;
            slo.VerticalAlign = PdfStringVerticalAlign.Bottom;
            slo.X = page.Width / 2;
            slo.Y = page.Height / 2 - 10;
            page.Graphics.DrawString("XFINIUM.PDF", sao, slo);
            slo.Y = page.Height / 2 + 10;
            slo.VerticalAlign = PdfStringVerticalAlign.Top;
            sao.Font.Size = 16;
            page.Graphics.DrawString("This is a sample PDF/A document that shows the PDF/A capabilities in XFINIUM.PDF library", sao, slo);

            // The document is formatted as PDF/A using the PdfAFormatter class:
            // PdfAFormatter.Save(document, outputStream, PdfAFormat.PdfA1b);
            SampleOutputInfo[] output = new SampleOutputInfo[] { new SampleOutputInfo(document, "xfinium.pdf.sample.pdfa.pdf") };
            return output;
        }
    }
}