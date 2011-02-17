using System;
using System.ComponentModel;
using System.Windows;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// A filter which performs a perspective distortion on an image.
	/// The filter maps the original image onto an arbitrary convex quadrilateral.
	/// </summary>
	public class DistortCornersFilter : TransformFilter
	{
		#region Fields

		private float _dx1, _dy1, _dx2, _dy2, _dx3, _dy3;
		private float _a, _b, _c, _d, _e, _f, _g, _h, _i;
		private float _a11, _a12, _a13, _a21, _a22, _a23, _a31, _a32, _a33;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the top-left corner X-coordinate.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the top-left corner X-coordinate.")]
		public int X1
		{
			get { return (int)(ViewState["X1"] ?? 0); }
			set { ViewState["X1"] = value; }
		}

		/// <summary>
		/// Gets or sets the top-left corner X-coordinate.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the top-left corner Y-coordinate.")]
		public int Y1
		{
			get { return (int)(ViewState["Y1"] ?? 0); }
			set { ViewState["Y1"] = value; }
		}

		/// <summary>
		/// Gets or sets the top-right corner X-coordinate.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the top-right corner X-coordinate.")]
		public int X2
		{
			get { return (int)(ViewState["X2"] ?? 0); }
			set { ViewState["X2"] = value; }
		}

		/// <summary>
		/// Gets or sets the top-right corner X-coordinate.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the top-right corner Y-coordinate.")]
		public int Y2
		{
			get { return (int)(ViewState["Y2"] ?? 0); }
			set { ViewState["Y2"] = value; }
		}

		/// <summary>
		/// Gets or sets the bottom-right corner X-coordinate.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the bottom-right corner X-coordinate.")]
		public int X3
		{
			get { return (int)(ViewState["X3"] ?? 0); }
			set { ViewState["X3"] = value; }
		}

		/// <summary>
		/// Gets or sets the bottom-right corner X-coordinate.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the bottom-right corner Y-coordinate.")]
		public int Y3
		{
			get { return (int)(ViewState["Y3"] ?? 0); }
			set { ViewState["Y3"] = value; }
		}

		/// <summary>
		/// Gets or sets the bottom-left corner X-coordinate.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the bottom-left corner X-coordinate.")]
		public int X4
		{
			get { return (int)(ViewState["X4"] ?? 0); }
			set { ViewState["X4"] = value; }
		}

		/// <summary>
		/// Gets or sets the bottom-left corner X-coordinate.
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the bottom-left corner Y-coordinate.")]
		public int Y4
		{
			get { return (int)(ViewState["Y4"] ?? 0); }
			set { ViewState["Y4"] = value; }
		}

		#endregion

		#region Methods

		protected override void OnBeginApplyFilter(FastBitmap bitmap)
		{
			MapSquareToQuad();

			_a = _a22 * _a33 - _a32 * _a23;
			_b = _a31 * _a23 - _a21 * _a33;
			_c = _a21 * _a32 - _a31 * _a22;
			_d = _a32 * _a13 - _a12 * _a33;
			_e = _a11 * _a33 - _a31 * _a13;
			_f = _a31 * _a12 - _a11 * _a32;
			_g = _a12 * _a23 - _a22 * _a13;
			_h = _a21 * _a13 - _a11 * _a23;
			_i = _a11 * _a22 - _a21 * _a12;
		}

		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			Int32Rect sourceRect = new Int32Rect(0, 0, source.Width, source.Height);
			Int32Rect destRect = GetTransformedSpace(sourceRect);
			width = destRect.Width;
			height = destRect.Height;
			return true;
		}

		protected override Int32Rect GetTransformedSpace(Int32Rect rect)
		{
			rect.X = Math.Min(Math.Min(X1, X2), Math.Min(X3, X4));
			rect.Y = Math.Min(Math.Min(Y1, Y2), Math.Min(Y3, Y4));
			rect.Width = Math.Max(Math.Max(X1, X2), Math.Max(X3, X4)) - rect.X;
			rect.Height = Math.Max(Math.Max(Y1, Y2), Math.Max(Y3, Y4)) - rect.Y;
			return rect;
		}

		protected override Point TransformInverse(int x, int y)
		{
			return new Point(OriginalSpace.Width * (_a * x + _b * y + _c) / (_g * x + _h * y + _i),
				OriginalSpace.Height * (_d * x + _e * y + _f) / (_g * x + _h * y + _i));
		}

		#region Helper methods

		private Point GetPoint2D(Point srcPt)
		{
			double x = srcPt.X;
			double y = srcPt.Y;
			double f = 1.0f / (x * _a13 + y * _a23 + _a33);
			return new Point((x * _a11 + y * _a21 + _a31) * f, (x * _a12 + y * _a22 + _a32) * f);
		}

		/// <summary>
		/// Set the transform to map the unit square onto a quadrilateral. When filtering, all coordinates will be scaled
		/// by the size of the image.
		/// </summary>
		private void MapSquareToQuad()
		{
			_dx1 = X2 - X3;
			_dy1 = Y2 - Y3;
			_dx2 = X4 - X3;
			_dy2 = Y4 - Y3;
			_dx3 = X1 - X2 + X3 - X4;
			_dy3 = Y1 - Y2 + Y3 - Y4;

			if (_dx3 == 0 && _dy3 == 0)
			{
				_a11 = X2 - X1;
				_a21 = X3 - X2;
				_a31 = X1;
				_a12 = Y2 - Y1;
				_a22 = Y3 - Y2;
				_a32 = Y1;
				_a13 = _a23 = 0;
			}
			else
			{
				_a13 = (_dx3 * _dy2 - _dx2 * _dy3) / (_dx1 * _dy2 - _dy1 * _dx2);
				_a23 = (_dx1 * _dy3 - _dy1 * _dx3) / (_dx1 * _dy2 - _dy1 * _dx2);
				_a11 = X2 - X1 + _a13 * X2;
				_a21 = X4 - X1 + _a23 * X4;
				_a31 = X1;
				_a12 = Y2 - Y1 + _a13 * Y2;
				_a22 = Y4 - Y1 + _a23 * Y4;
				_a32 = Y1;
			}
			_a33 = 1;
		}

		#endregion

		#endregion
	}
}