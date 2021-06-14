using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;
using The_Binding_of_Isaac_knowledge_base.Common;
namespace The_Binding_of_Isaac_knowledge_base
{
	// Token: 0x0200000F RID: 15
	public sealed partial class TBoI_Page1 : Page
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003B82 File Offset: 0x00001D82
		public TBoI_Page1()
		{
			this.InitializeComponent();
            this.backButton.Click += BackButton_Click;
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Navigate(typeof(MainPage));
			Window.Current.Content = (frame);
			Window.Current.Activate();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003B90 File Offset: 0x00001D90
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003B94 File Offset: 0x00001D94
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Navigate(typeof(TBoI_MonstersPage));
			Window.Current.Content=(frame);
			Window.Current.Activate();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Navigate(typeof(TBoI_ItemsPage));
			Window.Current.Content=(frame);
			Window.Current.Activate();
		}

		
	}
}
