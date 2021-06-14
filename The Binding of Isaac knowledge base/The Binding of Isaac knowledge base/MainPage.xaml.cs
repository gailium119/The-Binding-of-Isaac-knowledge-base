using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

namespace The_Binding_of_Isaac_knowledge_base
{
	// Token: 0x0200000C RID: 12
	public sealed partial class MainPage : Page
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000037D9 File Offset: 0x000019D9
		public MainPage()
		{
			this.InitializeComponent();
            this.btn.Click += Button_Click_1;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037E7 File Offset: 0x000019E7
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000037EC File Offset: 0x000019EC
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Navigate(typeof(TBoI_Page1));
			Window.Current.Content=(frame);
			Window.Current.Activate();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003825 File Offset: 0x00001A25
		

		// Token: 0x0600005C RID: 92 RVA: 0x00003848 File Offset: 0x00001A48
		
	}
}
