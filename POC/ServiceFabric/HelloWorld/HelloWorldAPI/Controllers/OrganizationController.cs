using System;
using System.Collections.Generic;
using System.Web.Http;
using Denodo;
using Models;
using System.Net.Http;

namespace DenodoTestingApi.Controllers
{
    [RoutePrefix("api/organizations")]
    public class OrganizationController : ApiController
    {
        private readonly IDenodoContext _denodoContext;

        public OrganizationController()
        {
            _denodoContext = new DenodoContext("http://c201mf92:9090/server/poc/", "admin", "admin");
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            List<Organization> organizations = _denodoContext.GetData<Organization>("wtorganization/views/wtorganization");
            return Ok<List<Organization>>(organizations);
        }

        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            return Ok<Organization>(_denodoContext.GetData<Organization>("wtorganization/views/wtorganization", id));
        }

        public IHttpActionResult Post(Organization organization)
        {
            try
            {
                if (_denodoContext.Insert("wtorganization/views/wtorganization", organization))
                    return Ok();
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                if (_denodoContext.Delete("wtorganization/views/wtorganization", id))
                    return Ok();
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Put(string id, Organization organization)
        {
            try
            {
                if (_denodoContext.Update("wtorganization/views/wtorganization", id, organization))
                    return Ok();
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
