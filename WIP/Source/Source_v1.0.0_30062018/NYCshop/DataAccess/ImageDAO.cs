using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.DataAccess
{
    public class ImageDAO
    {
        private ExLoverShopDb db = new ExLoverShopDb();

        public ImageDAO()
        {

        }

        /// <summary>
        /// Trả về danh sách các hình ảnh trong CSDL
        /// </summary>
        /// <returns></returns>
        public List<ImageUrl> GetListImageUrls()
        {
            var images = from i in db.ImageUrls
                         select i;

            return images.ToList();
        }

        /// <summary>
        /// Trả về 1 bộ từ điển (Dictionary):
        /// - Key: ImageID
        /// - Value: Url
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetDictImageUrls()
        {
            Dictionary<int, string> dictImages = new Dictionary<int, string>();
            var images = GetListImageUrls();

            foreach (ImageUrl img in images)
                if (!dictImages.ContainsKey(img.ProductID))
                    dictImages.Add(img.ProductID, img.Url);

            return dictImages;
        }
    }
}