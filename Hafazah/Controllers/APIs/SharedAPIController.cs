using Hafazah.Model;
using Hafazah.Services;
using Newtonsoft.Json;
using System;
using System.Web.Http;

namespace Hafazah.Controllers.APIs
{
    public class SharedAPIController : ApiController
    {
        private SharedServices _svc;
        public SharedAPIController()
        {
            _svc = new SharedServices();
        }
        [HttpPost]
        [Route("RegisterNewMember")]
        public IHttpActionResult RegisterNewMember(Member member)
        {
            try
            {
                _svc.AddNewMember(member);
            }
            catch (Exception exp)
            {
                return InternalServerError(exp);
                throw;
            }
            

            return Ok(true);
        }

    }
}
