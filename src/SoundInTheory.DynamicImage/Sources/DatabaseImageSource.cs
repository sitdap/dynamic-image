using System;
using SoundInTheory.DynamicImage.Caching;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections.Generic;
using System.Data;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Sources
{
	public class DatabaseImageSource : ImageSource
	{
		#region Properties

		[Category("Data"), DefaultValue("")]
		public string ConnectionStringName
		{
			get
			{
				object value = this.PropertyStore["ConnectionStringName"];
				if (value != null)
					return (string) value;
				return string.Empty;
			}
			set
			{
				this.PropertyStore["ConnectionStringName"] = value;
			}
		}

		[Category("Data"), DefaultValue("")]
		public string TableName
		{
			get
			{
				object value = this.PropertyStore["TableName"];
				if (value != null)
					return (string) value;
				return string.Empty;
			}
			set
			{
				this.PropertyStore["TableName"] = value;
			}
		}

		[Category("Data"), DefaultValue("")]
		public string ColumnName
		{
			get
			{
				object value = this.PropertyStore["ColumnName"];
				if (value != null)
					return (string) value;
				return string.Empty;
			}
			set
			{
				this.PropertyStore["ColumnName"] = value;
			}
		}

		[Category("Data"), TypeConverter(typeof(StringArrayConverter))]
		public string[] PrimaryKeyNames
		{
			get
			{
				object value = this.PropertyStore["PrimaryKeyNames"];
				if (value != null)
					return (string[]) value;
				return null;
			}
			set
			{
				this.PropertyStore["PrimaryKeyNames"] = value;
			}
		}

		[Category("Data"), DefaultValue("")]
		public string PrimaryKeyName
		{
			get { return (this.PrimaryKeyNames != null && this.PrimaryKeyNames.Length > 0) ? this.PrimaryKeyNames[0] : string.Empty; }
			set { this.PrimaryKeyNames = new string[] { value }; }
		}

		[Category("Data"), TypeConverter(typeof(StringArrayConverter))]
		public string[] PrimaryKeyValues
		{
			get
			{
				object value = this.PropertyStore["PrimaryKeyValues"];
				if (value != null)
					return (string[]) value;
				return null;
			}
			set
			{
				this.PropertyStore["PrimaryKeyValues"] = value;
			}
		}

		[Category("Data"), DefaultValue("")]
		public string PrimaryKeyValue
		{
			get { return (this.PrimaryKeyValues != null && this.PrimaryKeyValues.Length > 0) ? this.PrimaryKeyValues[0] : string.Empty; }
			set { this.PrimaryKeyValues = new string[] { value }; }
		}

		#endregion

		public override FastBitmap GetBitmap()
		{
			SqlConnection conn = null; SqlDataAdapter adapter = null; DataTable dt = null;
			try
			{
				string connectionString = ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString;
				conn = new SqlConnection(connectionString);
				conn.Open();

				string whereFilter = string.Empty;
				for (int i = 0, length = this.PrimaryKeyNames.Length; i < length; ++i)
				{
					string primaryKeyName = this.PrimaryKeyNames[i];
					string primaryKeyValue = this.PrimaryKeyValues[i];
					whereFilter += string.Format("{0} = {1}", primaryKeyName, primaryKeyValue);
					if (i < length - 1)
						whereFilter += " AND ";
				}

				string selectSql = string.Format("SELECT {0} FROM {1} WHERE {2}",
					this.ColumnName, this.TableName, whereFilter);

				adapter = new SqlDataAdapter(selectSql, conn);

				dt = new DataTable();
				adapter.Fill(dt);

				if (dt.Rows.Count != 1)
					throw new InvalidOperationException("Could not retrieve value from database");

				object value = dt.Rows[0][0];

				if (value == null || value is DBNull)
					return null; // this is fine, just means there's no value in the database

				if (!(value is byte[]))
					throw new InvalidOperationException(string.Format("Expected object of type '{0}' but found object of type '{1}'", typeof(byte[]).FullName, value.GetType().FullName));

				return new FastBitmap((byte[]) value);
			}
			finally
			{
				if (dt != null)
					dt.Dispose();
				if (adapter != null)
					adapter.Dispose();
				if (conn != null)
				{
					conn.Close();
					conn.Dispose();
				}
			}
		}

		public override void PopulateDependencies(List<Dependency> dependencies)
		{
			Dependency dependency = new Dependency();
			dependency.Text1 = ConnectionStringName;
			dependency.Text2 = TableName;
			dependency.Text3 = (PrimaryKeyValues != null) ? string.Join(",", PrimaryKeyValues) : string.Empty;
			dependencies.Add(dependency);
		}
	}
}
