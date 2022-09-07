using Hafazah.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hafazah.API.Services
{
    public class CommonService
    {
        CommonRepository _commonRepository;
        public CommonService()
        {
            _commonRepository = new CommonRepository();
        }

    }
}