namespace ION.UITests.Droid.Activities {

	using System;

	using NUnit.Framework;

	using Xamarin.UITest;
	using Xamarin.UITest.Android;
	using Xamarin.UITest.Queries;

	[TestFixture (Platform.Android)]
	public class WorkbenchActivityFixture {

		private Platform platform;
		private AndroidApp app;

		public WorkbenchActivityFixture(Platform platform) {
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest() {
			AppInitializer.StartApp(platform);
		}

		[Test]
		public void TestAddDevices() {
			app.WaitForElement(c => c.Marked("list"));
			app.Query(q => q.Id("list").Descendant());
		}
	}
}
