namespace SoundInTheory.DynamicImage
{
	public class Padding : DirtyTrackingObject
	{
		#region Properties

		public int Top
		{
			get
			{
				object value = this.PropertyStore["Top"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.PropertyStore["Top"] = value;
			}
		}

		public int Left
		{
			get
			{
				object value = this.PropertyStore["Left"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.PropertyStore["Left"] = value;
			}
		}

		public int Bottom
		{
			get
			{
				object value = this.PropertyStore["Bottom"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.PropertyStore["Bottom"] = value;
			}
		}

		public int Right
		{
			get
			{
				object value = this.PropertyStore["Right"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.PropertyStore["Right"] = value;
			}
		}

		#endregion
	}
}
