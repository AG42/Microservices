using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceOrder.DataLayer.Interfaces;

namespace ServiceOrder.DataLayer.Adapters
{
    public class SqlAdapter:ISqlAdapter
    {
        public string ConnectionString { get; set; }

        public IEnumerable<T> Get<T>(string query) where T:class, new()
        {
            var dataSet = Execute(query);
            return dataSet.Tables[0].ToList<T>();
        }

        public int Insert(string query)
        {
            return ExecuteNonQuery(query);
        }

        public int Update(string query)
        {
            return ExecuteNonQuery(query);
        }
        public int Delete(string query)
        {
            return ExecuteNonQuery(query);
        }
        private DataSet Execute(string query)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = query;

                var dataAdapter = new SqlDataAdapter(command);
                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }
        private int ExecuteNonQuery(string query)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = query;
                return command.ExecuteNonQuery();
            }
        }
    }
}
