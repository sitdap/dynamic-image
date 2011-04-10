using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SoundInTheory.DynamicImage.Caching.Xml
{
	public class XmlCacheProvider : DiskCacheProviderBase, IDisposable
	{
		private readonly string _docPath;
		private readonly FileSystemWatcher _watcher;
		private XDocument _doc;

		public XmlCacheProvider()
		{
			_docPath = HttpContext.Current.Server.MapPath("~/App_Data/DynamicImage/DynamicImageCache.xml");

			EnsureDocument();

			_watcher = new FileSystemWatcher(Path.GetDirectoryName(_docPath), "DynamicImageCache.xml");
			_watcher.Deleted += (sender, e) => _doc = null;
		}

		void IDisposable.Dispose()
		{
			if (_watcher != null)
				_watcher.Dispose();
		}

		private void EnsureDocument()
		{
			lock (this)
			{
				if (!File.Exists(_docPath))
				{
					string xml = new XElement("cache").ToString();
					using (var stream = File.CreateText(_docPath))
						stream.Write(xml);
				}

				if (_doc == null)
					_doc = XDocument.Load(_docPath);
			}
		}

		public override bool ExistsInCache(string cacheKey)
		{
			EnsureDocument();

			lock (_doc)
				return _doc.Root.Elements().Any(e => e.Attribute("uniqueKey").Value == cacheKey);
		}

		public override void AddToCache(string cacheKey, CompositionImage compositionImage, Dependency[] dependencies)
		{
			EnsureDocument();

			// Save image to disk.
			SaveImageToDiskCache(compositionImage);

			lock (_doc)
			{
				var itemElement = new XElement("item",
					new XAttribute("id", compositionImage.Properties.CacheProviderKey),
					new XAttribute("uniqueKey", cacheKey),
					new XAttribute("isImagePresent", compositionImage.Properties.IsImagePresent),
					new XAttribute("format", compositionImage.Properties.Format),
					new XAttribute("colourDepth", compositionImage.Properties.ColourDepth));
				if (compositionImage.Properties.Width != null)
					itemElement.Add(new XAttribute("width", compositionImage.Properties.Width.Value));
				if (compositionImage.Properties.Height != null)
					itemElement.Add(new XAttribute("height", compositionImage.Properties.Height.Value));
				if (compositionImage.Properties.JpegCompressionLevel != null)
					itemElement.Add(new XAttribute("jpegCompressionLevel", compositionImage.Properties.JpegCompressionLevel.Value));

				XElement dependenciesElement = new XElement("dependencies");
				itemElement.Add(dependenciesElement);

				foreach (var dependency in dependencies)
					dependenciesElement.Add(new XElement("dependency",
						new XAttribute("text1", dependency.Text1),
						new XAttribute("text2", dependency.Text2),
						new XAttribute("text3", dependency.Text3),
						new XAttribute("text4", dependency.Text4)));

				_doc.Root.Add(itemElement);
				_doc.Save(_docPath);
			}
		}

		public override ImageProperties GetPropertiesFromCache(string cacheKey)
		{
			EnsureDocument();

			lock (_doc)
			{
				ImageProperties result = null;

				XElement itemElement = _doc.Root.Elements().SingleOrDefault(e => e.Attribute("uniqueKey").Value == cacheKey);
				if (itemElement != null)
					result = GetImageProperties(itemElement);

				return result;
			}
		}

		private static ImageProperties GetImageProperties(XElement itemElement)
		{
			var result = new ImageProperties();
			result.UniqueKey = itemElement.Attribute("uniqueKey").Value;
			result.IsImagePresent = Convert.ToBoolean(itemElement.Attribute("isImagePresent").Value);
			result.Format = (DynamicImageFormat)Enum.Parse(typeof(DynamicImageFormat), itemElement.Attribute("format").Value);
			if (itemElement.Attribute("width") != null)
				result.Width = Convert.ToInt32(itemElement.Attribute("width").Value);
			if (itemElement.Attribute("height") != null)
				result.Height = Convert.ToInt32(itemElement.Attribute("height").Value);
			result.ColourDepth = Convert.ToInt32(itemElement.Attribute("colourDepth").Value);
			if (itemElement.Attribute("jpegCompressionLevel") != null)
				result.JpegCompressionLevel = Convert.ToInt32(itemElement.Attribute("jpegCompressionLevel").Value);
			result.CacheProviderKey = itemElement.Attribute("id").Value;
			return result;
		}

		public override void RemoveAllFromCache()
		{
			EnsureDocument();

			lock (_doc)
			{
				foreach (XElement itemElement in _doc.Root.Elements())
				{
					DeleteImageFromDiskCache(GetImageProperties(itemElement), HttpContext.Current);
					itemElement.Remove();
				}
				_doc.Save(_docPath);
			}
		}

		public override void RemoveFromCache(Dependency dependency)
		{
			EnsureDocument();

			lock (_doc)
			{
				Func<XElement, bool> attributeMatcher = d => d.Attribute("text1").Value == dependency.Text1
					&& d.Attribute("text2").Value == dependency.Text2
						&& d.Attribute("text3").Value == dependency.Text3
							&& d.Attribute("text4").Value == dependency.Text4;

				var items = _doc.Root.Elements().Where(e => e.Element("dependencies").Elements().Any(attributeMatcher)).ToList();
				foreach (XElement itemElement in items)
				{
					DeleteImageFromDiskCache(GetImageProperties(itemElement), HttpContext.Current);
					itemElement.Remove();
				}

				_doc.Save(_docPath);
			}
		}
	}
}