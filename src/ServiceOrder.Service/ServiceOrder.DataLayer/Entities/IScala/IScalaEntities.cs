using System;
using System.Collections.Generic;
using ServiceOrder.DataLayer.Interfaces;

namespace ServiceOrder.DataLayer.Entities.IScala
{
    public class IScalaEntities:IiScalaEntities
    {
        private readonly ISqlAdapter _sqlAdapter;
        public string ConnectionString {
            set
            {
                _sqlAdapter.ConnectionString = value;
            }
        }

        public IScalaEntities(ISqlAdapter sqlAdapter)
        {
            _sqlAdapter = sqlAdapter;
        }

        public IEnumerable<T> Get<T>(string tableName, bool isTransactionalDataRequire = false) where T : class, new()
        {
            return _sqlAdapter.Get<T>($"Select {GetColumns(new T())} from {tableName}");
        }

        public IEnumerable<T> Where<T>(string tableName, string condition, bool isTransactionalDataRequire = false) where T : class, new()
        {
            return _sqlAdapter.Get<T>($"Select {GetColumns(new T())} from {tableName} WHERE {condition}");
        }

        public IEnumerable<T> GetJoinData<T>(string primaryTableName, string JoinConditions, bool isTransactionalDataRequire = false) where T : class, new()
        {
            return _sqlAdapter.Get<T>($"Select {GetColumns(new T())} from {primaryTableName} {JoinConditions}");
        }

        public IEnumerable<T> WhereJoin<T>(string primaryTableName, string JoinConditions, string whereCondition,
            bool isTransactionalDataRequire = false) where T : class, new()
        {
            return _sqlAdapter.Get<T>($"Select {GetColumns(new T())} from {primaryTableName} {JoinConditions} WHERE {whereCondition}");
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
