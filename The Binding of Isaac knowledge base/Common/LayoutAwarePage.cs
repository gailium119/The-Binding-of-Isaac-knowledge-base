// Decompiled with JetBrains decompiler
// Type: The_Binding_of_Isaac_knowledge_base.Common.LayoutAwarePage
// Assembly: The_Binding_of_Isaac_knowledge_base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A5631F63-2E87-4FA2-B22A-F260D1B3D3AD
// Assembly location: C:\Users\Administrator\3D Objects\27358WPMarcel.The_Binding_of_Isaac_knowledge_base_1.1.0.2_neutral__b5d8taw6jn9tc\The_Binding_of_Isaac_knowledge_base.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace The_Binding_of_Isaac_knowledge_base.Common
{

  public class LayoutAwarePage : Page
  {
      public static readonly DependencyProperty DefaultViewModelProperty= DependencyProperty.Register("DefaultViewModel", typeof(IObservableMap<string, object>), typeof(LayoutAwarePage), (PropertyMetadata)null);
    private List<Control> _layoutAwareControls;
    private string _pageKey;

    public LayoutAwarePage()
    {

     if (DesignMode.DesignModeEnabled)
			{
				return;
			}
			this.DefaultViewModel = new LayoutAwarePage.ObservableDictionary<string, object>();
            this.Loaded +=LayoutAwarePage_Loaded;
            this.Unloaded += LayoutAwarePage_UnLoaded;

    }
     void LayoutAwarePage_Loaded(object sender, RoutedEventArgs e)
      {
        this.StartLayoutUpdates(sender, e);
        if (this.ActualHeight != Window.Current.Bounds.Height || this.ActualWidth != Window.Current.Bounds.Width)
          return;
         Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated+=this.CoreDispatcher_AcceleratorKeyActivated;
       Window.Current.CoreWindow.PointerPressed+=this.CoreWindow_PointerPressed;
      }
     void LayoutAwarePage_UnLoaded(object sender, RoutedEventArgs e)
      {
        this.StopLayoutUpdates(sender, e);
        Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated -= this.CoreDispatcher_AcceleratorKeyActivated;
        Window.Current.CoreWindow.PointerPressed -= this.CoreWindow_PointerPressed;
      }
    protected IObservableMap<string, object> DefaultViewModel
    {
        get
        {
            return (base.GetValue(DefaultViewModelProperty) as IObservableMap<string, object>);
        }
        set
        {
            base.SetValue(DefaultViewModelProperty, value);
        }
       
    }

    protected virtual void GoHome(object sender, RoutedEventArgs e)
    {
      if (this.Frame == null)
        return;
      while (this.Frame.CanGoBack)
        this.Frame.GoBack();
    }

    protected virtual void GoBack(object sender, RoutedEventArgs e)
    {
      if (this.Frame == null || !this.Frame.CanGoBack)
        return;
      this.Frame.GoBack();
    }

    protected virtual void GoForward(object sender, RoutedEventArgs e)
    {
      if (this.Frame == null || !this.Frame.CanGoForward)
        return;
      this.Frame.GoForward();
    }

    private void CoreDispatcher_AcceleratorKeyActivated(
      CoreDispatcher sender,
      AcceleratorKeyEventArgs args)
    {
      VirtualKey virtualKey = args.VirtualKey;
      if (args.EventType != CoreAcceleratorKeyEventType.SystemKeyDown && args.EventType != CoreAcceleratorKeyEventType.KeyDown || virtualKey != VirtualKey.Left && virtualKey != VirtualKey.Right && (virtualKey != (VirtualKey.Up | VirtualKey.F17) && virtualKey != (VirtualKey.Right | VirtualKey.F17)))
        return;
      CoreWindow coreWindow = Window.Current.CoreWindow;
      CoreVirtualKeyStates virtualKeyStates = CoreVirtualKeyStates.Down;
      bool flag1 = (coreWindow.GetKeyState(VirtualKey.Menu) & virtualKeyStates) == virtualKeyStates;
      bool flag2 = (coreWindow.GetKeyState(VirtualKey.Control) & virtualKeyStates) == virtualKeyStates;
      bool flag3 = (coreWindow.GetKeyState(VirtualKey.Shift) & virtualKeyStates) == virtualKeyStates;
      bool flag4 = !flag1 && !flag2 && !flag3;
      bool flag5 = flag1 && !flag2 && !flag3;
      if (virtualKey == (VirtualKey.Up | VirtualKey.F17) && flag4 || virtualKey == VirtualKey.Left && flag5)
      {
        args.Handled = true;
        this.GoBack((object) this, new RoutedEventArgs());
      }
      else
      {
        if ((virtualKey != (VirtualKey.Right | VirtualKey.F17) || !flag4) && (virtualKey != VirtualKey.Right || !flag5))
          return;
        args.Handled = true;
        this.GoForward((object) this, new RoutedEventArgs());
      }
    }

    private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs args)
    {
      PointerPointProperties properties = args.CurrentPoint.Properties;
      if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed || properties.IsMiddleButtonPressed)
        return;
      bool isXbutton1Pressed = properties.IsXButton1Pressed;
      bool isXbutton2Pressed = properties.IsXButton2Pressed;
      if (!(isXbutton1Pressed ^ isXbutton2Pressed))
        return;
      args.Handled = true;
      if (isXbutton1Pressed)
        this.GoBack((object) this, new RoutedEventArgs());
      if (!isXbutton2Pressed)
        return;
      this.GoForward((object) this, new RoutedEventArgs());
    }

    public void StartLayoutUpdates(object sender, RoutedEventArgs e)
    {
     Control control = sender as Control;
			if (control == null)
			{
				return;
			}
      if (this._layoutAwareControls == null)
      {
        Window.Current.SizeChanged +=this.WindowSizeChanged;
        this._layoutAwareControls = new List<Control>();
      }
      this._layoutAwareControls.Add(control);
      VisualStateManager.GoToState(control, this.DetermineVisualState(ApplicationView.Value), false);
      Window.Current.SizeChanged += this.WindowSizeChanged;
    }

   private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			this.InvalidateVisualState();
		}

    public void StopLayoutUpdates(object sender, RoutedEventArgs e)
    {
     Control control = sender as Control;
			if (control == null || this._layoutAwareControls == null)
       {
				return;
			}
      this._layoutAwareControls.Remove(control);
      if (this._layoutAwareControls.Count != 0)
        return;
      this._layoutAwareControls = (List<Control>) null;
      Window.Current.SizeChanged += this.WindowSizeChanged;
    }

    protected virtual string DetermineVisualState(ApplicationViewState viewState)
		{
			return viewState.ToString();
		}

    public void InvalidateVisualState()
    {
      if (this._layoutAwareControls == null)
        return;
      string visualState = this.DetermineVisualState(ApplicationView.Value);
      foreach (Control layoutAwareControl in this._layoutAwareControls)
        VisualStateManager.GoToState(layoutAwareControl, visualState, false);
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      if (this._pageKey != null)
        return;
      Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(this.Frame);
      this._pageKey = "Page-" + (object) this.Frame.BackStackDepth;
      if (e.NavigationMode == NavigationMode.New)
      {
        string key = this._pageKey;
        int backStackDepth = this.Frame.BackStackDepth;
        for (; dictionary.Remove(key); key = "Page-" + (object) backStackDepth)
          ++backStackDepth;
        this.LoadState(e.Parameter, (Dictionary<string, object>) null);
      }
      else
        this.LoadState(e.Parameter, (Dictionary<string, object>) dictionary[this._pageKey]);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(this.Frame);
      Dictionary<string, object> pageState = new Dictionary<string, object>();
      this.SaveState(pageState);
      dictionary[this._pageKey] = (object) pageState;
    }

    protected virtual void LoadState(
      object navigationParameter,
      Dictionary<string, object> pageState)
    {
    }

    protected virtual void SaveState(Dictionary<string, object> pageState)
    {
    }

    public class ObservableDictionary<K, V> : 
      IObservableMap<K, V>,
      IDictionary<K, V>,
      ICollection<KeyValuePair<K, V>>,
      IEnumerable<KeyValuePair<K, V>>,
      IEnumerable
    {
      private Dictionary<K, V> _dictionary = new Dictionary<K, V>();

        public event   MapChangedEventHandler<K, V> MapChanged
      {
          add
          {
              lock (this)
              {
                  EventRegistrationTokenTable<MapChangedEventHandler<K, V>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged2).AddEventHandler(value);
                  while (true)
                  {
                      ;
                  }
              }
          }
        remove{  EventRegistrationTokenTable<MapChangedEventHandler<K, V>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged2).RemoveEventHandler(value);}
      }
        private EventRegistrationTokenTable<MapChangedEventHandler<K, V>> MapChanged2;

      private void InvokeMapChanged(CollectionChange change, K key)
      {
        MapChangedEventHandler<K, V> invocationList = EventRegistrationTokenTable<MapChangedEventHandler<K, V>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged2).InvocationList;
        if (invocationList == null)
          return;
        invocationList((IObservableMap<K, V>) this, (IMapChangedEventArgs<K>) new LayoutAwarePage.ObservableDictionary<K, V>.ObservableDictionaryChangedEventArgs(change, key));
      }

      public void Add(K key, V value)
      {
        this._dictionary.Add(key, value);
        this.InvokeMapChanged(CollectionChange.ItemInserted, key);
      }

    public void Add(KeyValuePair<K, V> item)
			{
				this.Add(item.Key, item.Value);
			}

      public bool Remove(K key)
      {
        if (!this._dictionary.Remove(key))
          return false;
        this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
        return true;
      }

      public bool Remove(KeyValuePair<K, V> item)
      {
        V v;
        if (!this._dictionary.TryGetValue(item.Key, out v) || !object.Equals((object) item.Value, (object) v) || !this._dictionary.Remove(item.Key))
          return false;
        this.InvokeMapChanged(CollectionChange.ItemRemoved, item.Key);
        return true;
      }

      public V this[K key]
      {
       get
				{
					return this._dictionary[key];
				}
        set
        {
          this._dictionary[key] = value;
          this.InvokeMapChanged(CollectionChange.ItemChanged, key);
        }
      }

      public void Clear()
      {
        K[] array = this._dictionary.Keys.ToArray<K>();
        this._dictionary.Clear();
        foreach (K key in array)
          this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
      }

     public ICollection<K> Keys
			{
				get
				{
					return this._dictionary.Keys;
				}
			}


     public bool ContainsKey(K key)
			{
				return this._dictionary.ContainsKey(key);
			}

public bool TryGetValue(K key, out V value)
			{
				return this._dictionary.TryGetValue(key, out value);
			}
public ICollection<V> Values
			{
				get
				{
					return this._dictionary.Values;
				}
			}

public bool Contains(KeyValuePair<K, V> item)
			{
				return this._dictionary.Contains(item);
			}
             public int Count{
                 get
                 {
                     return this._dictionary.Count;
                 }
             }
public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}
public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
			{
				return this._dictionary.GetEnumerator();
			}
IEnumerator IEnumerable.GetEnumerator()
			{
				return this._dictionary.GetEnumerator();
			}
      public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
      {
        int length = array.Length;
        foreach (KeyValuePair<K, V> keyValuePair in this._dictionary)
        {
          if (arrayIndex >= length)
            break;
          array[arrayIndex++] = keyValuePair;
        }
      }

      private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<K>
      {
        public ObservableDictionaryChangedEventArgs(CollectionChange change, K key)
        {
          this.CollectionChange = change;
          this.Key = key;
        }

        public CollectionChange CollectionChange { get; private set; }

        public K Key { get; private set; }
      }
    }
  }
}
