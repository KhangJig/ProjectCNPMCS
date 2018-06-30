using NYCshop.Metadata.UserMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.UserViewModel
{
    [MetadataType(typeof(ConfirmPasswordViewModelMetadata))]
    public class ConfirmPasswordViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CorrectPassword { get; set; }
    }
}