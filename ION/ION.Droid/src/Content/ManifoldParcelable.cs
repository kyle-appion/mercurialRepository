namespace ION.Droid.Content {

	using System;

	using Android.OS;

	using Java.Interop;

	using ION.Core.App;
	using ION.Core.Content;
	using ION.Core.Sensors;

	public abstract class ManifoldParcelable : GenericParcelable {
		public ManifoldParcelable() {
		}

		public ManifoldParcelable(Parcel source) {
		}

		public abstract Manifold Get(IION ion);
	}

	public class WorkbenchManifoldParcelable : ManifoldParcelable {
		[ExportField("CREATOR")]
		public static IParcelableCreator GetCreator() {
			return new GenericParcelableCreator<WorkbenchManifoldParcelable>();
		}

		private int index;

		public WorkbenchManifoldParcelable(int index) {
			this.index = index;
		}

		public WorkbenchManifoldParcelable(Parcel source) : base(source) {
			index = source.ReadInt();
		}

		public override void WriteToParcel(Parcel dest, ParcelableWriteFlags flags) {
			dest.WriteInt(index);
		}

		public override Manifold Get(IION ion) {
			return ion.currentWorkbench[index];
		}
	}

	public class AnalyzerManifoldParcelable : ManifoldParcelable {
		[ExportField("CREATOR")]
		public static IParcelableCreator GetCreator() {
			return new GenericParcelableCreator<WorkbenchManifoldParcelable>();
		}

		private bool isLow;

		public AnalyzerManifoldParcelable(bool isLow) {
			this.isLow = isLow;
		}

		public AnalyzerManifoldParcelable(Parcel source) : base(source) {
			isLow = source.ReadInt() == 1;
		}

		public override void WriteToParcel(Parcel dest, ParcelableWriteFlags flags) {
			dest.WriteInt(isLow ? 1 : 0);
		}

		public override Manifold Get(IION ion) {
			if (isLow) {
				return ion.currentAnalyzer.lowSideManifold;
			} else {
				return ion.currentAnalyzer.highSideManifold;
			}
		}
	}
}
