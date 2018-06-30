using NYCshop.Metadata.UserMetadatas;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.UserViewModel
{
    [MetadataType(typeof(UserInfoViewModelMetadata))]
    public class UserInfoViewModel
    {
        public UserInfoViewModel(User user = null)
        {
            if(user != null)
            {
                this.Username = user.Username;
                this.Address = user.Address;
                this.Email = user.Email;
                this.Name = user.Name;
                this.Phone = user.Phone;
            }
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}