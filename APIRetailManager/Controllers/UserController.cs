using APIRetailManager.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XRMDataManager.Library.Data;
using XRMDataManager.Library.Internal.DatatAccess;
using XRMDataManager.Library.Models;

namespace APIRetailManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        // GET: api/User
        [HttpGet]
        public UserModel GetUserById()
        {
            ISqlDataAccess sqlData = new SqlDataAccess();
            UserData userData = new UserData(sqlData);
            string userid = RequestContext.Principal.Identity.GetUserId();
            var result = userData.GetUserByUd(userid);
            return result;
        }
     
    }
}
