using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.UserMetadatas
{
    public class LoginViewModelMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Password", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Role", ResourceType = typeof(UserDisplay))]
        public string Role { get; set; }
    }
}