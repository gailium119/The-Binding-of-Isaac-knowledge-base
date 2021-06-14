using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace The_Binding_of_Isaac_knowledge_base.Common
{
	// Token: 0x02000003 RID: 3
	[WebHostHidden]
	public abstract class BindableBase : INotifyPropertyChanged
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002188 File Offset: 0x00000388
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x000021C0 File Offset: 0x000003C0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600000B RID: 11 RVA: 0x000021F5 File Offset: 0x000003F5
		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (object.Equals(storage, value))
			{
				return false;
			}
			storage = value;
			this.OnPropertyChanged(propertyName);
			return true;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002220 File Offset: 0x00000420
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
