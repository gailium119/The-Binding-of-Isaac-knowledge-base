using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace The_Binding_of_Isaac_knowledge_base.Common
{
	// Token: 0x02000005 RID: 5
	public sealed class BooleanToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000228A File Offset: 0x0000048A
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return (value is bool && (bool)value) ? 0 : 1;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022A5 File Offset: 0x000004A5
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return value is Visibility && (Visibility)value == 0;
		}
	}
}
