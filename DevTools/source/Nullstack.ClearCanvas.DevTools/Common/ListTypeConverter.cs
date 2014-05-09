using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Nullstack.ClearCanvas.DevTools.Common
{
	public sealed class ListTypeConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof (IList))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof (string) && value is IList)
				return string.Format(SR.fmListDescription, ((IList) value).Count);
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value is IList)
			{
				IList list = (IList) value;
				PropertyDescriptor[] properties = new PropertyDescriptor[list.Count];
				for (int n = 0; n < list.Count; n++)
					properties[n] = new ListItemPropertyDescriptor(n, list[n]);
				return new PropertyDescriptorCollection(properties, true);
			}
			return base.GetProperties(context, value, attributes);
		}

		private class ListItemPropertyDescriptor : PropertyDescriptor, IComparable
		{
			private readonly int _index;
			private readonly object _listItem;

			public ListItemPropertyDescriptor(int index, object listItem)
				: base(string.Format(SR.fmIndex, index), null)
			{
				_index = index;
				_listItem = listItem;
			}

			public override bool CanResetValue(object component)
			{
				return false;
			}

			public override Type ComponentType
			{
				get { return null; }
			}

			public override object GetValue(object component)
			{
				return _listItem;
			}

			public override bool IsReadOnly
			{
				get { return true; }
			}

			public override Type PropertyType
			{
				get { return _listItem.GetType(); }
			}

			public override void ResetValue(object component) {}

			public override void SetValue(object component, object value) {}

			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			public override TypeConverter Converter
			{
				get { return new ExpandableObjectConverter(); }
			}

			public override string Category
			{
				get { return "Content"; }
			}

			#region IComparable Members

			public int CompareTo(object obj)
			{
				if (obj == null)
					return 1;

				if (obj is ListItemPropertyDescriptor)
					return this._index.CompareTo(((ListItemPropertyDescriptor) obj)._index);
				return this.GetHashCode().CompareTo(obj.GetHashCode());
			}

			#endregion
		}
	}
}