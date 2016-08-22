using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Web.Http;
using HelloWorldAPI.Controllers;
using System.Net;
using System.Net.Http;
using FizzWare.NBuilder;
using HelloWorldAPI.Models;
using Rhino.Mocks;
using System.Web.Http.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using DenodoTestingApi.Controllers;
using Denodo;
using Models;
using System.Web.Http.Results;

namespace HelloWorld.Test  
{
   // [TestClass]
    public class OrganizationContollerTest
    {

        private OrganizationController controller = null;
        [TestInitialize]
        public void Initialize()
        {
            // _client = new HttpClient { BaseAddress = new Uri("http://localhost:5001/") };
            controller = new OrganizationController();
            
            //controller.Request = new HttpRequestMessage();
            //controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        //[TestMethod]
        //public void GetOrganizationListTest()
        //{
        //    var mockContext = MockRepository.GenerateMock<IDenodoContext>();
        //    mockContext.Expect(x => x.GetData<Organization>("")).Return(GetOrganizationList());
                
            

        //    var result = controller.Get();
        //    NegotiatedContentResult<string> negResult = Assert.IsType<NegotiatedContentResult<List<Organization>>(result);
        //    Assert.Equal(HttpStatusCode.Accepted, negResult.StatusCode);

        //    Assert.AreEqual(result, HttpStatusCode.OK);
        //    //Assert.AreEqual("Hello World !!!", result.Content.ReadAsAsync<string>().Result);
        //}

        //[TestMethod]
        //public void GetAllProductListTest()
        //{
        //    // var products = Builder<Product>.CreateListOfSize(4).Build();
        //    // var mockRepository = MockRepository.GenerateMock<>
        //    var result = controller.GetAllProduct();
        //    // installb System.Net.Http.Formatting - for ReadAsAsync
        //    var data = result.Content.ReadAsAsync<List<Product>>().Result;
        //    Assert.IsNotNull(data);
        //    Assert.AreEqual(5, data.Count);
        //}

        private List<Organization> GetOrganizationList()
        {
            var organizations = Builder<Organization>.CreateListOfSize(5).Build();
            return organizations as List<Organization>;
        }
    }
}
