using System;

namespace The_Binding_of_Isaac_knowledge_base.Common
{
	// Token: 0x02000007 RID: 7
	public class SuspensionManagerException : Exception
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000331F File Offset: 0x0000151F
		public SuspensionManagerException()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003327 File Offset: 0x00001527
		public SuspensionManagerException(Exception e) : base("SuspensionManager failed", e)
		{
		}
	}
}
