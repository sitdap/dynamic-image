using Meshellator;
using Nexus.Graphics;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Represents an imaginary viewing position and direction in 3-D coordinate space 
	/// that describes how a 3-D model is projected onto a 2-D visual.
	/// </summary>
	public abstract class Camera : DirtyTrackingObject
	{
		public abstract Nexus.Graphics.Cameras.Camera GetNexusCamera(Scene scene, Viewport viewport);
	}
}