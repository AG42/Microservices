using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderedSecuredMargin.DataAccessLayer.Entities.Datalake;
using OrderedSecuredMargin.DataAccessLayer.Interface;
using OrderedSecuredMargin.Common;
using Microservices.Common.Interface;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using OrderedSecuredMargin.Common.Logger;

namespace OrderedSecuredMargin.DataAccessLayer
{
    public class DataLayerContext : IDataLayerContext
    {

        private readonly ConfigReader _configReader;
        private const string OrderNo = "or03001";
        private const string UnitPrice = "or03008";
        private const string UnitCostPric = "or03009";
        private const string UnitCode = "or03010";
        private const string ORQtyOrdered = "or03011";

        [Import]
        public IDatabase Database { get; set; }

        public DataLayerContext()
        {
            _configReader = new ConfigReader();
            GetContainer();
            Database.ConnectionString = _configReader.DatalakeConnectionString;
        }

        /// <summary>
        /// This method initialise the Import interface object with matchs of export with same type.
        /// here it initialise to IDatabase object.
        /// </summary>
        private void GetContainer()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(_configReader.DatasourceLibraryPath);
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        public IEnumerable<Or03> GetOrderSecuredMarginByCompanyCode(string companyCode)
        {
            companyCode = ReplaceSingleCode(companyCode);
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetCreditStatusByCompanyCode :: Custome Input: companyCode: [{companyCode}] ,[{companyCode}]");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, "");
            var lstOfOr03 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Get<Or03>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name :: GetCreditStatusByCompanyCode : Success");
            return lstOfOr03;
        }
        public IEnumerable<Or03> GetOrderSecuredMarginByCost(string companyCode, decimal minCost, decimal maxCost)
        {
            companyCode = ReplaceSingleCode(companyCode);
            ApplicationLogger.InfoLogger("DataLayer :: GetCreditStatusByCustomerName : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, "");
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var tableName = dicTableName[Constants.TableNameKey] + $" Group BY {OrderNo} HAVING (((SUM(CASE WHEN {UnitPrice}=0 THEN {UnitCostPric} ELSE {UnitPrice} END * {ORQtyOrdered}) - SUM({UnitCostPric} * {ORQtyOrdered})) / SUM(CASE WHEN {UnitPrice}=0 THEN {int.MaxValue} ELSE {UnitPrice} END * CASE WHEN {ORQtyOrdered}=0 THEN {int.MaxValue} ELSE {ORQtyOrdered} END)) * 100) >=  { minCost}  AND(((SUM({UnitPrice} * {ORQtyOrdered}) - SUM({UnitCostPric} * {ORQtyOrdered})) / SUM( CASE WHEN {UnitPrice} = 0 THEN {int.MaxValue} ELSE {UnitPrice} END * CASE WHEN {ORQtyOrdered} = 0 THEN {int.MaxValue} ELSE {ORQtyOrdered} END )) * 100) <=  {maxCost} ";
            var lstOfOr03 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Get<Or03>(tableName, $" {OrderNo}, ((SUM({UnitPrice} *  {ORQtyOrdered}) - SUM({UnitCostPric} * {ORQtyOrdered})) / SUM({UnitPrice} * {ORQtyOrdered})) * 100  as MarginPercentage") : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetCreditStatusByCustomerName : Success");
            return lstOfOr03;
        }
        public IEnumerable<Or03> GetOrderSecuredMarginByOrderNo(string companyCode, string orderNo)
        {
            companyCode = ReplaceSingleCode(companyCode);
            orderNo = ReplaceSingleCode(orderNo);
            ApplicationLogger.InfoLogger("DataLayer :: GetCreditStatusByCustomerName : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, "");
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({OrderNo})) like '%{orderNo.ToLower().Trim()}%'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Or03>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetCreditStatusByCustomerName : Success");
            return lstOfSl01;
        }

        private string ReplaceSingleCode(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
