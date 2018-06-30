using NYCshop.CustomTypes;
using NYCshop.Models;
using NYCshop.Resources.DataAccessMessage;
using NYCshop.Resources.ResourceFiles;
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

        /// <summary>
        /// Lấy ra danh sách các đường dẫn hình ảnh có mã sản phẩm được xác định trước
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public SuccessAndMsg GetListImageUrls(int productID)
        {
            try
            {
                var images = db.ImageUrls.Where(i => i.ProductID == productID);
                if (images != null)
                    // lấy danh sách đường dẫn hình ảnh thành công
                    return new SuccessAndMsg(true, ImageUrlErrorMsg.GetListUrlSuccessful, images.Select(i => i.Url).ToList());
            }
            catch
            {
                
            }

            // lấy danh sách đường dẫn hình ảnh thất bại
            return new SuccessAndMsg(false, ImageUrlErrorMsg.GetListUrlFailed);
        }

        /// <summary>
        /// Lấy đường dẫn hình ảnh đầu tiên có mã sản phẩm cho trước
        /// </summary>
        /// <param name="productID">Mã sản phẩm</param>
        /// <returns></returns>
        public SuccessAndMsg GetFirstUrlString(int productID)
        {
            try
            {
                ImageUrl img = db.ImageUrls.FirstOrDefault(i => i.ProductID == productID);
                if (img != null)
                    // lấy đường dẫn hình ảnh thành công
                    return new SuccessAndMsg(true, ImageUrlDAOMsg.GetFirstUrlStringSuccessful, img.Url);
            }
            catch
            {

            }

            // lấy đường dẫn hình ảnh thất bại
            return new SuccessAndMsg(false, ImageUrlDAOMsg.GetFirstUrlStringFailed);
        }
    }
}