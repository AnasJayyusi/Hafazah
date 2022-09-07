using Hafazah.API.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;


namespace Hafazah.API.Controllers
{
    public class CommonApiController : ApiController
    {
        CommonService _commonService;
        public CommonApiController()
        {
            _commonService = new CommonService();
        }
    }
}
