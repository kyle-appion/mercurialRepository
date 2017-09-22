#pragma clang diagnostic ignored "-Wdeprecated-declarations"
#pragma clang diagnostic ignored "-Wtypedef-redefinition"
#pragma clang diagnostic ignored "-Wobjc-designated-initializers"
#define DEBUG 1
#include <stdarg.h>
#include <objc/objc.h>
#include <objc/runtime.h>
#include <objc/message.h>
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <CoreLocation/CoreLocation.h>
#import <WebKit/WebKit.h>
#import <CoreBluetooth/CoreBluetooth.h>
#import <QuickLook/QuickLook.h>
#import <QuartzCore/QuartzCore.h>
#import <MediaPlayer/MediaPlayer.h>
#import <CloudKit/CloudKit.h>
#import <MessageUI/MessageUI.h>
#import <Intents/Intents.h>
#import <CoreGraphics/CoreGraphics.h>

@protocol UIPickerViewModel;
@class Foundation_InternalNSNotificationHandler;
@class __MonoMac_NSActionDispatcher;
@class __Xamarin_NSTimerActionDispatcher;
@class __MonoMac_NSAsyncActionDispatcher;
@class UIKit_UIControlEventProxy;
@class IONNavigationCell;
@class JobManagerViewController;
@class IONRemoteStatusCell;
@class IONRemoteWirelessCell;
@class IONRemoteBatteryCell;
@class IONRemoteMemoryCell;
@class ION_IOS_ViewController_MailDelegate;
@class ION_IOS_ViewController_DeviceGrid_DeviceGridSource;
@class ION_IOS_ViewController_DeviceGrid_Header;
@class ION_IOS_ViewController_DeviceGrid_DeviceGridFlowDelegate;
@class ION_IOS_ViewController_DeviceGrid_GridCellDisconnected;
@class ION_IOS_ViewController_BaseIONViewController;
@class AccessRequestViewController;
@class ION_IOS_ViewController_AccessRequest_AccessRequestTableCell;
@class ION_IOS_ViewController_AccessRequest_ViewingUserCell;
@class ION_IOS_ViewController_AccessRequest_AllowingUserCell;
@class RemoteSensorTableCell;
@class ION_IOS_ViewController_RemoteDeviceManager_SpaceRecord;
@class RemoteDeviceTableCell;
@class RemoteDeviceSectionHeaderTableCell;
@class ION_IOS_ViewController_RemoteAccess_RemoteAccessTableCell;
@class ION_IOS_ViewController_RssFeed_RssFeedDataSource;
@class ION_IOS_ViewController_RssFeed_RssFeedCell;
@class ION_IOS_ViewController_JobManager_CreatedJobSource;
@class ION_IOS_ViewController_JobManager_AssociatedSessionSource;
@class ION_IOS_ViewController_Logging_JobView;
@class ION_IOS_ViewController_Logging_JobCell;
@class ION_IOS_ViewController_Logging_LoggingJobSource;
@class ION_IOS_ViewController_Logging_ChooseReporting;
@class ION_IOS_ViewController_Logging_ChooseGraphing;
@class ION_IOS_ViewController_Logging_ModalPickerTransitionDelegate;
@class ION_IOS_ViewController_Logging_ModalPickerViewController;
@class ION_IOS_ViewController_Logging_graphingTableSource;
@class ION_IOS_ViewController_Logging_legendSource;
@class ION_IOS_ViewController_Logging_LegendView;
@class ION_IOS_ViewController_Logging_CustomPickerModel;
@class ION_IOS_ViewController_Logging_PDFViewDataSource;
@class ION_IOS_ViewController_Logging_PDFViewItem;
@class ION_IOS_ViewController_Logging_QLPreviewItemFileSystem;
@class ION_IOS_ViewController_Logging_QLPreviewItemBundle;
@class ION_IOS_ViewController_Logging_PreviewControllerDS;
@class ION_IOS_ViewController_Logging_SpreadsheetCell;
@class HelpViewController;
@class InfoCell;
@class LinkCell;
@class ION_IOS_ViewController_Help_HelpSource;
@class ION_IOS_ViewController_ScreenshotReport_EntryDelegate;
@class DisplayCell;
@class ION_IOS_ViewController_ScreenshotReport_ScreenshotReportSource;
@class ION_IOS_ViewController_ScreenshotReport_TextDelegate;
@class FileBrowserViewController;
@class ION_IOS_ViewController_FileManager_DataSource;
@class ION_IOS_ViewController_FileManager_QLItem;
@class FileCell;
@class ION_IOS_ViewController_Analyzer_AnalyzerDisplayView;
@class DrawAnalyserView;
@class ION_IOS_ViewController_Analyzer_AnalyzerTableSource;
@class ION_IOS_ViewController_Analyzer_SHSCTableCell;
@class ION_IOS_ViewController_Analyzer_PTTableCell;
@class ION_IOS_ViewController_Analyzer_secondarySensorCell;
@class SettingsGroupCell;
@class SettingsLabelCell;
@class SettingsNavigationItemCell;
@class SettingsSwitchCell;
@class SettingsViewController;
@class ION_IOS_ViewController_PressureTemperatureChart_PTSlideView;
@class FluidTableCell;
@class AddViewerTableCell;
@class MeasurementSensorPropertyTableCell;
@class ViewerTableCell;
@class FluidSubviewCell;
@class TimerSensorPropertyCell;
@class SecondarySensorCell;
@class TargetSHSCCell;
@class DeviceSectionHeaderTableCell;
@class SWTableViewCell;
@class DeviceTableCell;
@class SerialNumberTableCell;
@class SensorTableCell;
@class ION_IOS_Connections_BaseIOSConnection;
@class ION_IOS_Connections_IosRigadoConnection;
@class NinePatchButtonView;
@class AppDelegate;
@class IONPrimaryScreenController;
@class DeviceGridViewController;
@class ION_IOS_ViewController_DeviceGrid_GridCellConnected;
@class SessionsViewController;
@class CodeGenViewController;
@class ViewingControlViewController;
@class RemoteViewingViewController;
@class ION_IOS_ViewController_AccessRequest_AccessRequestTableSource;
@class ION_IOS_ViewController_AccessRequest_AccessViewingTableSource;
@class ION_IOS_ViewController_AccessRequest_AccessAllowingTableSource;
@class ION_IOS_ViewController_RemoteDeviceManager_RemoteDeviceManagerTableSource;
@class RemoteDeviceManagerViewController;
@class RemoteSystemViewController;
@class ION_IOS_ViewController_RemoteAccess_RemoteAccessTableSource;
@class ION_IOS_ViewController_RemoteAccess_RemoteWorkbenchViewController;
@class RssFeedViewController;
@class WalkthroughScreenshotViewController;
@class JobViewController;
@class ION_IOS_ViewController_JobManager_CreatedJobCell;
@class JobEditViewController;
@class ION_IOS_ViewController_JobManager_FloatLabeledTextField;
@class ION_IOS_ViewController_JobManager_AssociatedSessionCell;
@class ION_IOS_ViewController_JobManager_AvailableSessionCell;
@class ION_IOS_ViewController_JobManager_AvailableSessionSource;
@class LoggingViewController;
@class ION_IOS_ViewController_Logging_SessionCell;
@class ION_IOS_ViewController_Logging_LoggingSessionSource;
@class ION_IOS_ViewController_Logging_ModalPickerAnimatedDismissed;
@class ION_IOS_ViewController_Logging_ModalPickerAnimatedTransitioning;
@class ION_IOS_ViewController_Logging_graphCell;
@class ION_IOS_ViewController_Logging_GraphingView;
@class ION_IOS_ViewController_Logging_legendCell;
@class ION_IOS_ViewController_Logging_SpreadsheetTableSource;
@class GraphingViewController;
@class EntryCell;
@class ScreenshotReportViewController;
@class SelectionCell;
@class NotesCell;
@class ION_IOS_ViewController_FileManager_FileManagerSource;
@class AnalyzerViewController;
@class ION_IOS_ViewController_Analyzer_minimumTableCell;
@class ION_IOS_ViewController_Analyzer_maximumTableCell;
@class ION_IOS_ViewController_Analyzer_holdTableCell;
@class ION_IOS_ViewController_Analyzer_altTableCell;
@class ION_IOS_ViewController_Analyzer_RoCTableCell;
@class PTChartViewController;
@class SensorAlarmViewController;
@class ION_IOS_ViewController_FluidManager_FluidSource;
@class FluidManagerViewController;
@class SuperheatSubcoolViewController;
@class WorkbenchViewController;
@class RateOfChangeSensorPropertyCell;
@class ION_IOS_ViewController_Workbench_WorkbenchTableSource;
@class RateofChangeSettingsViewController;
@class DeviceManagerViewController;
@class ION_IOS_ViewController_DeviceManager_DeviceManagerTableSource;
@class ION_IOS_Connections_IosLeConnection;
@class ION_IOS_Connections_IonCBCentralManagerDelegate;
@class ION_IOS_UI_Toast;
@class CoreLocation_CLLocationManager__CLLocationManagerDelegate;
@class CoreBluetooth_CBPeripheral__CBPeripheralDelegate;
@class UIKit_UIGestureRecognizer__UIGestureRecognizerDelegate;
@class __UIGestureRecognizerToken;
@class __UIGestureRecognizerParameterlessToken;
@class __UIGestureRecognizerParametrizedToken;
@class __NSObject_Disposer;
@class UIKit_UIView_UIViewAppearance;
@class UIKit_UILabel_UILabelAppearance;
@class __UILongPressGestureRecognizer;
@class __UIPanGestureRecognizer;
@class __UIPinchGestureRecognizer;
@class UIKit_UIAlertView__UIAlertViewDelegate;
@class UIKit_UIBarButtonItem_Callback;
@class __UIRotationGestureRecognizer;
@class __UITapGestureRecognizer;
@class __UISwipeGestureRecognizer;
@class __UIScreenEdgePanGestureRecognizer;
@class UIKit_UITableViewCell_UITableViewCellAppearance;
@class UIKit_UISearchBar__UISearchBarDelegate;
@class UIKit_UITextField__UITextFieldDelegate;
@class UIKit_UIScrollView__UIScrollViewDelegate;
@class UIKit_UITextView__UITextViewDelegate;
@class UIKit_UIWebView__UIWebViewDelegate;
@class FlyoutNavigation_OpenMenuGestureRecognizer;
@class FlyoutNavigation_FlyoutNavigationController_UAUIView;
@class FlyoutNavigationController;
@class MonoTouch_Dialog_MessageSummaryView;
@class MonoTouch_Dialog_RefreshTableHeaderView;
@class MonoTouch_Dialog_GlassButton;
@class MonoTouch_Dialog_BaseBooleanImageElement_TextWithImageCellView;
@class MonoTouch_Dialog_HtmlElement_WebViewController;
@class MonoTouch_Dialog_ImageElement_MyDelegate;
@class MonoTouch_Dialog_DateTimeElement_MyViewController;
@class MonoTouch_Dialog_DialogViewController_SearchDelegate;
@class MonoTouch_Dialog_DialogViewController_Source;
@class MonoTouch_Dialog_DialogViewController_SizingSource;
@class MonoTouch_Dialog_DialogViewController;
@class MonoTouch_Dialog_MessageElement_MessageCell;
@class MonoTouch_Dialog_OwnerDrawnElement_OwnerDrawnCell;
@class MonoTouch_Dialog_OwnerDrawnElement_OwnerDrawnCellView;
@class MonoTouch_Dialog_JsonElement_ConnectionDelegate;
@class OxyPlot_Xamarin_iOS_PanZoomGestureRecognizer;
@class PlotView;
@class SWCellScrollView;
@class SWLongPressGestureRecognizer;
@protocol SWTableViewCellDelegate;
@class SWUtilityButtonTapGestureRecognizer;
@class SWUtilityButtonView;

@protocol UIPickerViewModel<UIPickerViewDataSource, UIPickerViewDelegate>
@end

@interface IONNavigationCell : UITableViewCell {
}
	@property (nonatomic, assign) UIImageView * imageIcon;
	@property (nonatomic, assign) UILabel * labelTitle;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIImageView *) imageIcon;
	-(void) setImageIcon:(UIImageView *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface JobManagerViewController : UIViewController {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface IONRemoteStatusCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelTitle;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface IONRemoteWirelessCell : UITableViewCell {
}
	@property (nonatomic, assign) UIImageView * cellImage;
	@property (nonatomic, assign) UILabel * labelTitle;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIImageView *) cellImage;
	-(void) setCellImage:(UIImageView *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface IONRemoteBatteryCell : UITableViewCell {
}
	@property (nonatomic, assign) UIImageView * cellImage;
	@property (nonatomic, assign) UILabel * labelTitle;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIImageView *) cellImage;
	-(void) setCellImage:(UIImageView *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface IONRemoteMemoryCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelTitle;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_DeviceGrid_DeviceGridSource : NSObject<UICollectionViewDataSource> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInCollectionView:(UICollectionView *)p0;
	-(NSInteger) collectionView:(UICollectionView *)p0 numberOfItemsInSection:(NSInteger)p1;
	-(UICollectionViewCell *) collectionView:(UICollectionView *)p0 cellForItemAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) collectionView:(UICollectionView *)p0 canMoveItemAtIndexPath:(NSIndexPath *)p1;
	-(UICollectionReusableView *) collectionView:(UICollectionView *)p0 viewForSupplementaryElementOfKind:(NSString *)p1 atIndexPath:(NSIndexPath *)p2;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_DeviceGrid_Header : UICollectionReusableView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) initWithFrame:(CGRect)p0;
@end

@interface ION_IOS_ViewController_DeviceGrid_DeviceGridFlowDelegate : NSObject<UICollectionViewDelegateFlowLayout, UICollectionViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(CGSize) collectionView:(UICollectionView *)p0 layout:(UICollectionViewLayout *)p1 sizeForItemAtIndexPath:(NSIndexPath *)p2;
	-(void) collectionView:(UICollectionView *)p0 didSelectItemAtIndexPath:(NSIndexPath *)p1;
	-(void) scrollViewWillBeginDragging:(UIScrollView *)p0;
	-(void) scrollViewDidEndDragging:(UIScrollView *)p0 willDecelerate:(BOOL)p1;
	-(BOOL) collectionView:(UICollectionView *)p0 shouldHighlightItemAtIndexPath:(NSIndexPath *)p1;
	-(void) collectionView:(UICollectionView *)p0 didHighlightItemAtIndexPath:(NSIndexPath *)p1;
	-(void) collectionView:(UICollectionView *)p0 didUnhighlightItemAtIndexPath:(NSIndexPath *)p1;
	-(CGSize) collectionView:(UICollectionView *)p0 layout:(UICollectionViewLayout *)p1 referenceSizeForHeaderInSection:(NSInteger)p2;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_DeviceGrid_GridCellDisconnected : UICollectionViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) initWithFrame:(CGRect)p0;
@end

@interface ION_IOS_ViewController_BaseIONViewController : UIViewController {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) viewDidLoad;
	-(void) viewDidAppear:(BOOL)p0;
	-(void) viewWillDisappear:(BOOL)p0;
	-(void) viewDidUnload;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface AccessRequestViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * accessHolderView;
	-(UIView *) accessHolderView;
	-(void) setAccessHolderView:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewWillAppear:(BOOL)p0;
	-(void) didReceiveMemoryWarning;
@end

@interface ION_IOS_ViewController_AccessRequest_AccessRequestTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_AccessRequest_ViewingUserCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_AccessRequest_AllowingUserCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface RemoteSensorTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_RemoteDeviceManager_SpaceRecord : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface RemoteDeviceTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) prepareForReuse;
	-(void) removeFromSuperview;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface RemoteDeviceSectionHeaderTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_RemoteAccess_RemoteAccessTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_RssFeed_RssFeedDataSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_RssFeed_RssFeedCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_JobManager_CreatedJobSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(NSString *) tableView:(UITableView *)p0 titleForDeleteConfirmationButtonForRowAtIndexPath:(NSIndexPath *)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_JobManager_AssociatedSessionSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_JobView : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_JobCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_Logging_LoggingJobSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSString *) tableView:(UITableView *)p0 titleForHeaderInSection:(NSInteger)p1;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_ChooseReporting : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_ChooseGraphing : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_ModalPickerTransitionDelegate : NSObject<UIViewControllerTransitioningDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(id) animationControllerForPresentedController:(UIViewController *)p0 presentingController:(UIViewController *)p1 sourceController:(UIViewController *)p2;
	-(id) animationControllerForDismissedController:(UIViewController *)p0;
	-(id) interactionControllerForPresentation:(id)p0;
	-(id) interactionControllerForDismissal:(id)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_Logging_ModalPickerViewController : UIViewController {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) viewDidLoad;
	-(void) viewWillAppear:(BOOL)p0;
	-(void) didRotateFromInterfaceOrientation:(NSInteger)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_graphingTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_Logging_legendSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_LegendView : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_CustomPickerModel : NSObject<UIPickerViewModel> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfComponentsInPickerView:(UIPickerView *)p0;
	-(NSInteger) pickerView:(UIPickerView *)p0 numberOfRowsInComponent:(NSInteger)p1;
	-(UIView *) pickerView:(UIPickerView *)p0 viewForRow:(NSInteger)p1 forComponent:(NSInteger)p2 reusingView:(UIView *)p3;
	-(void) didChangeValueForKey:(NSString *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_PDFViewDataSource : NSObject<QLPreviewControllerDataSource> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfPreviewItemsInPreviewController:(QLPreviewController *)p0;
	-(id) previewController:(QLPreviewController *)p0 previewItemAtIndex:(NSInteger)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_PDFViewItem : NSObject<QLPreviewItem> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSString *) previewItemTitle;
	-(NSURL *) previewItemURL;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_QLPreviewItemFileSystem : NSObject<QLPreviewItem> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSString *) previewItemTitle;
	-(NSURL *) previewItemURL;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_QLPreviewItemBundle : NSObject<QLPreviewItem> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSString *) previewItemTitle;
	-(NSURL *) previewItemURL;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_PreviewControllerDS : NSObject<QLPreviewControllerDataSource> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfPreviewItemsInPreviewController:(QLPreviewController *)p0;
	-(id) previewController:(QLPreviewController *)p0 previewItemAtIndex:(NSInteger)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_SpreadsheetCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface HelpViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UITableView * table;
	-(UITableView *) table;
	-(void) setTable:(UITableView *)p0;
	-(void) viewDidLoad;
@end

@interface InfoCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelDescription;
	@property (nonatomic, assign) UILabel * labelHeader;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelDescription;
	-(void) setLabelDescription:(UILabel *)p0;
	-(UILabel *) labelHeader;
	-(void) setLabelHeader:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface LinkCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelHeader;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelHeader;
	-(void) setLabelHeader:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Help_HelpSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface DisplayCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelDisplay;
	@property (nonatomic, assign) UILabel * labelHeader;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelDisplay;
	-(void) setLabelDisplay:(UILabel *)p0;
	-(UILabel *) labelHeader;
	-(void) setLabelHeader:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_ScreenshotReport_ScreenshotReportSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface FileBrowserViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UITableView * table;
	-(UITableView *) table;
	-(void) setTable:(UITableView *)p0;
	-(void) viewDidLoad;
@end

@interface FileCell : UITableViewCell {
}
	@property (nonatomic, assign) UIImageView * imageIcon;
	@property (nonatomic, assign) UILabel * labelFilePath;
	@property (nonatomic, assign) UIView * viewBackground;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIImageView *) imageIcon;
	-(void) setImageIcon:(UIImageView *)p0;
	-(UILabel *) labelFilePath;
	-(void) setLabelFilePath:(UILabel *)p0;
	-(UIView *) viewBackground;
	-(void) setViewBackground:(UIView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Analyzer_AnalyzerDisplayView : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) drawRect:(CGRect)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface DrawAnalyserView : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) drawRect:(CGRect)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_Analyzer_AnalyzerTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canMoveRowAtIndexPath:(NSIndexPath *)p1;
	-(NSInteger) tableView:(UITableView *)p0 editingStyleForRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 moveRowAtIndexPath:(NSIndexPath *)p1 toIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Analyzer_SHSCTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Analyzer_PTTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Analyzer_secondarySensorCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface SettingsGroupCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelTitle;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface SettingsLabelCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * label;
	@property (nonatomic, assign) UILabel * labelDescription;
	@property (nonatomic, assign) UILabel * labelTitle;
	@property (nonatomic, assign) UIView * viewPreferenceArea;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) label;
	-(void) setLabel:(UILabel *)p0;
	-(UILabel *) labelDescription;
	-(void) setLabelDescription:(UILabel *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(UIView *) viewPreferenceArea;
	-(void) setViewPreferenceArea:(UIView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface SettingsNavigationItemCell : UITableViewCell {
}
	@property (nonatomic, assign) UIView * labelTitle;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIView *) labelTitle;
	-(void) setLabelTitle:(UIView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface SettingsSwitchCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelDescription;
	@property (nonatomic, assign) UILabel * labelTitle;
	@property (nonatomic, assign) UISwitch * switcher;
	@property (nonatomic, assign) UIView * viewPreferenceArea;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelDescription;
	-(void) setLabelDescription:(UILabel *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(UISwitch *) switcher;
	-(void) setSwitcher:(UISwitch *)p0;
	-(UIView *) viewPreferenceArea;
	-(void) setViewPreferenceArea:(UIView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface SettingsViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UITableView * tableContent;
	-(UITableView *) tableContent;
	-(void) setTableContent:(UITableView *)p0;
@end

@interface ION_IOS_ViewController_PressureTemperatureChart_PTSlideView : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) drawRect:(CGRect)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface FluidTableCell : UITableViewCell {
}
	@property (nonatomic, assign) UIImageView * imageFavorite;
	@property (nonatomic, assign) UIImageView * imageSelected;
	@property (nonatomic, assign) UILabel * labelFluidName;
	@property (nonatomic, assign) UILabel * safetyClassification;
	@property (nonatomic, assign) UIView * viewFluidColor;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIImageView *) imageFavorite;
	-(void) setImageFavorite:(UIImageView *)p0;
	-(UIImageView *) imageSelected;
	-(void) setImageSelected:(UIImageView *)p0;
	-(UILabel *) labelFluidName;
	-(void) setLabelFluidName:(UILabel *)p0;
	-(UILabel *) safetyClassification;
	-(void) setSafetyClassification:(UILabel *)p0;
	-(UIView *) viewFluidColor;
	-(void) setViewFluidColor:(UIView *)p0;
	-(void) awakeFromNib;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface AddViewerTableCell : UITableViewCell {
}
	@property (nonatomic, assign) UIButton * buttonAdd;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) buttonAdd;
	-(void) setButtonAdd:(UIButton *)p0;
	-(void) awakeFromNib;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface MeasurementSensorPropertyTableCell : UITableViewCell {
}
	@property (nonatomic, assign) UIButton * button;
	@property (nonatomic, assign) UIButton * buttonIcon;
	@property (nonatomic, assign) UILabel * labelMeasurement;
	@property (nonatomic, assign) UILabel * labelTitle;
	@property (nonatomic, assign) UIView * viewBackground;
	@property (nonatomic, assign) UIView * viewDivider;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) button;
	-(void) setButton:(UIButton *)p0;
	-(UIButton *) buttonIcon;
	-(void) setButtonIcon:(UIButton *)p0;
	-(UILabel *) labelMeasurement;
	-(void) setLabelMeasurement:(UILabel *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(UIView *) viewBackground;
	-(void) setViewBackground:(UIView *)p0;
	-(UIView *) viewDivider;
	-(void) setViewDivider:(UIView *)p0;
	-(void) awakeFromNib;
	-(void) prepareForReuse;
	-(void) removeFromSuperview;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ViewerTableCell : UITableViewCell {
}
	@property (nonatomic, assign) UIActivityIndicatorView * activityConnectStatus;
	@property (nonatomic, assign) UIImageView * batteryImage;
	@property (nonatomic, assign) UIButton * buttonConnection;
	@property (nonatomic, assign) UIImageView * imageAlarmIcon;
	@property (nonatomic, assign) UIImageView * imageSensorIcon;
	@property (nonatomic, assign) UILabel * labelConnectionStatus;
	@property (nonatomic, assign) UILabel * labelHeader;
	@property (nonatomic, assign) UILabel * labelMeasurement;
	@property (nonatomic, assign) UILabel * labelUnit;
	@property (nonatomic, assign) UIView * viewBackground;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIActivityIndicatorView *) activityConnectStatus;
	-(void) setActivityConnectStatus:(UIActivityIndicatorView *)p0;
	-(UIImageView *) batteryImage;
	-(void) setBatteryImage:(UIImageView *)p0;
	-(UIButton *) buttonConnection;
	-(void) setButtonConnection:(UIButton *)p0;
	-(UIImageView *) imageAlarmIcon;
	-(void) setImageAlarmIcon:(UIImageView *)p0;
	-(UIImageView *) imageSensorIcon;
	-(void) setImageSensorIcon:(UIImageView *)p0;
	-(UILabel *) labelConnectionStatus;
	-(void) setLabelConnectionStatus:(UILabel *)p0;
	-(UILabel *) labelHeader;
	-(void) setLabelHeader:(UILabel *)p0;
	-(UILabel *) labelMeasurement;
	-(void) setLabelMeasurement:(UILabel *)p0;
	-(UILabel *) labelUnit;
	-(void) setLabelUnit:(UILabel *)p0;
	-(UIView *) viewBackground;
	-(void) setViewBackground:(UIView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface FluidSubviewCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelFluid;
	@property (nonatomic, assign) UILabel * labelMeasurement;
	@property (nonatomic, assign) UIView * viewBackground;
	@property (nonatomic, assign) UIView * viewDivider;
	@property (nonatomic, assign) UILabel * labelTitle;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelFluid;
	-(void) setLabelFluid:(UILabel *)p0;
	-(UILabel *) labelMeasurement;
	-(void) setLabelMeasurement:(UILabel *)p0;
	-(UIView *) viewBackground;
	-(void) setViewBackground:(UIView *)p0;
	-(UIView *) viewDivider;
	-(void) setViewDivider:(UIView *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface TimerSensorPropertyCell : UITableViewCell {
}
	@property (nonatomic, assign) UIButton * buttonPlayPause;
	@property (nonatomic, assign) UIButton * buttonReset;
	@property (nonatomic, assign) UILabel * labelMeasurement;
	@property (nonatomic, assign) UILabel * labelTitle;
	@property (nonatomic, assign) UIView * viewBackground;
	@property (nonatomic, assign) UIView * viewDivider;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) buttonPlayPause;
	-(void) setButtonPlayPause:(UIButton *)p0;
	-(UIButton *) buttonReset;
	-(void) setButtonReset:(UIButton *)p0;
	-(UILabel *) labelMeasurement;
	-(void) setLabelMeasurement:(UILabel *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(UIView *) viewBackground;
	-(void) setViewBackground:(UIView *)p0;
	-(UIView *) viewDivider;
	-(void) setViewDivider:(UIView *)p0;
	-(void) awakeFromNib;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface SecondarySensorCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelMeasurement;
	@property (nonatomic, assign) UILabel * labelTitle;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelMeasurement;
	-(void) setLabelMeasurement:(UILabel *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface TargetSHSCCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelOffset;
	@property (nonatomic, assign) UILabel * labelTarget;
	@property (nonatomic, assign) UILabel * labelTitle;
	@property (nonatomic, assign) UIView * viewBackground;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelOffset;
	-(void) setLabelOffset:(UILabel *)p0;
	-(UILabel *) labelTarget;
	-(void) setLabelTarget:(UILabel *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(UIView *) viewBackground;
	-(void) setViewBackground:(UIView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface DeviceSectionHeaderTableCell : UITableViewCell {
}
	@property (nonatomic, assign) UIButton * buttonOptions;
	@property (nonatomic, assign) UILabel * labelCounter;
	@property (nonatomic, assign) UILabel * labelTitle;
	@property (nonatomic, assign) UIView * viewBackground;
	@property (nonatomic, assign) UIView * viewDivider;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) buttonOptions;
	-(void) setButtonOptions:(UIButton *)p0;
	-(UILabel *) labelCounter;
	-(void) setLabelCounter:(UILabel *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(UIView *) viewBackground;
	-(void) setViewBackground:(UIView *)p0;
	-(UIView *) viewDivider;
	-(void) setViewDivider:(UIView *)p0;
	-(void) awakeFromNib;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface SWTableViewCell : UITableViewCell {
}
	-(void) hideUtilityButtonsAnimated:(BOOL)p0;
	-(void) setLeftUtilityButtons:(NSArray *)p0 WithButtonWidth:(CGFloat)p1;
	-(void) setRightUtilityButtons:(NSArray *)p0 WithButtonWidth:(CGFloat)p1;
	-(void) showLeftUtilityButtonsAnimated:(BOOL)p0;
	-(void) showRightUtilityButtonsAnimated:(BOOL)p0;
	-(BOOL) isUtilityButtonsHidden;
	-(NSArray *) leftUtilityButtons;
	-(void) setLeftUtilityButtons:(NSArray *)p0;
	-(NSArray *) rightUtilityButtons;
	-(void) setRightUtilityButtons:(NSArray *)p0;
	-(NSObject *) delegate;
	-(void) setDelegate:(NSObject *)p0;
	-(id) init;
	-(id) initWithCoder:(NSCoder *)p0;
@end

@interface DeviceTableCell : SWTableViewCell {
}
	@property (nonatomic, assign) UIActivityIndicatorView * activityConnectStatus;
	@property (nonatomic, assign) UIButton * buttonConnect;
	@property (nonatomic, assign) UIImageView * imageDeviceIcon;
	@property (nonatomic, assign) UILabel * labelDeviceName;
	@property (nonatomic, assign) UILabel * labelDeviceType;
	@property (nonatomic, assign) UIView * viewBackground;
	@property (nonatomic, assign) UIView * viewDivider;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIActivityIndicatorView *) activityConnectStatus;
	-(void) setActivityConnectStatus:(UIActivityIndicatorView *)p0;
	-(UIButton *) buttonConnect;
	-(void) setButtonConnect:(UIButton *)p0;
	-(UIImageView *) imageDeviceIcon;
	-(void) setImageDeviceIcon:(UIImageView *)p0;
	-(UILabel *) labelDeviceName;
	-(void) setLabelDeviceName:(UILabel *)p0;
	-(UILabel *) labelDeviceType;
	-(void) setLabelDeviceType:(UILabel *)p0;
	-(UIView *) viewBackground;
	-(void) setViewBackground:(UIView *)p0;
	-(UIView *) viewDivider;
	-(void) setViewDivider:(UIView *)p0;
	-(void) awakeFromNib;
	-(void) prepareForReuse;
	-(void) removeFromSuperview;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface SerialNumberTableCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * labelSerialNumber;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) labelSerialNumber;
	-(void) setLabelSerialNumber:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface SensorTableCell : UITableViewCell {
}
	@property (nonatomic, assign) UIButton * buttonAdd;
	@property (nonatomic, assign) UILabel * labelMeasurement;
	@property (nonatomic, assign) UILabel * labelType;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) buttonAdd;
	-(void) setButtonAdd:(UIButton *)p0;
	-(UILabel *) labelMeasurement;
	-(void) setLabelMeasurement:(UILabel *)p0;
	-(UILabel *) labelType;
	-(void) setLabelType:(UILabel *)p0;
	-(void) awakeFromNib;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_Connections_BaseIOSConnection : NSObject<CBPeripheralDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) peripheral:(CBPeripheral *)p0 didDiscoverServices:(NSError *)p1;
	-(void) peripheral:(CBPeripheral *)p0 didDiscoverCharacteristicsForService:(CBService *)p1 error:(NSError *)p2;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_Connections_IosRigadoConnection : ION_IOS_Connections_BaseIOSConnection<CBPeripheralDelegate> {
}
	-(void) peripheral:(CBPeripheral *)p0 didUpdateValueForCharacteristic:(CBCharacteristic *)p1 error:(NSError *)p2;
@end

@interface NinePatchButtonView : UIButton {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) setBackgroundImage:(UIImage *)p0 forState:(NSUInteger)p1;
	-(void) setImage:(UIImage *)p0 forState:(NSUInteger)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface AppDelegate : NSObject<UIApplicationDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIWindow *) window;
	-(void) setWindow:(UIWindow *)p0;
	-(BOOL) application:(UIApplication *)p0 didFinishLaunchingWithOptions:(NSDictionary *)p1;
	-(void) applicationWillResignActive:(UIApplication *)p0;
	-(void) applicationDidEnterBackground:(UIApplication *)p0;
	-(void) applicationWillEnterForeground:(UIApplication *)p0;
	-(void) applicationDidBecomeActive:(UIApplication *)p0;
	-(void) applicationWillTerminate:(UIApplication *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface IONPrimaryScreenController : UIViewController {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) viewDidLoad;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface DeviceGridViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UICollectionView * gridView;
	-(UICollectionView *) gridView;
	-(void) setGridView:(UICollectionView *)p0;
	-(void) viewDidLoad;
	-(void) viewDidAppear:(BOOL)p0;
	-(void) viewWillDisappear:(BOOL)p0;
	-(void) didReceiveMemoryWarning;
@end

@interface ION_IOS_ViewController_DeviceGrid_GridCellConnected : UICollectionViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) initWithFrame:(CGRect)p0;
@end

@interface SessionsViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * cloudSessionHolder;
	-(UIView *) cloudSessionHolder;
	-(void) setCloudSessionHolder:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) didReceiveMemoryWarning;
@end

@interface CodeGenViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * codeGenHolder;
	-(UIView *) codeGenHolder;
	-(void) setCodeGenHolder:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) didReceiveMemoryWarning;
@end

@interface ViewingControlViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * accessHolderView;
	-(UIView *) accessHolderView;
	-(void) setAccessHolderView:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) didReceiveMemoryWarning;
@end

@interface RemoteViewingViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * remoteHolderView;
	-(UIView *) remoteHolderView;
	-(void) setRemoteHolderView:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewDidAppear:(BOOL)p0;
	-(void) didReceiveMemoryWarning;
@end

@interface ION_IOS_ViewController_AccessRequest_AccessRequestTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_AccessRequest_AccessViewingTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_AccessRequest_AccessAllowingTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_RemoteDeviceManager_RemoteDeviceManagerTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) tableView:(UITableView *)p0 didEndDisplayingCell:(UITableViewCell *)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(NSString *) tableView:(UITableView *)p0 titleForDeleteConfirmationButtonForRowAtIndexPath:(NSIndexPath *)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface RemoteDeviceManagerViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UILabel * labelEmpty;
	-(UILabel *) labelEmpty;
	-(void) setLabelEmpty:(UILabel *)p0;
	-(void) viewDidLoad;
	-(void) viewWillAppear:(BOOL)p0;
	-(void) viewWillDisappear:(BOOL)p0;
	-(void) viewDidUnload;
@end

@interface RemoteSystemViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * remoteHolderView;
	-(UIView *) remoteHolderView;
	-(void) setRemoteHolderView:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewWillAppear:(BOOL)p0;
	-(void) didReceiveMemoryWarning;
@end

@interface ION_IOS_ViewController_RemoteAccess_RemoteAccessTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_RemoteAccess_RemoteWorkbenchViewController : ION_IOS_ViewController_BaseIONViewController {
}
	-(void) viewDidLoad;
	-(void) viewDidAppear:(BOOL)p0;
	-(void) viewDidUnload;
@end

@interface RssFeedViewController : ION_IOS_ViewController_BaseIONViewController {
}
	-(void) viewDidLoad;
	-(void) didReceiveMemoryWarning;
@end

@interface WalkthroughScreenshotViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * walkthroughHolder;
	-(UIView *) walkthroughHolder;
	-(void) setWalkthroughHolder:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) didReceiveMemoryWarning;
@end

@interface JobViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * containerView;
	-(UIView *) containerView;
	-(void) setContainerView:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewDidAppear:(BOOL)p0;
	-(void) didReceiveMemoryWarning;
@end

@interface ION_IOS_ViewController_JobManager_CreatedJobCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface JobEditViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIButton * dataLogginButton;
	@property (nonatomic, assign) UILabel * dataLoggingHighlight;
	@property (nonatomic, assign) UIView * holderView;
	@property (nonatomic, assign) UIScrollView * infoScroller;
	@property (nonatomic, assign) UIButton * jobInfoButton;
	@property (nonatomic, assign) UILabel * jobInfoHighlight;
	-(UIButton *) dataLogginButton;
	-(void) setDataLogginButton:(UIButton *)p0;
	-(UILabel *) dataLoggingHighlight;
	-(void) setDataLoggingHighlight:(UILabel *)p0;
	-(UIView *) holderView;
	-(void) setHolderView:(UIView *)p0;
	-(UIScrollView *) infoScroller;
	-(void) setInfoScroller:(UIScrollView *)p0;
	-(UIButton *) jobInfoButton;
	-(void) setJobInfoButton:(UIButton *)p0;
	-(UILabel *) jobInfoHighlight;
	-(void) setJobInfoHighlight:(UILabel *)p0;
	-(void) viewDidLoad;
	-(void) viewDidAppear:(BOOL)p0;
	-(void) didReceiveMemoryWarning;
@end

@interface ION_IOS_ViewController_JobManager_FloatLabeledTextField : UITextField {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSString *) placeholder;
	-(void) setPlaceholder:(NSString *)p0;
	-(CGRect) textRectForBounds:(CGRect)p0;
	-(CGRect) editingRectForBounds:(CGRect)p0;
	-(CGRect) clearButtonRectForBounds:(CGRect)p0;
	-(void) layoutSubviews;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_JobManager_AssociatedSessionCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_JobManager_AvailableSessionCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_JobManager_AvailableSessionSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface LoggingViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * containerView;
	-(UIView *) containerView;
	-(void) setContainerView:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewDidAppear:(BOOL)p0;
	-(void) didReceiveMemoryWarning;
@end

@interface ION_IOS_ViewController_Logging_SessionCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_Logging_LoggingSessionSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_ModalPickerAnimatedDismissed : NSObject<UIViewControllerAnimatedTransitioning> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(double) transitionDuration:(id)p0;
	-(void) animateTransition:(id)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_Logging_ModalPickerAnimatedTransitioning : NSObject<UIViewControllerAnimatedTransitioning> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(double) transitionDuration:(id)p0;
	-(void) animateTransition:(id)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface ION_IOS_ViewController_Logging_graphCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_GraphingView : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_legendCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Logging_SpreadsheetTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(NSString *) tableView:(UITableView *)p0 titleForDeleteConfirmationButtonForRowAtIndexPath:(NSIndexPath *)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface GraphingViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * containerView;
	-(UIView *) containerView;
	-(void) setContainerView:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) didReceiveMemoryWarning;
@end

@interface EntryCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) awakeFromNib;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ScreenshotReportViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIImageView * imageScreenshot;
	@property (nonatomic, assign) UIScrollView * scrollview;
	@property (nonatomic, assign) UITableView * table;
	-(UIImageView *) imageScreenshot;
	-(void) setImageScreenshot:(UIImageView *)p0;
	-(UIScrollView *) scrollview;
	-(void) setScrollview:(UIScrollView *)p0;
	-(UITableView *) table;
	-(void) setTable:(UITableView *)p0;
	-(void) viewDidLoad;
@end

@interface SelectionCell : UITableViewCell {
}
	@property (nonatomic, assign) UIButton * button;
	@property (nonatomic, assign) UILabel * labelHeader;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) button;
	-(void) setButton:(UIButton *)p0;
	-(UILabel *) labelHeader;
	-(void) setLabelHeader:(UILabel *)p0;
	-(void) awakeFromNib;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface NotesCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) awakeFromNib;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_FileManager_FileManagerSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(NSString *) tableView:(UITableView *)p0 titleForDeleteConfirmationButtonForRowAtIndexPath:(NSIndexPath *)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface AnalyzerViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIView * viewAnalyzerContainer;
	-(UIView *) viewAnalyzerContainer;
	-(void) setViewAnalyzerContainer:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewDidAppear:(BOOL)p0;
@end

@interface ION_IOS_ViewController_Analyzer_minimumTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Analyzer_maximumTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Analyzer_holdTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Analyzer_altTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Analyzer_RoCTableCell : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface PTChartViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIButton * buttonPressureUnit;
	@property (nonatomic, assign) UIButton * buttonTemperatureUnit;
	@property (nonatomic, assign) UITextField * editPressure;
	@property (nonatomic, assign) UITextField * editTemperature;
	@property (nonatomic, assign) UIImageView * imagePressureIcon;
	@property (nonatomic, assign) UIImageView * imagePressureLock;
	@property (nonatomic, assign) UIImageView * imageTemperatureIcon;
	@property (nonatomic, assign) UIImageView * imageTemperatureLock;
	@property (nonatomic, assign) UILabel * labelFluidName;
	@property (nonatomic, assign) UILabel * labelPressure;
	@property (nonatomic, assign) UILabel * labelTemperature;
	@property (nonatomic, assign) UISegmentedControl * switchFluidState;
	@property (nonatomic, assign) UIView * viewFluidColor;
	@property (nonatomic, assign) UIView * viewFluidHeader;
	@property (nonatomic, assign) UIView * viewPressureSection;
	@property (nonatomic, assign) UIView * viewPressureTouchArea;
	@property (nonatomic, assign) UIView * viewTemperatureSection;
	@property (nonatomic, assign) UIView * viewTemperatureTouchArea;
	-(UIButton *) buttonPressureUnit;
	-(void) setButtonPressureUnit:(UIButton *)p0;
	-(UIButton *) buttonTemperatureUnit;
	-(void) setButtonTemperatureUnit:(UIButton *)p0;
	-(UITextField *) editPressure;
	-(void) setEditPressure:(UITextField *)p0;
	-(UITextField *) editTemperature;
	-(void) setEditTemperature:(UITextField *)p0;
	-(UIImageView *) imagePressureIcon;
	-(void) setImagePressureIcon:(UIImageView *)p0;
	-(UIImageView *) imagePressureLock;
	-(void) setImagePressureLock:(UIImageView *)p0;
	-(UIImageView *) imageTemperatureIcon;
	-(void) setImageTemperatureIcon:(UIImageView *)p0;
	-(UIImageView *) imageTemperatureLock;
	-(void) setImageTemperatureLock:(UIImageView *)p0;
	-(UILabel *) labelFluidName;
	-(void) setLabelFluidName:(UILabel *)p0;
	-(UILabel *) labelPressure;
	-(void) setLabelPressure:(UILabel *)p0;
	-(UILabel *) labelTemperature;
	-(void) setLabelTemperature:(UILabel *)p0;
	-(UISegmentedControl *) switchFluidState;
	-(void) setSwitchFluidState:(UISegmentedControl *)p0;
	-(UIView *) viewFluidColor;
	-(void) setViewFluidColor:(UIView *)p0;
	-(UIView *) viewFluidHeader;
	-(void) setViewFluidHeader:(UIView *)p0;
	-(UIView *) viewPressureSection;
	-(void) setViewPressureSection:(UIView *)p0;
	-(UIView *) viewPressureTouchArea;
	-(void) setViewPressureTouchArea:(UIView *)p0;
	-(UIView *) viewTemperatureSection;
	-(void) setViewTemperatureSection:(UIView *)p0;
	-(UIView *) viewTemperatureTouchArea;
	-(void) setViewTemperatureTouchArea:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewWillAppear:(BOOL)p0;
	-(void) viewWillDisappear:(BOOL)p0;
	-(void) viewWillUnload;
@end

@interface SensorAlarmViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIButton * buttonHighAlarmUnit;
	@property (nonatomic, assign) UIButton * buttonLowAlarmUnit;
	@property (nonatomic, assign) UITextField * editHighAlarmMeasurement;
	@property (nonatomic, assign) UITextField * editLowAlarmMeasurement;
	@property (nonatomic, assign) UILabel * labelHighAlarmDescription;
	@property (nonatomic, assign) UILabel * labelHighAlarmHeader;
	@property (nonatomic, assign) UILabel * labelLowAlarmDescription;
	@property (nonatomic, assign) UILabel * labelLowAlarmHeader;
	@property (nonatomic, assign) UISwitch * switchHighAlarmEnabler;
	@property (nonatomic, assign) UISwitch * switchLowAlarmEnabler;
	@property (nonatomic, assign) UIView * viewHighAlarmContent;
	@property (nonatomic, assign) UIView * viewLowAlarmSection;
	-(UIButton *) buttonHighAlarmUnit;
	-(void) setButtonHighAlarmUnit:(UIButton *)p0;
	-(UIButton *) buttonLowAlarmUnit;
	-(void) setButtonLowAlarmUnit:(UIButton *)p0;
	-(UITextField *) editHighAlarmMeasurement;
	-(void) setEditHighAlarmMeasurement:(UITextField *)p0;
	-(UITextField *) editLowAlarmMeasurement;
	-(void) setEditLowAlarmMeasurement:(UITextField *)p0;
	-(UILabel *) labelHighAlarmDescription;
	-(void) setLabelHighAlarmDescription:(UILabel *)p0;
	-(UILabel *) labelHighAlarmHeader;
	-(void) setLabelHighAlarmHeader:(UILabel *)p0;
	-(UILabel *) labelLowAlarmDescription;
	-(void) setLabelLowAlarmDescription:(UILabel *)p0;
	-(UILabel *) labelLowAlarmHeader;
	-(void) setLabelLowAlarmHeader:(UILabel *)p0;
	-(UISwitch *) switchHighAlarmEnabler;
	-(void) setSwitchHighAlarmEnabler:(UISwitch *)p0;
	-(UISwitch *) switchLowAlarmEnabler;
	-(void) setSwitchLowAlarmEnabler:(UISwitch *)p0;
	-(UIView *) viewHighAlarmContent;
	-(void) setViewHighAlarmContent:(UIView *)p0;
	-(UIView *) viewLowAlarmSection;
	-(void) setViewLowAlarmSection:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewWillDisappear:(BOOL)p0;
@end

@interface ION_IOS_ViewController_FluidManager_FluidSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface FluidManagerViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UILabel * fluidNameLabel;
	@property (nonatomic, assign) UILabel * fluidSafetyLabel;
	@property (nonatomic, assign) UILabel * labelFluidName;
	@property (nonatomic, assign) UIButton * safetyHelpButton;
	@property (nonatomic, assign) UISegmentedControl * switchFluidSource;
	@property (nonatomic, assign) UITableView * tableContent;
	@property (nonatomic, assign) UIView * viewFluidColor;
	@property (nonatomic, assign) UIView * viewSelectedFluidContainer;
	-(UILabel *) fluidNameLabel;
	-(void) setFluidNameLabel:(UILabel *)p0;
	-(UILabel *) fluidSafetyLabel;
	-(void) setFluidSafetyLabel:(UILabel *)p0;
	-(UILabel *) labelFluidName;
	-(void) setLabelFluidName:(UILabel *)p0;
	-(UIButton *) safetyHelpButton;
	-(void) setSafetyHelpButton:(UIButton *)p0;
	-(UISegmentedControl *) switchFluidSource;
	-(void) setSwitchFluidSource:(UISegmentedControl *)p0;
	-(UITableView *) tableContent;
	-(void) setTableContent:(UITableView *)p0;
	-(UIView *) viewFluidColor;
	-(void) setViewFluidColor:(UIView *)p0;
	-(UIView *) viewSelectedFluidContainer;
	-(void) setViewSelectedFluidContainer:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewWillDisappear:(BOOL)p0;
@end

@interface SuperheatSubcoolViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UIButton * buttonPressureUnit;
	@property (nonatomic, assign) UIButton * buttonTemperatureUnit;
	@property (nonatomic, assign) UITextField * editPressure;
	@property (nonatomic, assign) UITextField * editTemperature;
	@property (nonatomic, assign) UIImageView * imageNegativeWarning;
	@property (nonatomic, assign) UIImageView * imagePressureIcon;
	@property (nonatomic, assign) UIImageView * imagePressureLock;
	@property (nonatomic, assign) UIImageView * imageTemperatureIcon;
	@property (nonatomic, assign) UIImageView * imageTemperatureLock;
	@property (nonatomic, assign) UILabel * labelFluidDelta;
	@property (nonatomic, assign) UILabel * labelFluidName;
	@property (nonatomic, assign) UILabel * labelFluidState;
	@property (nonatomic, assign) UILabel * labelPressure;
	@property (nonatomic, assign) UILabel * labelSatTemp;
	@property (nonatomic, assign) UILabel * labelSatTempMeasurement;
	@property (nonatomic, assign) UILabel * labelSatTempUnit;
	@property (nonatomic, assign) UILabel * labelTemperature;
	@property (nonatomic, assign) UISegmentedControl * switchFluidState;
	@property (nonatomic, assign) UIView * viewDivider1;
	@property (nonatomic, assign) UIView * viewDivider2;
	@property (nonatomic, assign) UIView * viewDivider3;
	@property (nonatomic, assign) UIView * viewDivider4;
	@property (nonatomic, assign) UIView * viewFluidColor;
	@property (nonatomic, assign) UIView * viewFluidColorBorder;
	@property (nonatomic, assign) UIView * viewFluidHeader;
	@property (nonatomic, assign) UIView * viewPressureSection;
	@property (nonatomic, assign) UIView * viewPressureTouchArea;
	@property (nonatomic, assign) UIView * viewSatTempSection;
	@property (nonatomic, assign) UIView * viewTemperatureSection;
	@property (nonatomic, assign) UIView * viewTemperatureTouchArea;
	-(UIButton *) buttonPressureUnit;
	-(void) setButtonPressureUnit:(UIButton *)p0;
	-(UIButton *) buttonTemperatureUnit;
	-(void) setButtonTemperatureUnit:(UIButton *)p0;
	-(UITextField *) editPressure;
	-(void) setEditPressure:(UITextField *)p0;
	-(UITextField *) editTemperature;
	-(void) setEditTemperature:(UITextField *)p0;
	-(UIImageView *) imageNegativeWarning;
	-(void) setImageNegativeWarning:(UIImageView *)p0;
	-(UIImageView *) imagePressureIcon;
	-(void) setImagePressureIcon:(UIImageView *)p0;
	-(UIImageView *) imagePressureLock;
	-(void) setImagePressureLock:(UIImageView *)p0;
	-(UIImageView *) imageTemperatureIcon;
	-(void) setImageTemperatureIcon:(UIImageView *)p0;
	-(UIImageView *) imageTemperatureLock;
	-(void) setImageTemperatureLock:(UIImageView *)p0;
	-(UILabel *) labelFluidDelta;
	-(void) setLabelFluidDelta:(UILabel *)p0;
	-(UILabel *) labelFluidName;
	-(void) setLabelFluidName:(UILabel *)p0;
	-(UILabel *) labelFluidState;
	-(void) setLabelFluidState:(UILabel *)p0;
	-(UILabel *) labelPressure;
	-(void) setLabelPressure:(UILabel *)p0;
	-(UILabel *) labelSatTemp;
	-(void) setLabelSatTemp:(UILabel *)p0;
	-(UILabel *) labelSatTempMeasurement;
	-(void) setLabelSatTempMeasurement:(UILabel *)p0;
	-(UILabel *) labelSatTempUnit;
	-(void) setLabelSatTempUnit:(UILabel *)p0;
	-(UILabel *) labelTemperature;
	-(void) setLabelTemperature:(UILabel *)p0;
	-(UISegmentedControl *) switchFluidState;
	-(void) setSwitchFluidState:(UISegmentedControl *)p0;
	-(UIView *) viewDivider1;
	-(void) setViewDivider1:(UIView *)p0;
	-(UIView *) viewDivider2;
	-(void) setViewDivider2:(UIView *)p0;
	-(UIView *) viewDivider3;
	-(void) setViewDivider3:(UIView *)p0;
	-(UIView *) viewDivider4;
	-(void) setViewDivider4:(UIView *)p0;
	-(UIView *) viewFluidColor;
	-(void) setViewFluidColor:(UIView *)p0;
	-(UIView *) viewFluidColorBorder;
	-(void) setViewFluidColorBorder:(UIView *)p0;
	-(UIView *) viewFluidHeader;
	-(void) setViewFluidHeader:(UIView *)p0;
	-(UIView *) viewPressureSection;
	-(void) setViewPressureSection:(UIView *)p0;
	-(UIView *) viewPressureTouchArea;
	-(void) setViewPressureTouchArea:(UIView *)p0;
	-(UIView *) viewSatTempSection;
	-(void) setViewSatTempSection:(UIView *)p0;
	-(UIView *) viewTemperatureSection;
	-(void) setViewTemperatureSection:(UIView *)p0;
	-(UIView *) viewTemperatureTouchArea;
	-(void) setViewTemperatureTouchArea:(UIView *)p0;
	-(void) viewDidLoad;
	-(void) viewWillAppear:(BOOL)p0;
	-(void) viewWillDisappear:(BOOL)p0;
@end

@interface WorkbenchViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UITableView * tableContent;
	-(UITableView *) tableContent;
	-(void) setTableContent:(UITableView *)p0;
	-(void) viewDidLoad;
	-(void) viewDidAppear:(BOOL)p0;
	-(void) viewDidUnload;
@end

@interface RateOfChangeSensorPropertyCell : UITableViewCell {
}
	@property (nonatomic, assign) UIButton * button;
	@property (nonatomic, assign) UIButton * buttonIcon;
	@property (nonatomic, assign) UILabel * labelMeasurement;
	@property (nonatomic, assign) UILabel * labelTitle;
	@property (nonatomic, assign) UIView * viewBackground;
	@property (nonatomic, assign) UIView * viewBottomDivider;
	@property (nonatomic, assign) UIView * viewDivider;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) button;
	-(void) setButton:(UIButton *)p0;
	-(UIButton *) buttonIcon;
	-(void) setButtonIcon:(UIButton *)p0;
	-(UILabel *) labelMeasurement;
	-(void) setLabelMeasurement:(UILabel *)p0;
	-(UILabel *) labelTitle;
	-(void) setLabelTitle:(UILabel *)p0;
	-(UIView *) viewBackground;
	-(void) setViewBackground:(UIView *)p0;
	-(UIView *) viewBottomDivider;
	-(void) setViewBottomDivider:(UIView *)p0;
	-(UIView *) viewDivider;
	-(void) setViewDivider:(UIView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_ViewController_Workbench_WorkbenchTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 didEndDisplayingCell:(UITableViewCell *)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) tableView:(UITableView *)p0 canMoveRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 moveRowAtIndexPath:(NSIndexPath *)p1 toIndexPath:(NSIndexPath *)p2;
	-(NSString *) tableView:(UITableView *)p0 titleForDeleteConfirmationButtonForRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface RateofChangeSettingsViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UILabel * BLMeasurement;
	@property (nonatomic, assign) UILabel * BRMeasurement;
	@property (nonatomic, assign) UIView * graphView;
	@property (nonatomic, assign) UIView * legendView;
	@property (nonatomic, assign) UIImageView * linkedLegendImage;
	@property (nonatomic, assign) UILabel * linkedLegendLabel;
	@property (nonatomic, assign) UIImageView * primaryLegendImage;
	@property (nonatomic, assign) UILabel * primaryLegendLabel;
	@property (nonatomic, assign) UILabel * TLMeasurement;
	@property (nonatomic, assign) UILabel * trendGraphHeader;
	@property (nonatomic, assign) UILabel * trendInfoHeader;
	@property (nonatomic, assign) UILabel * trendInfoInterval;
	@property (nonatomic, assign) UILabel * trendIntervalSettings;
	@property (nonatomic, assign) UILabel * TRMeasurement;
	-(UILabel *) BLMeasurement;
	-(void) setBLMeasurement:(UILabel *)p0;
	-(UILabel *) BRMeasurement;
	-(void) setBRMeasurement:(UILabel *)p0;
	-(UIView *) graphView;
	-(void) setGraphView:(UIView *)p0;
	-(UIView *) legendView;
	-(void) setLegendView:(UIView *)p0;
	-(UIImageView *) linkedLegendImage;
	-(void) setLinkedLegendImage:(UIImageView *)p0;
	-(UILabel *) linkedLegendLabel;
	-(void) setLinkedLegendLabel:(UILabel *)p0;
	-(UIImageView *) primaryLegendImage;
	-(void) setPrimaryLegendImage:(UIImageView *)p0;
	-(UILabel *) primaryLegendLabel;
	-(void) setPrimaryLegendLabel:(UILabel *)p0;
	-(UILabel *) TLMeasurement;
	-(void) setTLMeasurement:(UILabel *)p0;
	-(UILabel *) trendGraphHeader;
	-(void) setTrendGraphHeader:(UILabel *)p0;
	-(UILabel *) trendInfoHeader;
	-(void) setTrendInfoHeader:(UILabel *)p0;
	-(UILabel *) trendInfoInterval;
	-(void) setTrendInfoInterval:(UILabel *)p0;
	-(UILabel *) trendIntervalSettings;
	-(void) setTrendIntervalSettings:(UILabel *)p0;
	-(UILabel *) TRMeasurement;
	-(void) setTRMeasurement:(UILabel *)p0;
	-(void) viewDidLoad;
	-(void) didReceiveMemoryWarning;
@end

@interface DeviceManagerViewController : ION_IOS_ViewController_BaseIONViewController {
}
	@property (nonatomic, assign) UILabel * labelEmpty;
	@property (nonatomic, assign) UITableView * tableContent;
	-(UILabel *) labelEmpty;
	-(void) setLabelEmpty:(UILabel *)p0;
	-(UITableView *) tableContent;
	-(void) setTableContent:(UITableView *)p0;
	-(void) viewDidLoad;
	-(void) viewWillAppear:(BOOL)p0;
	-(void) viewWillDisappear:(BOOL)p0;
	-(void) viewDidUnload;
@end

@interface ION_IOS_ViewController_DeviceManager_DeviceManagerTableSource : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) tableView:(UITableView *)p0 didEndDisplayingCell:(UITableViewCell *)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 commitEditingStyle:(NSInteger)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(BOOL) tableView:(UITableView *)p0 canEditRowAtIndexPath:(NSIndexPath *)p1;
	-(NSString *) tableView:(UITableView *)p0 titleForDeleteConfirmationButtonForRowAtIndexPath:(NSIndexPath *)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_Connections_IosLeConnection : ION_IOS_Connections_BaseIOSConnection<CBPeripheralDelegate> {
}
	-(void) peripheral:(CBPeripheral *)p0 didUpdateValueForCharacteristic:(CBCharacteristic *)p1 error:(NSError *)p2;
@end

@interface ION_IOS_Connections_IonCBCentralManagerDelegate : NSObject<CBCentralManagerDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) centralManager:(CBCentralManager *)p0 didDiscoverPeripheral:(CBPeripheral *)p1 advertisementData:(NSDictionary *)p2 RSSI:(NSNumber *)p3;
	-(void) centralManager:(CBCentralManager *)p0 didConnectPeripheral:(CBPeripheral *)p1;
	-(void) centralManager:(CBCentralManager *)p0 didDisconnectPeripheral:(CBPeripheral *)p1 error:(NSError *)p2;
	-(void) centralManager:(CBCentralManager *)p0 didFailToConnectPeripheral:(CBPeripheral *)p1 error:(NSError *)p2;
	-(void) centralManager:(CBCentralManager *)p0 didRetrieveConnectedPeripherals:(NSArray *)p1;
	-(void) centralManager:(CBCentralManager *)p0 willRestoreState:(NSDictionary *)p1;
	-(void) centralManagerDidUpdateState:(CBCentralManager *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ION_IOS_UI_Toast : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface __UIGestureRecognizerToken : NSObject {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface __UIGestureRecognizerParameterlessToken : __UIGestureRecognizerToken {
}
	-(void) target;
@end

@interface __UIGestureRecognizerParametrizedToken : __UIGestureRecognizerToken {
}
	-(void) target:(UIGestureRecognizer *)p0;
@end

@interface UIKit_UIView_UIViewAppearance : NSObject {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIColor *) backgroundColor;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface UIKit_UILabel_UILabelAppearance : UIKit_UIView_UIViewAppearance {
}
	-(void) setFont:(UIFont *)p0;
@end

@interface UIKit_UITableViewCell_UITableViewCellAppearance : UIKit_UIView_UIViewAppearance {
}
@end

@interface FlyoutNavigation_OpenMenuGestureRecognizer : UIPanGestureRecognizer {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface FlyoutNavigationController : UIViewController {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) shouldAutomaticallyForwardRotationMethods;
	-(void) viewDidLayoutSubviews;
	-(void) viewWillAppear:(BOOL)p0;
	-(void) viewDidAppear:(BOOL)p0;
	-(void) viewWillDisappear:(BOOL)p0;
	-(void) animationEnded;
	-(BOOL) shouldAutorotateToInterfaceOrientation:(NSInteger)p0;
	-(NSUInteger) supportedInterfaceOrientations;
	-(void) willRotateToInterfaceOrientation:(NSInteger)p0 duration:(double)p1;
	-(void) didRotateFromInterfaceOrientation:(NSInteger)p0;
	-(void) willAnimateRotationToInterfaceOrientation:(NSInteger)p0 duration:(double)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface MonoTouch_Dialog_MessageSummaryView : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) drawRect:(CGRect)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface MonoTouch_Dialog_RefreshTableHeaderView : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) layoutSubviews;
	-(void) drawRect:(CGRect)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface MonoTouch_Dialog_GlassButton : UIButton {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) isEnabled;
	-(void) setEnabled:(BOOL)p0;
	-(BOOL) beginTrackingWithTouch:(UITouch *)p0 withEvent:(UIEvent *)p1;
	-(void) endTrackingWithTouch:(UITouch *)p0 withEvent:(UIEvent *)p1;
	-(BOOL) continueTrackingWithTouch:(UITouch *)p0 withEvent:(UIEvent *)p1;
	-(void) drawRect:(CGRect)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface MonoTouch_Dialog_BaseBooleanImageElement_TextWithImageCellView : UITableViewCell {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) layoutSubviews;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface MonoTouch_Dialog_DialogViewController_Source : NSObject<UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) tableView:(UITableView *)p0 accessoryButtonTappedForRowWithIndexPath:(NSIndexPath *)p1;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(NSInteger) numberOfSectionsInTableView:(UITableView *)p0;
	-(NSString *) tableView:(UITableView *)p0 titleForHeaderInSection:(NSInteger)p1;
	-(NSString *) tableView:(UITableView *)p0 titleForFooterInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 willDisplayCell:(UITableViewCell *)p1 forRowAtIndexPath:(NSIndexPath *)p2;
	-(void) tableView:(UITableView *)p0 didDeselectRowAtIndexPath:(NSIndexPath *)p1;
	-(void) tableView:(UITableView *)p0 didSelectRowAtIndexPath:(NSIndexPath *)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForHeaderInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForHeaderInSection:(NSInteger)p1;
	-(UIView *) tableView:(UITableView *)p0 viewForFooterInSection:(NSInteger)p1;
	-(CGFloat) tableView:(UITableView *)p0 heightForFooterInSection:(NSInteger)p1;
	-(void) scrollViewDidScroll:(UIScrollView *)p0;
	-(void) scrollViewWillBeginDragging:(UIScrollView *)p0;
	-(void) scrollViewDidEndDragging:(UIScrollView *)p0 willDecelerate:(BOOL)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface MonoTouch_Dialog_DialogViewController_SizingSource : MonoTouch_Dialog_DialogViewController_Source<UIScrollViewDelegate> {
}
	-(CGFloat) tableView:(UITableView *)p0 heightForRowAtIndexPath:(NSIndexPath *)p1;
@end

@interface MonoTouch_Dialog_DialogViewController : UITableViewController {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) shouldAutorotateToInterfaceOrientation:(NSInteger)p0;
	-(void) didRotateFromInterfaceOrientation:(NSInteger)p0;
	-(void) loadView;
	-(void) viewWillAppear:(BOOL)p0;
	-(void) viewWillDisappear:(BOOL)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface OxyPlot_Xamarin_iOS_PanZoomGestureRecognizer : UIGestureRecognizer {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) touchesBegan:(NSSet *)p0 withEvent:(UIEvent *)p1;
	-(void) touchesMoved:(NSSet *)p0 withEvent:(UIEvent *)p1;
	-(void) touchesEnded:(NSSet *)p0 withEvent:(UIEvent *)p1;
	-(void) touchesCancelled:(NSSet *)p0 withEvent:(UIEvent *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface PlotView : UIView {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) requiresConstraintBasedLayout;
	-(void) drawRect:(CGRect)p0;
	-(void) motionBegan:(NSInteger)p0 withEvent:(UIEvent *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
	-(id) initWithCoder:(NSCoder *)p0;
@end

@interface SWCellScrollView : UIScrollView {
}
	-(id) init;
	-(id) initWithCoder:(NSCoder *)p0;
@end

@interface SWLongPressGestureRecognizer : UILongPressGestureRecognizer {
}
	-(id) init;
@end

@protocol SWTableViewCellDelegate
@end

@interface SWUtilityButtonTapGestureRecognizer : UITapGestureRecognizer {
}
	-(NSUInteger) buttonIndex;
	-(void) setButtonIndex:(NSUInteger)p0;
	-(id) init;
@end

@interface SWUtilityButtonView : UIView {
}
	-(void) popBackgroundColors;
	-(void) pushBackgroundColors;
	-(void) setUtilityButtons:(NSArray *)p0 WithButtonWidth:(CGFloat)p1;
	-(id) parentCell;
	-(NSArray *) utilityButtons;
	-(void) setUtilityButtons:(NSArray *)p0;
	-(SEL) utilityButtonSelector;
	-(void) setUtilityButtonSelector:(SEL)p0;
	-(id) init;
	-(id) initWithCoder:(NSCoder *)p0;
	-(id) initWithUtilityButtons:(NSArray *)p0 parentCell:(id)p1 utilityButtonSelector:(SEL)p2;
	-(id) initWithFrame:(CGRect)p0 utilityButtons:(NSArray *)p1 parentCell:(id)p2 utilityButtonSelector:(SEL)p3;
@end


