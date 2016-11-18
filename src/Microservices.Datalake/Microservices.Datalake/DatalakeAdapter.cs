using System.Data;
using System.Data.Odbc;

namespace Microservices.Datalake
{
    internal class DatalakeAdapter : IDatalakeAdapter
    {
        internal string ConnectionString { get; set; }

        /// <summary>
        /// Perform Execute query on database
        /// </summary>
        /// <param name="query">database query as string</param>
        /// <returns>Return DataSet as result</returns>
        public DataSet Execute(string query)
        {
            using (var connection = new OdbcConnection(ConnectionString))
            {
                var dataAdapter = new OdbcDataAdapter(query, connection);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// perform ExecuteNonQuery on database
        /// </summary>
        /// <param name="query">database query as string</param>
        /// <returns>return integer as result</returns>
        public int ExecuteNonQuery(string query)
        {
            using (var connection = new OdbcConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = query;
                return command.ExecuteNonQuery();
            }
        }
    }
}
