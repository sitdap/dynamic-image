using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.ShaderEffects.LayerBlending;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// This class controls how layers and blended together. It uses a shader effect to do the actual blending.
	/// </summary>
	/// <see cref="http://delivery.acm.org/10.1145/810000/808606/p253-porter.pdf?key1=808606&key2=4919031521&coll=GUIDE&dl=GUIDE&CFID=50119817&CFTOKEN=68259268"/>
	internal static class LayerBlender
	{
		public static FastBitmap BlendLayers(FastBitmap output, IEnumerable<Layer> layers)
		{
			DrawingVisual dv = new DrawingVisual();
			RenderTargetBitmap rtb = RenderTargetBitmapUtility.CreateRenderTargetBitmap(output.Width, output.Height);

			// Draw background.
			var dc2 = dv.RenderOpen();
			dc2.DrawImage(output.InnerBitmap, new System.Windows.Rect(0, 0, output.Width, output.Height));
			dc2.Close();
			rtb.Render(dv);

		    var layersList = layers.ToList();

            // As an optimisation, if BlendMode=Normal for all layers, don't use LayerBlenderEffect.
            // Just draw images directly using DrawImage.
            if (layersList.All(x => x.BlendMode == BlendMode.Normal))
		    {
		        foreach (var layer in layersList)
		            DrawImage(dv, layer, rtb);
		    }
		    else
		    {
                ImageSource imageSource = output.InnerBitmap;

                foreach (var layer in layersList)
		        {
		            using (var effect = new LayerBlenderEffect(layer.BlendMode, output.Width, output.Height)
		            {
		                Background = new ImageBrush(imageSource)
		                {
		                    TileMode = TileMode.None,
		                    Stretch = Stretch.None,
		                    ViewportUnits = BrushMappingMode.RelativeToBoundingBox,
		                    Viewport = new System.Windows.Rect(0, 0, 1, 1)
		                }
		            })
		            {
		                dv.Effect = effect;
                        DrawImage(dv, layer, rtb);
		            }

		            imageSource = rtb;
		        }
		    }

		    return new FastBitmap(rtb);
		}

	    private static void DrawImage(DrawingVisual dv, Layer layer, RenderTargetBitmap rtb)
	    {
	        var dc = dv.RenderOpen();
	        dc.PushTransform(new TranslateTransform(layer.X + layer.Padding.Left, layer.Y + layer.Padding.Top));
	        dc.DrawImage(layer.Bitmap.InnerBitmap, new System.Windows.Rect(0, 0, layer.Bitmap.Width, layer.Bitmap.Height));
	        dc.Pop();
	        dc.Close();

	        rtb.Render(dv);
	    }
	}
}