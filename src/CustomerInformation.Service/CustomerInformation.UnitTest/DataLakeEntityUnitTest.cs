using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.Generic;
using CustomerInformation.DataLayer.Interfaces;
using CustomerInformation.DataLayer.Entities.Datalake;
using CustomerInformation.DataLayer.Adapters;

namespace CustomerInformation.UnitTest
{
    [TestClass]
    public class DataLakeEntityUnitTest
    {
        #region Declaration
        IDatalakeAdapter _datalakeAdapter;
        Sl01 _sl01 = new Sl01();
        public List<Sl01> CustomerList = new List<Sl01>();
        #endregion

        #region Methods
        [TestInitialize]
        public void Initialize()
        {
            _datalakeAdapter = MockRepository.GenerateMock<IDatalakeAdapter>();
            _datalakeAdapter.ConnectionString = "TestConnectionString   ";
        }


        [TestMethod]
        public void GetDataFromTable()
        {
            _datalakeAdapter.Stub(x => x.Get<Sl01>("")).IgnoreArguments().Return(CustomerList);
            var data = new DatalakeEntities(_datalakeAdapter);
            var result = data.Get<Sl01>("tableName");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDataFromTableWithWhereCondition()
        {
            _datalakeAdapter.Stub(x => x.Get<Sl01>("")).IgnoreArguments().Return(CustomerList);
            var data = new DatalakeEntities(_datalakeAdapter);
            var result = data.Where<Sl01>("tableName","whereCondition");
            Assert.IsNotNull(result);
        }

        #endregion

        //#region DataAdaptor Test Methods
        //public void GetInfoFromTable()
        //{
        //    DatalakeAdapter dataAdaptor= new DatalakeAdapter();

        //}
        //#endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForCustomerModels()
        {
            #region SampleDataCustomerMaster
            _sl01 = new Sl01
            {
                sl01001 = "C002",
                sl01002 = "Customer Test2",
                sl01003 = "Commer Zone",
                sl01004 = "Brad Pitt, Eric Bana, Orlando Bloom",
                sl01005 = "Pune",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "428201",
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            };

            CustomerList.Add(new Sl01()
            {
                sl01001 = "C006",
                sl01002 = "Customer 2",
                sl01003 = "Commer Zone",
                sl01004 = "Brad Pitt, Eric Bana, Orlando Bloom",
                sl01005 = "Pune",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "428201",
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"

                //String.Format(customerMaster.sl01003 + "{0}" + customerMaster.sl01004 + "{0}" + customerMaster.sl01005 + "{0}" + customerMaster.sl01099 + "{0}" +
                //                customerMaster.sl0194 + "{0}" + customerMaster.sl01195 + "{0}" + customerMaster.sl01196, Environment.NewLine),
            });
            CustomerList.Add(new Sl01()
            {
                sl01001 = "C009",
                sl01002 = "Customer 1",
                sl01003 = "Commer Zone 1234",
                sl01004 = "Qwerty Leonardo DiCaprio, Kate Winslet",
                sl01005 = "Pune qwerty",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "425001",
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            });
            CustomerList.Add(new Sl01()
            {
                sl01001 = "C004",
                sl01002 = "Customer 1",
                sl01003 = "Commer Zone 12",
                sl01004 = "Leonardo DiCaprio, Kate Winslet qwerty",
                sl01005 = "Pune",
                sl01099 = "AddressLine41",
                sl01152 = "CityPune Code",
                sl01011 = "9898543210",
                sl01022 = "USD",
                sl01083 = "405201",
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            });
            #endregion
        }
        #endregion
    }
}
