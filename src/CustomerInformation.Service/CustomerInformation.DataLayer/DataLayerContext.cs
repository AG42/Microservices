using System;
using System.Collections.Generic;
using CustomerInformation.Common.Logger;
using CustomerInformation.DataLayer.Entities;
using DenodoAdapter;
using CustomerInformation.DataLayer.Interfaces;

namespace CustomerInformation.DataLayer
{
    public class DataLayerContext: IDataLayerContext
    {
        private const string LIKE_OPERATOR = "like";
        private const string CUSTOMERNAME_FIELD = "sl01002";
        private const string COMPANYCODE_PLACEHOLDER = "{CompanyCode}";
        private readonly IDenodoContext _denodoContext;
        private readonly string _viewUri;

        public DataLayerContext()
        {
            try
            {
                ConfigReader configReader = new ConfigReader(false);
                _denodoContext = new DenodoContext(configReader.BaseUri, configReader.DenodoUsername, configReader.DenodoPassword);
                _viewUri = configReader.CustomerInformationViewUri;
            }
            catch (Exception exception)
            {
                ApplicationLogger.Error(exception.Message,Category.Database, exception.StackTrace, null);
                throw;
            }
        }


        public List<CustomerMaster> GetCustomerByName(string companyCode, string customerName)
        {
            try
            {
                string companyViewUri = _viewUri.Replace(COMPANYCODE_PLACEHOLDER, companyCode);
                string filter = $"{CUSTOMERNAME_FIELD} {LIKE_OPERATOR} '%{customerName}%'";
                return _denodoContext.SearchData<CustomerMaster>(companyViewUri, filter);
            }
            catch (Exception exception)
            {
                ApplicationLogger.Error(exception.Message, Category.Database, exception.StackTrace, exception.InnerException.ToString());
                throw;
            }
        }
        public List<CustomerMaster> GetCustomers(string companyCode)
        {
            try
            {
                string companyViewUri = _viewUri.Replace(COMPANYCODE_PLACEHOLDER, companyCode);
                return _denodoContext.GetData<CustomerMaster>(companyViewUri);
            }
            catch (Exception exception)
            {
                ApplicationLogger.Error(exception.Message, Category.Database, exception.StackTrace, exception.InnerException.ToString());
                throw;
            }
        }
        public CustomerMaster GetCustomerById(string companyCode, string customerCode)
        {
            try
            {
                string companyViewUri = _viewUri.Replace(COMPANYCODE_PLACEHOLDER, companyCode);
                var data = _denodoContext.GetData<CustomerMaster>(companyViewUri, customerCode);
                return data;
            }
            catch (Exception exception)
            {
                ApplicationLogger.Error(exception.Message, Category.Database, exception.StackTrace,
                    exception.InnerException.ToString());
                throw;
            }
        }
    }
}
