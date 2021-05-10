
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Extensions
{
    /// <summary>
	/// Applies database updates at application startup
	/// </summary>
	public  class DBUpdate
    {
        public static IConfiguration _configuration;

        public DBUpdate(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Runs an embedded script as an SQL command to the database defined in web.config
        /// </summary>
        public void RunDBUpdate()
        {
            Update("DataSP.SP_Update.sql");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public  void Update(string fileName)
        {
            string sql = EmbeddedResource.GetString(fileName);
            var conString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production" ? _configuration.GetConnectionString("MemberShipDBPro") : _configuration.GetConnectionString("MemberShipDB");

                using (
                SqlConnection connect = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(sql, connect);

                connect.Open();

                command.ExecuteNonQuery();

                connect.Close();
            }
        }
    }
}
