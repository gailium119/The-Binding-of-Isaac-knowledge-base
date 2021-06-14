using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace The_Binding_of_Isaac_knowledge_base.Common
{
	// Token: 0x02000009 RID: 9
	[ContentProperty(Name = "RichTextContent")]
	public sealed class RichTextColumns : Panel
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002B63 File Offset: 0x00000D63
		public RichTextColumns()
		{
			base.HorizontalAlignment=(0);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002B72 File Offset: 0x00000D72
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002B84 File Offset: 0x00000D84
		public RichTextBlock RichTextContent
		{
			get
			{
				return (RichTextBlock)base.GetValue(RichTextColumns.RichTextContentProperty);
			}
			set
			{
				base.SetValue(RichTextColumns.RichTextContentProperty, value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002B92 File Offset: 0x00000D92
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public DataTemplate ColumnTemplate
		{
			get
			{
				return (DataTemplate)base.GetValue(RichTextColumns.ColumnTemplateProperty);
			}
			set
			{
				base.SetValue(RichTextColumns.ColumnTemplateProperty, value);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private static void ResetOverflowLayout(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RichTextColumns richTextColumns = d as RichTextColumns;
			if (richTextColumns != null)
			{
				richTextColumns._overflowColumns = null;
				richTextColumns.Children.Clear();
				richTextColumns.InvalidateMeasure();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BE4 File Offset: 0x00000DE4
		protected override Size MeasureOverride(Size availableSize)
		{
			if (this.RichTextContent == null)
			{
				return new Size(0.0, 0.0);
			}
			if (this._overflowColumns == null)
			{
				base.Children.Add(this.RichTextContent);
				this._overflowColumns = new List<RichTextBlockOverflow>();
			}
			this.RichTextContent.Measure(availableSize);
			double num = this.RichTextContent.DesiredSize.Width;
			double num2 = this.RichTextContent.DesiredSize.Height;
			bool hasOverflowContent = this.RichTextContent.HasOverflowContent;
			int num3 = 0;
			while (hasOverflowContent && num < availableSize.Width && this.ColumnTemplate != null)
			{
				RichTextBlockOverflow richTextBlockOverflow;
				if (this._overflowColumns.Count > num3)
				{
					richTextBlockOverflow = this._overflowColumns[num3];
				}
				else
				{
					richTextBlockOverflow = (RichTextBlockOverflow)this.ColumnTemplate.LoadContent();
					this._overflowColumns.Add(richTextBlockOverflow);
					base.Children.Add(richTextBlockOverflow);
					if (num3 == 0)
					{
						this.RichTextContent.OverflowContentTarget=(richTextBlockOverflow);
					}
					else
					{
						this._overflowColumns[num3 - 1].OverflowContentTarget=(richTextBlockOverflow);
					}
				}
				richTextBlockOverflow.Measure(new Size(availableSize.Width - num, availableSize.Height));
				num += richTextBlockOverflow.DesiredSize.Width;
				num2 = Math.Max(num2, richTextBlockOverflow.DesiredSize.Height);
				hasOverflowContent = richTextBlockOverflow.HasOverflowContent;
				num3++;
			}
			if (this._overflowColumns.Count > num3)
			{
				if (num3 == 0)
				{
					this.RichTextContent.OverflowContentTarget=(null);
				}
				else
				{
					this._overflowColumns[num3 - 1].OverflowContentTarget=(null);
				}
				while (this._overflowColumns.Count > num3)
				{
					this._overflowColumns.RemoveAt(num3);
					base.Children.RemoveAt(num3 + 1);
				}
			}
			return new Size(num, num2);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DC0 File Offset: 0x00000FC0
		protected override Size ArrangeOverride(Size finalSize)
		{
			double num = 0.0;
			double num2 = 0.0;
			foreach (UIElement uielement in base.Children)
			{
				uielement.Arrange(new Rect(num, 0.0, uielement.DesiredSize.Width, finalSize.Height));
				num += uielement.DesiredSize.Width;
				num2 = Math.Max(num2, uielement.DesiredSize.Height);
			}
			return new Size(num, num2);
		}

		// Token: 0x0400000B RID: 11
		public static readonly DependencyProperty RichTextContentProperty = DependencyProperty.Register("RichTextContent", typeof(RichTextBlock), typeof(RichTextColumns), new PropertyMetadata(null, new PropertyChangedCallback(RichTextColumns.ResetOverflowLayout)));

		// Token: 0x0400000C RID: 12
		public static readonly DependencyProperty ColumnTemplateProperty = DependencyProperty.Register("ColumnTemplate", typeof(DataTemplate), typeof(RichTextColumns), new PropertyMetadata(null, new PropertyChangedCallback(RichTextColumns.ResetOverflowLayout)));

		// Token: 0x0400000D RID: 13
		private List<RichTextBlockOverflow> _overflowColumns;
	}
}
