using NYCshop.Metadata.CategoryMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NYCshop.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
        
    }
}
