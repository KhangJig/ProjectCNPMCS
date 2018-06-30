using NYCshop.Assets;
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
    /// Lớp dùng để truy cập dữ liệu, hỗ trợ cho controller AcccountController
    /// </summary>
    public class AccountDAO
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private MD5Assets md5 = new MD5Assets();

        public AccountDAO()
        {

        }

        /// <summary>
        /// Thực hiện đăng nhập
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LoginResultType Login(LoginViewModel model)
        {
            LoginResultType result = new LoginResultType();

            string password = md5.GetMd5Hash(model.Password);
            var currUser = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == password);

            // đăng nhập thành công
            if (currUser != null)
            {
                if (!currUser.Active)
                    // 1.3 Tài khoản bị khóa
                    return new LoginResultType(false, LoginResult.AccountBlocked);

                // đăng nhập thành công
                return new LoginResultType(true, LoginResult.LoginSuccessful, currUser);
            }

            // 1.1 Sai tên tài khoản hoặc mật khẩu
            return new LoginResultType(false, LoginResult.WrongUsernameOrPassword);
        }

        public SuccessAndMsg Register(RegisterViewModel user)
        {
            // kiểm tra xem có tồn tại người dùng hay chưa?
            bool isUserExists = db.Users.Any(u => u.Username == user.Username);

            // chưa tồn tại người dùng
            if (!isUserExists)
            {
                // tạo mới người dùng
                User newUser = new User(user);

                db.Users.Add(newUser);
                db.SaveChanges();

                return new SuccessAndMsg(true, RegisterResult.RegisterSuccessful);
            }
            else // đã tồn tại người dùng
                return new SuccessAndMsg(false, RegisterResult.UsernameExists);
        }

        public SuccessAndMsg ChangePassword(ChangePasswordViewModel model)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user != null)
            {
                // 1.4 Mật khẩu cũ không đúng
                if (md5.GetMd5Hash(model.OldPassword) != user.Password)
                    return new SuccessAndMsg(false, ChangePassResult.IncorrectOldPassword);
                else
                {
                    string newPassword = md5.GetMd5Hash(model.NewPassword);
                    // 1.1 trường hợp trùng mật khẩu mới và mật khẩu cũ
                    if (newPassword == user.Password)
                        return new SuccessAndMsg(false, ChangePassResult.OldAndNewPassNotAllowSame);
                    else
                    {
                        // 2. Đổi mật khẩu thành công
                        user.Password = md5.GetMd5Hash(model.NewPassword);

                        db.SaveChanges();
                        return new SuccessAndMsg(true, ChangePassResult.ChangePassSuccessful);
                    }
                }
            }

            return new SuccessAndMsg(false, ChangePassResult.ChangePassFailed);
        }

        public SuccessAndMsg ChangePersonalDetail(AccountInfoViewModel model)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user != null)
            {
                user.Username = model.Username;
                user.Address = model.Address;
                user.Email = model.Email;
                user.Name = model.Name;
                user.Phone = model.Phone;

                db.SaveChanges();
                return new SuccessAndMsg(true, ChangeUserInfoResult.ChangeUserInfoSuccessful);
            }
            
            return new SuccessAndMsg(false, ChangeUserInfoResult.ChangeUserInfoFailed);
        }
    }
}