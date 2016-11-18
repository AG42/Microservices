using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using CustomerSiteLocation.DataLayer.Interfaces;

namespace CustomerSiteLocation.DataLayer.Adapters
{
    public class DatalakeAdapter : IDatalakeAdapter
    {
        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }

        public IEnumerable<T> Get<T>(string query) where T:class, new()
        {
            DataSet dataSet = Execute(query);
            return dataSet.Tables[0].ToList<T>();
        }

        private DataSet Execute(string query)
        {
            using (var connection = new OdbcConnection(_connectionString))
            {
                var dataAdapter = new OdbcDataAdapter(query, connection);

                var ds = new DataSet();

                dataAdapter.Fill(ds);
                return ds;
            }
        }
    }
}
