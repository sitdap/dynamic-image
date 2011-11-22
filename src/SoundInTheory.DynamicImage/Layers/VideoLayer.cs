using System;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Sources;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Layers
{
	public class VideoLayer : Layer
	{
		public string SourceFileName
		{
			get { return this["SourceFileName"] as string ?? string.Empty; }
			set { this["SourceFileName"] = value; }
		}

		public TimeSpan SnapshotTime
		{
			get { return (TimeSpan)(this["SnapshotTime"] ?? TimeSpan.Zero); }
			set { this["SnapshotTime"] = value; }
		}

		public override bool HasFixedSize
		{
			get { return true; }
		}

		protected override void CreateImage()
		{
			string filename = FileSourceHelper.ResolveFileName(SourceFileName);

			MediaPlayer mediaPlayer = new MediaPlayer
			{
				ScrubbingEnabled = true
			};

			object monitorObject = new object();
			mediaPlayer.MediaOpened += (sender, e) => Monitor.Exit(monitorObject);

			Monitor.Enter(monitorObject);
			mediaPlayer.Open(new Uri(filename));
			Monitor.Wait(monitorObject, 1000);

			int width = mediaPlayer.NaturalVideoWidth;
			int height = mediaPlayer.NaturalVideoHeight;

			// Seek to specified time.
			mediaPlayer.BufferingEnded += (sender, e) => Monitor.Exit(monitorObject);
			Monitor.Enter(monitorObject);
			mediaPlayer.Position = SnapshotTime;
			Monitor.Wait(monitorObject, 1000);

			DrawingVisual dv = new DrawingVisual();
			DrawingContext dc = dv.RenderOpen();
			dc.DrawVideo(mediaPlayer, new System.Windows.Rect(0, 0, width, height));
			dc.Close();

			RenderTargetBitmap rtb = RenderTargetBitmapUtility.CreateRenderTargetBitmap(width, height);
			rtb.Render(dv);
			Bitmap = new FastBitmap(rtb);

			mediaPlayer.Close();
		}
	}
}
