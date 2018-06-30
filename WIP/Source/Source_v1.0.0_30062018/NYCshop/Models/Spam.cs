using NYCshop.Metadata.SpamMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NYCshop.Models
{
    [MetadataType(typeof(SpamMetadata))]
    public partial class Spam
    {

    }
}
