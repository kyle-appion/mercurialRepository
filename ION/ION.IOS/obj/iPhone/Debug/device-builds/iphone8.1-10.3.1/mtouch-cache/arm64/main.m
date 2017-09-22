#include "xamarin/xamarin.h"

extern void *mono_aot_module_IONIOS_info;
extern void *mono_aot_module_mscorlib_info;
extern void *mono_aot_module_Newtonsoft_Json_info;
extern void *mono_aot_module_System_Core_info;
extern void *mono_aot_module_System_info;
extern void *mono_aot_module_Mono_Security_info;
extern void *mono_aot_module_System_Xml_info;
extern void *mono_aot_module_System_Numerics_info;
extern void *mono_aot_module_System_Xml_Linq_info;
extern void *mono_aot_module_System_Runtime_Serialization_info;
extern void *mono_aot_module_Microsoft_CSharp_info;
extern void *mono_aot_module_ION_Core_info;
extern void *mono_aot_module_Xfinium_Pdf_Pcl_info;
extern void *mono_aot_module_Appion_Commons_info;
extern void *mono_aot_module_SQLite_Net_info;
extern void *mono_aot_module_System_Net_Http_info;
extern void *mono_aot_module_Xamarin_iOS_info;
extern void *mono_aot_module_FlexCel_info;
extern void *mono_aot_module_System_Data_info;
extern void *mono_aot_module_FlyoutNavigation_info;
extern void *mono_aot_module_MonoTouch_Dialog_1_info;
extern void *mono_aot_module_System_Json_info;
extern void *mono_aot_module_OxyPlot_Xamarin_iOS_info;
extern void *mono_aot_module_OxyPlot_info;
extern void *mono_aot_module_Xfinium_Pdf_Xamarin_iOS_info;
extern void *mono_aot_module_SWTableViewCell_info;
extern void *mono_aot_module_System_IO_Compression_FileSystem_info;
extern void *mono_aot_module_System_IO_Compression_info;
extern void *mono_aot_module_SQLite_Net_Platform_XamarinIOS_Unified_info;

void xamarin_register_modules_impl ()
{
	mono_aot_register_module (mono_aot_module_IONIOS_info);
	mono_aot_register_module (mono_aot_module_mscorlib_info);
	mono_aot_register_module (mono_aot_module_Newtonsoft_Json_info);
	mono_aot_register_module (mono_aot_module_System_Core_info);
	mono_aot_register_module (mono_aot_module_System_info);
	mono_aot_register_module (mono_aot_module_Mono_Security_info);
	mono_aot_register_module (mono_aot_module_System_Xml_info);
	mono_aot_register_module (mono_aot_module_System_Numerics_info);
	mono_aot_register_module (mono_aot_module_System_Xml_Linq_info);
	mono_aot_register_module (mono_aot_module_System_Runtime_Serialization_info);
	mono_aot_register_module (mono_aot_module_Microsoft_CSharp_info);
	mono_aot_register_module (mono_aot_module_ION_Core_info);
	mono_aot_register_module (mono_aot_module_Xfinium_Pdf_Pcl_info);
	mono_aot_register_module (mono_aot_module_Appion_Commons_info);
	mono_aot_register_module (mono_aot_module_SQLite_Net_info);
	mono_aot_register_module (mono_aot_module_System_Net_Http_info);
	mono_aot_register_module (mono_aot_module_Xamarin_iOS_info);
	mono_aot_register_module (mono_aot_module_FlexCel_info);
	mono_aot_register_module (mono_aot_module_System_Data_info);
	mono_aot_register_module (mono_aot_module_FlyoutNavigation_info);
	mono_aot_register_module (mono_aot_module_MonoTouch_Dialog_1_info);
	mono_aot_register_module (mono_aot_module_System_Json_info);
	mono_aot_register_module (mono_aot_module_OxyPlot_Xamarin_iOS_info);
	mono_aot_register_module (mono_aot_module_OxyPlot_info);
	mono_aot_register_module (mono_aot_module_Xfinium_Pdf_Xamarin_iOS_info);
	mono_aot_register_module (mono_aot_module_SWTableViewCell_info);
	mono_aot_register_module (mono_aot_module_System_IO_Compression_FileSystem_info);
	mono_aot_register_module (mono_aot_module_System_IO_Compression_info);
	mono_aot_register_module (mono_aot_module_SQLite_Net_Platform_XamarinIOS_Unified_info);

}

void xamarin_register_assemblies_impl ()
{
	guint32 exception_gchandle = 0;
	xamarin_open_and_register ("FlyoutNavigation.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("OxyPlot.Xamarin.iOS.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);
	xamarin_open_and_register ("SWTableViewCell.dll", &exception_gchandle);
	xamarin_process_managed_exception_gchandle (exception_gchandle);

}

extern "C" void xamarin_create_classes();
void xamarin_setup_impl ()
{
	xamarin_create_classes();
	xamarin_gc_pump = FALSE;
	xamarin_init_mono_debug = TRUE;
	xamarin_executable_name = "IONIOS.exe";
	mono_use_llvm = FALSE;
	xamarin_log_level = 3;
	xamarin_arch_name = "arm64";
	xamarin_marshal_objectivec_exception_mode = MarshalObjectiveCExceptionModeDisable;
	xamarin_debug_mode = TRUE;
	setenv ("MONO_GC_PARAMS", "nursery-size=512k,major=marksweep", 1);
}

int main (int argc, char **argv)
{
	NSAutoreleasePool *pool = [[NSAutoreleasePool alloc] init];
	int rv = xamarin_main (argc, argv, XamarinLaunchModeApp);
	[pool drain];
	return rv;
}
void xamarin_initialize_callbacks () __attribute__ ((constructor));
void xamarin_initialize_callbacks ()
{
	xamarin_setup = xamarin_setup_impl;
	xamarin_register_assemblies = xamarin_register_assemblies_impl;
	xamarin_register_modules = xamarin_register_modules_impl;
}
