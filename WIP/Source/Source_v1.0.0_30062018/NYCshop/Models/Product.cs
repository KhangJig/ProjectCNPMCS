using NYCshop.Metadata.ProductMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NYCshop.Models
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        
    }
}
