/*



using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoreGraphics;
using UIKit;
using System.IO;
using SQLite;
using System.Threading.Tasks;
using System.Linq;

using ION.IOS.ViewController;
using ION.IOS.Util;
using ION.IOS.UI;

using ION.Core.Database;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Util;
using ION.Core.App;



namespace ION.IOS.ViewController.Logging {
  public partial class LoggingViewController : BaseIONViewController {
    public ChooseReporting reportingSection;
    public ChooseData dataSection;
    public ChooseSaved savedReportsSection;
    public ChooseGraphing graphingSection;
    private IION ion;

    public LoggingViewController(IntPtr handle) : base(handle) {
    
    }

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      // Perform any additional setup after loading the view, typically from a nib.
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));

      InitNavigationBar("ic_nav_workbench", false);

      backAction = () => {
        root.navigation.ToggleMenu();
      };
      //*************************************************************
      /*
      UIBarButtonItem dataRecord = new UIBarButtonItem(
        //UIImage.FromBundle("ic_record"),
        "R",
        UIBarButtonItemStyle.Plain, 
        (sender, e) => {
          Console.WriteLine("Clicked record");
        }
      );

      UIBarButtonItem dataStop= new UIBarButtonItem(
        "S", 
        UIBarButtonItemStyle.Plain, 
        (sender, e) => {
          Console.WriteLine("Clicked stop");
        }
      );
      //dataStop.Image = UIImage.FromBundle("ic_stop");

      UIBarButtonItem showRecords= new UIBarButtonItem(
        //UIImage.FromBundle("ic_menu"), 
        "P",
        UIBarButtonItemStyle.Plain, 
        (sender, e) => {
          Console.WriteLine("Clicked pause");
        }
      );
      //showRecords.Image = UIImage.FromBundle("ic_pause");

      UIBarButtonItem [] recordButtons = new UIBarButtonItem[]{dataStop,showRecords,dataRecord,};
      NavigationItem.SetRightBarButtonItems(recordButtons, true);
      //**************************************************************///
      ion = AppState.context;
      SetupLoggingUI();
    }
    /// <summary>
    /// Creates the job and session views and adds their views/subviews to the mainview
    /// </summary>
    public void SetupLoggingUI(){
      reportingSection = new ChooseReporting(View);
      reportingSection.savedReports.TouchUpInside += loadSavedReports;
      reportingSection.newReport.TouchUpInside += showDataSection;

      dataSection = new ChooseData(View);
      dataSection.showGraphButton.TouchUpInside += showGraphSection;

      savedReportsSection = new ChooseSaved(View);

      graphingSection = new ChooseGraphing(View);

      View.AddSubview(reportingSection.reportType);
      View.BringSubviewToFront(reportingSection.reportType);
      View.AddSubview(dataSection.DataType);
      View.BringSubviewToFront(dataSection.DataType);
      View.AddSubview(savedReportsSection.showReports);
      View.BringSubviewToFront(savedReportsSection.showReports);
      View.AddSubview(graphingSection.graphingType);
      View.BringSubviewToFront(graphingSection.graphingType);
    }
      
    public void loadSavedReports (object sender, EventArgs e){

      UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
        () =>{ 
          reportingSection.reportType.Frame = new CGRect(.01 * View.Bounds.Width, 0, .98 * View.Bounds.Width, .15 * View.Bounds.Height);
          reportingSection.newReport.Hidden = true;
          reportingSection.savedReports.Hidden = true;
        }, 
        () => {
          resizeSavedReportsSectionLarger();
          reportingSection.step1.Hidden = false;
        });
      
      reportingSection.resize = new UITapGestureRecognizer (() => {
        resizeSavedReportsSectionSmaller();
      });
      reportingSection.reportType.AddGestureRecognizer(reportingSection.resize);
    }
    /// <summary>
    /// Triggers the job and session section to appear/expand from the new reports button in the reporting section
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void showDataSection (object sender, EventArgs e){
      UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
        () =>{ 
          reportingSection.reportType.Frame = new CGRect(.01 * View.Bounds.Width, 0, .98 * View.Bounds.Width, .15 * View.Bounds.Height);
          reportingSection.newReport.Hidden = true;
          reportingSection.savedReports.Hidden = true;
        }, 
        () => {
          resizeDataSectionLarger();
          reportingSection.step1.Hidden = false;
        });

      reportingSection.resize = new UITapGestureRecognizer (() => {
        resizeDataSectionSmaller();
      });

      reportingSection.reportType.AddGestureRecognizer(reportingSection.resize);
    }
    /// <summary>
    /// Expands the job and session section
    /// </summary>
    public void resizeDataSectionLarger(){
      dataSection.DataType.Hidden = false;
      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        dataSection.DataType.Frame = new CGRect(.01 * View.Bounds.Width, .15 * View.Bounds.Height, .98 * View.Bounds.Width, .8 * View.Bounds.Height);
      }, 
      () => {
          dataSection.jobButton.Hidden = false;
          dataSection.sessionButton.Hidden = false;
          dataSection.showGraphButton.Hidden = false;
          dataSection.jobTable.Hidden = true;
          dataSection.sessionTable.Hidden = true;
      });
    }
    /// <summary>
    /// Collapses the job and session section
    /// </summary>
    public void resizeDataSectionSmaller(){      
      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        reportingSection.step1.Hidden = true;
        dataSection.jobButton.Hidden = true;
        dataSection.jobTable.BackgroundColor = UIColor.LightGray;
        dataSection.sessionButton.Hidden = true;
        dataSection.sessionButton.BackgroundColor = UIColor.LightGray;
        dataSection.showGraphButton.Hidden = true;
        dataSection.jobTable.Hidden = true;
        dataSection.sessionTable.Hidden = true;
        dataSection.selectedSessions.CollectionChanged -= dataSection.checkForSelected;
        dataSection.selectedSessions = new ObservableCollection<int>();
        dataSection.selectedSessions.CollectionChanged += dataSection.checkForSelected;
        dataSection.showGraphButton.Enabled = false;
        dataSection.showGraphButton.Alpha = .6f;
        dataSection.DataType.Frame = new CGRect(.01 * View.Bounds.Width, .15 * View.Bounds.Height, .98 * View.Bounds.Width, .08 * View.Bounds.Height);
      }, 
      () => {
        dataSection.DataType.Hidden = true;
        resizeReportingSectionLarger();
      });      
    }
    /// <summary>
    /// Expands the reporting type section
    /// </summary>
    public void resizeReportingSectionLarger(){
      UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
        () =>{
          reportingSection.reportType.Frame = new CGRect(.01 * View.Bounds.Width, 0, .98 * View.Bounds.Width, .55 * View.Bounds.Height);
        }, 
        () => {
          reportingSection.newReport.BackgroundColor = UIColor.LightGray;
          reportingSection.savedReports.BackgroundColor = UIColor.LightGray;
          reportingSection.newReport.Hidden = false;
          reportingSection.savedReports.Hidden = false;
          reportingSection.reportType.RemoveGestureRecognizer(reportingSection.resize);
        });
    }     
    /// <summary>
    /// Expands the saved reports section
    /// </summary>
    public void resizeSavedReportsSectionLarger(){
      
      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        savedReportsSection.showReports.Hidden = false;
        savedReportsSection.showReports.Frame = new CGRect(.01 * View.Bounds.Width, .15 * View.Bounds.Height, .98 * View.Bounds.Width, .8 * View.Bounds.Height);
      }, 
        () => {
          savedReportsSection.reportTable.Hidden = false;
          savedReportsSection.header.Hidden = false;
        }); 
    }
    /// <summary>
    /// Collapses the saved report section and then expands the reporting type section
    /// </summary>
    public void resizeSavedReportsSectionSmaller(){
      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        reportingSection.step1.Hidden = true;
        savedReportsSection.reportTable.Hidden = true;
        savedReportsSection.header.Hidden = true;
        savedReportsSection.showReports.Frame = new CGRect(.01 * View.Bounds.Width, .15 * View.Bounds.Height, .98 * View.Bounds.Width, .08 * View.Bounds.Height);
      }, 
        () => {
          savedReportsSection.showReports.Hidden = true;
          resizeReportingSectionLarger();
        });  
    }
    /// <summary>
    /// Expands the graphing section 
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void showGraphSection(object sender, EventArgs e){
      dataSection.jobButton.Hidden = true;
      dataSection.jobButton.BackgroundColor = UIColor.LightGray;
      dataSection.sessionButton.Hidden = true;
      dataSection.sessionButton.BackgroundColor = UIColor.LightGray;
      dataSection.showGraphButton.Hidden = true;
      dataSection.jobTable.Hidden = true;
      dataSection.sessionTable.Hidden = true;

      reportingSection.reportType.RemoveGestureRecognizer(reportingSection.resize);

      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {        
        dataSection.DataType.Frame = new CGRect(.01 * View.Bounds.Width, .15 * View.Bounds.Height, .98 * View.Bounds.Width, .08 * View.Bounds.Height);
      }, 
      () => {
        dataSection.step2.Hidden = false;
        resizeGraphingSectionLarger();
      });
    }

    /// <summary>
    /// Resizes the graphing section larger.
    /// </summary>
    public void resizeGraphingSectionLarger(){       
      var paramList = new List<string>();
      string recordText = "";

      foreach (var num in dataSection.selectedSessions) {
        paramList.Add('"' + num.ToString() + '"');
      }

      var graphResult = ion.database.Query<ION.Core.Database.Session>("SELECT SID, sessionStart, sessionEnd FROM Session WHERE SID in (" + string.Join(",",paramList.ToArray()) + ")");

      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        graphingSection.graphingType.Hidden = false;
        graphingSection.graphingType.Frame = new CGRect(.01 * View.Bounds.Width, .23 * View.Bounds.Height, .98 * View.Bounds.Width, .72 * View.Bounds.Height);
      }, 
      () => {
          graphingSection.header.Hidden = false;
          graphingSection.rawData.Hidden = false;

          if(graphResult.Count > 0){
//            foreach(var record in graphResult){
//              recordText += string.Concat(Environment.NewLine, (record.sessionStart.ToLocalTime() + " - " + record.sessionEnd.ToLocalTime()));
//              var sessionM = ion.database.Query<ION.Core.Database.SessionMeasurement>("SELECT deviceSN, deviceMeasurement FROM SessionMeasurement WHERE frnSID = ? ORDER BY MID DESC", record.SID);
//
//              foreach(var value in sessionM){
//                recordText += string.Concat(Environment.NewLine, "\t" + value.deviceSN + " " + value.deviceMeasurement);
//              }
//            }

            for(int s = 0; s < graphResult.Count; s++){
              var deviceCount = ion.database.Query<ION.Core.Database.SessionMeasurement>("SELECT DISTINCT deviceSN FROM SessionMeasurement WHERE frnSID = " + graphResult[s].SID);
              recordText += string.Concat(Environment.NewLine, graphResult[s].sessionStart.ToLocalTime() + " - " + graphResult[s].sessionEnd.ToLocalTime());

              for(int m = 0; m < deviceCount.Count; m++){
                recordText += string.Concat(Environment.NewLine, "\t" + deviceCount[m].deviceSN);
                var measurementCount = ion.database.Query<ION.Core.Database.SessionMeasurement>("SELECT * FROM SessionMeasurement WHERE deviceSN = ? AND frnSID = ?",deviceCount[m].deviceSN, graphResult[s].SID);

                foreach(var meas in measurementCount){
                  recordText += string.Concat(Environment.NewLine, "\t\t" + meas.deviceMeasurement);
                }
              }
            }
            graphingSection.rawData.Text = recordText;
          } else {
            graphingSection.rawData.Text = "No Session data available";
          }
      });

      dataSection.resize = new UITapGestureRecognizer(() => {
        resizeGraphingSectionSmaller();
      });
      dataSection.DataType.AddGestureRecognizer(dataSection.resize);
    }

    public void resizeGraphingSectionSmaller(){
      reportingSection.reportType.AddGestureRecognizer(reportingSection.resize);
      dataSection.selectedSessions.CollectionChanged -= dataSection.checkForSelected;
      dataSection.selectedSessions = new ObservableCollection<int>();
      dataSection.selectedSessions.CollectionChanged += dataSection.checkForSelected;
      dataSection.showGraphButton.Enabled = false;
      dataSection.showGraphButton.Alpha = .6f;
      graphingSection.rawData.Text = ""; 

      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        dataSection.step2.Hidden = true;
        graphingSection.header.Hidden = true;
        graphingSection.rawData.Hidden = true;
        graphingSection.graphingType.Frame = new CGRect(.01 * View.Bounds.Width, .23 * View.Bounds.Height, .98 * View.Bounds.Width, .15 * View.Bounds.Height);
      }, 
      () => {
          graphingSection.header.Hidden = true;
          graphingSection.graphingType.Hidden = true;
          dataSection.DataType.RemoveGestureRecognizer(dataSection.resize);
          resizeDataSectionLarger();
      });
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


*/