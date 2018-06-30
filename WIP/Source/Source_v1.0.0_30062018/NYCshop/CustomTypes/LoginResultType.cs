using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.CustomTypes
{
    /// <summary>
    /// Kiểu trả về khi đăng nhập
    /// </summary>
    public class LoginResultType : SuccessAndMsg
    {
        /// <summary>
        /// Người dùng hiện tại khi đăng nhập thành công
        /// </summary>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Hàm khởi tạo lớp LoginResult
        /// </summary>
        /// <param name="isSuccess">Thành công hay không?</param>
        /// <param name="message">Thông điệp gởi tới người dùng</param>
        /// <param name="currentUser">Người dùng hiện tại nếu đăng nhập thành công</param>
        public LoginResultType(bool isSuccess = false, string message = "", User currentUser = null)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;

            if (currentUser != null)
                this.CurrentUser = currentUser;
            else this.CurrentUser = new User();
        }
    }
}