using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.ShaderEffects;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Adjusts the brightness of the layer.
	/// </summary>
	/// <include file="../documentation.xml" path="docs/types/type[@name = 'FeatherFilter']/*" />
	public class FeatherFilter : ShaderEffectFilter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the feather radius. Values range from 0 to 100.
		/// </summary>
		[DefaultValue(5), Description("Gets or sets the feather radius. Values range from 0 to 100.")]
		public int Radius
		{
			get { return (int) (PropertyStore["Radius"] ?? 5); }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("value", "Feather radius values must range from 0 to 100.");
				PropertyStore["Radius"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the shape to use for feathering.
		/// </summary>
		[DefaultValue(FeatherShape.Rectangle), Description("Gets or sets the shape to use for feathering.")]
		public FeatherShape Shape
		{
			get { return (FeatherShape)(PropertyStore["Shape"] ?? FeatherShape.Rectangle); }
			set { PropertyStore["Shape"] = value; }
		}

		#endregion

		#region Methods

		protected override bool GetDestinationDimensions(FastBitmap source, out int width, out int height)
		{
			width = source.Width;
			height = source.Height;
			return true;
		}

		protected override Effect GetEffect(FastBitmap source)
		{
			// Fill temporary graphics buffer with mask (currently always a rectangle).
			DrawingVisual dv = new DrawingVisual
			{
				Effect = new BlurEffect
				{
					Radius = Radius,
					KernelType = KernelType.Gaussian
				}
			};
			DrawingContext dc = dv.RenderOpen();
			dc.DrawRectangle(new SolidColorBrush(Colors.Transparent), null, new Rect(0, 0, source.Width, source.Height));

			switch (Shape)
			{
				case FeatherShape.Rectangle :
					dc.DrawRectangle(new SolidColorBrush(Colors.White), null, new Rect(Radius, Radius, source.Width - Radius * 2, source.Height - Radius * 2));
					break;
				case FeatherShape.Oval :
					dc.DrawEllipse(new SolidColorBrush(Colors.White), null, new Point(source.Width / 2.0, source.Height / 2.0), source.Width / 2 - Radius * 2, source.Height / 2 - Radius * 2);
					break;
				default :
					throw new NotSupportedException();
			}
			dc.Close();

			RenderTargetBitmap rtb = RenderTargetBitmapUtility.CreateRenderTargetBitmap(source.Width, source.Height);
			rtb.Render(dv);

			Brush alphaMask = new ImageBrush(rtb);

			return new FeatherEffect
			{
				AlphaMask = alphaMask
			};
		}

		/*protected override void ApplyFilter(FastBitmap source, DrawingContext dc, int width, int height)
		{
			Brush opacityBrushX = GetOpacityBrush(new Point(0, 0), new Point(1, 0), width);
			Brush opacityBrushY = GetOpacityBrush(new Point(0, 0), new Point(0, 1), height);

			dc.PushOpacityMask(opacityBrushX);
			dc.PushOpacityMask(opacityBrushY);
			dc.DrawImage(source.InnerBitmap, new Rect(0, 0, width, height));
			dc.Pop();
			dc.Pop();

			dc.PushClip(new RectangleGeometry(new Rect(0, 0, Radius, height)));
			dc.DrawRectangle(new SolidColorBrush(Colors.Transparent), null, new Rect(0, 0, width, height));
			dc.Pop();
			dc.Pop();
		}

		private Brush GetOpacityBrush(Point startPoint, Point endPoint, float size)
		{
			float distance = Radius / size;

			LinearGradientBrush brush = new LinearGradientBrush
			{
				StartPoint = startPoint,
				EndPoint = endPoint
			};
			brush.GradientStops.Add(new GradientStop(Colors.Transparent, 0));
			brush.GradientStops.Add(new GradientStop(Colors.White, distance));
			brush.GradientStops.Add(new GradientStop(Colors.White, 1 - distance));
			brush.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

			return brush;
		}*/

		/*
		protected override void OnBeginApplyFilter(FastBitmap bitmap)
		{
			// Fill temporary graphics buffer with mask (currently always a rectangle).
			FastBitmap alphaMaskBitmap = new FastBitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
			using (Graphics alphaMaskGraphics = Graphics.FromImage(alphaMaskBitmap.InnerBitmap))
			{
				// Draw mask.
				alphaMaskGraphics.Clear(Color.Transparent);
				using (SolidBrush whiteBrush = new SolidBrush(Color.White))
				{
					alphaMaskGraphics.FillRectangle(whiteBrush, this.Radius, this.Radius,
						bitmap.Width - (2 * this.Radius),
						bitmap.Height - (2 * this.Radius));
				}
			}

			// Blur mask.
			_blurredAlphaMaskBitmap = new FastBitmap(alphaMaskBitmap.Width, alphaMaskBitmap.Height, PixelFormat.Format32bppArgb);
			GaussianBlurFilterHelper.ApplyFilter(alphaMaskBitmap, _blurredAlphaMaskBitmap, this.Radius, EdgeMode.BorderColor, Color.Transparent);

			_blurredAlphaMaskBitmap.Lock();
		}

		protected override Color GetOutputColour(Color inputColour, int x, int y)
		{
			int alpha = Math.Min(_blurredAlphaMaskBitmap[x, y].A, inputColour.A);
			//return _blurredAlphaMaskBitmap[x, y];
			return Color.FromArgb(alpha, inputColour);
		}

		protected override void OnEndApplyFilter()
		{
			_blurredAlphaMaskBitmap.Unlock();
		}*/

		public override string ToString()
		{
			return "Feather";
		}

		#endregion
	}
}
