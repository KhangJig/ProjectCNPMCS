using NYCshop.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.CustomTypes
{
    public class GetAccountInfoResultType : SuccessAndMsg
    {
        /// <summary>
        /// Thông tin của người dùng
        /// </summary>
        public AccountInfoViewModel AccountInfo { get; set; }

        public GetAccountInfoResultType(bool isSuccess = false, string message = "", AccountInfoViewModel accountInfo = null)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;

            if (accountInfo != null)
                this.AccountInfo = accountInfo;
            else this.AccountInfo = new AccountInfoViewModel();
        }
    }
}