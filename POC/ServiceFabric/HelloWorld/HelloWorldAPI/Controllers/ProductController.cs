using HelloWorldAPI.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Net.Http;

namespace HelloWorldAPI.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        [HttpGet]  
        [Route("")]
        public HttpResponseMessage GetHelloWorld()
        {
           return  Request.CreateResponse(System.Net.HttpStatusCode.OK, "Hello World !!!");
        }

        [HttpGet]
        [Route("all")]        
        public HttpResponseMessage GetAllProduct()
        {
            var products = new List<Product>();
            products.Add(new Product { Id = 1, Name = "Laptop", Code = "LP", Type = "111" });
            products.Add(new Product { Id = 2, Name = "Mobile", Code = "MB", Type = "222" });
            products.Add(new Product { Id = 3, Name = "iPad", Code = "iP", Type = "333" });
            products.Add(new Product { Id = 4, Name = "Tablet", Code = "TL", Type = "444" });
            products.Add(new Product { Id = 5, Name = "Desktop", Code = "DT", Type = "555" });

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, products);
        }

        [HttpGet]
        [Route("{productId}")]       
        public Product Product(int productId)
        {
            return GetAllProduct().Content.ReadAsAsync<List<Product>>().Result.Where(x => x.Id == productId).FirstOrDefault();
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
