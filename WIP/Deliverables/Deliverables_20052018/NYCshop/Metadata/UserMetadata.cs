using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata
{
    public class UserMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        [MinLength(6, ErrorMessageResourceName = "UsernameMinLength", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [RegularExpression(@"(\S)+", ErrorMessageResourceName = "InvalidUsername", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [System.Web.Mvc.Remote("IsUsernameExist", "User", ErrorMessage = "Tên tài khoản đã tồn tại")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Password", ResourceType = typeof(UserDisplay))]
        [MinLength(6, ErrorMessageResourceName = "PasswordMinLength", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Name", ResourceType = typeof(UserDisplay))]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Email { get; set; }

        [Display(Name = "Role", ResourceType = typeof(UserDisplay))]
        public string Role { get; set; }

        [Required(ErrorMessageResourceName = "AddressRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Address", ResourceType = typeof(UserDisplay))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "PhoneRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Phone", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceName = "InvalidPhone", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Phone { get; set; }

        [Display(Name = "JoiningDate", ResourceType = typeof(UserDisplay))]
        public DateTime JoiningDate { get; set; }

        [Display(Name = "Active", ResourceType = typeof(UserDisplay))]
        public bool Active { get; set; }
    }

    public class RegisterViewModelMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        [MinLength(6, ErrorMessageResourceName = "UsernameMinLength", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [RegularExpression(@"(\S)+", ErrorMessageResourceName = "InvalidUsername", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [System.Web.Mvc.Remote("IsUsernameExist", "Account", ErrorMessage = "Tên tài khoản đã tồn tại")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Password", ResourceType = typeof(UserDisplay))]
        [MinLength(6, ErrorMessageResourceName = "PasswordMinLength", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceName = "InvalidComfirmPassword", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Name", ResourceType = typeof(UserDisplay))]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "AddressRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Address", ResourceType = typeof(UserDisplay))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "PhoneRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Phone", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceName = "InvalidPhone", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Phone { get; set; }
    }


    public class LoginViewModelMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        [MinLength(6, ErrorMessageResourceName = "UsernameMinLength", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [RegularExpression(@"(\S)+", ErrorMessageResourceName = "InvalidUsername", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Password", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Role", ResourceType = typeof(UserDisplay))]
        public string Role { get; set; }
    }

    public class UserInfoViewModelMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        [MinLength(6, ErrorMessageResourceName = "UsernameMinLength", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [RegularExpression(@"(\S)+", ErrorMessageResourceName = "InvalidUsername", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Name", ResourceType = typeof(UserDisplay))]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "AddressRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Address", ResourceType = typeof(UserDisplay))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "PhoneRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Phone", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceName = "InvalidPhone", ErrorMessageResourceType = typeof(UserErrorMsg))]
        public string Phone { get; set; }
    }

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

    public class ConfirmPasswordViewModelMetadata
    {
        [Key]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string CorrectPassword { get; set; }
    }
}