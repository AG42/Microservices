using System;
using System.Collections.Generic;
using System.Web.Http;
using CustomerSiteLocation.Common.Logger;
using CustomerSiteLocation.DataLayer.Entities.Datalake;
using CustomerSiteLocation.DataLayer.Interfaces;
using System.Linq;
using Microservices.Common.Interface;
using System.ComponentModel.Composition;
using CustomerSiteLocation.Common;
using System.ComponentModel.Composition.Hosting;

namespace CustomerSiteLocation.DataLayer
{
    public class DataLayerContext : IDataLayerContext
    {

        private const string EqualOperator = "=";
        private const string OrOperator = "OR";
        private const string LikeOperator = "like";


        private readonly ConfigReader _configReader;
        private const string CountryField = "sy80048";
        private const string CustomerCodeField = "sy80001";
        private const string CustomerNameField = "sy80003";
        private const string SiteNo = "sy80002";
        private const string AddressOne = "sy80004";
        const string AddressTwo = "sy80005";
        private const string AddressThree = "sy80006";
        private const string AddressFour = "sy80007";
        private const string AddressFive = "sy80050";
        private const string AddressSix = "sy80051";
        private const string AddressSeven = "sy80052";
        private const string Like = "like";
        private const string ZipField = "sy80010";
        private const string TelephoneField = "sy80011";
        private const string FaxField = "sy80012";
        private const string EmailId = "sy80049";
        private const string ServiceEngineerCodeField = "Sy80046";
        private const string ParentCompanyCode = "";
        //....added to direct connection
        // private readonly IDatalakeEntities _datalakeEntities;


            [Import]
        public IDatabase objDb { get; set; }
    

        public DataLayerContext()
        {
           _configReader = new ConfigReader();
            GetContainer();
            objDb.ConnectionString = _configReader.DatalakeConnectionString;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsByCompanyCode(string companyCode, string parentCompanyCode = "")
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            ApplicationLogger.InfoLogger("DataLayer :: GetSitesByCompanyCode : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  parentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var lstOfSy80 = objDb.Get<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]);
            ApplicationLogger.InfoLogger($"Site Location count: {lstOfSy80.Count()}");
            return lstOfSy80;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="country"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsByCountry(string companyCode, string country)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            country = ReplaceSingleCode(country);
            ApplicationLogger.InfoLogger(
                "DataLayer :: GetCustomerSiteLocationsByCountry : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
            string query = $"trim(lower({CountryField}))='{country.ToLower().Trim()}'";
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var lstOfSy80 = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query);
            ApplicationLogger.InfoLogger($"Site Location count: {lstOfSy80.Count()}");
            return lstOfSy80;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="zipCode"></param>
        /// <returns>List of customer site locations</returns>        
        public IEnumerable<Sy80> GetCustomerSiteLocationsByZip(string companyCode, string zipCode)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            zipCode = ReplaceSingleCode(zipCode);
            ApplicationLogger.InfoLogger(
                "DataLayer :: GetCustomerSiteLocationsByZip : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
            string query = $"trim(lower({ZipField}))='{zipCode.ToLower().Trim()}'";
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var lstOfSy80 = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query);
            ApplicationLogger.InfoLogger($"Site Location count: {lstOfSy80.Count()}");
            return lstOfSy80;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerName"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsByCustomerName(string companyCode, string customerName)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            customerName = ReplaceSingleCode(customerName);
            ApplicationLogger.InfoLogger(
                "DataLayer :: GetCustomerSiteLocationsByCustomerName : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var sy80List = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey],
                $"rtrim(lower({CustomerNameField})){LikeOperator}'%{customerName.ToLower().Trim()}%'");
            return sy80List;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="siteNo"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsBySiteNo(string companyCode, string siteNo)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            siteNo = ReplaceSingleCode(siteNo);
            ApplicationLogger.InfoLogger("DataLayer :: GetCustomerSiteLocationsBySiteNo : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
            string query = $"trim(lower({SiteNo})){Like}'{siteNo.ToLower().Trim()}'";
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var lstOfSy80 = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query);
            ApplicationLogger.InfoLogger($"Site Location count: {lstOfSy80.Count()}");
            return lstOfSy80;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="faxNo"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsByFaxNo(string companyCode, string faxNo)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            faxNo = ReplaceSingleCode(faxNo);
            ApplicationLogger.InfoLogger(
                "DataLayer :: GetCustomerSiteLocationsByFaxNo : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
            string query = $"trim(lower({FaxField}))='{faxNo.ToLower().Trim()}'";
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var lstOfSy12 = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query);
            ApplicationLogger.InfoLogger($"Site Location count: {lstOfSy12.Count()}");
            return lstOfSy12;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="address"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsByAddress(string companyCode, string address)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            address = ReplaceSingleCode(address);
            string lowerAdress = address.ToLower().Trim();
            ApplicationLogger.InfoLogger(
                "DataLayer :: GetCustomerSiteLocationsByAddress : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
            string query =
                $"trim(lower({AddressOne})){Like}'%{lowerAdress}%' {OrOperator} trim(lower({AddressTwo})){Like}'%{lowerAdress}%' {OrOperator} trim(lower({AddressThree})){Like}'%{lowerAdress}%' {OrOperator} trim(lower({AddressFour})){Like}'%{lowerAdress}%' {OrOperator} trim(lower({AddressFive})){Like}'%{lowerAdress}%' {OrOperator} trim(lower({AddressSix})){Like}'%{lowerAdress}%' {OrOperator} trim(lower({AddressSeven})){Like}'%{lowerAdress}%' ";
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var lstOfSy80 = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query);
            ApplicationLogger.InfoLogger($"Site Location count: {lstOfSy80.Count()}");
            return lstOfSy80;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="telePhone"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsByTelephoneNo(string companyCode, string telePhone)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            telePhone = ReplaceSingleCode(telePhone);
            ApplicationLogger.InfoLogger(
                "DataLayer :: GetCustomerSiteLocationsByTelephoneNo : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
            string query = $"trim(lower({TelephoneField}))='{telePhone.ToLower().Trim()}'";
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var lstOfSy11 = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query);
            ApplicationLogger.InfoLogger($"Site Location count: {lstOfSy11.Count()}");
            return lstOfSy11;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="emailId"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsByEmailId(string companyCode, string emailId)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            emailId = ReplaceSingleCode(emailId);
            ApplicationLogger.InfoLogger("DataLayer :: GetCustomerSiteLocationsByEmailId : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
            string query = $"trim(lower({EmailId})){Like}'%{emailId.ToLower().Trim()}%'";
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}");
            var lstOfSy80 = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query);
            ApplicationLogger.InfoLogger($"Site Location count: {lstOfSy80.Count()}");
            return lstOfSy80;
        }
        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsByCustomerCode(string companyCode, string customerCode)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            customerCode = ReplaceSingleCode(customerCode);
            ApplicationLogger.InfoLogger("DataLayer :: GetCustomerSiteLocationsByCustomerCode : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            var sy80List = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], $"trim(lower({CustomerCodeField})){EqualOperator}'{customerCode.ToLower().Trim()}'");
            return sy80List;
        }
        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="serviceEngineerCode"></param>
        /// <returns>List of customer site locations</returns>
        public IEnumerable<Sy80> GetCustomerSiteLocationsByServiceEngineerCode(string companyCode, string serviceEngineerCode)
        {
            var dicTableName = new Dictionary<string, string>();
            companyCode = ReplaceSingleCode(companyCode);
            serviceEngineerCode = ReplaceSingleCode(serviceEngineerCode);
            ApplicationLogger.InfoLogger(
            "DataLayer :: GetCustomerSiteLocationsByServiceEngineerCode : Reading datalake table name from config");
            dicTableName = _configReader.GetDatabaseTableName(companyCode,  ParentCompanyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
                var sy80List = objDb.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey],
                    $"trim(lower({ServiceEngineerCodeField})){EqualOperator}'{serviceEngineerCode.ToLower().Trim()}'");
                return sy80List;
        }

        private string ReplaceSingleCode(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
