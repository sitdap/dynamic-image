﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Windows.Media;
using System.Windows.Media.Effects;
using SoundInTheory.DynamicImage.ShaderEffects;
using SoundInTheory.DynamicImage.Sources;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	public class CurvesAdjustmentFilter : ShaderEffectFilter
	{
		private CurveCollection _curves;

		#region Properties

		/// <summary>
		/// Specifies which ACV file (exported from Photoshop) contains the curves to be used. May not be used in combination with the Curves collection.
		/// </summary>
		[DefaultValue(""), Description("Specifies which ACV file (exported from Photoshop) contains the curves to be used. May not be used in combination with the Curves collection.")]
		public string PhotoshopCurvesFileName
		{
			get { return (string)(ViewState["PhotoshopCurvesFileName"] ?? string.Empty); }
			set { ViewState["PhotoshopCurvesFileName"] = value; }
		}

		/// <summary>
		/// Specifies curves declaratively. May not be used in combination with PhotoshopCurvesFileName.
		/// </summary>
		[Browsable(true), PersistenceMode(PersistenceMode.InnerProperty), NotifyParentProperty(true)]
		public CurveCollection Curves
		{
			get
			{
				if (_curves == null)
				{
					_curves = new CurveCollection();
					if (((IStateManager)this).IsTrackingViewState)
						((IStateManager)_curves).TrackViewState();
				}
				return _curves;
			}
			set
			{
				if (_curves != null)
					throw new Exception("You can only set a new curves collection if one does not already exist");

				_curves = value;
				if (((IStateManager)this).IsTrackingViewState)
					((IStateManager)_curves).TrackViewState();
			}
		}

		#endregion

		protected override Effect GetEffect(FastBitmap source)
		{
			// Get curves either from Curves collection or ACV file.
			CurveCollection curves = GetCurves();

			// Check that there are at least 4 curves.
			if (curves.Count < 4)
				throw new DynamicImageException(
					"At least 4 curves (corresponding to Composite, Red, Green, Blue) must be specified.");

			// Convert mathematical curve definitions into 4x256 lookup texture (Composite, Red, Green, Blue are the 4 "columns").
			FastBitmap curvesLookup = new FastBitmap(4, 256);
			curvesLookup.Lock();
			for (int x = 0; x < 4; ++x)
			{
				IEnumerable<CurvePoint> points = curves[x].Points.Cast<CurvePoint>();
				float[] xValues = points.Select(p => (float) p.Input).ToArray();
				float[] yValues = points.Select(p => (float) p.Output).ToArray();
				float[] derivatives = CubicSplineUtility.CalculateSpline(xValues, yValues);
				for (int y = 0; y < 256; ++y)
					curvesLookup[x, y] = Color.FromRgb((byte) CubicSplineUtility.InterpolateSpline(xValues, yValues, derivatives, y), 0, 0);
			}
			curvesLookup.Unlock();

			return new CurvesEffect
			{
				CurvesLookup = new ImageBrush(curvesLookup.InnerBitmap)
			};
		}

		private CurveCollection GetCurves()
		{
			if (!string.IsNullOrEmpty(PhotoshopCurvesFileName))
				return PhotoshopCurvesReader.ReadPhotoshopCurvesFile(FileSourceHelper.ResolveFileName(PhotoshopCurvesFileName));

			return Curves;
		}

		public override string ToString()
		{
			return "Curves";
		}
	}
}