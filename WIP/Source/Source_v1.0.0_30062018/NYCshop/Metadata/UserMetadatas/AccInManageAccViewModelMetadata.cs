using NYCshop.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Metadata.UserMetadatas
{
    public class AccInManageAccViewModelMetadata
    {
        [Key]
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Username", ResourceType = typeof(UserDisplay))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Name", ResourceType = typeof(UserDisplay))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "JoiningDateRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "JoiningDate", ResourceType = typeof(UserDisplay))]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [Required(ErrorMessageResourceName = "RoleRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Role", ResourceType = typeof(UserDisplay))]
        public string Role { get; set; }

        [Required(ErrorMessageResourceName = "ActiveRequired", ErrorMessageResourceType = typeof(UserErrorMsg))]
        [Display(Name = "Active", ResourceType = typeof(UserDisplay))]
        public bool Active { get; set; }

        public bool PerformAction { get; set; }
    }
}