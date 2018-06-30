using NYCshop.Assets;
using NYCshop.CustomTypes;
using NYCshop.Models;
using NYCshop.Resources.ActionMessageInController;
using NYCshop.Resources.DataAccessMessage;
using NYCshop.ViewModels.ProductViewModel;
using NYCshop.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NYCshop.DataAccess
{
    /// <summary>
    /// Lớp dùng để xử lý những thao tác trong controller UserController hoặc liên quan tới bảng Users trong CSDL
    /// </summary>
    public class UserDAO
    {
        /// <summary>
        /// Lớp dùng để xử lý CSDL
        /// </summary>
        private ExLoverShopDb db = new ExLoverShopDb();
        /// <summary>
        /// Hỗ trợ chuyển 1 chuỗi sang dạng MD5
        /// </summary>
        private MD5Assets md5 = new MD5Assets();
        /// <summary>
        /// Lớp xử lý CSDL liên quan tới bảng ImageUrl
        /// </summary>
        private ImageDAO imageDAO = new ImageDAO();

        /// <summary>
        /// Hàm khởi tạo cho UserDAO
        /// </summary>
        public UserDAO()
        {

        }

        /// <summary>
        /// Trả về danh sách các sản phẩm theo tên người dùng hiện tại
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <returns></returns>
        public List<ProductManagerViewModel> GetMyPost(string username)
        {
            Dictionary<int, string> dictImages = imageDAO.GetDictImageUrls();

            var products = (from p in db.Products
                            where p.Username == username
                            select new ProductManagerViewModel
                            {
                                ProductID = p.ProductID,
                                ProductName = p.ProductName,
                                Price = p.Price,
                                Quanlity = p.Quanlity,
                                SaleStatus = p.SaleStatus,
                                Image = "",
                                Censor = p.Censor,
                                Username = p.Username,
                                UploadDate = p.UploadDate,
                                PerformAction = false
                            }).ToList();

            foreach (ProductManagerViewModel p in products)
                if (dictImages.ContainsKey(p.ProductID))
                {
                    p.Image = dictImages[p.ProductID];
                }

            return products;
        }

        /// <summary>
        /// Trả về danh sách các sản phẩm theo điều kiện lọc
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <param name="viewType">Loại sản phẩm: còn hàng, hết hàng, tất cả</param>
        /// <param name="search">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public List<ProductManagerViewModel> GetMyPost(string username, string viewType, string search)
        {
            string query = GetQuery(username, viewType, search);
            var products = db.Products.SqlQuery(query).ToList();

            // thiết lập danh sách hình ảnh
            Dictionary<int, string> dictImages = imageDAO.GetDictImageUrls();

            // khởi tạo lại model
            List<ProductManagerViewModel> model = new List<ProductManagerViewModel>();

            foreach (Product product in products)
            {
                string imageUrl = dictImages.ContainsKey(product.ProductID) ? dictImages[product.ProductID] : "";
                ProductManagerViewModel p = new ProductManagerViewModel(product, imageUrl);

                model.Add(p);
            }

            return model;
        }

        /// <summary>
        /// Trả về câu truy vấn để lấy ra danh sách sản phẩm theo yêu cầu
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <param name="viewType">Loại sản phẩm: còn hàng, hết hàng, tất cả</param>
        /// <param name="search">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        private string GetQuery(string username, string viewType, string search)
        {
            // thiết lập cho thuộc tính viewType:
            // true: tin đã duyệt
            // false: tin chưa duyệt
            string saleStatus = "";
            string searchKey = ""; // thiết lập từ khóa tìm kiếm theo tên sản phẩm

            // xử lý theo từng cách hiển thị:
            viewType = viewType == null ? "" : viewType;
            search = search == null ? "" : search;
            switch (viewType)
            {
                case "Hết hàng":
                    saleStatus = "SaleStatus='True'";
                    break;
                case "Còn hàng":
                    saleStatus = "SaleStatus='False'";
                    break;
                default:
                    saleStatus = "1=1";
                    viewType = "Tất cả";
                    break;
            }

            // thiết lập từ khóa tìm kiếm cho chuỗi truy vấn
            if (String.Compare("", search) != 0)
                searchKey = string.Format("LOWER(ProductName) LIKE N'%{0}%'", search);
            else searchKey = "ProductName <> ''";

            // thiết lập tên người dùng cho chuỗi truy vấn
            if (String.Compare("", username) != 0)
                username = string.Format("Username='{0}'", username);
            else username = "Username <> ''";

            string sql = string.Format("SELECT * FROM Products WHERE {0} AND {1} AND {2}", username, saleStatus, searchKey);
            
            return sql;
        }

        /// <summary>
        /// Thông báo hết hàng đối với các sản phẩm được chọn
        /// </summary>
        /// <param name="model">Danh sách sản phẩm</param>
        public void SetOutOfProducts(List<ProductManagerViewModel> model)
        {
            if (model != null)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    ProductManagerViewModel p = model[i];
                    // báo hết hàng
                    if (p.PerformAction)
                    {
                        var product = db.Products.FirstOrDefault(pro => pro.ProductID == p.ProductID);
                        // tồn tại sản phẩm này
                        if (product != null)
                            // cập nhật trạng thái bán
                            product.SaleStatus = true;
                    }
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Thông báo còn hàng đối với các sản phẩm được chọn
        /// </summary>
        /// <param name="model">Danh sách sản phẩm</param>
        public void SetRemainProducts(List<ProductManagerViewModel> model)
        {
            if (model != null)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    ProductManagerViewModel p = model[i];
                    // báo hết hàng
                    if (p.PerformAction)
                    {
                        var product = db.Products.FirstOrDefault(pro => pro.ProductID == p.ProductID);
                        // tồn tại sản phẩm này
                        if (product != null)
                            // cập nhật trạng thái bán
                            product.SaleStatus = false;
                    }
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Thực hiện thao tác xóa sản phẩm
        /// </summary>
        /// <param name="model">Danh sách các sản phẩm</param>
        /// <returns></returns>
        public DeleteProductsResultType PerformActionDelete(List<ProductManagerViewModel> model)
        {
            List<Product> lstSaveDeleteProduct = new List<Product>();

            if (model != null)
            {
                // tìm sản phẩm cần xóa và xóa đi
                List<Product> lstDeleteProduct = new List<Product>();
                for (int i = 0; i < model.Count; i++)
                {
                    ProductManagerViewModel p = model[i];
                    // sản phẩm cần xóa
                    if (p.PerformAction)
                    {
                        var product = db.Products.FirstOrDefault(pro => pro.ProductID == p.ProductID);
                        // tồn tại sản phẩm này
                        if (product != null)
                        {
                            lstDeleteProduct.Add(product);

                            Product delProduct = new Product(product);
                            lstSaveDeleteProduct.Add(delProduct);
                        }
                    }
                }

                // xóa các sản phẩm
                db.Products.RemoveRange(lstDeleteProduct);
                db.SaveChanges();

                // xóa sản phẩm thành công
                return new DeleteProductsResultType(true, string.Format(DeleteProductResult.DeleteProductsSuccessful, lstSaveDeleteProduct.Count), lstSaveDeleteProduct);
            }

            // xóa sản phẩm thất bại
            return new DeleteProductsResultType(false, DeleteProductResult.DeleteProductsFailed);
        }

        /// <summary>
        /// Thêm sản phẩm và các hình ảnh vào CSDL
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <param name="model">Thông tin sản phẩm</param>
        /// <param name="files"></param>
        /// <param name="serverPath"></param>
        public SuccessAndMsg AddProductToDb(string username, Product model, List<HttpPostedFileBase> files, string serverPath)
        {
            if(files != null && files[0] != null)
            {
                SuccessAndMsg addProductResult = AddNewProduct(username, model);
                if (!addProductResult.IsSuccess)
                    return new SuccessAndMsg(false, AddNewProductResult.AddProductFailed);

                SuccessAndMsg copyImagesResult = CopyImages(model, files, serverPath);
                if (!copyImagesResult.IsSuccess)
                    return new SuccessAndMsg(false, AddNewProductResult.AddProductFailed);

                try
                {
                    // lưu xuống CSDL
                    db.SaveChanges();

                    return new SuccessAndMsg(true, AddNewProductResult.AddProductSuccessful);
                }
                catch
                {
                    return new SuccessAndMsg(false, AddNewProductResult.AddProductFailed);
                }
            }

            return new SuccessAndMsg(false, AddNewProductResult.MissingPictures);
        }

        /// <summary>
        /// Thêm sản phẩm mới
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <param name="model">Thông tin sản phẩm</param>
        private SuccessAndMsg AddNewProduct(string username, Product model)
        {
            try
            {
                // lấy productID lớn nhất
                int? maxProductID = db.Products.Max(p => (int?)p.ProductID);

                // thiết lập lại giá trị cho maxProductID nếu nó là null
                maxProductID = maxProductID == null ? 0 : maxProductID;

                // thêm sản phẩm mới
                model.SaleStatus = false;
                model.Username = username;
                model.ProductID = (int)maxProductID + 1;
                model.UploadDate = DateTime.Now;

                db.Products.Add(model);

                return new SuccessAndMsg(true);
            }
            catch(Exception e)
            {
                return new SuccessAndMsg(false, e.ToString());
            }
        }

        /// <summary>
        /// Sao chép hình ảnh vào thư mục /Images/Products và đổi tên thích hợp
        /// </summary>
        /// <param name="model">Thông tin sản phẩm</param>
        /// <param name="files">Các file hình ảnh</param>
        /// <param name="serverPath">Đường dẫn tới thư mục chứa hình ảnh</param>
        private SuccessAndMsg CopyImages(Product model, List<HttpPostedFileBase> files, string serverPath)
        {
            try
            {
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }

                // lấy id lớn nhất trong ImagesUrl
                int? maxImageID = db.ImageUrls.Max(i => (int?)i.ImageID);
                maxImageID = maxImageID == null ? 0 : maxImageID;

                int currentMaxID = (int)maxImageID + 1;

                List<ImageUrl> images = new List<ImageUrl>();
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase postedFile = files[i];
                    if (files != null)
                    {
                        string extension = Path.GetExtension(postedFile.FileName);
                        string fileName = model.ProductID + "_" + (i + 1).ToString() + extension;
                        postedFile.SaveAs(Path.Combine(serverPath, fileName));

                        // thêm đường dẫn hình ảnh vào CSDL
                        ImageUrl imageUrl = new ImageUrl(currentMaxID, "/Images/Products/" + fileName, model.ProductID);
                        images.Add(imageUrl);

                        currentMaxID++;
                    }
                }

                db.ImageUrls.AddRange(images);

                return new SuccessAndMsg(true);
            }
            catch (Exception e)
            {
                return new SuccessAndMsg(false, e.ToString());
            }
        }

        public void EditProduct(EditProductViewModel model, List<HttpPostedFileBase> files, int productID)
        {

        }

        /// <summary>
        /// Lấy ra người dùng với tên người dùng
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <returns></returns>
        public SuccessAndMsg GetUser(string username)
        {
            User user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
                return new SuccessAndMsg(true, UserDAOMsg.GetUserSuccessful, user);

            return new SuccessAndMsg(false, UserDAOMsg.GetUserFailed);
        }
    }
}