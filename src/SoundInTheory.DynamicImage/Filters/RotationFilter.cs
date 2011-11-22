using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Rotates an image based on a specified angle. The image rotates around its centre point.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'RotateFilter']/*" />
	public class RotationFilter : ImageReplacementFilter
	{
		private Transform _rotateTransform;

		#region Properties

		/// <summary>
		/// Gets or sets the rotation angle. The angle is measured counter-clockwise from the x-axis.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the rotation angle. The angle is measured counter-clockwise from the x-axis.")]
		public int Angle
		{
			get { return (int) (this["Angle"] ?? 0); }
			set
			{
				if (value < 0 || value > 360)
					throw new ArgumentException("The angle must be between 0 and 360 degrees.", "value");
				this["Angle"] = value;
			}
		}

		#endregion

		/// <summary>
		/// Returns the dimensions of the output image.
		/// </summary>
		/// <param name="source">The source image.</param>
		/// <param name="width">The desired width of the output image.</param>
		/// <param name="height">The desired height of the output image.</param>
		/// <returns><c>true</c> if the destination image should be created; otherwise <c>false</c>.
		/// The <see cref="RotationFilter" /> will only create the destination image
		/// if the <see cref="RotationFilter.Angle" /> property is not 0.</returns>
		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			if (Angle == 0)
			{
				width = height = 0;
				return false;
			}

			// get the current rectangle points
			Point[] points = new[]
			{
				new Point(0, 0),
				new Point(source.Width, 0),
				new Point(0, source.Height),
				new Point(source.Width, source.Height)
			};

			// Transform corner points into rotated position.
			_rotateTransform = new RotateTransform(Angle, source.Width / 2.0, source.Height / 2.0);
			for (int i = 0; i < points.Length; ++i)
				points[i] = _rotateTransform.Transform(points[i]);

			// Calculate bounds of new rectangle.
			Rect rect = Rect.Empty;
			for (int i = 0; i < 4; i++)
				rect = Rect.Union(rect, points[i]);

			// Reset the points to a 0,0 base
			for (int i = 0; i < 4; i++)
			{
				points[i].X -= rect.Left;
				points[i].Y -= rect.Top;
			}

			width = (int) rect.Width;
			height = (int) rect.Height;

			return true;
		}

		/// <summary>
		/// Applies the <see cref="RotationFilter" /> to the specified <paramref name="source"/>.
		/// </summary>
		protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
			dc.PushTransform(new TransformGroup
			{
				Children = new TransformCollection
				{
					_rotateTransform,
					new TranslateTransform((width - source.Width) / 2.0, (height - source.Height) / 2.0)
				}
			});
			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, source.Width, source.Height));
			dc.Pop();
		}
	}
}
