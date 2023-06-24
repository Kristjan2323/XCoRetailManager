using APIRetailManager.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using XRMDataManager.Library.Data;
using XRMDataManager.Library.Internal.DatatAccess;
using XRMDataManager.Library.Models;

namespace APIRetailManager.Controllers
{
    [Authorize]
  
    [RoutePrefix("api/Admin")]
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

        [HttpGet]
        [Route("GetAllRoles")]
        public List<string> GetAllRoles()
        {
            //  var roles = default;
            List<string> roleNames = new List<string>();
            using (var context = new ApplicationDbContext())
            {
               var roles = context.Roles;
                foreach (var roleName in roles)
                {
                    roleNames.Add(roleName.Name);
                }
            }
            return roleNames;   
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetUserInfo")]
        public ApplicationUserModel GetUserSpecificData()
        {
            ISqlDataAccess sqlData = new SqlDataAccess();
            UserData userData = new UserData(sqlData);
            string userid = RequestContext.Principal.Identity.GetUserId();
            var users = GetAllRolesForUsers();
            var userInfo = GetAllRolesForUsers().Where(x => x.Id == userid).FirstOrDefault();
            return userInfo;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("GetAllRolesForUsers")]
        public List<ApplicationUserModel> GetAllRolesForUsers()
        {
        
            List<ApplicationUserModel> usersModel = new List<ApplicationUserModel>();
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var users = userManager.Users.ToList();
                var roles = context.Roles.ToList();

                foreach (var user in users)
                {
                    ApplicationUserModel u = new ApplicationUserModel
                    {
                        Id = user.Id,
                        Email = user.Email
                    };
                    foreach (var role in user.Roles)
                    {
                        u.Roles.Add(role.RoleId, roles.Where(x => x.Id == role.RoleId).FirstOrDefault().Name);
                       
                    }
                    usersModel.Add(u);
                }
              
            }

            return usersModel;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddRole")]
        public void AddRole(UserRolePairModel userRole)
        {
            using(var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                userManager.AddToRole(userRole.UserId, userRole.RoleId);
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("RemoveRole")]
        public void RemoveRole(UserRolePairModel userRole)
        {
            using( var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                userManager.RemoveFromRole(userRole.UserId, userRole.RoleId);
            }
        }

    }
}
