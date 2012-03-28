using System;
using System.ComponentModel;
using System.Globalization;

namespace SoundInTheory.DynamicImage
{
	public struct Unit
	{
		#region Fields

		public static readonly Unit Empty = new Unit();

		private readonly float _value;
		private readonly UnitType _type;

		#endregion

		#region Properties

		public bool IsEmpty
		{
			get { return (_type == (UnitType) 0); }
		}

		public UnitType Type
		{
			get
			{
				if (!this.IsEmpty)
					return _type;
				return UnitType.Pixel;
			}
		}

		public float Value
		{
			get { return _value; }
		}

		#endregion

		#region Constructors

		public Unit(int value)
		{
			if (value < -32768 || value > 0x7fff)
				throw new ArgumentOutOfRangeException("value");
			_value = value;
			_type = UnitType.Pixel;
		}

		public Unit(string value, CultureInfo culture)
			: this(value, culture, UnitType.Pixel)
		{
		}

		internal Unit(string value, CultureInfo culture, UnitType defaultType)
		{
			if (string.IsNullOrEmpty(value))
			{
				_value = 0.0f;
				_type = (UnitType) 0;
			}
			else
			{
				if (culture == null)
					culture = CultureInfo.CurrentCulture;

				string str = value.Trim().ToLower(CultureInfo.InvariantCulture);
				int length = str.Length;
				int num2 = -1;
				for (int i = 0; i < length; i++)
				{
					char ch = str[i];
					if (((ch < '0') || (ch > '9')) && (((ch != '-') && (ch != '.')) && (ch != ',')))
						break;
					num2 = i;
				}
				if (num2 == -1)
					throw new FormatException(string.Format("'{0}' cannot be parsed as a unit as there are no numeric values in it. Examples of valid unit strings are '30px' and '50%'.", value));

				if (num2 < (length - 1))
					_type = GetTypeFromString(str.Substring(num2 + 1).Trim());
				else
					_type = defaultType;

				string text = str.Substring(0, num2 + 1);
				try
				{
					TypeConverter converter = new SingleConverter();
					_value = (float) converter.ConvertFromString(null, culture, text);
					if (_type == UnitType.Pixel)
						_value = (int) _value;
				}
				catch
				{
					throw new FormatException(string.Format("The numeric part ('{1}') of '{0}' cannot be parsed as a numeric part of a {2} unit.", value, text, _type.ToString("G")));
				}

				if ((_value < -32768.0) || (_value > 32767.0))
					throw new ArgumentOutOfRangeException("value");
			}
		}

		#endregion

		#region Methods

		private static string GetStringFromType(UnitType type)
		{
			switch (type)
			{
				case UnitType.Pixel:
					return "px";

				case UnitType.Percentage:
					return "%";
			}
			return string.Empty;
		}

		private static UnitType GetTypeFromString(string value)
		{
			if (string.IsNullOrEmpty(value))
				return UnitType.Pixel;

			if (value.Equals("px"))
				return UnitType.Pixel;

			if (value.Equals("%"))
				return UnitType.Percentage;

			throw new ArgumentOutOfRangeException("value");
		}

		public static Unit Parse(string s, CultureInfo culture)
		{
			return new Unit(s, culture);
		}

		public static Unit Pixel(int n)
		{
			return new Unit(n);
		}

		public static Unit Percentage(int n)
		{
			return new Unit(n.ToString(), null, UnitType.Percentage);
		}

		public override string ToString()
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		public string ToString(CultureInfo culture)
		{
			return ToString((IFormatProvider) culture);
		}

		public string ToString(IFormatProvider formatProvider)
		{
			string str;
			if (this.IsEmpty)
				return string.Empty;
			if (_type == UnitType.Pixel)
				str = ((int) _value).ToString(formatProvider);
			else
				str = ((float) _value).ToString(formatProvider);
			return (str + GetStringFromType(_type));
		}

		public static int GetCalculatedValue(Unit dimension, int sourceDimension)
		{
			switch (dimension.Type)
			{
				case UnitType.Pixel:
					return (int)dimension.Value;
				case UnitType.Percentage:
					return (int)((dimension.Value / 100.0) * sourceDimension);
				default:
					throw new NotSupportedException();
			}
		}

		public bool Equals(Unit other)
		{
			return other == this;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (obj.GetType() != typeof(Unit)) return false;
			return Equals((Unit)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_value.GetHashCode() * 397) ^ _type.GetHashCode();
			}
		}

		#endregion

		#region Operators

		public static bool operator==(Unit left, Unit right)
		{
			return left._type == right._type && left._value == right._value;
		}

		public static bool operator !=(Unit left, Unit right)
		{
			return !(left == right);
		}

		#endregion
	}
}
