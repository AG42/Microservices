using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSiteLocation.DataLayer.Entities.Datalake;
using CustomerSiteLocation.DataLayer.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerSiteLocation.Model;
using Rhino.Mocks;

namespace CustomerSiteLocation.UnitTest
{
    [TestClass]
    public class DatalakeEntitiesUnitTest
    {
        //#region "Members"

        //private IDatalakeAdapter _datalakeAdapter;
        //private readonly List<Sy80> _sy80EntitiesList = new List<Sy80>();
        // CustomerSiteLocationModel _customerSiteLocationModel = new CustomerSiteLocationModel();
        //List<CustomerSiteLocationModel> _customerSiteLocationModelList = new List<CustomerSiteLocationModel>();

        //private readonly string _companyCode = "j1";

        //#endregion

        //#region "Initialization"

        ///// <summary>
        ///// Initialize all the required resources
        ///// </summary>
        //[TestInitialize]
        //public void Initialize()
        //{
        //    _datalakeAdapter = MockRepository.GenerateMock<IDatalakeAdapter>();
        //    _datalakeAdapter.ConnectionString = "TestConnectionString";
        //}

        //#endregion

        //#region "Unit Test Methods"
        ///// <summary>
        ///// Unit Testing Get table method
        ///// </summary>
        //[TestMethod]
        //public void GetDataFrofmTable()
        //{
        //    SetMockDataForCustomerSiteLocationHeaders();
        //    _datalakeAdapter.Stub(x => x.Get<Sy80>("")).IgnoreArguments().Return(_sy80EntitiesList);
        //    var data = new DatalakeEntities(_datalakeAdapter);
        //    var result = data.Get<Sy80>("table", _companyCode);
        //    Assert.IsNotNull(result);
        //}

        ///// <summary>
        ///// Unit testing Get method with where clause
        ///// </summary>
        //[TestMethod]
        //public void GetDataFromTableWithWhereCondition()
        //{
        //    _datalakeAdapter.Stub(x => x.Get<Sy80>("")).IgnoreArguments().Return(_sy80EntitiesList);
        //    var data = new DatalakeEntities(_datalakeAdapter);
        //    var result = data.Where<Sy80>("table", "Condition", _companyCode);
        //    Assert.IsNotNull(result);
        //}

        ///// <summary>
        ///// Unit testing Public properties
        ///// </summary>
        //[TestMethod]
        //public void PublicPropertiesTest()
        //{
        //    var data = new DatalakeEntities(_datalakeAdapter);
        //    data.ConnectionString = "TestConnectionString";
        //    Assert.IsNotNull(data.ConnectionString);
        //}

        //#endregion

        //#region Private Methods

        ///// <summary>
        ///// To create a mock data of customerSiteLocation header for unit test
        ///// </summary>
        //private void SetMockDataForCustomerSiteLocationHeaders()
        //{
        //    #region SampleCustomerSiteLocationHeaders
        //    //_customerSiteLocationModelList.Add(new CustomerSiteLocationModel()
        //    //{
        //    //    Name = "กระเบื้องกระดาษไทย",
        //    //    ERP_Customer_Code__c = "FI010",
        //    //    ERP_Site_Code__c = "0001",
        //    //    SVMXC__Street__c = "Siam Fiber Lamphang จังหวัด ลำปาง",
        //    //    SVMXC__City__c = string.Empty,
        //    //    County__c = string.Empty,
        //    //    SVMXC__State__c = string.Empty,
        //    //    Region__c = string.Empty,
        //    //    SVMXC__Country__c = "",
        //    //    SVMXC__Zip__c = "",
        //    //    SVMXC__Site_Fax__c = "0-2803-6733-5#114",
        //    //    SVMXC__Site_Phone__c = "0-2803-6733-5#114",
        //    //    SVMXC__Email__c = "",
        //    //    SVMXC__Latitude__c = "0.00000000",
        //    //    SVMXC__Longitude__c = "0.00000000",
        //    //    Altitude__c = "",
        //    //    SVMXC__Service_Engineer__c = string.Empty,
        //    //    Service_Engineer_Code__c = "",
        //    //    SVMXC__Stocking_Location__c = true

        //    //});

        //    _sy80EntitiesList.Add(new Sy80()
        //    {
        //        //Customer Code
        //        Sy80001 = "FI010",
        //        //Siteno
        //        Sy80002 = "0001",
        //        //Customer Name
        //        Sy80003 = "กระเบื้องกระดาษไทย",
        //        //ZipCode
        //        Sy80010 = "",
        //        //telephonno
        //        Sy80011 = "",
        //        //telefaxno
        //        Sy80012 = "",
        //        //Default Engineer
        //        Sy80046 = "",
        //        //City Code
        //        Sy80045 = "",
        //        //Country  Code
        //        Sy80048 = "",
        //        //Email
        //        Sy80049 = "",
        //        //Addressline1
        //        Sy80004 = "",
        //        //Addressline2
        //        Sy80005 = "",
        //        //Addressline3
        //        Sy80006 = "",
        //        //Addressline4
        //        Sy80007 = "",
        //        //Addressline5
        //        Sy80050 = "",
        //        //Addressline6
        //        Sy80051 = "",
        //        //Addressline7
        //        Sy80052 = "",
        //        //Longitude
        //        Sy80053 = "0.00000000",
        //        //Latitude
        //        Sy80054 = "0.00000000",
        //        //Altitude       
        //        Sy80055 = "0.00000000"
        //    });

        //    _sy80EntitiesList.Add(new Sy80()
        //    {
        //        Sy80001 = "1945",
        //        Sy80002 = "0004",
        //        Sy80003 = "HABTOOR STAFF ACCOM. DIP BLOCK H&J",
        //        Sy80004 = "DIP P.O. BOX 24454 Dubai",
        //        Sy80005 = "",
        //        Sy80006 = "",
        //        Sy80007 = "",
        //        Sy80010 = "",
        //        Sy80011 = "04-3995000",
        //        Sy80012 = "04-3992262",
        //        Sy80045 = "",
        //        Sy80046 = "",
        //        Sy80048 = "",
        //        Sy80049 = "",
        //        Sy80050 = "",
        //        Sy80051 = "",
        //        Sy80052 = "",
        //        Sy80053 = "",
        //        Sy80054 = "",
        //        Sy80055 = ""
        //    });


        //    #endregion
        //}

        //#endregion

    }
}
