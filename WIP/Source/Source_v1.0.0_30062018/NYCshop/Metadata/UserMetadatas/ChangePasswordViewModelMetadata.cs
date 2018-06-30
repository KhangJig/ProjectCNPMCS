using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.UserMetadatas
{
    public class ChangePasswordViewModelMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        [MinLength(6, ErrorMessageResourceName = "UsernameMinLength", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [RegularExpression(@"(\S)+", ErrorMessageResourceName = "InvalidUsername", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "OldPasswordRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "OldPassword", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "NewPasswordRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "NewPassword", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessageResourceName = "InvalidComfirmPassword", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}