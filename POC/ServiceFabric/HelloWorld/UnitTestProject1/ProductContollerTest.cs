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

namespace HelloWorld.Test  
{
    [TestClass]
    public class ProductContollerTest
    {
        private ProductController controller = null;
        [TestInitialize]
        public void Initialize()
        {
            // _client = new HttpClient { BaseAddress = new Uri("http://localhost:5001/") };
            controller = new ProductController();
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        [TestMethod]
        public void GetHelloWorldString()
        {
            var result = controller.GetHelloWorld();
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual("Hello World !!!", result.Content.ReadAsAsync<string>().Result);
        }

        [TestMethod]
        public void GetAllProductListTest()
        {
            // var products = Builder<Product>.CreateListOfSize(4).Build();
            // var mockRepository = MockRepository.GenerateMock<>
            var result = controller.GetAllProduct();
            // installb System.Net.Http.Formatting - for ReadAsAsync
            var data = result.Content.ReadAsAsync<List<Product>>().Result;
            Assert.IsNotNull(data);
            Assert.AreEqual(5, data.Count);
        }
    }
}
