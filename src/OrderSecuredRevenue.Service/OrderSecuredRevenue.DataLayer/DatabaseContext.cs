using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using Microservices.Common.Interface;
using OrderSecuredRevenue.Common;
using OrderSecuredRevenue.Common.Logger;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;
using OrderSecuredRevenue.DataLayer.Interfaces;

namespace OrderSecuredRevenue.DataLayer
{
    public class DatabaseContext: IDatabaseContext
    {
        private readonly ConfigReader _configReader;
        readonly Stopwatch _stopwatch;

        [Import]
        public IDatabase Database { get; set; }

        public DatabaseContext()
        {
            _configReader = new ConfigReader();
            GetContainer();
            Database.ConnectionString = _configReader.DatabaseConnectionString;
            _stopwatch = new Stopwatch();
        }

        private void GetContainer()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(_configReader.DatasourceLibraryPath);
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public IEnumerable<OR03> GetOrderSecuredRevenueByOrderNumber(string companyCode, string orderNumber)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredRevenueByOrderNumber : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY);
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");

            _stopwatch.Reset();
            _stopwatch.Start();
            var salesOrder = Database.Where<OR03>(tableName, columns, $"trim(lower({Constants.ORDER_NUMBER_FIELD})) {Constants.EQUAL_OPERATOR} '{orderNumber.ToLower().Trim()}'");
            _stopwatch.Stop();
            ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
            ApplicationLogger.InfoLogger($"Sales order line history count: {salesOrder.Count()}");
            return salesOrder;

        }

        //public IEnumerable<OR03> GetOrderSecuredRevenueByInvoiceNumber(string companyCode, string invoiceNumber)
        //{
        //    ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredRevenueByInvoiceNumber : Reading datalake table name from config");
        //    var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY);
        //    string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
        //    string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
        //    ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");

        //    _stopwatch.Reset();
        //    _stopwatch.Start();
        //    var salesOrder = Database.Where<OR03>(tableName, columns, $"trim(lower({Constants.INVOICE_NUMBER_FIELD})) {Constants.EQUAL_OPERATOR} '{invoiceNumber.ToLower().Trim()}'");
        //    _stopwatch.Stop();
        //    ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
        //    ApplicationLogger.InfoLogger($"Sales order line history count: {salesOrder.Count()}");
        //    return salesOrder;
        //}
    }
}
