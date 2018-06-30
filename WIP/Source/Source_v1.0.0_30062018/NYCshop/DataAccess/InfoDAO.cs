using NYCshop.CustomTypes;
using NYCshop.Models;
using NYCshop.Resources.ActionMessageInController;
using NYCshop.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.DataAccess
{
    /// <summary>
    /// Lấy ra các thông tin từ CSDL
    /// </summary>
    public class InfoDAO
    {
        private ExLoverShopDb db = new ExLoverShopDb();

        public InfoDAO()
        {

        }

        /// <summary>
        /// Lấy ra thông tin của người dùng
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public GetAccountInfoResultType GetAccountInfo(string username)
        {
            User user = db.Users.FirstOrDefault(u => u.Active == true && u.Username == username);
            if(user != null)
            {
                AccountInfoViewModel accountInfo = new AccountInfoViewModel(user);
                return new GetAccountInfoResultType(true, GetAccountInfoResult.GetAccountInfoSuccessful, accountInfo);
            }

            return new GetAccountInfoResultType(false, GetAccountInfoResult.GetAccountInfoFailed);
        }
    }
}