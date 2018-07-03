using NYCshop.Metadata.WishListMetadatas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    [MetadataType(typeof(WishListMetadata))]
    public partial class WishList
    {

    }
}