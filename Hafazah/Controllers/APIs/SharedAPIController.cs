using Hafazah.Model;
using Hafazah.Model.Dtos;
using Hafazah.Model.Entities.Program;
using Hafazah.Model.Enums;
using Hafazah.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [HttpGet]
        [Route("GetRegistrationStatus")]
        public IHttpActionResult GetRegistrationStatus()
        {
            try
            {
                return Ok(_svc.GetRegistrationStatus());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("RegisterNewMember")]
        public IHttpActionResult RegisterNewMember(Member member)
        {
            try
            {
                _svc.AddMember(member, out List<string> validations);

                if (validations.Any())
                    return Content(HttpStatusCode.NotAcceptable, new ValidationError() { ErrorList = validations });

                return Ok(@"'isSucceeded':'true'");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("UpdateStudentProfileInfo")]
        public IHttpActionResult UpdateStudentProfileInfo(MemberProfileInfo memberProfileInfo)
        {
            try
            {
                if (_svc.UpdateMember(memberProfileInfo))
                    return Ok(@"'isSucceeded':'true'");

                return Content(HttpStatusCode.NotAcceptable, ErrorCode.MemberNotExists.ToString());

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("SetPageNumberWithSentDate")]
        public IHttpActionResult SetPageNumberWithSentDate(int pageNumber, DateTime sendDate)
        {
            try
            {
                return Ok(_svc.SetPageNumberWithSentDate(pageNumber, sendDate, User.Identity.GetUserName()));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("GetTokenByUserName")]
        public IHttpActionResult GetTokenByUserName(string username)
        {
            string token = _svc.GetToken(username);
            return Ok(token);
        }

        [HttpGet]
        [Route("SetTokenByUserName")]
        public IHttpActionResult SetTokenByUserName(string username, string token)
        {
            try
            {
                _svc.SetToken(username, token);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

            return Ok(@"{'isSucceeded':'true'}");
        }

        [HttpGet]
        [Route("GetDDLsOptions")]
        public IHttpActionResult GetDDLsOptions()
        {
            try
            {
                var ddls = _svc.GetDDLsOptions();
                return Ok(ddls);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("GettingLazyMemberWithCounter")]
        public IHttpActionResult GettingLazyMemberWithCounter()
        {
            try
            {
                List<Member> lazyMembers = _svc.GettingLazyMemberWithCounter();
                return Ok(lazyMembers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("GettingPaths")]
        public IHttpActionResult GettingPaths()
        {
            try
            {
                List<Level> paths = _svc.GettingPaths();
                return Ok(paths);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("UpdateProfilePicture")]
        public IHttpActionResult UpdateProfilePicture(string username, string imgBase64)
        {
            try
            {
                return Ok(_svc.UpdateProfilePicture(username, imgBase64));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("IsUserNameExists")]
        public IHttpActionResult IsUserNameExists(string username)
        {
            try
            {
                return Ok(_svc.IsUsernameToken(username));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("IsEmailAlreadyUsed")]
        public IHttpActionResult IsEmailAlreadyUsed(string email)
        {
            try
            {
                return Ok(_svc.IsEmaiAlreadylExists(email));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("IsPhoneNumberAlreadyUsed")]
        public IHttpActionResult IsPhoneNumberAlreadyUsed(string phonenumber)
        {
            try
            {
                return Ok(_svc.IsPhoneNumberAlreadylExists(phonenumber));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        #region Core API

        [HttpGet]
        [Route("GetUserProgram")]
        public IHttpActionResult GetUserProgram(string username)
        {
            try
            {

                return Ok(_svc.GetUserProgram(username));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("GetHomeWorks")]
        public IHttpActionResult GetHomeWorks(ProgramType programType, int selectedNumber)
        {
            try
            {
                return Ok(_svc.GetHomeWorks(programType, selectedNumber));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion



    }
}
