using NYCshop.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYCshop.Models
{
    [MetadataType(typeof(ImageUrlMetadata))]
    public class ImageUrl
    {
        public int ImageID { get; set; }
        public string Url { get; set; }
        public int ProductID { get; set; }
        private Product Product { get; set; }
    }
}