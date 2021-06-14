using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using The_Binding_of_Isaac_knowledge_base.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace The_Binding_of_Isaac_knowledge_base
{
	// Token: 0x0200000D RID: 13
	public sealed partial class TBoI_ItemsPage : LayoutAwarePage
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00003892 File Offset: 0x00001A92
		public TBoI_ItemsPage()
		{
			this.InitializeComponent();
            this.backButton.Click += GoBack;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000038A0 File Offset: 0x00001AA0
		protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000038A2 File Offset: 0x00001AA2
		protected override void SaveState(Dictionary<string, object> pageState)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000038A4 File Offset: 0x00001AA4
		private new void GoBack(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Navigate(typeof(TBoI_Page1));
			Window.Current.Content=(frame);
			Window.Current.Activate();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000038E0 File Offset: 0x00001AE0
		
	}
}
