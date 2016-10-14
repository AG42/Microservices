using System.Collections.Generic;
using ServiceOrder.Model;
using ServiceOrder.DataLayer.Entities.Datalake;
using System;
using ServiceOrder.Common;

namespace ServiceOrder.BusinessLayer
{
    class Converter
    {
        // List<SM03> _sm03, List<SM05> _sm05, List<SM07> _sm07, 
        public static ServiceOrderModel Convert(SM01 sm01, string companyCode)
        {
          var data = new ServiceOrderModel()
            {
                Id = GetId(companyCode, sm01.SM01210, sm01.SM01001),
                ERP_SO_Key__c = GetERPSOKey(companyCode, sm01.SM01001),
                CurrencyIsoCode = sm01.SM01049,
                // SVMX_MD_Problem_Summary__c = string.Format(sm01.SM01011 + "{0}" + sm01.SM01012 + "{0}" + sm01.SM01013 + "{0}" + sm01.SM01014 + "{0}", Environment.NewLine),
                ERP_Component__c = sm01.SM01007,
                SVMXC__Invoice_Number__c = sm01.SM01038,
                ERP_WO_Status__c = sm01.SM01064.ToUpper() == "OTHER" ? null : sm01.SM01064,
                SVMXC__Order_Type__c = sm01.SM01097,
                SVMXC__Company__c = sm01.SM01003,
                Bill_To_Account__c = sm01.SM01002,
                Ordering_Customer__c = sm01.SM01004,
                ERP_Servicing_Site_Code__c = sm01.SM01071,
                ERP_Project_Contract_Number__c = sm01.SM01010,
                //need clarificaton on substring condition from iscala team
                ERP_Project_Key__c = sm01.SM01113.Substring(0, sm01.SM01113.Length),
                ERP_Technician_Code__c = sm01.SM01016,
                //need clarificaton on substring condition from iscala team
                ERP_Cost_Center__c = sm01.SM01113.Substring(0, sm01.SM01113.Length),
                ERP_Master_SONumber__c = sm01.SM01078,
                //need clarificaton on substring condition from iscala team
                ERP_Service_Order_Number__c = sm01.SM01210,
                ERP_Customer_PO_Number__c = sm01.SM01005,
                ERP_Invoice_date__c = GetInvoiceDate(sm01.SM01037),
                ERP_Payment_Terms_Code__c = sm01.SM01036,
                ERP_Delivery_Blocked__c = System.Convert.ToBoolean(System.Convert.ToInt16(string.IsNullOrEmpty(sm01.SM01205)?"0":sm01.SM01205)),
                ERP_Credit_Check_Passed__c = System.Convert.ToBoolean(System.Convert.ToInt16(string.IsNullOrEmpty(sm01.SM01209)?"0": sm01.SM01209)),
                //Clarification required on logic for exchange rate from iscala
                ERP_CRM_Exchange_Rate___c = System.Convert.ToDouble(string.IsNullOrEmpty(sm01.SM01146)?"0":sm01.SM01146),
                ERP_Allocated_Team_code__c = sm01.SM01110,
                Job_Type__c = GetJobType(sm01.SM01006),
                ERP_Company_Code__c = companyCode
            };

            if (!string.IsNullOrWhiteSpace(sm01.SM01011))
                data.SVMX_MD_Problem_Summary__c = sm01.SM01011;
            if (!string.IsNullOrWhiteSpace(sm01.SM01012))
                data.SVMX_MD_Problem_Summary__c = ($"{data.SVMX_MD_Problem_Summary__c} {Environment.NewLine} {sm01.SM01012}");
            if (!string.IsNullOrWhiteSpace(sm01.SM01013))
                data.SVMX_MD_Problem_Summary__c = ($"{data.SVMX_MD_Problem_Summary__c} {Environment.NewLine} {sm01.SM01013}");
            if (!string.IsNullOrWhiteSpace(sm01.SM01014))
                data.SVMX_MD_Problem_Summary__c = ($"{data.SVMX_MD_Problem_Summary__c} {Environment.NewLine} {sm01.SM01014}");

            return data;
        }

        public static List<ServiceOrderLineLaborModel> ConvertLabour(IEnumerable<SM03> _sm03List, SM01 sm01, string companyCode)
        {

            var serviceOrderLineLaborModels = new List<ServiceOrderLineLaborModel>();
            foreach (var labor in _sm03List)
                serviceOrderLineLaborModels.Add(ConvertLabor(companyCode, labor, sm01));

            return serviceOrderLineLaborModels;
        }

        public static ServiceOrderLineLaborModel ConvertLabor(string companyCode, SM03 _sm03, SM01 sm01)
        {
            return new ServiceOrderLineLaborModel()
            {
                Id = GetId(companyCode, _sm03.SM03071, _sm03.SM03001),
                ERP_SO_Line_Number__c = GetERPSOLineNumber(companyCode, _sm03.SM03001, Constants.Labor, _sm03.SM03002),
                //Need clarification from iscala team
                SVMXC__Service_Order__c = !string.IsNullOrWhiteSpace(sm01.SM01210) ? sm01.SM01210 : null,
                Line_Cost_per_unit__c = System.Convert.ToDouble(_sm03.SM03012),
                SVMXC__Line_Status__c = Constants.Confirmed,
                SVMXC__Line_Type__c = "Labor",
                Invoice_Number__c = _sm03.SM03024,
                Invoice_Date__c = GetInvoiceDate(_sm03.SM03023),
                ERP_Billable_Hours__c = System.Convert.ToDouble(_sm03.SM03028)
            };
        }

        public static List<ServiceOrderLineMOItemModel> ConvertMOItem(IEnumerable<SM07> _sm07List, SM01 sm01, string companyCode)
        {

            var serviceOrderLineMOItemModels = new List<ServiceOrderLineMOItemModel>();
            foreach (var moItem in _sm07List)
                serviceOrderLineMOItemModels.Add(ConvertMOItem(companyCode, moItem, sm01));

            return serviceOrderLineMOItemModels;
        }

        public static ServiceOrderLineMOItemModel ConvertMOItem(string companyCode, SM07 _sm07, SM01 sm01)
        {
            return new ServiceOrderLineMOItemModel()
            {
                Id = GetId(companyCode, _sm07.SM07140, _sm07.SM07001),
                ERP_SO_Line_Number__c = GetERPSOLineNumber(companyCode, _sm07.SM07001, Constants.Parts, _sm07.SM07002),
                //Need clarification from iscala team
                SVMXC__Service_Order__c = !string.IsNullOrWhiteSpace(sm01.SM01210) ? sm01.SM01210 : null,
                SVMXC__Line_Type__c = "Parts",
                SVMXC__Actual_Price2__c = GetActualPrice(sm01.SM01097, _sm07.SM07025),
                Line_Cost_per_unit__c = System.Convert.ToDouble(_sm07.SM07010),
                ERP_Price_Multiplier__c = CalculateCost(_sm07.SM07017, _sm07.SM07008),
                ERP_Cost_Multiplier__c = CalculateCost(_sm07.SM07017, _sm07.SM07011),
                ERP_Stock_Code__c = _sm07.SM07004,
                SVMXC__Line_Status__c = Constants.Confirmed,
                ERP_Tax_code__c = _sm07.SM07019,
                SVMXC__Quantity_Shipped2__c = System.Convert.ToDouble(_sm07.SM07017),
                Invoice_Number__c = _sm07.SM07043,
                Invoice_Date__c = GetInvoiceDate(_sm07.SM07042)
            };
        }

        public static List<ServiceOrderLineToolModel> ConvertTool(IEnumerable<SM05> _sm05List, SM01 sm01, string companyCode)
        {

            var serviceOrderLineToolModels = new List<ServiceOrderLineToolModel>();
            foreach (var tool in _sm05List)
                serviceOrderLineToolModels.Add(ConvertTool(companyCode, tool, sm01));

            return serviceOrderLineToolModels;
        }

        public static ServiceOrderLineToolModel ConvertTool(string companyCode, SM05 _sm05, SM01 sm01)
        {
            return new ServiceOrderLineToolModel()
            {
                Id = GetId(companyCode, _sm05.SM05078, _sm05.SM05001),
                ERP_SO_Line_Number__c = GetERPSOLineNumber(companyCode, _sm05.SM05001, Constants.Expenses, _sm05.SM05002),
                ////Need clarification from iscala team
                SVMXC__Service_Order__c = !string.IsNullOrWhiteSpace(sm01.SM01210) ? sm01.SM01210 : null,
                SVMXC__Line_Type__c = "Expenses",

                ERP_Cost_Code_Number__c = _sm05.SM05004,
                Description__c = _sm05.SM05005,
                ERP_Tax_code__c = _sm05.SM05012,
                ERP_Resource_Code__c = _sm05.SM05028,
                SVMXC__Actual_Price2__c = System.Convert.ToDouble(_sm05.SM05040),
                Line_Cost_per_unit__c = System.Convert.ToDouble(_sm05.SM05009),
                SVMXC__Actual_Quantity2__c = System.Convert.ToDouble(_sm05.SM05024),
                ERP_Actual_Expense_Date__c = DateTimeOffset.Parse(_sm05.SM05053).UtcDateTime,
                Invoice_Number__c = _sm05.SM05020,
                Invoice_Date__c = GetInvoiceDate(_sm05.SM05019),
                SVMXC__Line_Status__c = Constants.Confirmed
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        #region ServiceOrder Conversions

        public static string GetId(string companyCode, string externalId, string serviceOrderNo)
        {
            if (string.IsNullOrWhiteSpace(externalId))
                return GetERPSOKey(companyCode, serviceOrderNo);
            return externalId;
        }
        public static string GetERPSOKey(string companyCode, string serviceOrderNo)
        {
            return "I" + companyCode + serviceOrderNo;
        }

        public static DateTimeOffset? GetInvoiceDate(string invoiceDate)
        {
            DateTime defaultDate = System.Convert.ToDateTime("1900-01-01 00:00:00.0");
            if (System.Convert.ToDateTime(invoiceDate).Date == defaultDate.Date || string.IsNullOrEmpty(invoiceDate))
                return null;
            return DateTimeOffset.Parse(invoiceDate).UtcDateTime;
        }

        public static string GetJobType(string stockItemCode)
        {
            switch (stockItemCode.ToUpper())
            {
                case "S-BAS":
                    return "BAS";
                case "S-SEC":
                    return "Security";
                case "S-EPS":
                    return "Fire";
                case "S-HVAC":
                    return "Mechanical (HVAC)";
                case "S-YORK":
                    return "Mechanical (York)";
                case "S-ELEC":
                    return "Electrical";
                default:
                    return null;
            }
        }

        public static string GetERPSOLineNumber(string companyCode, string serviceOrderNo, string lineNumber, string serviceLine)
        {
            return "I" + companyCode + serviceOrderNo + serviceLine + lineNumber;
        }

        public static double? GetActualPrice(string MOType, string unitPriceOCU)
        {
            if (MOType.ToLower() == "system warranty" || MOType.ToLower() == "system commissioning" || MOType.ToLower() == "retrofit")
                return System.Convert.ToDouble(string.IsNullOrEmpty(unitPriceOCU)?"0":unitPriceOCU);
            return null;
        }

        public static double CalculateCost(string qty, string price)
        {
            double cost = System.Convert.ToDouble(string.IsNullOrEmpty(qty)?"0":qty) * System.Convert.ToDouble(string.IsNullOrEmpty(price)?"0":price);
            return cost;
        }


      
        #endregion
    }
}
