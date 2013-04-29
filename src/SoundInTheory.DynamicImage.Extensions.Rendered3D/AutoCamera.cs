using System.ComponentModel;
using Meshellator;
using Nexus;
using Nexus.Graphics;

namespace SoundInTheory.DynamicImage
{
	public class AutoCamera : Camera
	{
		[Browsable(true), DefaultValue(0)]
		public int Yaw
		{
			get { return (int)(this["Yaw"] ?? 0); }
			set { this["Yaw"] = value; }
		}

		[Browsable(true), DefaultValue(0)]
		public int Pitch
		{
			get { return (int)(this["Pitch"] ?? 0); }
			set { this["Pitch"] = value; }
		}

		[Browsable(true), DefaultValue(1)]
		public float Zoom
		{
			get { return (float)(this["Zoom"] ?? 1.0f); }
			set { this["Zoom"] = value; }
		}

		public override Nexus.Graphics.Cameras.Camera GetNexusCamera(Scene scene, Viewport viewport)
		{
			return Nexus.Graphics.Cameras.PerspectiveCamera.CreateFromBounds(
				scene.Bounds, viewport, MathUtility.PI_OVER_4, MathUtility.ToRadians(Yaw),
				MathUtility.ToRadians(-Pitch), Zoom);
		}
	}
}