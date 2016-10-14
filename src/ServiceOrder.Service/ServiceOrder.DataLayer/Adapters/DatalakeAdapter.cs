using ServiceOrder.DataLayer.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace ServiceOrder.DataLayer.Adapters
{
    public class DatalakeAdapter:IDatalakeAdapter
    {
        public string ConnectionString { get; set; }

        public IEnumerable<T> Get<T>(string query) where T:class, new()
        {
            DataSet dataSet = Execute(query);
            return dataSet.Tables[0].ToList<T>();
        }

        private DataSet Execute(string query)
        {
            using (var connection = new OdbcConnection(ConnectionString))
            {
                var dataAdapter = new OdbcDataAdapter(query, connection) {SelectCommand = {CommandTimeout = 300}};
                var ds = new DataSet();

                dataAdapter.Fill(ds);
                return ds;
            }
        }
    }
}
