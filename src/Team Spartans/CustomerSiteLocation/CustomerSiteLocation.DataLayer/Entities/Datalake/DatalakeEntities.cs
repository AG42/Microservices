using System.Collections.Generic;
using CustomerSiteLocation.DataLayer.Interfaces;

namespace CustomerSiteLocation.DataLayer.Entities.Datalake
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

        public IEnumerable<T> Get<T>(string tableName,string companyCode, bool isTransactionDataRequire = false) where T : class, new()
        {
            //TODO: Need to implement "isTransactionDataRequire" logic in case of transactional data retrieval
            return _datalakeAdapter.Get<T>($"Select {GetColumns(companyCode)} from {tableName}");
        }

        public IEnumerable<T> Where<T>(string tableName, string condition, string companyCode, bool isTransactionDataRequire = false) where T : class, new()
        {
            return _datalakeAdapter.Get<T>($"Select {GetColumns(companyCode)} from {tableName} WHERE {condition}");
        }

        private string GetColumns(string companyCode)
        {
            if(companyCode.ToLower() != "na")
            return
                "sy80001,sy80002,sy80003,sy80004,sy80005,sy80006,sy80007,sy80050,sy80051,sy80045,sy80048,sy80010,sy80012,sy80011,sy80049,sy80054,sy80053,sy80055,sy80046";

            return "sy80001,sy80002,sy80003,sy80004,sy80005,sy80006,sy80007,sy80045,sy80010,sy80012,sy80011,sy80046";
        }
    }
}
