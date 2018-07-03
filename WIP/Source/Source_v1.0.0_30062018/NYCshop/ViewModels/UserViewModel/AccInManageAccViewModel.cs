using NYCshop.Metadata.UserMetadatas;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.UserViewModel
{
    /// <summary>
    /// Thông tin tài khoản trong quản lý tài khoản của Admin
    /// </summary>
    [MetadataType(typeof(AccInManageAccViewModelMetadata))]
    public class AccInManageAccViewModel
    {
        public AccInManageAccViewModel()
        {
            this.PerformAction = false;
        }

        public AccInManageAccViewModel(User user)
        {
            this.Username = user.Username;
            this.Name = user.Name;
            this.Role = user.Role;
            this.JoiningDate = user.JoiningDate;
            this.Active = user.Active;
            this.PerformAction = false;
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
        public bool PerformAction { get; set; }
    }
}