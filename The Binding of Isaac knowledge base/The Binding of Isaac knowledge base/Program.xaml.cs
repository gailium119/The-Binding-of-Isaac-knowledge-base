using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace The_Binding_of_Isaac_knowledge_base
{
	// Token: 0x02000010 RID: 16
	public static class Program
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003CBE File Offset: 0x00001EBE
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		private static void Main(string[] args)
		{
			Application.Start(delegate(ApplicationInitializationCallbackParams p)
			{
				new App();
			});
		}
	}
}
