using System;
using Windows.UI.Xaml.Data;

namespace The_Binding_of_Isaac_knowledge_base.Common
{
	// Token: 0x02000004 RID: 4
	public sealed class BooleanNegationConverter : IValueConverter
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000224C File Offset: 0x0000044C
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return !(value is bool) || !(bool)value;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002267 File Offset: 0x00000467
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return !(value is bool) || !(bool)value;
		}
	}
}
