using System;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using UIKit;
using Foundation;
using CoreGraphics;

using ION.Core.App;

namespace ION.IOS.ViewController.JobManager {
  
  public class JobNotesView {
    public UIView notesView;
    public UITextView notesText;
    public UILabel notesHeader;
    public UILabel saveStatus;
    public UIButton saveNotes;
    public UIButton readNotes;
    public IION ion;
    public string fileDir;
    
    public JobNotesView(UIView parentView,int frnJID) {
      ion = AppState.context;
      
      var accessoryView = new UIView(new CGRect(0,0,parentView.Bounds.Width, 40));
      accessoryView.BackgroundColor = UIColor.LightGray;
      var accessoryDoneButton = new UIButton(new CGRect(.8 * accessoryView.Bounds.Width,0,.2 * accessoryView.Bounds.Width,accessoryView.Bounds.Height));
      accessoryDoneButton.SetTitle("Done",UIControlState.Normal);
      accessoryDoneButton.TouchUpInside += (sender, e) => {
				notesText.ResignFirstResponder();
			};
      accessoryView.AddSubview(accessoryDoneButton);

      notesView = new UIView(new CGRect(0,0,parentView.Bounds.Width,parentView.Bounds.Height - 70));
      notesView.Hidden = true;
      notesView.AddGestureRecognizer(new UITapGestureRecognizer(() => {
        notesText.ResignFirstResponder();
      }));

      saveStatus = new UILabel(new CGRect(.1 * notesView.Bounds.Width,.8 * notesView.Bounds.Height + 50,.8 * notesView.Bounds.Width,.1 * notesView.Bounds.Height));
      saveStatus.AdjustsFontSizeToFitWidth = true;
      saveStatus.TextAlignment = UITextAlignment.Center;
      saveStatus.TextColor = UIColor.FromRGB(49, 111, 18);
      saveStatus.Text = "Notes Saved";
      saveStatus.Hidden = true;   

      notesText = new UITextView(new CGRect(.05 * notesView.Bounds.Width, .05 * notesView.Bounds.Height ,.9 * notesView.Bounds.Width,.55 * notesView.Bounds.Height));
      notesText.Font = UIFont.SystemFontOfSize(14);
      notesText.Layer.BorderWidth = 1f;
      notesText.UserInteractionEnabled = true;
      notesText.Editable = true;
      notesText.InputAccessoryView = accessoryView;
      notesText.AutocorrectionType = UITextAutocorrectionType.No;
      notesText.AutocapitalizationType = UITextAutocapitalizationType.None;
      notesText.SpellCheckingType = UITextSpellCheckingType.No;

      var infoQuery = ion.database.Query<ION.Core.Database.JobRow>("SELECT jobName FROM JobRow WHERE JID = ?", frnJID);
      if (infoQuery.Count > 0) {
        fileDir = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), infoQuery[0].jobName + ".xml");
      }

      saveNotes = new UIButton(new CGRect(.35 * notesView.Bounds.Width, .71 * notesView.Bounds.Height, .3 * notesView.Bounds.Width, .05 * notesView.Bounds.Height));
      saveNotes.SetTitle("Save Notes", UIControlState.Normal);
      saveNotes.SetTitleColor(UIColor.Black, UIControlState.Normal);
      saveNotes.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      saveNotes.Layer.BorderWidth = 1f;
      saveNotes.TouchDown += (sender, e) => {saveNotes.BackgroundColor = UIColor.Blue;};
      saveNotes.TouchUpOutside += (sender, e) => {saveNotes.BackgroundColor = UIColor.FromRGB(255, 215, 101);};
      saveNotes.TouchUpInside += (sender, e) => {
        saveNotes.BackgroundColor = UIColor.FromRGB(255, 215, 101);
        notesText.ResignFirstResponder();

        updateNotes(frnJID);
        using (XmlReader reader = XmlReader.Create(fileDir))
        {
          while (reader.Read())
          {
            // Only detect start elements.
            if (reader.IsStartElement())
            {
              // Get element name and switch on it.
              switch (reader.Name)
              {
                case "Info":
                  // Search for the attribute name on this current node.
                  string attribute = reader["Info"];
                  if (attribute != null) {
                    Console.WriteLine(" Has attribute name: " + attribute);
                  } 
                  // Next read will contain text.
                  if (reader.Read())
                  {
                    notesText.Text = reader.Value.Trim();
                    Console.WriteLine("Notes: "); 
                    Console.WriteLine(reader.Value.Trim());
                  }
                  break; 
              }
            }
          }
        }

        saveStatus.Hidden = false;
        fadeStatus();
      };

      loadNotes(frnJID);

      notesView.AddSubview(notesText);
      notesView.AddSubview(saveNotes);
      notesView.AddSubview(saveStatus);
    }

    public async void fadeStatus(){
      await Task.Delay(TimeSpan.FromSeconds(3));
      saveStatus.Hidden = true;
    }

    public void updateNotes(int frnJID){
      if (!frnJID.Equals(0)) {          
        if(File.Exists(fileDir)){
          File.Delete(fileDir);
          using (XmlWriter writer = XmlWriter.Create(fileDir))
          {
            writer.WriteStartDocument();
            writer.WriteStartElement("Job");

            writer.WriteStartElement("Notes");

            writer.WriteElementString("Info", notesText.Text);   // <-- These are new

            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
          }
        } else {
          using (XmlWriter writer = XmlWriter.Create(fileDir))
          {
            writer.WriteStartDocument();
            writer.WriteStartElement("Job");

            writer.WriteStartElement("Notes");

            writer.WriteElementString("Info", notesText.Text);   // <-- These are new

            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
          }
        }
      }
    }

    public void loadNotes(int frnJID){
      if (!frnJID.Equals(0)) {        
        if(File.Exists(fileDir)){
          // Create an XML reader for this file.
          using (XmlReader reader = XmlReader.Create(fileDir))
          {
            while (reader.Read())
            {
              // Only detect start elements.
              if (reader.IsStartElement())
              {
                // Get element name and switch on it.
                switch (reader.Name)
                {
                  case "Info":
                    // Search for the attribute name on this current node.
                    string attribute = reader["Info"];
                    if (attribute != null) {
                      Console.WriteLine(" Has attribute name: " + attribute);
                    } 
                    // Next read will contain text.
                    if (reader.Read())
                    {
                      notesText.Text = reader.Value.Trim();
                    }
                    break;
                }
              }
            }
          }
        }         
      }
    }

  }

}

