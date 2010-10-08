using System.IO;
using System.Text;
using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Util
{
	public static class PhotoshopCurvesReader
	{
		public static CurveCollection ReadPhotoshopCurvesFile(string filePath)
		{
			CurveCollection curves = new CurveCollection();

			using (Stream fileStream = File.OpenRead(filePath))
			{
				using (BinaryReader reader = new BigEndianBinaryReader(fileStream))
				{
					// Read version.
					ushort version = reader.ReadUInt16();
					//reader.ReadUInt16();

					// Read number of curves in file.
					ushort numCurves = reader.ReadUInt16();

					// Read all curves.
					for (int i = 0; i < numCurves; ++i)
					{
						Curve curve = new Curve();
						curves.Add(curve);

						// Read number of points in the curve.
						ushort numPoints = reader.ReadUInt16();

						// Read all points.
						for (int j = 0; j < numPoints; ++j)
						{
							// First number is output.
							ushort output = reader.ReadUInt16();

							// Second number is input.
							ushort input = reader.ReadUInt16();

							curve.Points.Add(new CurvePoint
							{
								Input = input,
								Output = output
							});
						}
					}
				}
			}

			return curves;
		}

		private class BigEndianBinaryReader : BinaryReader
		{
			public BigEndianBinaryReader(Stream input)
				: base(input)
			{
			}

			public BigEndianBinaryReader(Stream input, Encoding encoding)
				: base(input, encoding)
			{
			}

			public override ushort ReadUInt16()
			{
				byte[] bytes = ReadBytes(2);
				return (ushort)(bytes[0] << 8 | bytes[1]);
			}
		}
	}
}