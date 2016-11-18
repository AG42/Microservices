using System.Collections.Generic;
using CustomerProjectOrder.DataLayer.Interfaces;

namespace CustomerProjectOrder.DataLayer.Entities.Datalake
{
    public class DatalakeEntities : IDatalakeEntities
    {
        private readonly IDatalakeAdapter _datalakeAdapter;
        private string _connectionString;

        public DatalakeEntities(IDatalakeAdapter iDatalakeAdapter)
        {
            _datalakeAdapter = iDatalakeAdapter;
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
                _datalakeAdapter.ConnectionString = _connectionString;
            }
        }

        public IEnumerable<T> Get<T>(string tableName, bool isTransactionDataRequire = false) where T : class, new()
        {
            //TODO: Need to implement "isTransactionDataRequire" logic in case of transactional data retrieval
            return _datalakeAdapter.Get<T>($"Select {GetColumns()} from {tableName}");
        }

        public IEnumerable<T> Where<T>(string tableName, string condition, bool isTransactionDataRequire = false) where T : class, new()
        {
            return _datalakeAdapter.Get<T>($"Select {GetColumns()} from {tableName} WHERE {condition}");
        }

        private string GetColumns()
        {
            return
                "pr01001,pr01009,pr01010,pr01011,pr01067,pr01069,pr01003,pr01106";
        }
    }
}
