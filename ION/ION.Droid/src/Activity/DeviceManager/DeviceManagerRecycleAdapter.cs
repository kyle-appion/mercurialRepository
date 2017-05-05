namespace ION.Droid.Activity.DeviceManager {

	using System;
	using System.Collections.Generic;

	using Android.App;
	using Android.Content;
	using Android.Support.V7.Widget;
	using Android.OS;
	using Android.Views;

	using Appion.Commons.Util;
	using L = Appion.Commons.Util.Log;

	using ION.Core.App;
	using ION.Core.Connections;
	using ION.Core.Devices;
	using ION.Core.Devices.Sorters;
	using ION.Core.Sensors;

	using ION.Droid.Dialog;
	using ION.Droid.Util;
	using ION.Droid.Views;
	using ION.Droid.Widgets.RecyclerViews;

	internal enum EViewType {
		Section,
		IDevice,
		SerialNumber,
		Sensor,
		Space,
	}

	/// <summary>
	/// The delegate that will be notified when a sensor is clicked.
	/// </summary>
	public delegate void OnSensorReturnClicked(Sensor sensor);
	/// <summary>
	/// The adapter that will list and present ION devices to the user.
	/// </summary>
	public class DeviceManagerRecycleAdapter : RecordAdapter {
		[Flags]
		private enum EActions {
			ConnectAll = 1 << 0,
			DisconnectAll = 1 << 1,
			ForgetAll = 1 << 2,
			AddAllToWorkbench = 1 << 3
		}

		public OnSensorReturnClicked onSensorReturnClicked;

		public IFilter<Sensor> sensorFilter {
			get {
				return __sensorFilter;
			}
			set {
				if (value == null) {
					value = new YesFilter<Sensor>();
				}
				__sensorFilter = value;
			}
		}
		IFilter<Sensor> __sensorFilter;

		public IFilter<IDevice> deviceFilter {
			get {
				return __deviceFilter;
			}
			set {
				if (value == null) {
					value = new YesFilter<IDevice>();
				}
				__deviceFilter = value;
			}
		}
		IFilter<IDevice> __deviceFilter;

		private IION ion;
		private BitmapCache cache;

		/// <summary>
		/// The mapping of devices to the section that they are currently present in.
		/// </summary>
		private Dictionary<IDevice, Section> deviceToSection = new Dictionary<IDevice, Section>();
		/// <summary>
		/// The lookup table for the sections.
		/// </summary>
		private Dictionary<ESection, Section> allSections = new Dictionary<ESection, Section>();
		/// <summary>
		/// The list of the shown sections.
		/// </summary>
		private List<Section> shownSections = new List<Section>();
		/// <summary>
		/// The current expanded device.
		/// </summary>
		private IDevice expandedDevice;

		public DeviceManagerRecycleAdapter(IION ion, BitmapCache cache) {
			this.ion = ion;
			this.cache = cache;

			var connected = new Section(ESection.Connected);
			connected.actions = BuildBatchOptionsDialog(EActions.DisconnectAll | EActions.ForgetAll | EActions.AddAllToWorkbench,
																									Resource.String.device_manager_batch_connected_actions, connected);
			allSections.Add(ESection.Connected, connected);

			var broadcasting = new Section(ESection.Broadcasting);
			broadcasting.actions = BuildBatchOptionsDialog(EActions.ConnectAll | EActions.AddAllToWorkbench,
																										 Resource.String.device_manager_batch_long_range_actions, broadcasting);
			allSections.Add(ESection.Broadcasting, broadcasting);

			var @new = new Section(ESection.New);
			@new.actions = BuildBatchOptionsDialog(EActions.ConnectAll, Resource.String.device_manager_batch_new_device_actions, @new);
			allSections.Add(ESection.New, @new);

			var avail = new Section(ESection.Available);
			@avail.actions = BuildBatchOptionsDialog(EActions.ConnectAll | EActions.ForgetAll,
																							 Resource.String.device_manager_batch_available_actions, avail);
			allSections.Add(ESection.Available, avail);

			var disconnected = new Section(ESection.Disconnected);
			disconnected.actions = BuildBatchOptionsDialog(EActions.ConnectAll | EActions.ForgetAll,
																										 Resource.String.device_manager_batch_disconnected_actions, disconnected);
			allSections.Add(ESection.Disconnected, disconnected);

			onSensorReturnClicked = null;
			ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			var rv = recyclerView as SwipeRecyclerView;
			switch ((EViewType)viewType) {
				case EViewType.Section:
					return new SectionViewHolder(parent);
				case EViewType.IDevice:
					return new DeviceViewHolder(rv, cache);
				case EViewType.Sensor:
					return new SensorViewHolder(parent, ion, cache);
				case EViewType.Space:
					return new SpaceViewHolder(parent);
				default:
					throw new Exception("Cannot create view holder for viewType: " + viewType);
			}
		}

		public override void OnAttachedToRecyclerView(RecyclerView recyclerView) {
			base.OnAttachedToRecyclerView(recyclerView);
			ion.deviceManager.onDeviceManagerEvent += OnDeviceManagerEvent;
		}

		public override void OnDetachedFromRecyclerView(RecyclerView recyclerView) {
			base.OnDetachedFromRecyclerView(recyclerView);
			ion.deviceManager.onDeviceManagerEvent -= OnDeviceManagerEvent;
		}

		/// <summary>
		/// Binds the given record to the view holder.
		/// </summary>
		/// <param name="record">Record.</param>
		/// <param name="vh">Vh.</param>
		/// <param name="position">Position.</param>
		public override void OnBindViewHolder(RecyclerView.ViewHolder vh, int position) {
			switch ((EViewType)GetItemViewType(position)) {
				case EViewType.IDevice: {
					var dr = records[position] as DeviceRecord;
					dr.onDeleteClicked = (obj) => {
						RequestDeleteDevices(obj);
					};
				} break; // EViewType.IDevice
				case EViewType.Section: {
					var sr = records[position] as SectionRecord;
					sr.clickAction = () => {
						if (sr.data.actions != null) {
							sr.data.actions();
						}
					};
				} break; // EViewType.Section
				case EViewType.Sensor: {
					var sr = records[position] as SensorRecord;
					sr.onSensorClicked = (sensor) => {
						if (onSensorReturnClicked != null) {
							onSensorReturnClicked(sensor);
						}
					};
				} break; // EViewType.Sensor
			}
			base.OnBindViewHolder(vh, position);
		}

		protected override bool OnInterceptItemClicked(int position) {
			var dr = records[position] as DeviceRecord;
			if (dr != null) {
				ToggleRecord(position);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Reloads the content of the adapter. Note: this is a full reload that does not perform pretty animations.
		/// </summary>
		public void Reload() {
			foreach (var section in allSections.Values) {
				section.ClearDevices();
			}
			records.Clear();
			shownSections.Clear();

			foreach (var device in ion.deviceManager.devices) {
				var state = device.Section();
				var section = allSections[state];
				if (AllowsDevice(device)) {
					deviceToSection[device] = section;
					AddDeviceToSection(section, device, false);
					if (device == expandedDevice) {
						ExpandRecord(IndexOfDevice(device), false);
					}
				}
			}

			NotifyDataSetChanged();
		}

		/// <summary>
		/// Requests the delete devices.
		/// </summary>
		/// <param name="devices">Devices.</param>
		private void RequestDeleteDevices(params IDevice[] devices) {
			var adb = new IONAlertDialog(recyclerView.Context);
			adb.SetTitle(Resource.String.devices_forget);
			adb.SetMessage(Resource.String.devices_forget_warning);
			adb.SetNegativeButton(Resource.String.cancel, (sender, e) => {});
			adb.SetPositiveButton(Resource.String.forget, (sender, e) => {
				ForgetDevices(devices);
			});
			adb.Show(); 
		}

		/// <summary>
		/// Reloads the contents of the device.
		/// </summary>
		/// <param name="device">Device.</param>
		private void ReloadDevice(IDevice device) {
			var index = IndexOfDevice(device);
			NotifyItemChanged(index);
		}

		/// <summary>
		/// Adds the given device to the given section and performs an add animation. Returns true if the device was added
		/// to the section.
		/// </summary>
		/// <param name="section">Section.</param>
		/// <param name="device">Device.</param>
		private bool AddDeviceToSection(Section section, IDevice device, bool animate = true) {
			lock (this) {
				if (section.HasDevice(device) || !AllowsDevice(device)) {
					return false;
				}

				section.AddDevice(device);
				section.Sort(new DeviceSerialNumberSorter());
				deviceToSection[device] = section;

				var sectionIndex = IndexOfSection(section);

				if (!shownSections.Contains(section)) {
					shownSections.Add(section);
					shownSections.Sort(new SectionSorter());
					sectionIndex = FindInsertionIndexForSection(section);
					records.Insert(sectionIndex, new SectionRecord(section));
					if (animate) {
						NotifyItemInserted(sectionIndex);
					}
				}

				var deviceIndex = FindInsertionIndexForDevice(section, device);
				var record = CreateRecordFor(device);
				if (deviceIndex >= records.Count) {
					records.Add(record);
					records.Add(new SpaceRecord());
				} else {
					records.Insert(deviceIndex, new SpaceRecord());
					records.Insert(deviceIndex, record);
				}

				if (animate) {
					NotifyItemRangeInserted(deviceIndex, 2);
				}

				NotifyItemChanged(sectionIndex);
				return true;
			}
		}

		/// <summary>
		/// Removes the given device from the given section and performs an add animation.
		/// </summary>
		/// <param name="section">Section.</param>
		/// <param name="device">Device.</param>
		private void RemoveDeviceFromSection(Section section, IDevice device, bool animate = true) {
			lock (this) {
				if (!section.HasDevice(device)) {
					return;
				}

				var sectionIndex = IndexOfSection(section);
				var deviceIndex = IndexOfDevice(device);
				var record = RecordForDevice(device);
				var size = SizeOfRecord(record);
				section.RemoveDevice(device);
				deviceToSection.Remove(device);

				if (deviceIndex != -1) {
					Log.D(this, "Removing record of size: " + size);
					records.RemoveRange(deviceIndex, size);
					if (animate) {
						NotifyItemRangeRemoved(deviceIndex, size);
					}

					if (shownSections.Contains(section) && section.count <= 0) {
						shownSections.Remove(section);
						records.RemoveAt(sectionIndex);
						if (animate) {
							NotifyItemRemoved(sectionIndex);
						}
					} else {
						NotifyItemChanged(sectionIndex);
					}

					if (device == expandedDevice) {
						expandedDevice = null;
					}
				}
			}
		}

		/// <summary>
		/// Toggles whether or not the given record is expanded or not.
		/// </summary>
		/// <param name="index">Index.</param>
		private void ToggleRecord(int index) {
			var dr = records[index] as DeviceRecord;
			if (dr != null) {
				if (dr.data == expandedDevice) {
					CollapseRecord(index, true);
				} else {
					ExpandRecord(index, true);
				}
			}
		}

		/// <summary>
		/// Expands the record at the given index, if it is expandable.
		/// </summary>
		/// <param name="index">Index.</param>
		private void ExpandRecord(int index, bool animate = true) {
			var dr = records[index] as DeviceRecord;

			if (!IsRecordExpandable(dr) || dr == null || dr.data == expandedDevice) {
				return;
			}

			var added = 0;

			if (dr.data is GaugeDevice) {
				var gd = dr.data as GaugeDevice;
				var sensors = gd.GetSensorsMatchingFilter(sensorFilter);
				for (int i = sensors.Count; i > 0; i--) {
					records.Insert(index + 1, new SensorRecord(sensors[i - 1]));
				}
				added = sensors.Count;
			}

			if (animate && added > 0) {
				NotifyItemRangeInserted(index + 1, added);
			}

			if (expandedDevice != null && expandedDevice != dr.data) {
				CollapseRecord(IndexOfDevice(expandedDevice));
			}

			expandedDevice = dr.data;
			dr.isExpanded = true;
			NotifyItemChanged(index);
		}

		/// <summary>
		/// Collapses the record at the given index.
		/// </summary>
		/// <param name="index">Index.</param>
		private void CollapseRecord(int index, bool animate = true) {
			var dr = records[index] as DeviceRecord;

			if (!IsRecordExpandable(dr) || dr == null) {
				return;
			}

			var section = deviceToSection[dr.data];
			var removed = 0;

			if (dr.data is GaugeDevice) {
				var gd = dr.data as GaugeDevice;
				var sensors = gd.GetSensorsMatchingFilter(sensorFilter);
				records.RemoveRange(index + 1, sensors.Count);
				removed = sensors.Count;
			}

			if (animate && removed > 0) {
				NotifyItemRangeRemoved(index + 1, removed);
			}

			expandedDevice = null;
			dr.isExpanded = false;
			NotifyItemChanged(index);
		}

		/// <summary>
		/// Deletes a device from the adapter.
		/// THIS CALLED AFTER THE DEVICE HAS ALREADY BEEN REMOVED FROM THE DEVICE MANAGER.
		/// </summary>
		/// <param name="device">Device.</param>
		/// <param name="animate">If set to <c>true</c> animate.</param>
		private void DeleteDevice(IDevice device, bool animate = true) {
			foreach (var s in allSections.Values) {
				if (s.HasDevice(device)) {
					RemoveDeviceFromSection(s, device, animate);
				}
			}
		}

		/// <summary>
		/// Queries whether or not the record is expandable.
		/// </summary>
		/// <returns><c>true</c> if this instance is record expandable the specified record; otherwise, <c>false</c>.</returns>
		/// <param name="record">Record.</param>
		private bool IsRecordExpandable(IRecord record) {
			var dr = record as DeviceRecord;
			return dr != null && dr.data is GaugeDevice;
		}

		/// <summary>
		/// Queries the size of the given record. Because records can expand, allowing for "child" records, this method is
		/// used to query how many records are associated to the given record. 1 means the record is only responsible for
		/// itself. Most records will have a size of 2.
		/// </summary>
		/// <returns>The of record.</returns>
		/// <param name="record">Record.</param>
		private int SizeOfRecord(IRecord record) {
			var dr = record as DeviceRecord;
			if (dr != null) {
				var gd = dr.data as GaugeDevice;
				if (gd != null && gd == expandedDevice) {
					return 2 + gd.GetSensorsMatchingFilter(sensorFilter).Count;
				} else {
					return 2;
				}
			} else {
				return 2;
			}
		}

		/// <summary>
		/// Find the record insertion index for the given device within the given section.
		/// </summary>
		/// <returns>The insertion index for device.</returns>
		/// <param name="section">Section.</param>
		/// <param name="device">Device.</param>
		private int FindInsertionIndexForDevice(Section section, IDevice device) {
			var ret = IndexOfSection(section) + 1;

			foreach (var d in section.GetDevices()) {
				if (d == device) {
					return ret;
				} else {
					ret += SizeOfRecord(RecordForDevice(d));
				}
			}

			return -1;
		}

		/// <summary>
		/// Find the record insertion index for the given section.
		/// </summary>
		/// <returns>The insertion index for section.</returns>
		/// <param name="section">Section.</param>
		private int FindInsertionIndexForSection(Section section) {
			var c = recyclerView.Context;
			var name = section.section.ToLocalizedString(c);
			var ret = 0;

			var index = shownSections.IndexOf(section);
			for (int i = 0; i < index; i++) {
				var size = SizeOfSection(shownSections[i]);
				ret += size;
			}

			return ret;
		}

		/// <summary>
		/// Creates a record for the given device.
		/// </summary>
		/// <returns>The record for.</returns>
		/// <param name="device">Device.</param>
		private IRecord CreateRecordFor(IDevice device) {
			return new DeviceRecord(device, (obj) => {
				//				ShowRequestForgetDevices(new IDevice[] { device });
			});
		}

		/// <summary>
		/// Queries whether or not the source will allow the device to be used.
		/// </summary>
		/// <returns><c>true</c>, if device was allowsed, <c>false</c> otherwise.</returns>
		/// <param name="device">Device.</param>
		private bool AllowsDevice(IDevice device) {
			if (deviceFilter.Matches(device)) {
				var gd = device as GaugeDevice;
				if (gd != null) {
					return gd.GetSensorsMatchingFilter(sensorFilter).Count > 0;
				} else {
					return false;
				}
			} else {
				return false;
			}
		}

		/// <summary>
		/// Builds an options dialog for a device state category.
		/// </summary>
		/// <param name="actions">Actions.</param>
		/// <param name="title">Title.</param>
		/// <param name="devices">Devices.</param>
		private Action BuildBatchOptionsDialog(EActions actions, int title, Section section) {
			return () => {
				var ldb = new ListDialogBuilder(recyclerView.Context);
				ldb.SetTitle(title);

				for (int i = 1; i <= 32; i++) {
					if ((i & (int)actions) == i) {
						switch ((EActions)i) {
							case EActions.ConnectAll:
								ldb.AddItem(Resource.String.connect_all, () => {
									foreach (var device in section.GetDevices()) {
										device.connection.Connect();
									}
								});
								break;

							case EActions.DisconnectAll:
								ldb.AddItem(Resource.String.disconnect_all, () => {
									foreach (var device in section.GetDevices()) {
										device.connection.Disconnect();
									}
								});
								break;

							case EActions.ForgetAll:
								ldb.AddItem(Resource.String.forget_all, () => {
									ShowRequestForgetDevices(section.GetDevices());
								});
								break;

							case EActions.AddAllToWorkbench:
								ldb.AddItem(Resource.String.device_manager_add_all_to_workbench, () => {
									foreach (var device in section.GetDevices()) {
										var gd = device as GaugeDevice;
										if (gd != null) {
											foreach (var sensor in gd.sensors) {
												ion.currentWorkbench.AddSensor(sensor);
											}
										}
									}
								});
								break;
						}
					}
				}

				ldb.Show();
			};
		}

		/// <summary>
		/// Shows a dialog that will confirm with the user the intention to delete a collection of devices fromt the
		/// application.
		/// </summary>
		/// <param name="devices">Devices.</param>
		private void ShowRequestForgetDevices(IEnumerable<IDevice> devices) {
			var adb = new IONAlertDialog(recyclerView.Context);

			adb.SetTitle(Resource.String.devices_forget);
			adb.SetMessage(Resource.String.devices_forget_warning);

			adb.SetNegativeButton(Resource.String.cancel, (obj, e) => {
				var dialog = obj as Dialog;
				dialog.Dismiss();
			});

			adb.SetPositiveButton(Resource.String.forget, (obj, e) => {
				var dialog = obj as Dialog;
				dialog.Dismiss();
				ForgetDevices(devices);
			});

			adb.Show();
		}

		/// <summary>
		/// Deleted the devices from the app.
		/// </summary>
		/// <param name="devices">Devices.</param>
		private void ForgetDevices(IEnumerable<IDevice> devices) {
			foreach (var d in devices) {
				ion.deviceManager.DeleteDevice(d.serialNumber);
			}
		}

		/// <summary>
		/// Queries the record that is represented by the given device.
		/// </summary>
		/// <returns>The for device.</returns>
		/// <param name="device">Device.</param>
		private IRecord RecordForDevice(IDevice device) {
			var index = IndexOfDevice(device);
			if (index >= 0) {
				return records[index];
			} else {
				return null;
			}
		}

		/// <summary>
		/// Queries the size of the section (inclusive of its own record).
		/// </summary>
		/// <returns>The of section.</returns>
		/// <param name="section">Section.</param>
		private int SizeOfSection(Section section) {
			var ret = 1;

			var sectionIndex = IndexOfSection(section);
			foreach (var device in section.GetDevices()) {
				ret += SizeOfRecord(RecordForDevice(device));
			}

			return ret;
		}

		/// <summary>
		/// Queries the index of the device. If the device is not in the adapter, -1 will be returned.
		/// </summary>
		/// <returns>The of device.</returns>
		/// <param name="device">Device.</param>
		private int IndexOfDevice(IDevice device) {
			for (int i = 0; i < records.Count; i++) {
				var record = records[i] as DeviceRecord;

				if (record != null && record.data.Equals(device)) {
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// Queries the index of the section. If the section is not in the adapter, -1 will be returned.
		/// </summary>
		/// <returns>The of section.</returns>
		/// <param name="section">Section.</param>
		private int IndexOfSection(Section section) {
			for (int i = 0; i < records.Count; i++) {
				var record = records[i] as SectionRecord;
				if (record != null && record.data.Equals(section)) {
					return i;
				}
			}

			return -1;
		}

		private EActions GetActionsForSection(ESection section) {
			switch (section) {
				case ESection.Connected: {
						return EActions.DisconnectAll | EActions.AddAllToWorkbench | EActions.ForgetAll;
					} // ESection.Connected

				case ESection.Broadcasting: {
						return EActions.ConnectAll | EActions.AddAllToWorkbench | EActions.ForgetAll;
					} // ESection.Broadcasting

				case ESection.New: {
						return EActions.ConnectAll;
					} // ESection.New

				case ESection.Available: {
						return EActions.ConnectAll | EActions.AddAllToWorkbench | EActions.ForgetAll;
					} // ESection.Available

				case ESection.Disconnected: {
						return EActions.ConnectAll | EActions.AddAllToWorkbench | EActions.ForgetAll;
					} // ESection.Disconnected
				default: {
						return 0;
					}
			}
		}

		private void OnDeviceManagerEvent(DeviceManagerEvent de) {
			lock (this) {
				if (DeviceManagerEvent.EType.DeviceEvent == de.type) {
					var device = de.deviceEvent.device;
					var et = de.deviceEvent.type;
					var destSection = allSections[device.Section()];

					switch (et) {
						case DeviceEvent.EType.Found:
						//							AddDeviceToSection(destSection, device, true);
						//              break;
						// Fallthrough
						case DeviceEvent.EType.ConnectionChange:
							bool added = false;
							if (deviceToSection.ContainsKey(device)) {
								var oldSection = deviceToSection[device];

								if (oldSection != destSection) {
									foreach (var section in allSections.Values) {
										RemoveDeviceFromSection(section, device);
									}
									added = AddDeviceToSection(destSection, device);
								} // Else the device is already in the proper section.
							} else {
								// The device is not known
								added = AddDeviceToSection(destSection, device);
							}

							if (added) {
								if (device.Section() == ESection.Connected) {
									ExpandRecord(IndexOfDevice(device));
								}
							}
							break;
						case DeviceEvent.EType.Deleted:
							DeleteDevice(de.deviceEvent.device);
							break;
						case DeviceEvent.EType.NameChanged:
							ReloadDevice(device);
							break;
						case DeviceEvent.EType.NewData:
							break;
					}
				}
			}
		}

		/// <summary>
		/// The sorter that will sort sections based on their EDeviceState.
		/// </summary>
		internal class SectionSorter : IComparer<Section> {
			public int Compare(Section s1, Section s2) {
				return s1.section.CompareTo(s2.section);
			}
		}
	}

	[Flags]
	public enum ESection {
		Connected = 1 << 0,
		Broadcasting = 1 << 1,
		New = 1 << 2,
		Available = 1 << 3,
		Disconnected = 1 << 4,
	}

	internal static class Extensions {
		public static string ToLocalizedString(this ESection section, Context context) {
			switch (section) {
				case ESection.Connected:
					return context.GetString(Resource.String.connected);
				case ESection.Broadcasting:
					return context.GetString(Resource.String.long_range_mode);
				case ESection.New:
					return context.GetString(Resource.String._new);
				case ESection.Available:
					return context.GetString(Resource.String.available);
				case ESection.Disconnected:
					return context.GetString(Resource.String.disconnected);
				default:
					return context.GetString(Resource.String.unknown);
			}
		}

		public static ESection Section(this IDevice device) {
			var ion = AppState.context;
			var connectionState = device.connection.connectionState;

			if (EConnectionState.Connected == connectionState) {
				return ESection.Connected;
			} else if (EConnectionState.Broadcasting == connectionState) {
				return ESection.Broadcasting;
			} else if (!ion.deviceManager.IsDeviceKnown(device)) {
				return ESection.New;
			} else if ((ion.deviceManager.IsDeviceKnown(device) && device.isNearby) || EConnectionState.Connecting == connectionState) {
				return ESection.Available;
			} else {
				return ESection.Disconnected;
			}
		}

		public static Android.Graphics.Color Color(this ESection section, Context context) {
			int c = 0;
			switch (section) {
				case ESection.Connected: {
						c = Resource.Color.green;
					}
					break; // ESection.Conntext

				case ESection.Broadcasting: {
						c = Resource.Color.light_blue;
					}
					break; // ESection.Broadcasting

				case ESection.New: {
						c = Resource.Color.light_gray;
					}
					break; // ESection.New

				case ESection.Available: {
						c = Resource.Color.yellow;
					}
					break; // ESection.Available

				case ESection.Disconnected: {
						c = Resource.Color.red;
					}
					break; // ESection.Disconnected

				default: {
						c = Resource.Color.red;
					}
					break;
			}

			return new Android.Graphics.Color(context.Resources.GetColor(c));
		}
	}

	/// <summary>
	/// A section is a collection of devices that are to be displayed in the list view.
	/// </summary>
	public class Section {
		/// <summary>
		/// The state that all the sections devices are in.
		/// </summary>
		public ESection section;
		/// <summary>
		/// The list of devices that are present in the section. This is used simply to maintain a sorted devices list.
		/// </summary>
		private List<IDevice> devices = new List<IDevice>();
		/// <summary>
		/// The actions that are allowed for the section.
		/// </summary>
		public Action actions;

		public int count {
			get {
				Log.D(this, "Section: " + section + " has a count of: " + devices.Count);
				return devices.Count;
			}
		}

		public Section(ESection section) {
			this.section = section;
		}

		public void ClearDevices() {
			devices.Clear();
		}

		public List<IDevice> GetDevices() {
			return devices;
		}

		public void AddDevice(IDevice device) {
			if (!HasDevice(device)) {
				devices.Add(device);
			}
		}

		public void RemoveDevice(IDevice device) {
			devices.Remove(device);
		}

		/// <summary>
		/// Queries whether or not the table manager source has the given device.
		/// </summary>
		/// <returns><c>true</c> if this instance has device the specified device; otherwise, <c>false</c>.</returns>
		/// <param name="device">Device.</param>
		public bool HasDevice(IDevice device) {
			return devices.Contains(device);
		}

		public void Sort(IComparer<IDevice> comparer) {
			devices.Sort();
		}
	}
}
