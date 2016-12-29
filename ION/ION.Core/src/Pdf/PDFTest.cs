namespace ION.Core.Pdf {

	using System;
	using System.IO;

	using Appion.Commons.Util;

	using ION.Core.App;
	using ION.Core.IO;

  public class PDFTest {
    public static void Test(string name, Stream outstream) {
      
    }

    public static void TestPdfSave() {
      try {
        var fm = AppState.context.fileManager;
        var ed = fm.GetApplicationExternalDirectory();
        var file = ed.GetFile("Testo2.pdf", EFileAccessResponse.CreateIfMissing);
        var outstream = file.OpenForWriting();


        var doc = new Xfinium.Pdf.PdfFixedDocument();
        doc.InsertFile(0, 0, EmbeddedResource.Load("digital_gauge_cert.pdf"));
        doc.Save(outstream);
        /*
        var fields = doc.Form.Fields;
        for (int i = 0; i < fields.Count; i++) {
          var v = fields[i];
          E(v.Name);
        }
        */
      } catch (Exception e) {
        E("Failed to open pdf", e);
      }
    }

    private static void E(string msg, Exception e = null) {
      Log.E("PDFTest", msg, e);
    }
  }
}

