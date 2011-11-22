namespace SoundInTheory.DynamicImage
{
	public class Padding : DirtyTrackingObject
	{
		#region Properties

		public int Top
		{
			get
			{
				object value = this.ViewState["Top"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.ViewState["Top"] = value;
			}
		}

		public int Left
		{
			get
			{
				object value = this.ViewState["Left"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.ViewState["Left"] = value;
			}
		}

		public int Bottom
		{
			get
			{
				object value = this.ViewState["Bottom"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.ViewState["Bottom"] = value;
			}
		}

		public int Right
		{
			get
			{
				object value = this.ViewState["Right"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.ViewState["Right"] = value;
			}
		}

		#endregion
	}
}
