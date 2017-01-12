using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using System;
using CustomerProjectOrder.API.Resolver;
using System.Web.Http;
using CustomerProjectOrder.API;

namespace CustomerProjectOrder.UnitTest
{
    [TestClass]
    public class UnitResolverTest
    {
        #region "Members"
        HttpConfiguration config = new HttpConfiguration();
        protected readonly IUnityContainer _container = new UnityContainer();
        private UnityResolver _unityResolver;
        #endregion

        #region "Initialization"

        /// <summary>
        /// Initialize all the required resources
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _unityResolver = new UnityResolver(_container); 
        }
        #endregion


        #region "Unit Test Methods"   

        [TestMethod]
        public void GetServiceTest()
        {
            // Type  serviceType=new Int32().GetType();
            Type t = typeof(UnityResolver);
            Object obj = _unityResolver.GetService(t);
            Assert.IsNotNull(obj);

        }

        [TestMethod]
        public void GetServicesTest()
        {
            // Type  serviceType=new Int32().GetType();
            Type t = typeof(UnityResolver);
            Object obj = _unityResolver.GetServices(t);
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void BeginScopeTest()
        {
            Object obj = _unityResolver.BeginScope();
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void DisposeTest()
        {
            _unityResolver.Dispose();
            Assert.IsTrue(true);
        }


        [TestMethod]
        public void UnityConfigTest()
        {
            // Type  serviceType=new Int32().GetType();
            UnityConfig.RegisterComponents(config);
            Assert.IsTrue(true);
        }

        #endregion
    }
}
