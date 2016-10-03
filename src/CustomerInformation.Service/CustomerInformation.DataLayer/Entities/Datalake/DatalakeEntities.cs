using System.Collections.Generic;
using CustomerInformation.DataLayer.Interfaces;

namespace CustomerInformation.DataLayer.Entities.Datalake
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
            get { return _connectionString; }

            set
            {
                _connectionString = value;
                _datalakeAdapter.ConnectionString = _connectionString;
            }
        }

        public IEnumerable<T> Get<T>(string tableName, bool isTransactionalDataRequire = false) where T : class, new()
        {
            //TODO: Need to implement "isTransactionDataRequire" logic in case of transactional data retrieval
            return _datalakeAdapter.Get<T>($"Select {GetColumns()} from {tableName}");
        }

        public IEnumerable<T> Where<T>(string tableName, string condition, bool isTransactionalDataRequire = false) where T : class, new()
        {
            return _datalakeAdapter.Get<T>($"Select {GetColumns()} from {tableName} WHERE {condition}");
        }

        private string GetColumns()
        {
            return
                "sl01001, sl01002, sl01003, sl01004, sl01005, sl01006, sl01007, sl01008, sl01009, sl01011, sl01013, sl01017, sl01022, sl01023, sl01026, sl01060, sl01083, sl01084, sl01099, sl01104, sl01107, sl01109, sl01152, sl01195, sl01196, sl01198, sl01194, sl01024, sl01037";
        }
    }
}
