using System.Collections.Generic;
using CustomerProjectOrder.Model;
using CustomerProjectOrder.DataLayer.Entities.Datalake;

namespace CustomerProjectOrder.BusinessLayer
{
    /// <summary>
    /// This class is used to convert denodo entity to the respective Business entity.
    /// i.e mapping CustomerProjectOrderHeader --> CustomerProjectOrderModel
    /// </summary>
    class Converter
    {
        #region "Members" 
        private const string DefaultStatus = "Working";
        private const string DefaultOrigin = "Integration";
        private const string DefaultRecordType = "Projects";
        #endregion

        #region "Public Methods"
        /// <summary>
        /// This method takes CustomerProjectOrderHeaders denodo entities and Company code as a input and converts it to 
        /// CustomerProjectOrderModel business entities
        /// </summary>
        /// <param name="customerProjectOrderHeaders"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public static List<CustomerProjectOrderModel> ConvertToCustomerProjectOrderModels(
            IEnumerable<Pr01> customerProjectOrderHeaders, string companyCode)
        {
            var customerProjectOrderModels = new List<CustomerProjectOrderModel>();
            foreach (var customerProjectHeader in customerProjectOrderHeaders)
            {
                customerProjectOrderModels.Add(ConvertToCustomerProjectOrderModel(customerProjectHeader,companyCode));
            }
            return customerProjectOrderModels;
        }
        /// <summary>
        /// This Method converts the customer project order header denodo entity and company code as input and converts it to
        /// CusotmerProjectModel business entity 
        /// </summary>
        /// <param name="customerProjectOrderHeader"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public static CustomerProjectOrderModel ConvertToCustomerProjectOrderModel(Pr01 customerProjectOrderHeader, string companyCode)
        {
            string erpProjectKey = "I" + companyCode + customerProjectOrderHeader.Pr01001;
            return new CustomerProjectOrderModel()
            {
                ERP_Project_Number__c = customerProjectOrderHeader.Pr01001,
                Subject = customerProjectOrderHeader.Pr01009,
                Description = customerProjectOrderHeader.Pr01010 + customerProjectOrderHeader.Pr01011,
                ERP_Project_Start_Date__c = customerProjectOrderHeader.Pr01067,
                ERP_Project_End_Date__c = customerProjectOrderHeader.Pr01069,
                Account = customerProjectOrderHeader.Pr01003,
                ERP_Customer_PO_Number__c = customerProjectOrderHeader.Pr01106,
                Status = DefaultStatus,
                Origin = DefaultOrigin,
                RecordType = DefaultRecordType,
                ERP_Project_Key__c = erpProjectKey?.ToUpper()
            };
        }

        #endregion
    }
}
