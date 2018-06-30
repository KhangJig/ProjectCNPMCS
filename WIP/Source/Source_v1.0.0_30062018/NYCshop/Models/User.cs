using NYCshop.Metadata.UserMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NYCshop.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        
    }
}
