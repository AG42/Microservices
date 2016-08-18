using HelloWorldAPI.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Net.Http;
using System.Net;

namespace HelloWorldAPI.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetHelloWorld()
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Hello World !!!");
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

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [HttpGet]
        [Route("{productId}")]
        public HttpResponseMessage Product(int productId)
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, GetAllProduct().Content.ReadAsAsync<List<Product>>().Result.Where(x => x.Id == productId).FirstOrDefault());
        }

        // POST api/values 
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage PostAdd([FromBody]Product productModel)
        {
            var products = GetAllProduct().Content.ReadAsAsync<List<Product>>();

            if (productModel == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            products.Result.Add(productModel);
            return Request.CreateResponse(HttpStatusCode.OK, products.Result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public HttpResponseMessage PutAdd(int id, [FromBody]Product productModel)
        {
            var products = GetAllProduct().Content.ReadAsAsync<List<Product>>().Result;

            if (productModel == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            products.ForEach(x => {
                if (x.Id == id)
                {
                    x.Id = id;
                    x.Name = productModel.Name;
                    x.Type = productModel.Type;
                    x.Code = productModel.Code;
                }
            });

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var products = GetAllProduct().Content.ReadAsAsync<List<Product>>().Result;

            Product removeItem = null;
            products.ForEach(x => {
                if (x.Id == id)
                    removeItem = x;
            });
            products.Remove(removeItem);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        //// PUT api/values/5 
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5 
        //public void Delete(int id)
        //{
        //}
    }
}
