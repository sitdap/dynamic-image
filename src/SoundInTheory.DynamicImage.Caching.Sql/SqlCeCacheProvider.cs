using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.IO;
using System.Web;

namespace SoundInTheory.DynamicImage.Caching.Sql
{
	public class SqlCeCacheProvider : SqlCacheProviderBase
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
					path = "~/App_Data/DynamicImage/DynamicImageCache.sdf";
				string absolutePath = HttpContext.Current.Server.MapPath(path);
				if (!Directory.Exists(Path.GetDirectoryName(absolutePath)))
					Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
				_connectionString = string.Format("Data Source={0}", absolutePath);
				if (!File.Exists(absolutePath))
				{
					using (SqlCeEngine en = new SqlCeEngine(_connectionString))
						en.CreateDatabase();

					UseConnection(conn =>
					{
						using (DbCommand comm = conn.CreateCommand())
						{
							// Create the Version table if it doesn't already exist.
							comm.CommandText = "CREATE TABLE Version (VersionNumber INT)";
							comm.ExecuteNonQuery();
						}
					});
				}
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException("Could not initialize connection string.", ex);
			}

			base.Initialize(name, config);
		}

		protected override DbProviderFactory GetDbProviderFactory()
		{
			return new SqlCeProviderFactory();
		}
	}
}