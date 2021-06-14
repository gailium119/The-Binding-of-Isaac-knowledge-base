using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace The_Binding_of_Isaac_knowledge_base.Common
{
	// Token: 0x02000006 RID: 6
	internal sealed class SuspensionManager
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A47 File Offset: 0x00000C47
		public static Dictionary<string, object> SessionState
		{
			get
			{
				return SuspensionManager._sessionState;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A4E File Offset: 0x00000C4E
		public static List<Type> KnownTypes
		{
			get
			{
				return SuspensionManager._knownTypes;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public static async Task SaveAsync()
    {
      try
      {
        foreach (WeakReference<Frame> registeredFrame in SuspensionManager._registeredFrames)
        {
          Frame target;
          if (registeredFrame.TryGetTarget(out target))
            SuspensionManager.SaveFrameNavigationState(target);
        }
        MemoryStream sessionData = new MemoryStream();
        DataContractSerializer serializer = new DataContractSerializer(typeof (Dictionary<string, object>), (IEnumerable<Type>) SuspensionManager._knownTypes);
        serializer.WriteObject((Stream) sessionData, (object) SuspensionManager._sessionState);
        StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("_sessionState.xml", CreationCollisionOption.ReplaceExisting);
        using (Stream fileStream = await file.OpenStreamForWriteAsync())
        {
          sessionData.Seek(0L, SeekOrigin.Begin);
          await sessionData.CopyToAsync(fileStream);
          await fileStream.FlushAsync();
        }
      }
      catch (Exception ex)
      {
        throw new SuspensionManagerException(ex);
      }
    }

		// Token: 0x0600003A RID: 58 RVA: 0x000030AC File Offset: 0x000012AC
        public static async Task RestoreAsync()
        {
            SuspensionManager._sessionState = new Dictionary<string, object>();
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("_sessionState.xml");
                using (IInputStream inStream = await file.OpenSequentialReadAsync())
                    SuspensionManager._sessionState = (Dictionary<string, object>)new DataContractSerializer(typeof(Dictionary<string, object>), (IEnumerable<Type>)SuspensionManager._knownTypes).ReadObject(inStream.AsStreamForRead());
                foreach (WeakReference<Frame> registeredFrame in SuspensionManager._registeredFrames)
                {
                    Frame target;
                    if (registeredFrame.TryGetTarget(out target))
                    {
                        target.ClearValue(SuspensionManager.FrameSessionStateProperty);
                        SuspensionManager.RestoreFrameNavigationState(target);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SuspensionManagerException(ex);
            }
        }

		// Token: 0x0600003B RID: 59 RVA: 0x000030EC File Offset: 0x000012EC
		public static void RegisterFrame(Frame frame, string sessionStateKey)
		{
			if (frame.GetValue(SuspensionManager.FrameSessionStateKeyProperty) != null)
			{
				throw new InvalidOperationException("Frames can only be registered to one session state key");
			}
			if (frame.GetValue(SuspensionManager.FrameSessionStateProperty) != null)
			{
				throw new InvalidOperationException("Frames must be either be registered before accessing frame session state, or not registered at all");
			}
			frame.SetValue(SuspensionManager.FrameSessionStateKeyProperty, sessionStateKey);
			SuspensionManager._registeredFrames.Add(new WeakReference<Frame>(frame));
			SuspensionManager.RestoreFrameNavigationState(frame);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003178 File Offset: 0x00001378
		public static void UnregisterFrame(Frame frame)
		{
			SuspensionManager.SessionState.Remove((string)frame.GetValue(SuspensionManager.FrameSessionStateKeyProperty));
			SuspensionManager._registeredFrames.RemoveAll(delegate(WeakReference<Frame> weakFrameReference)
			{
				Frame frame2;
				return !weakFrameReference.TryGetTarget(out frame2) || frame2 == frame;
			});
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000031CC File Offset: 0x000013CC
		public static Dictionary<string, object> SessionStateForFrame(Frame frame)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)frame.GetValue(SuspensionManager.FrameSessionStateProperty);
			if (dictionary == null)
			{
				string Text= (string)frame.GetValue(SuspensionManager.FrameSessionStateKeyProperty);
				if (Text!= null)
				{
					if (!SuspensionManager._sessionState.ContainsKey(Text))
					{
						SuspensionManager._sessionState[Text] = new Dictionary<string, object>();
					}
					dictionary = (Dictionary<string, object>)SuspensionManager._sessionState[Text];
				}
				else
				{
					dictionary = new Dictionary<string, object>();
				}
				frame.SetValue(SuspensionManager.FrameSessionStateProperty, dictionary);
			}
			return dictionary;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003244 File Offset: 0x00001444
		private static void RestoreFrameNavigationState(Frame frame)
		{
			Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(frame);
			if (dictionary.ContainsKey("Navigation"))
			{
				frame.SetNavigationState((string)dictionary["Navigation"]);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000327C File Offset: 0x0000147C
		private static void SaveFrameNavigationState(Frame frame)
		{
			Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(frame);
			dictionary["Navigation"] = frame.GetNavigationState();
		}

		// Token: 0x0400000A RID: 10
		private const string sessionStateFilename = "_sessionState.xml";

		// Token: 0x0400000B RID: 11
		private static Dictionary<string, object> _sessionState = new Dictionary<string, object>();

		// Token: 0x0400000C RID: 12
		private static List<Type> _knownTypes = new List<Type>();

		// Token: 0x0400000D RID: 13
		private static DependencyProperty FrameSessionStateKeyProperty = DependencyProperty.RegisterAttached("_FrameSessionStateKey", typeof(string), typeof(SuspensionManager), null);

		// Token: 0x0400000E RID: 14
		private static DependencyProperty FrameSessionStateProperty = DependencyProperty.RegisterAttached("_FrameSessionState", typeof(Dictionary<string, object>), typeof(SuspensionManager), null);

		// Token: 0x0400000F RID: 15
		private static List<WeakReference<Frame>> _registeredFrames = new List<WeakReference<Frame>>();
	}
}
