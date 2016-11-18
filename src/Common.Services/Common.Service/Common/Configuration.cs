using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using NLog;
using Common.Service.Logger;

namespace Common.Service
{
   public class Configuration
    {
        readonly SqlConnection _connection;
        public Configuration(string connectionString)
        {
            _connection = new SqlConnection(
                connectionString);

        }
        public Dictionary<string, string> GetConfiguration(string serviceName, string enviroment)
        {

            Dictionary<string, string> env = new Dictionary<string, string>();
            SqlDataReader rdr = null;
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand("SP_Configuration", _connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(
                    new SqlParameter("@serviceName", serviceName));

                cmd.Parameters.Add(
                    new SqlParameter("@enviroment", enviroment));

                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    env.Add(rdr.GetString(0), rdr.GetString(1));
                }
                return env;
            }
            catch (Exception ex)
            {
                //Exception innerException = null;
                //while (ex.InnerException != null)
                //{
                //    innerException = ex.InnerException;
                //}
                ApplicationLogger.Errorlog(ex.Message, Category.Database, ex.StackTrace,ex.InnerException);
                throw;
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // 5. Close the connection
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
        }
    }
}
