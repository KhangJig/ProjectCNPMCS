using NYCshop.Assets;
using NYCshop.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    public partial class User
    {
        private MD5Assets md5 = new MD5Assets();

        public User() { }

        public User(RegisterViewModel user)
        {
            this.Username = user.Username;
            this.Name = user.Name;
            this.Address = user.Address;
            this.Email = user.Email;
            this.Phone = user.Password;
            this.Active = true;
            this.Role = "User";
            this.JoiningDate = DateTime.Now;
            this.Password = md5.GetMd5Hash(user.Password);
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}