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
	// Token: 0x0200000E RID: 14
	public sealed partial class TBoI_MonstersPage : LayoutAwarePage
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003A0A File Offset: 0x00001C0A
		public TBoI_MonstersPage()
		{
			this.InitializeComponent();
            this.backButton.Click+=backButton_Click;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A18 File Offset: 0x00001C18
		protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003A1A File Offset: 0x00001C1A
		protected override void SaveState(Dictionary<string, object> pageState)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003A1C File Offset: 0x00001C1C
        private  void backButton_Click(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Navigate(typeof(TBoI_Page1));
			Window.Current.Content=(frame);
			Window.Current.Activate();
		}

		
	}
}
