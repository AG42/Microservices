using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using CustomerInformation.Common.Logger;
using CustomerInformation.DataLayer.Entities;
using DenodoAdapter;
using CustomerInformation.DataLayer.Interfaces;

namespace CustomerInformation.DataLayer
{
    public interface ITest
    { string DomainUri { get; set; } }
    public class DataLayerContext : IDataLayerContext, ITest
    {
        private const string LIKE_OPERATOR = "like";
        private const string EQUAL_OPERATOR = "=";
        private const string CUSTOMERNAME_FIELD = "sl01002";
        private const string CUSTOMERCODE_FIELD = "sl01001";
        private readonly IDenodoContext _denodoContext;
        private readonly string _denodoUrl;
        private readonly ConfigReader _configReader;
        public string DomainUri { get; set; }

        public DataLayerContext(params object[] theObjects)
        {
            try
            {
                _configReader = new ConfigReader();
                _denodoContext = new DenodoContext(_configReader.BaseUri, _configReader.DenodoUsername,
                    _configReader.DenodoPassword);

                if (theObjects.Length > 0)
                    _denodoContext = (IDenodoContext)theObjects[0];

                _denodoUrl = _configReader.BaseUri;
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                    exception.InnerException);
                throw;
            }
        }

        public List<CustomerMaster> GetCustomerByName(string companyCode, string customerName)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomerByName :: Reading view uri from config");
                string companyViewUri = _configReader.GetDenodoViewUri(companyCode);
                string filter = $"lower({CUSTOMERNAME_FIELD}) {LIKE_OPERATOR} '%{customerName.ToLower()}%'";
                ApplicationLogger.InfoLogger($"Denodo url: [{_denodoUrl}{companyViewUri}] filter: [{filter}]");
                var customers = _denodoContext.SearchData<CustomerMaster>(companyViewUri, filter);
                ApplicationLogger.InfoLogger($"Customers count: {customers.Count}");
                return customers;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        public List<CustomerMaster> GetCustomers(string companyCode)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomers :: Reading view uri from config");
                string companyViewUri = _configReader.GetDenodoViewUri(companyCode);
                ApplicationLogger.InfoLogger($"Denodo url: [{_denodoUrl}{companyViewUri}]");
                var customers = _denodoContext.GetData<CustomerMaster>(companyViewUri);
                ApplicationLogger.InfoLogger($"Customers count: {customers.Count}");
                return customers;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        public CustomerMaster GetCustomerById(string companyCode, string customerCode)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomerById :: Reading view uri from config");
                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode)}?{CUSTOMERCODE_FIELD}{EQUAL_OPERATOR}{customerCode}";
                ApplicationLogger.InfoLogger($"Denodo url: [{_denodoUrl}{finalViewUri}]");
                var data = _denodoContext.GetData<CustomerMaster>(finalViewUri);
                return data.FirstOrDefault();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        private static void LogException(Exception exception)
        {
            if (exception.GetType() == typeof(HttpResponseException))
            {
                HttpResponseException responseException = (HttpResponseException)exception;
                ApplicationLogger.Errorlog(responseException.Response.ReasonPhrase, Category.Database,
                    responseException.Response.Content.ReadAsStringAsync().Result, responseException.InnerException);
                ApplicationLogger.InfoLogger(
                    $"Denodo Adapter exception :: {responseException.Response.ReasonPhrase}");
                throw responseException;
            }
            ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                exception.InnerException);
        }
    }
}
