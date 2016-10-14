using System;
using System.Collections.Generic;
using ServiceOrder.DataLayer.Interfaces;

namespace ServiceOrder.DataLayer.Entities.Datalake
{
    public class DatalakeEntities:IDatalakeEntities
    {
        private readonly IDatalakeAdapter _datalakeAdapter;
        private string _connectionString;
        public DatalakeEntities(IDatalakeAdapter iDatalakeAdapter)
        {
            _datalakeAdapter = iDatalakeAdapter;
        }

        public string ConnectionString
        {
            set
            {
                _connectionString = value;
                _datalakeAdapter.ConnectionString = _connectionString;
            }
        }

        public IEnumerable<T> Get<T>(string tableName, bool isTransactionDataRequire = false) where T : class, new()
        {
            //TODO: Need to implement "isTransactionDataRequire" logic in case of transactional data retrieval
            return _datalakeAdapter.Get<T>($"Select {GetColumns(new T())} from {tableName}");
        }

        public IEnumerable<T> Where<T>(string tableName, string condition, bool isTransactionDataRequire = false) where T : class, new()
        {
            return _datalakeAdapter.Get<T>($"Select {GetColumns(new T())} from {tableName} WHERE {condition}");
        }

        public IEnumerable<T> GetJoinData<T>(string primaryTableName, string JoinConditions, bool isTransactionalDataRequire = false) where T : class, new()
        {
            return _datalakeAdapter.Get<T>($"Select {GetColumns(new T())} from {primaryTableName} {JoinConditions}");
        }

        public IEnumerable<T> WhereJoin<T>(string primaryTableName, string JoinConditions, string whereCondition,
            bool isTransactionalDataRequire = false) where T : class, new()
        {
            return _datalakeAdapter.Get<T>($"Select {GetColumns(new T())} from {primaryTableName} {JoinConditions} WHERE {whereCondition}");
        }

        private string GetColumns<T>(T t)
        {
            string columns = string.Empty;
            Type objecType = t.GetType();
            var properties = objecType.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                columns += properties[index].Name;
                if (index != properties.Length - 1) columns += ", ";
            }
            return columns;
        }
    }
}
