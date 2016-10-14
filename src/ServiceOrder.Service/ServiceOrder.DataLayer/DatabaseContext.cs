using System;
using System.Collections.Generic;
using System.Linq;
using ServiceOrder.Common.Logger;
using ServiceOrder.DataLayer.Interfaces;
using ServiceOrder.Common.Enum;
using ServiceOrder.DataLayer.Adapters;
using ServiceOrder.DataLayer.Entities.Datalake;
using ServiceOrder.DataLayer.Entities.IScala;
using SL01 = ServiceOrder.DataLayer.Entities.Datalake.SL01;
using SM01 = ServiceOrder.DataLayer.Entities.Datalake.SM01;
using SM03 = ServiceOrder.DataLayer.Entities.Datalake.SM03;
using SM05 = ServiceOrder.DataLayer.Entities.Datalake.SM05;
using SM07 = ServiceOrder.DataLayer.Entities.Datalake.SM07;
using System.Threading.Tasks;

namespace ServiceOrder.DataLayer
{
    public class DatabaseContext : IDatabaseContext
    {
        // private readonly IDatalakeEntities _datalakeEntities;
        private readonly IiScalaEntities _iScalaEntities;

        //private const string DATALAKE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY = "DatalakeServiceOrderMasterTableName";
        //private const string DATALAKE_CUSTOMERFILE_TABLE_NAME_KEY = "DatalakeCustomerFileTableName";
        //private const string DATALAKE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY = "DatalakeServiceOrderActivityLinesTableName";
        //private const string DATALAKE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY = "DatalakeServiceOrderCostLinesTableName";
        //private const string DATALAKE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY = "DatalakeServiceOrderMaterialLinesTableName";

        private const string iScala_SERVICE_ORDER_MASTER_TABLE_NAME_KEY = "iScalaServiceOrderMasterTableName";
        private const string iScala_CUSTOMERFILE_TABLE_NAME_KEY = "iScalaCustomerFileTableName";
        private const string iScala_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY = "iScalaServiceOrderActivityLinesTableName";
        private const string iScala_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY = "iScalaServiceOrderCostLinesTableName";
        private const string iScala_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY = "iScalaServiceOrderMaterialLinesTableName";

        private const string SERVICEORDER_MASTER_SERVICEORDERNO_FIELD = "SM01001";
        private const string SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD = "SM03001";
        private const string SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD = "SM05001";
        private const string SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD = "SM07001";

        private const string SERVICEORDER_MASTER_CUSTOMERCODE_FIELD = "SM01002";
        private const string CUSTOMERFILE_CUSTOMERCODE_FIELD = "SL01001";
        private const string SERVICEORDER_ACTIVITYLINES_CUSTOMERCODE_FIELD = "SM03022";
        private const string SERVICEORDER_COSTLINES_CUSTOMERCODE_FIELD = "SM05018";
        private const string SERVICEORDER_MATERIALLINES_CUSTOMERCODE_FIELD = "SM07041";


        private const string SERVICEORDER_MASTER_INVOICENUMBER_FIELD = "SM01038";
        private const string SERVICEORDER_ACTIVITYLINES_INVOICENUMBER_FIELD = "SM03024";
        private const string SERVICEORDER_COSTLINES_INVOICENUMBER_FIELD = "SM05020";
        private const string SERVICEORDER_MATERIALLINES_INVOICENUMBER_FIELD = "SM07043";

        private readonly ConfigReader _configReader;
        public DatabaseContext(IScalaEntities iScalaEntities)
        {
            try
            {
                _configReader = new ConfigReader();
                //_datalakeEntities = datalakeEntities;
                //_datalakeEntities.ConnectionString = _configReader.DatalakeConnectionString;
                _iScalaEntities = iScalaEntities;
                _iScalaEntities.ConnectionString = _configReader.IScalaConnectionString;
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace, exception.InnerException);
                throw;
            }
        }

        private static void LogException(Exception exception)
        {
            ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                exception.InnerException);
        }

        //#region Datalake Methods
        //public Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder : Reading datalake table name from config");
        //            string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
        //            string customerFileTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_CUSTOMERFILE_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake tables: [{serviceOrderMasterTableName}], [{customerFileTableName}]");
        //            var serviceOrderDetails = _datalakeEntities.Get<SM01>(serviceOrderMasterTableName);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
        //            return serviceOrderDetails;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}

        //public Task<SL01> GetCustomerDetailsAsync(string companyCode, string customerCode)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder : Reading datalake table name from config");
        //            string customerFileTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_CUSTOMERFILE_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{customerFileTableName}]");
        //            var customerDetails = _datalakeEntities.Where<SL01>(customerFileTableName, $"trim(lower({CUSTOMERFILE_CUSTOMERCODE_FIELD}))='{customerCode.ToLower().Trim()}'");
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {customerDetails.Count()}");
        //            return customerDetails.FirstOrDefault();
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}

        //public Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode, string serviceOrderNo)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder [serviceOrderNo] : Reading datalake table name from config");
        //            string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
        //            string customerFileTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_CUSTOMERFILE_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake tables: [{serviceOrderMasterTableName}], [{customerFileTableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_MASTER_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        //            var serviceOrderDetails = _datalakeEntities.Where<SM01>(serviceOrderMasterTableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
        //            return serviceOrderDetails;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}

        ////#region ASYNC
        ////public Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode, string serviceOrderNo)
        ////{
        ////    try
        ////    {
        ////        return Task.Run(() =>
        ////        {
        ////            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder [serviceOrderNo] : Reading datalake table name from config");
        ////            string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
        ////            string customerFileTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_CUSTOMERFILE_TABLE_NAME_KEY);
        ////            ApplicationLogger.InfoLogger($"Datalake tables: [{serviceOrderMasterTableName}], [{customerFileTableName}]");
        ////            string whereCondition = $"trim({SERVICEORDER_MASTER_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        ////            var serviceOrderDetails = _datalakeEntities.Where<SM01>(serviceOrderMasterTableName, whereCondition);
        ////            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
        ////            return serviceOrderDetails;
        ////        });
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        LogException(exception);
        ////        throw;
        ////    }
        ////}
        ////public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesAsync(string companyCode, string serviceOrderNo)
        ////{
        ////    try
        ////    {
        ////        return Task.Run(() =>
        ////        {
        ////            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLines : Reading datalake table name from config");
        ////            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);
        ////            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        ////            string whereCondition = $"trim({SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        ////            var serviceOrderActivityLines = _datalakeEntities.Where<SM03>(tableName, whereCondition);
        ////            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
        ////            return serviceOrderActivityLines;
        ////        });
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        LogException(exception);
        ////        throw;
        ////    }
        ////}
        ////public Task<IEnumerable<SM05>> GetServiceOrderCostLinesAsync(string companyCode, string serviceOrderNo)
        ////{
        ////    try
        ////    {
        ////        return Task.Run(() =>
        ////        {
        ////            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLines : Reading datalake table name from config");
        ////            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);
        ////            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        ////            string whereCondition = $"trim({SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        ////            var serviceOrderCostLines = _datalakeEntities.Where<SM05>(tableName, whereCondition);
        ////            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
        ////            return serviceOrderCostLines;
        ////        });
        ////}
        ////    catch (Exception exception)
        ////    {
        ////        LogException(exception);
        ////        throw;
        ////    }
        ////}
        ////public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesAsync(string companyCode, string serviceOrderNo)
        ////{
        ////    try
        ////    {
        ////        return Task.Run(() =>
        ////        {
        ////            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines : Reading datalake table name from config");
        ////            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
        ////            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        ////            string whereCondition = $"trim({SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        ////            var serviceOrderMaterialLines = _datalakeEntities.Where<SM07>(tableName, whereCondition);
        ////            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
        ////            return serviceOrderMaterialLines;
        ////        });
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        LogException(exception);
        ////        throw;
        ////    }
        ////}

        ////#endregion




        //public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesAsync(string companyCode, string serviceOrderNo)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLines : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        //            var serviceOrderActivityLines = _datalakeEntities.Where<SM03>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
        //            return serviceOrderActivityLines;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
               
        //public Task<IEnumerable<SM05>> GetServiceOrderCostLinesAsync(string companyCode, string serviceOrderNo)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLines : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        //            var serviceOrderCostLines = _datalakeEntities.Where<SM05>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
        //            return serviceOrderCostLines;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
               
        //public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesAsync(string companyCode, string serviceOrderNo)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}'";
        //            var serviceOrderMaterialLines = _datalakeEntities.Where<SM07>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
        //            return serviceOrderMaterialLines;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
               
        //public Task<IEnumerable<SM01>> GetServiceOrdersByInvoiceCustomerCodeAsync(string companyCode, string invoiceCustomerCode)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrdersByBillToCustomerCode : Reading datalake table name from config");
        //            string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
        //            string customerFileTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_CUSTOMERFILE_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake tables: [{serviceOrderMasterTableName}], [{customerFileTableName}]");
        //            string whereCondition = $"trim(lower({SERVICEORDER_MASTER_CUSTOMERCODE_FIELD}))='{invoiceCustomerCode.ToLower().Trim()}'";
        //            var serviceOrderDetails = _datalakeEntities.Where<SM01>(serviceOrderMasterTableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
        //            return serviceOrderDetails;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
        //public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string invoiceCustomerCode)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLinesByBillToCustomerCode [serviceOrderNo] : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({SERVICEORDER_ACTIVITYLINES_CUSTOMERCODE_FIELD}))='{invoiceCustomerCode.ToLower().Trim()}'";
        //            var serviceOrderActivityLines = _datalakeEntities.Where<SM03>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
        //            return serviceOrderActivityLines;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
        //public Task<IEnumerable<SM05>> GetServiceOrderCostLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string invoiceCustomerCode)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLinesByBillToCustomerCode [serviceOrderNo] : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({SERVICEORDER_COSTLINES_CUSTOMERCODE_FIELD}))='{invoiceCustomerCode.ToLower().Trim()}'";
        //            var serviceOrderCostLines = _datalakeEntities.Where<SM05>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
        //            return serviceOrderCostLines;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
        //public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string invoiceCustomerCode)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines [serviceOrderNo] : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({SERVICEORDER_MATERIALLINES_CUSTOMERCODE_FIELD}))='{invoiceCustomerCode.ToLower().Trim()}'";
        //            var serviceOrderMaterialLines = _datalakeEntities.Where<SM07>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
        //            return serviceOrderMaterialLines;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
        //public Task<IEnumerable<SM01>> GetServiceOrderByInvoiceNumberAsync(string companyCode, string invoiceNumber)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderByInvoiceNumber : Reading datalake table name from config");
        //            string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderMasterTableName}]");
        //            string whereCondition = $"trim(lower({SERVICEORDER_MASTER_INVOICENUMBER_FIELD}))='{invoiceNumber.ToLower().Trim()}'";
        //            var serviceOrderDetails = _datalakeEntities.Where<SM01>(serviceOrderMasterTableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
        //            return serviceOrderDetails;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
        //public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLinesByInvoiceNumber : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({SERVICEORDER_ACTIVITYLINES_INVOICENUMBER_FIELD}))='{invoiceNumber.ToLower().Trim()}'";
        //            var serviceOrderActivityLines = _datalakeEntities.Where<SM03>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
        //            return serviceOrderActivityLines;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
        //public Task<IEnumerable<SM05>> GetServiceOrderCostLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLinesByInvoiceNumber : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim(lower({SERVICEORDER_COSTLINES_INVOICENUMBER_FIELD}))='{invoiceNumber.ToLower().Trim()}'";
        //            var serviceOrderCostLines = _datalakeEntities.Where<SM05>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
        //            return serviceOrderCostLines;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
        //public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber)
        //{
        //    try
        //    {
        //        return Task.Run(() =>
        //        {
        //            ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines : Reading datalake table name from config");
        //            string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
        //            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
        //            string whereCondition = $"trim({SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD})='{serviceOrderNo.Trim()}' AND trim({SERVICEORDER_MATERIALLINES_INVOICENUMBER_FIELD})='{invoiceNumber.Trim()}'";
        //            var serviceOrderMaterialLines = _datalakeEntities.Where<SM07>(tableName, whereCondition);
        //            ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
        //            return serviceOrderMaterialLines;
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        LogException(exception);
        //        throw;
        //    }
        //}
        //#endregion

        #region IScala Methods
           
        public Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode)
        {
            try
            {
                return Task.Run(() =>
               {
                   ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder : Reading iScala table name from config");
                   string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
                   ApplicationLogger.InfoLogger($"iScala table: [{serviceOrderMasterTableName}]");
                   var serviceOrderDetails = _iScalaEntities.Get<SM01>(serviceOrderMasterTableName);
                   ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
                   return serviceOrderDetails;
               });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<SL01> GetCustomerDetailsAsync(string companyCode, string customerCode)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder : Reading iScala table name from config");
                    string customerFileTableName = _configReader.GetDatalakeTableName(companyCode, iScala_CUSTOMERFILE_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{customerFileTableName}]");
                    var customerDetails = _iScalaEntities.Where<SL01>(customerFileTableName, $"rtrim(ltrim(lower({CUSTOMERFILE_CUSTOMERCODE_FIELD})))='{customerCode.ToLower().Trim()}'");
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {customerDetails.Count()}");
                    return customerDetails.FirstOrDefault();
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode, string serviceOrderNo)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrder [serviceOrderNo] : Reading iScala table name from config");
                    string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{serviceOrderMasterTableName}]");
                    string whereCondition = $"rtrim(ltrim({SERVICEORDER_MASTER_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}'";
                    var serviceOrderDetails = _iScalaEntities.Where<SM01>(serviceOrderMasterTableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
                    return serviceOrderDetails;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesAsync(string companyCode, string serviceOrderNo)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLines : Reading iScala table name from config");
                    string tableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{tableName}]");
                    string whereCondition = $"ltrim(rtrim({SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}'";
                    var serviceOrderActivityLines = _iScalaEntities.Where<SM03>(tableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
                    return serviceOrderActivityLines;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM05>> GetServiceOrderCostLinesAsync(string companyCode, string serviceOrderNo)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLines : Reading iScala table name from config");
                    string tableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{tableName}]");
                    string whereCondition = $"ltrim(rtrim({SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}'";
                    var serviceOrderCostLines = _iScalaEntities.Where<SM05>(tableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
                    return serviceOrderCostLines;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesAsync(string companyCode, string serviceOrderNo)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines : Reading iScala table name from config");
                    string tableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{tableName}]");
                    string whereCondition = $"ltrim(rtrim({SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}'";
                    var serviceOrderMaterialLines = _iScalaEntities.Where<SM07>(tableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
                    return serviceOrderMaterialLines;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM01>> GetServiceOrdersByInvoiceCustomerCodeAsync(string companyCode, string billToCustomerCode)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrdersByBillToCustomerCode : Reading iScala table name from config");
                    string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala tables: [{serviceOrderMasterTableName}]");
                    string whereCondition = $"ltrim(rtrim(lower({SERVICEORDER_MASTER_CUSTOMERCODE_FIELD})))='{billToCustomerCode.ToLower().Trim()}'";
                    var serviceOrderDetails = _iScalaEntities.Where<SM01>(serviceOrderMasterTableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
                    return serviceOrderDetails;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string billToCustomerCode)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLinesByBillToCustomerCode [serviceOrderNo] : Reading iScala table name from config");
                    string tableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{tableName}]");
                    string whereCondition = $"ltrim(rtrim({SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}' AND ltrim(rtrim(lower({SERVICEORDER_ACTIVITYLINES_CUSTOMERCODE_FIELD})))='{billToCustomerCode.ToLower().Trim()}'";
                    var serviceOrderActivityLines = _iScalaEntities.Where<SM03>(tableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
                    return serviceOrderActivityLines;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM05>> GetServiceOrderCostLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string billToCustomerCode)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLinesByBillToCustomerCode [serviceOrderNo] : Reading iScala table name from config");
                    string tableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{tableName}]");
                    string whereCondition = $"ltrim(rtrim({SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}' AND ltrim(rtrim(lower({SERVICEORDER_COSTLINES_CUSTOMERCODE_FIELD})))='{billToCustomerCode.ToLower().Trim()}'";
                    var serviceOrderCostLines = _iScalaEntities.Where<SM05>(tableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
                    return serviceOrderCostLines;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string billToCustomerCode)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines [serviceOrderNo] : Reading iScala table name from config");
                    string tableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{tableName}]");
                    string whereCondition = $"ltrim(rtrim({SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}' AND ltrim(rtrim(lower({SERVICEORDER_MATERIALLINES_CUSTOMERCODE_FIELD})))='{billToCustomerCode.ToLower().Trim()}'";
                    var serviceOrderMaterialLines = _iScalaEntities.Where<SM07>(tableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
                    return serviceOrderMaterialLines;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM01>> GetServiceOrderByInvoiceNumberAsync(string companyCode, string invoiceNumber)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderByInvoiceNumber : Reading iScala table name from config");
                    string serviceOrderMasterTableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_MASTER_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{serviceOrderMasterTableName}]");
                    string whereCondition = $"ltrim(rtrim(lower({SERVICEORDER_MASTER_INVOICENUMBER_FIELD})))='{invoiceNumber.ToLower().Trim()}'";
                    var serviceOrderDetails = _iScalaEntities.Where<SM01>(serviceOrderMasterTableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderDetails.Count()}");
                    return serviceOrderDetails;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM03>> GetServiceOrderActivityLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderActivityLinesByInvoiceNumber : Reading iScala table name from config");
                    string tableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{tableName}]");
                    string whereCondition = $"ltrim(rtrim({SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}' AND ltrim(rtrim(lower({SERVICEORDER_ACTIVITYLINES_INVOICENUMBER_FIELD})))='{invoiceNumber.ToLower().Trim()}'";
                    var serviceOrderActivityLines = _iScalaEntities.Where<SM03>(tableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderActivityLines.Count()}");
                    return serviceOrderActivityLines;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM05>> GetServiceOrderCostLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderCostLinesByInvoiceNumber : Reading iScala table name from config");
                    string tableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{tableName}]");
                    string whereCondition = $"ltrim(rtrim({SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}' AND ltrim(rtrim(lower({SERVICEORDER_COSTLINES_INVOICENUMBER_FIELD})))='{invoiceNumber.ToLower().Trim()}'";
                    var serviceOrderCostLines = _iScalaEntities.Where<SM05>(tableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderCostLines.Count()}");
                    return serviceOrderCostLines;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber)
        {
            try
            {
                return Task.Run(() =>
                {
                    ApplicationLogger.InfoLogger("DataLayer :: GetServiceOrderMaterialLines : Reading iScala table name from config");
                    string tableName = _configReader.GetDatalakeTableName(companyCode, iScala_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY);
                    ApplicationLogger.InfoLogger($"iScala table: [{tableName}]");
                    string whereCondition = $"ltrim(rtrim({SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD}))='{serviceOrderNo.Trim()}' AND ltrim(rtrim(lower({SERVICEORDER_MATERIALLINES_INVOICENUMBER_FIELD})))='{invoiceNumber.ToLower().Trim()}'";
                    var serviceOrderMaterialLines = _iScalaEntities.Where<SM07>(tableName, whereCondition);
                    ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceOrderMaterialLines.Count()}");
                    return serviceOrderMaterialLines;
                });
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        
        #endregion
    }
}
