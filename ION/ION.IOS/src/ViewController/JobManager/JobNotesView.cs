using System;
using System.IO;
using System.Xml;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  
  public class JobNotesView {
    public UIView notesView;
    public UITextField notesText;
    public UILabel notesHeader;
    public UIButton saveNotes;
    public IION ion;
    public string fileDir;
    
    public JobNotesView(UIView parentView,int frnJID) {
      ion = AppState.context;

      notesView = new UIView(new CGRect(0,0,parentView.Bounds.Width,parentView.Bounds.Height - 70));
      notesView.Hidden = true;

      notesHeader = new UILabel(new CGRect(.25 * notesView.Bounds.Width, .05 * notesView.Bounds.Height,.5 * notesView.Bounds.Width,.05 * notesView.Bounds.Height));
      notesHeader.AdjustsFontSizeToFitWidth = true;
      notesHeader.TextAlignment = UITextAlignment.Center;
      notesHeader.Text = "Job Notes";

      notesText = new UITextField(new CGRect(.05 * notesView.Bounds.Width, .1 * notesView.Bounds.Height,.9 * notesView.Bounds.Width,.8 * notesView.Bounds.Height));
      notesText.Layer.BorderWidth = 1f;
      notesText.ShouldReturn += (textField) => {
        textField.ResignFirstResponder();
        return true;
      };

      saveNotes = new UIButton(new CGRect(.4 * notesView.Bounds.Width, .91 * notesView.Bounds.Height, .2 * notesView.Bounds.Width, .05 * notesView.Bounds.Height));
      saveNotes.SetTitle("Save Notes", UIControlState.Normal);
      saveNotes.SetTitleColor(UIColor.Black, UIControlState.Normal);
      saveNotes.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      saveNotes.Layer.BorderWidth = 1f;

      fileDir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));

      if (!frnJID.Equals(0)) {
        var infoQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT jobName FROM JobRow WHERE JID = ?", frnJID);
        if(File.Exists(infoQuery[0].jobName+".xml")){
          Console.WriteLine("XML file exists!");

          // Create an XML reader for this file.
          using (XmlReader reader = XmlReader.Create(infoQuery[0].jobName+".xml"))
          {
            while (reader.Read())
            {
              // Only detect start elements.
              if (reader.IsStartElement())
              {
                // Get element name and switch on it.
                switch (reader.Name)
                {
                  case "Notes":
                    // Detect this article element.
                    Console.WriteLine("Start <Notes> element.");
                    // Search for the attribute name on this current node.
                    string attribute = reader["Notes"];
                    if (attribute != null)
                    {
                      Console.WriteLine("  Has attribute name: " + attribute);
                    }
                    // Next read will contain text.
                    if (reader.Read())
                    {
                      Console.WriteLine("  Text node: " + reader.Value.Trim());
                    }
                    break;
                }
              }
            }
          }
        }
        else {
          Console.WriteLine("XML File does not exits");
        } 
      }

      notesView.AddSubview(notesHeader);
      notesView.AddSubview(notesText);
      notesView.AddSubview(saveNotes);
    }

  }

}

