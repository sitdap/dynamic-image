using System;
using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage.Util
{
	/// <summary>
	/// Reproduces the missing canonical spline functionality from WinForms in WPF.
	/// </summary>
	/// <see cref="http://www.charlespetzold.com/blog/2009/01/Canonical-Splines-in-WPF-and-Silverlight.html."/>
	internal static class CanonicalSplineHelper
	{
		internal static PathGeometry CreateSpline(PointCollection pts, double tension, DoubleCollection tensions,
																							bool isClosed, bool isFilled, double tolerance)
		{
			if (pts == null || pts.Count < 1)
				return null;

			PolyLineSegment polyLineSegment = new PolyLineSegment();
			PathFigure pathFigure = new PathFigure();
			pathFigure.IsClosed = isClosed;
			pathFigure.IsFilled = isFilled;
			pathFigure.StartPoint = pts[0];
			pathFigure.Segments.Add(polyLineSegment);
			PathGeometry pathGeometry = new PathGeometry();
			pathGeometry.Figures.Add(pathFigure);

			if (pts.Count < 2)
				return pathGeometry;

			else if (pts.Count == 2)
			{
				if (!isClosed)
				{
					Segment(polyLineSegment.Points, pts[0], pts[0], pts[1], pts[1], tension, tension, tolerance);
				}
				else
				{
					Segment(polyLineSegment.Points, pts[1], pts[0], pts[1], pts[0], tension, tension, tolerance);
					Segment(polyLineSegment.Points, pts[0], pts[1], pts[0], pts[1], tension, tension, tolerance);
				}
			}
			else
			{
				bool useTensionCollection = tensions != null && tensions.Count > 0;

				for (int i = 0; i < pts.Count; i++)
				{
					double T1 = useTensionCollection ? tensions[i % tensions.Count] : tension;
					double T2 = useTensionCollection ? tensions[(i + 1) % tensions.Count] : tension;

					if (i == 0)
					{
						Segment(polyLineSegment.Points, isClosed ? pts[pts.Count - 1] : pts[0],
																						pts[0], pts[1], pts[2], T1, T2, tolerance);
					}

					else if (i == pts.Count - 2)
					{
						Segment(polyLineSegment.Points, pts[i - 1], pts[i], pts[i + 1],
																						isClosed ? pts[0] : pts[i + 1], T1, T2, tolerance);
					}

					else if (i == pts.Count - 1)
					{
						if (isClosed)
						{
							Segment(polyLineSegment.Points, pts[i - 1], pts[i], pts[0], pts[1], T1, T2, tolerance);
						}
					}

					else
					{
						Segment(polyLineSegment.Points, pts[i - 1], pts[i], pts[i + 1], pts[i + 2], T1, T2, tolerance);
					}
				}
			}

			return pathGeometry;
		}

		static void Segment(PointCollection points, Point pt0, Point pt1, Point pt2, Point pt3, double T1, double T2, double tolerance)
		{
			// See Petzold, "Programming Microsoft Windows with C#", pages 645-646 or 
			//     Petzold, "Programming Microsoft Windows with Microsoft Visual Basic .NET", pages 638-639
			// for derivation of the following formulas:

			double SX1 = T1 * (pt2.X - pt0.X);
			double SY1 = T1 * (pt2.Y - pt0.Y);
			double SX2 = T2 * (pt3.X - pt1.X);
			double SY2 = T2 * (pt3.Y - pt1.Y);

			double AX = SX1 + SX2 + 2 * pt1.X - 2 * pt2.X;
			double AY = SY1 + SY2 + 2 * pt1.Y - 2 * pt2.Y;
			double BX = -2 * SX1 - SX2 - 3 * pt1.X + 3 * pt2.X;
			double BY = -2 * SY1 - SY2 - 3 * pt1.Y + 3 * pt2.Y;

			double CX = SX1;
			double CY = SY1;
			double DX = pt1.X;
			double DY = pt1.Y;

			int num = (int)((Math.Abs(pt1.X - pt2.X) + Math.Abs(pt1.Y - pt2.Y)) / tolerance);

			// Notice begins at 1 so excludes the first point (which is just pt1)
			for (int i = 1; i < num; i++)
			{
				double t = (double)i / (num - 1);
				Point pt = new Point(AX * t * t * t + BX * t * t + CX * t + DX,
														 AY * t * t * t + BY * t * t + CY * t + DY);
				points.Add(pt);
			}
		}
	}
}