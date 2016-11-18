using Microservices.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;

namespace Microservices.Datalake
{
    [Export(typeof(IDatabase))]
    public class DatalakeEntities : IDatabase
    {
        private DatalakeAdapter _datalakeAdapter;
        string _connectionString;
        public string ConnectionString
        {
            get
            { return _connectionString; }
            set
            {
                _connectionString = value;
                _datalakeAdapter.ConnectionString = value;
            }
        }

        public DatalakeEntities()
        {
            _datalakeAdapter = new DatalakeAdapter();
        }

        /// <summary>
        /// Execute Get methode on database
        /// </summary>
        /// <typeparam name="T">Generic type of class</typeparam>
        /// <param name="tableName">Table name as string</param>
        /// <param name="column">Column name as string</param>
        /// <returns>Return result in IEnumbrable type.</returns>
        public IEnumerable<T> Get<T>(string tableName, string column) where T : class, new()
        {
            var dataSet = _datalakeAdapter.Execute(FormSelectQuery(tableName, column, null));
            return dataSet.Tables[0].ToList<T>();
        }

        /// <summary>
        /// Execute Get methode with join on database
        /// </summary>
        /// <typeparam name="T">Generic type of class</typeparam>
        /// <param name="primaryTableName">Primary table name in join</param>
        /// <param name="columnName">Comma separated Column names</param>
        /// <param name="JoinConditions">Join condition</param>
        /// <param name="isTransactionalDataRequire"></param>
        /// <returns>Return result in IEnumbrable type.</returns>
        public IEnumerable<T> GetJoinData<T>(string primaryTableName, string columnName, string JoinConditions, bool isTransactionalDataRequire = false) where T : class, new()
        {
            var dataSet = _datalakeAdapter.Execute(FormJoinQuery(columnName, primaryTableName, JoinConditions, null));
            return dataSet.Tables[0].ToList<T>();
        }

        /// <summary>
        /// Execute Where condition methode on database
        /// </summary>
        /// <typeparam name="T">Generic type of class</typeparam>
        /// <param name="tableName">Database table name</param>
        /// <param name="column">Comma separated Column names</param>
        /// <param name="condition">Where condition on query</param>
        /// <param name="isTransactionalDataRequire"></param>
        /// <returns>Return result in IEnumbrable type.</returns>
        public IEnumerable<T> Where<T>(string tableName, string column, string condition, bool isTransactionalDataRequire = false) where T : class, new()
        {
            var dataSet = _datalakeAdapter.Execute(FormSelectQuery(tableName, column, condition));
            return dataSet.Tables[0].ToList<T>();
        }

        /// <summary>
        /// Execute Where condition methode with join on database
        /// </summary>
        /// <typeparam name="T">Generic type of class</typeparam>
        /// <param name="primaryTableName">Primary table name in join</param>
        /// <param name="columnName">Comma separated Column names</param>
        /// <param name="JoinConditions">Join condition</param>
        /// <param name="whereCondition">Where condition on query</param>
        /// <param name="isTransactionalDataRequire"></param>
        /// <returns>Return result in IEnumbrable type.</returns>
        public IEnumerable<T> WhereJoin<T>(string primaryTableName, string columnName, string JoinConditions, string whereCondition, bool isTransactionalDataRequire = false) where T : class, new()
        {
            var dataSet = _datalakeAdapter.Execute(FormJoinQuery(columnName, primaryTableName, JoinConditions, whereCondition));
            return dataSet.Tables[0].ToList<T>();
        }

        /// <summary>
        /// This method form select query for execution
        /// </summary>
        /// <param name="tableName">Database table name</param>
        /// <param name="column">comma separated column names</param>
        /// <param name="condition">where condition</param>
        /// <returns>return generated select query as string</returns>
        internal string FormSelectQuery(string tableName, string column, string condition)
        {
            StringBuilder query = new StringBuilder();

            query.Append($"Select {column} from {tableName}");

            if (!string.IsNullOrEmpty(condition))
            {
                query.Append($" WHERE { condition}");
            }
            return query.ToString();
        }

        /// <summary>
        /// This method form select query with join for execution
        /// </summary>
        /// <param name="column">Ncomma separated column as string</param>
        /// <param name="primaryTableName">primnary table name as string</param>
        /// <param name="JoinConditions">join table name as string</param>
        /// <param name="whereCondition">where condition as string if any</param>
        /// <returns>return generated join query as string</returns>
        private string FormJoinQuery(string column, string primaryTableName, string JoinConditions, string whereCondition)
        {
            StringBuilder query = new StringBuilder();

            query.Append($"Select {column} from {primaryTableName} {JoinConditions}");

            if (!string.IsNullOrEmpty(whereCondition))
            {
                query.Append($" WHERE {whereCondition}");
            }
            return query.ToString();
        }
    }
}
