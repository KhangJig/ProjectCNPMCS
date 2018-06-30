using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.UserMetadatas
{
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

        [Display(Name = "JoiningDate", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }
    }
}