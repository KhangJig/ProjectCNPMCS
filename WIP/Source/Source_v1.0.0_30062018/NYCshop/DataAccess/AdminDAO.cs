using NYCshop.CustomTypes;
using NYCshop.Models;
using NYCshop.Resources.DataAccessMessage;
using NYCshop.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.DataAccess
{
    public class AdminDAO
    {
        /// <summary>
        /// Lớp dùng để xử lý CSDL
        /// </summary>
        private ExLoverShopDb db = new ExLoverShopDb();

        /// <summary>
        /// Trả về câu truy vấn để lấy ra danh sách người dùng theo yêu cầu
        /// </summary>
        /// <param name="viewType">Kiểu xem: tất cả, người dùng bị khóa, người dùng không khóa</param>
        /// <param name="search"></param>
        /// <returns></returns>
        private string GetQuery(string viewType, string search)
        {
            // thiết lập trạng thái tài khoản
            string active = string.Empty;
            // thiết lập từ khóa tìm kiếm theo tên người dùng
            string searchKey = string.Empty;

            // xử lý theo từng cách hiển thị:
            viewType = viewType == null ? string.Empty : viewType;
            search = search == null ? string.Empty : search;
            switch (viewType)
            {
                case "Bị khóa":
                    active = "Active='False'";
                    break;
                case "Mở khóa":
                    active = "Active='True'";
                    break;
                default: // tất cả
                    active = "1=1";
                    viewType = "Tất cả";
                    break;
            }

            // thiết lập từ khóa tìm kiếm cho chuỗi truy vấn
            if (String.Compare("", search) != 0)
                searchKey = string.Format("LOWER(Username) LIKE N'%{0}%'", search);
            else searchKey = "Username <> ''";

            string sql = string.Format("SELECT * FROM Users WHERE {0} AND {1}", active, searchKey);

            return sql;
        }

        /// <summary>
        /// Lấy ra danh sách người dùng kiểu AccInManageAccViewModel
        /// </summary>
        /// <returns></returns>
        public SuccessAndMsg GetListAccInManageAccs(string viewType, string search)
        {
            try
            {
                // lấy ra danh sách các người dùng có vai trò là User
                string query = GetQuery(viewType, search);
                var users = db.Users.SqlQuery(query).Where(u => u.Role == "User");
                
                if(users != null)
                {
                    // chuyển danh sách người dùng kiểu User sang danh sách người dùng kiểu AccInManageAccViewModel
                    List<AccInManageAccViewModel> result = new List<AccInManageAccViewModel>();
                    foreach(User user in users)
                    {
                        AccInManageAccViewModel acc = new AccInManageAccViewModel(user);
                        result.Add(acc);
                    }
                    
                    return new SuccessAndMsg(true, UserDAOMsg.GetListAccInManageAccsSuccessful, result);
                }
            }
            catch(Exception e)
            {
                string s = e.ToString();
                // lấy danh sách người thất bại
                return new SuccessAndMsg(false, UserDAOMsg.GetListAccInManageAccsFailed);
            }
                
            // lấy danh sách người thất bại
            return new SuccessAndMsg(false, UserDAOMsg.GetListAccInManageAccsFailed);
        }

        /// <summary>
        /// Khóa các tài khoản được chọn
        /// </summary>
        /// <param name="model">Danh sách tài khoản</param>
        public void BlockAccounts(List<AccInManageAccViewModel> model)
        {
            if (model != null)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    AccInManageAccViewModel modelItem = model[i];
                    // tài khoản cần khóa
                    if (modelItem.PerformAction)
                    {
                        var account = db.Users.FirstOrDefault(acc => acc.Username == modelItem.Username);
                        // tồn tại người dùng này
                        if (account != null)
                            // khóa tài khoản
                            account.Active = false;
                    }
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Mở khóa các tài khoản được chọn
        /// </summary>
        /// <param name="model">Danh sách tài khoản</param>
        public void UnlockAccounts(List<AccInManageAccViewModel> model)
        {
            if (model != null)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    AccInManageAccViewModel modelItem = model[i];
                    // tài khoản cần khóa
                    if (modelItem.PerformAction)
                    {
                        var account = db.Users.FirstOrDefault(acc => acc.Username == modelItem.Username);
                        // tồn tại người dùng này
                        if (account != null)
                            // mở khóa tài khoản
                            account.Active = true;
                    }
                }
                db.SaveChanges();
            }
        }  
    }
}