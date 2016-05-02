﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoreGraphics;
using UIKit;
using System.IO;
using System.IO.IsolatedStorage;
using SQLite;
using System.Threading.Tasks;
using System.Linq;

using ION.IOS.ViewController;
using ION.IOS.Util;
using ION.IOS.UI;
using ION.Core.IO;

using ION.Core.Database;
using ION.Core.Content;
using ION.Core.Devices;
using ION.Core.Report.DataLogs;
using ION.Core.Util;
using ION.Core.App;
using ION.IOS.ViewController.FileManager;

namespace ION.IOS.ViewController.Logging {
  public static class ChosenDates {
    public static DateTime subLeft;
    public static DateTime subRight;
    public static DateTime earliest;
    public static DateTime latest;
    public static List<string> includeList;
    public static Dictionary<string,int> allTimes;
    public static Dictionary<int,string> allIndexes;
  }

  public partial class LoggingViewController : BaseIONViewController {
    public ChooseReporting reportingSection;
    public ChooseData dataSection;
    public ChooseSaved savedReportsSection;
    public ChooseGraphing graphingSection;
    private IION ion;
    public UIActivityIndicatorView activityLoadingGraphs;
    public ObservableCollection<int> selectedSessions;

    public LoggingViewController(IntPtr handle) : base(handle) {
    
    }

    public override void ViewDidLoad() {
      base.ViewDidLoad();
      // Perform any additional setup after loading the view, typically from a nib.
      View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("CarbonBackground"));

      InitNavigationBar("ic_graph_menu", false);

      backAction = () => {
        root.navigation.ToggleMenu();
      };
      Title = "Reports";
      ChosenDates.includeList = new List<string> ();
      selectedSessions = new ObservableCollection<int>();
      ChosenDates.allTimes = new Dictionary<string,int> ();
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

      dataSection = new ChooseData(View,selectedSessions,this);
      dataSection.showGraphButton.TouchUpInside += showGraphSection;

      savedReportsSection = new ChooseSaved(View);

      graphingSection = new ChooseGraphing(View, dataSection);

      View.AddSubview(reportingSection.reportType);
      View.AddSubview(dataSection.DataType);
      View.AddSubview(savedReportsSection.showReports);
      View.AddSubview(graphingSection.graphingType);
      View.AddSubview(dataSection.showGraphButton);
    }
     /// <summary>
     /// Loads the saved reports.
     /// </summary>
     /// <param name="sender">Sender.</param>
     /// <param name="e">E.</param>
    public void loadSavedReports (object sender, EventArgs e){
      reportingSection.newReport.Enabled = false;
      View.BringSubviewToFront(reportingSection.reportType);
      reportingSection.newReport.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
        () =>{          
          dataSection.jobButton.Hidden = true;
          dataSection.sessionButton.Hidden = true;
          dataSection.showGraphButton.Hidden = true;
          dataSection.jobTable.Hidden = true;
          dataSection.sessionTable.Hidden = true;
          dataSection.middleBorder.Hidden = true;
          dataSection.bottomBorder.Hidden = true;
          while(dataSection.selectedSessions.Count > 0){
            dataSection.selectedSessions.RemoveAt(0);
          }
          dataSection.showGraphButton.Enabled = false;
          dataSection.showGraphButton.Alpha = .6f;
          dataSection.DataType.Frame = new CGRect(.01 * View.Bounds.Width, .05 * View.Bounds.Height, .98 * View.Bounds.Width, .08 * View.Bounds.Height);
        }, 
        () => {
          dataSection.DataType.Hidden = true;
          resizeSavedReportsSectionLarger();
        });
    }
    /// <summary>
    /// Triggers the job and session section to appear/expand from the new reports button in the reporting section
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void showDataSection (object sender, EventArgs e){
      reportingSection.savedReports.Enabled = false;
      View.BringSubviewToFront(reportingSection.reportType);
      reportingSection.savedReports.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
        () =>{          
          savedReportsSection.reportTable.Hidden = true;
          savedReportsSection.header.Hidden = true;
          savedReportsSection.showReports.Frame = new CGRect(.01 * View.Bounds.Width, .05 * View.Bounds.Height, .98 * View.Bounds.Width, .08 * View.Bounds.Height);
        }, 
        () => {
          savedReportsSection.showReports.Hidden = true;
          resizeDataSectionLarger();
        });
    }
    /// <summary>
    /// Expands the reporting type section
    /// </summary>
    public void resizeReportingSectionLarger(){
      UIView.Animate(.5,0, UIViewAnimationOptions.CurveEaseInOut,
        () =>{
          reportingSection.reportType.Frame = new CGRect(.01 * View.Bounds.Width, 0, .98 * View.Bounds.Width, .15 * View.Bounds.Height);
        },
        () => {
          reportingSection.newReport.Hidden = false;
          reportingSection.savedReports.Hidden = false;
        });
    }
    /// <summary>
    /// Expands the saved reports section
    /// </summary>
    public void resizeSavedReportsSectionLarger(){
      List<string> files = new List<string>();
      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {        
        savedReportsSection.showReports.Hidden = false;
        savedReportsSection.showReports.Frame = new CGRect(.01 * View.Bounds.Width, .15 * View.Bounds.Height + 20, .98 * View.Bounds.Width, .75 * View.Bounds.Height);
      },
        () => {          
          savedReportsSection.reportTable.Hidden = false;
          savedReportsSection.header.Hidden = false;
          savedReportsSection.header.ClipsToBounds = true;
          var dir = ion.fileManager.GetApplicationInternalDirectory();
          foreach(var file in Directory.GetFiles(dir.fullPath,"*.xlsx")){
            files.Add(file);
          }
          savedReportsSection.reportTable.Source = new SpreadsheetTableSource(files);
          savedReportsSection.reportTable.ReloadData();
          savedReportsSection.reportTable.Hidden = false;
          reportingSection.newReport.Enabled = true;
        });
    }
    /// <summary>
    /// Expands the job and session section
    /// </summary>
    public void resizeDataSectionLarger(){
      dataSection.DataType.Hidden = false;
      reportingSection.reportType.Hidden = false;
      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        reportingSection.reportType.Frame = new CGRect(.01 * View.Bounds.Width, 20, .98 * View.Bounds.Width, .15 * View.Bounds.Height);
        dataSection.DataType.Frame = new CGRect(.01 * View.Bounds.Width, .15 * View.Bounds.Height + 20, .98 * View.Bounds.Width, .7 * View.Bounds.Height);
        dataSection.DataType.Layer.CornerRadius = 0f;
        dataSection.DataType.Layer.BorderWidth = 1f;
      },
      () => {
          dataSection.jobButton.SendActionForControlEvents(UIControlEvent.TouchUpInside);
          dataSection.jobButton.Hidden = false;
          dataSection.sessionButton.Hidden = false;
          dataSection.showGraphButton.Hidden = false;
          dataSection.jobTable.Hidden = false;
          dataSection.sessionTable.Hidden = true;
          dataSection.sessionHeader.Hidden = false;
          dataSection.middleBorder.Hidden = false;
          dataSection.bottomBorder.Hidden = false;
          reportingSection.newReport.Hidden = false;
          reportingSection.savedReports.Hidden = false;
          if(dataSection.selectedSessions.Count <= 0){
            dataSection.showGraphButton.Enabled = false;
            dataSection.showGraphButton.Alpha = .6f;
          }
          reportingSection.savedReports.Enabled = true;
        });
    }
    /// <summary>
    /// Expands the graphing section 
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void showGraphSection(object sender, EventArgs e){
      dataSection.jobButton.Hidden = true;
      dataSection.jobButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      dataSection.sessionButton.Hidden = true;
      dataSection.sessionButton.BackgroundColor = UIColor.FromRGB(255, 215, 101);
      dataSection.showGraphButton.Hidden = true;
      dataSection.jobTable.Hidden = true;
      dataSection.sessionHeader.Hidden = true;
      dataSection.sessionTable.Hidden = true;
      dataSection.middleBorder.Hidden = true;
      dataSection.bottomBorder.Hidden = true;
      reportingSection.newReport.Hidden = true;
      reportingSection.savedReports.Hidden = true;
      //////resize reporting choice off screen
      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        reportingSection.reportType.Frame = new CGRect(.01 * View.Bounds.Width, 0, .98 * View.Bounds.Width, 0);
      }, 
        () => {
          reportingSection.reportType.Hidden = true;
        });
      
      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        dataSection.DataType.Frame = new CGRect(.01 * View.Bounds.Width, 20, .98 * View.Bounds.Width, .08 * View.Bounds.Height);
        dataSection.DataType.Layer.CornerRadius = 8f;
        dataSection.DataType.Layer.BorderWidth = 0;
      },
      () => {
        dataSection.step2.Hidden = false;
        View.BringSubviewToFront(dataSection.DataType);
        resizeGraphingSectionLarger();
      });
    }

    /// <summary>
    /// Resizes the graphing section larger.
    /// </summary>
    public void resizeGraphingSectionLarger(){
      if (activityLoadingGraphs != null) {
        activityLoadingGraphs = null;
      }

      activityLoadingGraphs = new UIActivityIndicatorView(new CGRect(0, .14 * View.Bounds.Height, View.Bounds.Width,.83 * View.Bounds.Height));
      activityLoadingGraphs.Alpha = .4f;
      activityLoadingGraphs.Layer.CornerRadius = 8;
      activityLoadingGraphs.BackgroundColor = UIColor.DarkGray;

      View.AddSubview(activityLoadingGraphs);
      View.BringSubviewToFront(activityLoadingGraphs);

      ChosenDates.includeList = new List<string> ();
      ChosenDates.allTimes = new Dictionary<string,int> ();
      ChosenDates.allIndexes = new Dictionary<int,string> ();
      var paramList = new List<string>();

      foreach (var num in dataSection.selectedSessions) {
        paramList.Add('"' + num.ToString() + '"');
        Console.WriteLine("Querying with session:"+ num);
      }

      var graphResult = ion.database.Query<ION.Core.Database.SessionRow>("SELECT SID, sessionStart, sessionEnd, frn_JID FROM SessionRow WHERE SID in (" + string.Join(",",paramList.ToArray()) + ") ORDER BY SID");
      Console.WriteLine("Grabbed " + graphResult.Count + " session results");
      var tempResults = new List<deviceReadings>();
      var holderList = new List<string>();
      var sessionBreaks = new string[graphResult.Count];

      for(int s = 0; s < graphResult.Count; s++){
        var deviceCount = ion.database.Query<ION.Core.Database.SensorMeasurementRow>("SELECT DISTINCT serialNumber FROM SensorMeasurementRow WHERE frn_SID = ? ORDER BY serialNumber ASC", graphResult[s].SID);
        Console.WriteLine("Grabbed " + deviceCount.Count + " device results");
        for(int m = 0; m < deviceCount.Count; m++){
          var activeDevice = new deviceReadings();
          activeDevice.times = new List<DateTime>();
          activeDevice.readings = new List<double>();
          activeDevice.SID = graphResult[s].SID;
          activeDevice.frnJID = graphResult[s].frn_JID;
          activeDevice.name = deviceCount[m].serialNumber;

          var measurementCount = ion.database.Query<ION.Core.Database.SensorMeasurementRow>("SELECT * FROM SensorMeasurementRow WHERE serialNumber = ? AND frn_SID = ? ORDER BY MID ASC",activeDevice.name, graphResult[s].SID);
          Console.WriteLine("Using sensor index: " + measurementCount[0].sensorIndex);
          var df = ion.deviceManager.__deviceFactory;
          var tempD = df.GetDeviceDefinition(SerialNumberExtensions.ParseSerialNumber(activeDevice.name)) as GaugeDeviceDefinition;
          activeDevice.type = tempD.sensorDefinitions[measurementCount[0].sensorIndex].sensorType.ToString();

          foreach(var meas in measurementCount){
            activeDevice.times.Add(meas.recordedDate.ToLocalTime());
            if (!holderList.Contains(meas.recordedDate.ToLocalTime().ToString())) {
              holderList.Add(meas.recordedDate.ToLocalTime().ToString());
            }
            var measurement = Convert.ToDouble(meas.measurement);
            activeDevice.readings.Add(measurement);
          }
          tempResults.Add(activeDevice);
        }
        if (holderList.Count > 0) {
          sessionBreaks[s] = holderList[holderList.Count - 1];
        }
      }
      holderList.OrderBy(x => x);

      var extraPlots = holderList.Count;

      extraPlots = (extraPlots + (int)(extraPlots * .05)) - holderList.Count;
      if (extraPlots == 0) {
        extraPlots = 1;
      }
      var indexes = 0;
      var breakPoint = 0;

      foreach (var time in holderList) {
        ChosenDates.allTimes.Add(time, indexes);
        ChosenDates.allIndexes.Add(indexes, time);
        if (sessionBreaks[breakPoint].Equals(time)) {
          indexes = indexes + extraPlots;
          breakPoint = breakPoint + 1;
        } else {
          indexes = indexes + 1;
        }
      }

      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        activityLoadingGraphs.StartAnimating();
        graphingSection.graphingType.Hidden = false;
        graphingSection.graphingType.Frame = new CGRect(.01 * View.Bounds.Width, .14 * View.Bounds.Height + 20, .98 * View.Bounds.Width, .7 * View.Bounds.Height);
      },
      () => {
          graphingSection.graphingView = new GraphingView(graphingSection.graphingType,this, tempResults,selectedSessions);
          graphingSection.legendView = new LegendView(graphingSection.graphingType,tempResults,this);
          graphingSection.graphingType.AddSubview (graphingSection.graphingView.gView);
          graphingSection.graphingType.AddSubview (graphingSection.legendView.lView);
          graphingSection.graphingType.SendSubviewToBack (graphingSection.legendView.lView);
          if(holderList.Count > 0){
            graphingSection.SetupSettingsButtons(graphingSection.graphingType,activityLoadingGraphs);
            View.AddSubview(graphingSection.graphingView.resetButton);
            View.AddSubview(graphingSection.graphingView.exportGraph);
          } else {
            activityLoadingGraphs.StopAnimating();
          }
          graphingSection.graphingView.Hidden = false;
          graphingSection.legendView.Hidden = false;

          dataSection.resize = new UITapGestureRecognizer(() => {
            resizeGraphingSectionSmaller();
          });
          dataSection.DataType.AddGestureRecognizer(dataSection.resize);
        });
    }
    /// <summary>
    /// Resizes the graphing section smaller and returns the user to the session/job selection view
    /// </summary>
    public void resizeGraphingSectionSmaller(){
      View.BringSubviewToFront(dataSection.DataType);
      if (graphingSection.graphingView != null) {
        if (graphingSection.graphingView.resetButton != null) {
          graphingSection.graphingView.resetButton.Hidden = true;
          graphingSection.graphingView.exportGraph.Hidden = true;
        }
        if (graphingSection.graphingView.graphTable != null) {
          graphingSection.graphingView.graphTable.Source = null;
          graphingSection.graphingView.graphTable.ReloadData();
        }
        graphingSection.graphingView.gView.Hidden = true;
        graphingSection.graphingView = null;
        graphingSection.legendView.lView.Hidden = true;
        graphingSection.legendView = null;
      }

      UIView.Animate(.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
        dataSection.step2.Hidden = true;
        graphingSection.graphingType.Frame = new CGRect(.01 * View.Bounds.Width, .04 * View.Bounds.Height, .98 * View.Bounds.Width, .08 * View.Bounds.Height);
      }, 
      () => {
          graphingSection.graphingType.Hidden = true;
          dataSection.DataType.RemoveGestureRecognizer(dataSection.resize);
          resizeDataSectionLarger();
      });
    }

    public override void ViewDidAppear(bool animated) {
      if (dataSection.DataType != null) {
        dataSection.ReloadAllJobs();
      }
    }

    public override void DidReceiveMemoryWarning() {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


