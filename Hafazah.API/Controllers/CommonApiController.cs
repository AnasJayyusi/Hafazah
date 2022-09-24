using Hafazah.API.Services;
using System.Web.Http;


namespace Hafazah.API.Controllers
{
    [RoutePrefix("common")]
    public class CommonApiController : ApiController
    {
        CommonService _commonService;
        public CommonApiController()
        {
            _commonService = new CommonService();
        }

        [HttpGet]
        [Route(nameof(Test))]
        public string Test()
        {
            return "Hello";

        }
    }
}
