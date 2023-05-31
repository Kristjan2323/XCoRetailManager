using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRMDataManager.Library.Internal.DatatAccess;
using XRMDataManager.Library.Models;

namespace XRMDataManager.Library.Data
{
    public class UserData
    {
        private ISqlDataAccess _dataAccess;
        public UserData(ISqlDataAccess sqlAccess)
        {
            _dataAccess = sqlAccess;    
        }

        public UserModel GetUserByUd(string userId)
        {
            var result = _dataAccess.LoadData<UserModel,dynamic>("sp_GetUserById", new {userId});
            return result.First();
        }
    }
}
