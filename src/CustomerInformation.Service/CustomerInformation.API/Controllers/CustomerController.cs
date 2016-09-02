using CustomerInformation.BusinessLayer.Interface;
using CustomerInformation.Common.Error;
using System.Linq;
using System.Web.Http;

namespace CustomerInformation.API.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : BaseContoller
    {
        readonly ICustomerManager _customerManager = null;// = new CustomerManager();
        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companyCode/{companyCode}")]
        public IHttpActionResult GetCustomers(string companyCode)
        {
            var customers = _customerManager.GetCustomers(companyCode);

            if (customers != null && customers.Any())
            {
                if (Error.Errors.Any())
                {
                    return Content(System.Net.HttpStatusCode.Ambiguous, Error.Errors);
                }

                return Ok(customers);
            }

            return Content(Error.Errors.First().StatusCode, Error.Errors.First().Message);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/customerCode/{customerCode}")]
        public IHttpActionResult GetCustomerById(string companyCode, string customerCode)
        {
            var customers = _customerManager.GetCustomerById(companyCode, customerCode);
            if (customers != null)
            {
                return Ok(customers);
            }

            return Content(Error.Errors.First().StatusCode, Error.Errors.First().Message);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerName"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/customerName/{customerName}")]
        public IHttpActionResult GetCustomerByName(string companyCode, string customerName)
        {
            var customers = _customerManager.GetCustomerByName(companyCode, customerName);

            if (customers != null && customers.Any())
            {
                return Ok(customers);
            }

            return Content(Error.Errors.First().StatusCode, Error.Errors.First().Message);
        }
    }
}
