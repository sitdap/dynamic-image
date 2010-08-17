using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Web;

namespace SoundInTheory.DynamicImage.Caching.Sql
{
	public class SqliteCacheProvider : SqlCacheProviderBase
	{
		private string _connectionString;

		protected override string ConnectionString
		{
			get { return _connectionString; }
		}

		public override void Initialize(string name, NameValueCollection config)
		{
			try
			{
				// Initialize connection string.
				string path = config["path"];
				if (string.IsNullOrEmpty(path))
					path = "~/App_Data/DynamicImage/DynamicImageCache.db";
				string absolutePath = HttpContext.Current.Server.MapPath(path);
				if (!Directory.Exists(Path.GetDirectoryName(absolutePath)))
					Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
				_connectionString = string.Format("Data Source={0};Version=3;Compress=True;", absolutePath);
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException("Could not initialize connection string.", ex);
			}

			base.Initialize(name, config);
		}

		protected override DbProviderFactory GetDbProviderFactory()
		{
			return new SQLiteFactory();
		}
	}
}