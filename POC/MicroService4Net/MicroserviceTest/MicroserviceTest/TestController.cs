using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroserviceTest
{
    public class TestController: ApiController
    {
        [Route("")]
        public IHttpActionResult GetOpenion()
        {
            return Ok(new { Msg = "This is first MICROSERVICE4NET POC" } );
        }
    }
}
