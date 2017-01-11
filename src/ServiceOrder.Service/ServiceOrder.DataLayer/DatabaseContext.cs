using System;
using System.Collections.Generic;
using System.Linq;
using ServiceOrder.Common.Logger;
using ServiceOrder.DataLayer.Interfaces;
using ServiceOrder.Common.Enum;
using SL01 = ServiceOrder.DataLayer.Entities.Datalake.SL01;
using SM01 = ServiceOrder.DataLayer.Entities.Datalake.SM01;
using SM03 = ServiceOrder.DataLayer.Entities.Datalake.SM03;
using SM05 = ServiceOrder.DataLayer.Entities.Datalake.SM05;
using SM07 = ServiceOrder.DataLayer.Entities.Datalake.SM07;
using System.Threading.Tasks;
using Microservices.Common.Interface;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using ServiceOrder.Common;
using System.Diagnostics;

namespace ServiceOrder.DataLayer
{
    public class DatabaseContext : IDatabaseContext
    {
        [Import]
        public IDatabase Database { get; set; }       
        private readonly ConfigReader _configReader;
        private readonly Stopwatch _stopwatch;

        public DatabaseContext()
        {
            _configReader = new ConfigReader();
            GetContainer();
            Database.ConnectionString = _configReader.DatabaseConnectionString;
            _stopwatch = new Stopwatch();
        }

        public void GetContainer()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(_configReader.DatasourceLibraryPath);
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        #region Datalake Methods
        public Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder : Reading datalake table name from config");
                //string serviceOrderMasterTableName = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
                //string customerFileTableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_CUSTOMERFILE_TABLE_NAME_KEY);

                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_MASTER_COLUMN_NAME_KEY);
                var serviceOrderMasterTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY];
                var serviceOrderMasterColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MASTER_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"Datalake tables: [{serviceOrderMasterTableName}]");
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderDetails = Database.Get<SM01>(serviceOrderMasterTableName, serviceOrderMasterColumnName);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
                return serviceOrderDetails;
            });
        }

        public Task<SL01> GetCustomerDetailsAsync(string companyCode, string customerCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder : Reading datalake table name from config");
                var customerFileDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_CUSTOMERFILE_TABLE_NAME_KEY, Constants.DATABASE_CUSTOMERFILE_COLUMN_NAME_KEY);
                var customerFileTableName = customerFileDbDetails[Constants.DATABASE_CUSTOMERFILE_TABLE_NAME_KEY];
                var customerFileColumnName = customerFileDbDetails[Constants.DATABASE_CUSTOMERFILE_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"Datalake table: [{customerFileTableName}]");
                _stopwatch.Reset();
                _stopwatch.Start();
                var customerDetails = Database.Where<SL01>(customerFileTableName, customerFileColumnName, $"trim(lower({Constants.CUSTOMERFILE_CUSTOMERCODE_FIELD}))='{customerCode.ToLower().Trim()}'");
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");                
                ApplicationLogger.InfoLogger($"ServiceOrder count: {customerDetails.Count()}");
                return customerDetails.FirstOrDefault();
            });
        }

        public Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode, string serviceOrderNo)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder [serviceOrderNo] : Reading datalake table name from config");
                // string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_MASTER_COLUMN_NAME_KEY);
                var serviceOrderMasterTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY];
                var serviceOrderMasterColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MASTER_COLUMN_NAME_KEY];

                ApplicationLogger.InfoLogger($"Datalake tables: {serviceOrderMasterTableName}");
                string whereCondition = $"trim({Constants.SERVICEORDER_MASTER_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderDetails = Database.Where<SM01>(serviceOrderMasterTableName, serviceOrderMasterColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");                
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
                return serviceOrderDetails;
            });
        }

        //#region ASYNC
        //public Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode, string serviceOrderNo)
        //{
        //        return Task.Run(() =>
        //        {
        //    try
        //    {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder [serviceOrderNo] : Reading datalake table name from config");
        //            string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode.Trim(), DATALAKE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
        //            string customerFileTableName = _configReader.GetDatalakeTableName(companyCode.Trim(), DATALAKE_CUSTOMERFILE_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake tables: [{serviceOrderMasterTableName}], [{customerFileTableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_MASTER_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        //            var serviceOrderDetails = Database.Where<SM01>(serviceOrderMasterTableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
        //            return serviceOrderDetails;
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //        });
        //}
        //public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesAsync(string companyCode, string serviceOrderNo)
        //{
        //        return Task.Run(() =>
        //        {
        //    try
        //    {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLines : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        //            var serviceOrderActivityLines = Database.Where<SM03>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
        //            return serviceOrderActivityLines;
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //        });
        //}
        //public Task<IEnumerable<SM05>> GetServiceOrderCostLinesAsync(string companyCode, string serviceOrderNo)
        //{
        //        return Task.Run(() =>
        //        {
        //    try
        //    {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLines : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), DATALAKE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        //            var serviceOrderCostLines = Database.Where<SM05>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
        //            return serviceOrderCostLines;
        //}
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //        });
        //}
        //public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesAsync(string companyCode, string serviceOrderNo)
        //{
        //        return Task.Run(() =>
        //        {
        //    try
        //    {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), DATALAKE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        //            var serviceOrderMaterialLines = Database.Where<SM07>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
        //            return serviceOrderMaterialLines;
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //        });
        //}

        //#endregion




        public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesAsync(string companyCode, string serviceOrderNo)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLines : Reading datalake table name from config");
                // string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);

                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_COLUMN_NAME_KEY);
                var serviceOrderActivityLinesTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY];
                var serviceOrderActivityLinesColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_COLUMN_NAME_KEY];

                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderActivityLinesTableName}]");
                string whereCondition = $"trim({Constants.SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderActivityLines = Database.Where<SM03>(serviceOrderActivityLinesTableName, serviceOrderActivityLinesColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");

                
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
                return serviceOrderActivityLines;
            });
        }

        public Task<IEnumerable<SM05>> GetServiceOrderCostLinesAsync(string companyCode, string serviceOrderNo)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLines : Reading datalake table name from config");
                // string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);

                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_COSTLINES_COLUMN_NAME_KEY);
                var serviceOrderCostlineTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY];
                var serviceOrderCostlineColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_COSTLINES_COLUMN_NAME_KEY];


                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderCostlineTableName}]");
                string whereCondition = $"trim({Constants.SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";

                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderCostLines = Database.Where<SM05>(serviceOrderCostlineTableName, serviceOrderCostlineColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");                
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
                return serviceOrderCostLines;
            });
        }

        public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesAsync(string companyCode, string serviceOrderNo)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines : Reading datalake table name from config");
                // string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);

                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_COLUMN_NAME_KEY);
                var serviceOrderMaterialLineTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY];
                var serviceOrderMaterialLineColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_COLUMN_NAME_KEY];

                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderMaterialLineTableName}]");
                string whereCondition = $"trim({Constants.SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderMaterialLines = Database.Where<SM07>(serviceOrderMaterialLineTableName, serviceOrderMaterialLineColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
                return serviceOrderMaterialLines;
            });
        }

        public Task<IEnumerable<SM01>> GetServiceOrdersByInvoiceCustomerCodeAsync(string companyCode, string invoiceCustomerCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrdersByBillToCustomerCode : Reading datalake table name from config");
                // string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_MASTER_COLUMN_NAME_KEY);
                var serviceOrderMasterTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY];
                var serviceOrderMasterColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MASTER_COLUMN_NAME_KEY];

                string whereCondition = $"trim(lower({Constants.SERVICEORDER_MASTER_CUSTOMERCODE_FIELD}))='{invoiceCustomerCode.ToLower().Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderDetails = Database.Where<SM01>(serviceOrderMasterTableName, serviceOrderMasterColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
                return serviceOrderDetails;
            });
        }
        public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string invoiceCustomerCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLinesByBillToCustomerCode [serviceOrderNo] : Reading datalake table name from config");
                // string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);

                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_COLUMN_NAME_KEY);
                var serviceOrderActivityLinesTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY];
                var serviceOrderActivityLinesColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_COLUMN_NAME_KEY];

                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderActivityLinesTableName}]");
                string whereCondition = $"trim({Constants.SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({Constants.SERVICEORDER_ACTIVITYLINES_CUSTOMERCODE_FIELD}))='{invoiceCustomerCode.ToLower().Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderActivityLines = Database.Where<SM03>(serviceOrderActivityLinesTableName, serviceOrderActivityLinesColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
                return serviceOrderActivityLines;
            });
        }
        public Task<IEnumerable<SM05>> GetServiceOrderCostLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string invoiceCustomerCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLinesByBillToCustomerCode [serviceOrderNo] : Reading datalake table name from config");
                // string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);
                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_COSTLINES_COLUMN_NAME_KEY);
                var serviceOrderCostlineTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY];
                var serviceOrderCostlineColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_COSTLINES_COLUMN_NAME_KEY];


                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderCostlineTableName}]");
                string whereCondition = $"trim({Constants.SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({Constants.SERVICEORDER_COSTLINES_CUSTOMERCODE_FIELD}))='{invoiceCustomerCode.ToLower().Trim()}'";

                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderCostLines = Database.Where<SM05>(serviceOrderCostlineTableName, serviceOrderCostlineColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");

                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
                return serviceOrderCostLines;
            });
        }
        public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string invoiceCustomerCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines [serviceOrderNo] : Reading datalake table name from config");
                // string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_COLUMN_NAME_KEY);
                var serviceOrderMaterialLineTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY];
                var serviceOrderMaterialLineColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_COLUMN_NAME_KEY];


                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderMaterialLineTableName}]");
                string whereCondition = $"trim({Constants.SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({Constants.SERVICEORDER_MATERIALLINES_CUSTOMERCODE_FIELD}))='{invoiceCustomerCode.ToLower().Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderMaterialLines = Database.Where<SM07>(serviceOrderMaterialLineTableName, serviceOrderMaterialLineColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
                return serviceOrderMaterialLines;
            });
        }
        public Task<IEnumerable<SM01>> GetServiceOrderByInvoiceNumberAsync(string companyCode, string invoiceNumber)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderByInvoiceNumber : Reading datalake table name from config");
                // string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_MASTER_COLUMN_NAME_KEY);
                var serviceOrderMasterTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY];
                var serviceOrderMasterColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MASTER_COLUMN_NAME_KEY];

                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderMasterTableName}]");
                string whereCondition = $"trim(lower({Constants.SERVICEORDER_MASTER_INVOICENUMBER_FIELD}))='{invoiceNumber.ToLower().Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderDetails = Database.Where<SM01>(serviceOrderMasterTableName, serviceOrderMasterColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
                return serviceOrderDetails;

            });
        }
        public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLinesByInvoiceNumber : Reading datalake table name from config");
                // string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);
                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_COLUMN_NAME_KEY);
                var serviceOrderActivityLinesTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY];
                var serviceOrderActivityLinesColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_ACTIVITYLINES_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderActivityLinesTableName}]");
                string whereCondition = $"trim({Constants.SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({Constants.SERVICEORDER_ACTIVITYLINES_INVOICENUMBER_FIELD}))='{invoiceNumber.ToLower().Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderActivityLines = Database.Where<SM03>(serviceOrderActivityLinesTableName, serviceOrderActivityLinesColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
                return serviceOrderActivityLines;
            });
        }
        public Task<IEnumerable<SM05>> GetServiceOrderCostLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLinesByInvoiceNumber : Reading datalake table name from config");
                // string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);

                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_COSTLINES_COLUMN_NAME_KEY);
                var serviceOrderCostlineTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY];
                var serviceOrderCostlineColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_COSTLINES_COLUMN_NAME_KEY];

                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderCostlineTableName}]");
                string whereCondition = $"trim({Constants.SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({Constants.SERVICEORDER_COSTLINES_INVOICENUMBER_FIELD}))='{invoiceNumber.ToLower().Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderCostLines = Database.Where<SM05>(serviceOrderCostlineTableName, serviceOrderCostlineColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
                return serviceOrderCostLines;
            });
        }
        public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines : Reading datalake table name from config");
                // string tableName = _configReader.GetDatalakeTableName(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
                
                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_COLUMN_NAME_KEY);
                var serviceOrderMaterialLineTableName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY];
                var serviceOrderMaterialLineColumnName = serviceOrderDbDetails[Constants.DATABASE_SERVICE_ORDER_MATERIALLINES_COLUMN_NAME_KEY];

                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderMaterialLineTableName}]");
                string whereCondition = $"trim({Constants.SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim({Constants.SERVICEORDER_MATERIALLINES_INVOICENUMBER_FIELD})='{invoiceNumber.Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();
                var serviceOrderMaterialLines = Database.Where<SM07>(serviceOrderMaterialLineTableName, serviceOrderMaterialLineColumnName, whereCondition);
                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
                return serviceOrderMaterialLines;

            });
        }
        #endregion

    }
}
