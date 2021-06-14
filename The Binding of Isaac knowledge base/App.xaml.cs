using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
//using The_Binding_of_Isaac_knowledge_base.The_Binding_of_Isaac_knowledge_base_Xaml;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace The_Binding_of_Isaac_knowledge_base
{
	// Token: 0x02000002 RID: 2
	public sealed partial class App : Application
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public App()
		{
			this.InitializeComponent();
			base.Suspending+=(this.OnSuspending);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002094 File Offset: 0x00000294
		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			Frame frame = Window.Current.Content as Frame;
			if (frame == null)
			{
				frame = new Frame();
				ApplicationExecutionState previousExecutionState = args.PreviousExecutionState;
				Window.Current.Content=(frame);
			}
			if (frame.Content == null && !frame.Navigate(typeof(MainPage), args.Arguments))
			{
				throw new Exception("Failed to create initial page");
			}
			Window.Current.Activate();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002104 File Offset: 0x00000304
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
			deferral.Complete();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002123 File Offset: 0x00000323
		

		// Token: 0x06000005 RID: 5 RVA: 0x00002135 File Offset: 0x00000335
		

		// Token: 0x06000006 RID: 6 RVA: 0x0000213E File Offset: 0x0000033E
		
	}
}
