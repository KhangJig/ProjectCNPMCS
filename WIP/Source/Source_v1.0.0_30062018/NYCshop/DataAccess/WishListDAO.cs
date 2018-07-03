using NYCshop.CustomTypes;
using NYCshop.Models;
using NYCshop.Resources.DataAccessMessage;
using NYCshop.ViewModels.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.DataAccess
{
    public class WishListDAO
    {
        /// <summary>
        /// Dùng để tương tác với CSDL
        /// </summary>
        private ExLoverShopDb db = new ExLoverShopDb();
        private UserDAO userDAO = new UserDAO();
        private ImageDAO imageDAO = new ImageDAO();
        private ProductDAO productDAO = new ProductDAO();

        public WishListDAO() { }

        /// <summary>
        /// Thêm 1 sản phẩm vào Danh sách sản phẩm muốn mua trong CSDL
        /// </summary>
        /// <param name="model">Thông tin sản phẩm</param>
        /// <returns></returns>
        public SuccessAndMsg AddProdToWishListInDb(ProductInWishListViewModel model)
        {
            if(model != null)
            {

            }

            return new SuccessAndMsg(false);
        }

        /// <summary>
        /// Thêm 1 sản phẩm vào Danh sách sản phẩm muốn mua
        /// </summary>
        /// <param name="dictProducts">Danh sách sản phẩm muốn mua đã có: gồm mã sản phẩm và thông tin sản phẩm tương ứng</param>
        /// <param name="productID">Mã sản phẩm</param>
        /// <returns></returns>
        public SuccessAndMsg AddProdToWishList(Dictionary<int, ProductInWishListViewModel> dictProducts, int productID)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null && dictProducts != null)
            {
                // nếu chưa tồn tại sản phẩm đó
                if(!dictProducts.ContainsKey(productID))
                {
                    // lấy thông tin người dùng đăng sản phẩm này
                    SuccessAndMsg getUserResult = userDAO.GetUser(product.Username);
                    SuccessAndMsg getImageUrlResult = imageDAO.GetFirstUrlString(productID);

                    // lấy thông tin người dùng thất bại
                    if (!getUserResult.IsSuccess)
                        return new SuccessAndMsg(false, getUserResult.Message);

                    // lấy đường dẫn hình ảnh của sản phẩm thất bại
                    if (!getImageUrlResult.IsSuccess)
                        return new SuccessAndMsg(false, getImageUrlResult.Message);

                    User user = getUserResult.Value as User;
                    string imageUrl = getImageUrlResult.Value as string;

                    // tồn tại sản phẩm có mã sản phẩm là productID
                    ProductInWishListViewModel prodInWishList = new ProductInWishListViewModel(product, user, imageUrl);
                    dictProducts.Add(productID, prodInWishList);
                }

                return new SuccessAndMsg(true, WishListDAOMsg.AddProdToWishListSuccessful, dictProducts);
            }

            return new SuccessAndMsg(false, WishListDAOMsg.AddProdToWishListFailed);
        }

        /// <summary>
        /// Thêm sản phẩm vào Danh sách sản phẩm muốn mua trong CSDL
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public SuccessAndMsg AddProdToWishListInDb(string username, int productID)
        {
            try
            {
                var prodInWishList = db.WishLists.FirstOrDefault(p => p.ProductID == productID);
                if (prodInWishList != null)
                {
                    // tồn tại sản phẩm trong Danh sách sản phẩm muốn mua trong CSDL => không thêm nữa
                    return new SuccessAndMsg(true, WishListDAOMsg.AddProdToWishListInDbSuccessful);
                }
                else
                {
                    // không tồn tại sản phẩm trong Danh sách sản phẩm muốn mua trong CSDL => thêm vào

                    // kiểm tra xem người dùng có tồn tại không?
                    bool isUsernameExists = userDAO.IsUsernameExist(username);
                    if (isUsernameExists)
                    {
                        WishList item = new WishList(username, productID);
                        db.WishLists.Add(item);
                        db.SaveChanges();

                        return new SuccessAndMsg(true, WishListDAOMsg.AddProdToWishListInDbSuccessful);
                    }
                }
            }
            catch(Exception e)
            {
                string s = e.ToString();
            }

            return new SuccessAndMsg(false, WishListDAOMsg.AddProdToWishListInDbFailed);
        }

        /// <summary>
        /// Cập nhật Danh sách sản phẩm muốn mua
        /// </summary>
        /// <param name="dictProducts"></param>
        /// <returns></returns>
        public SuccessAndMsg UpdateWishList(List<ProductInWishListViewModel> lstProducts)
        {
            if (lstProducts != null)
            {
                // danh sách sản phẩm muốn mua sau khi cập nhật xong
                Dictionary<int, ProductInWishListViewModel> result = new Dictionary<int, ProductInWishListViewModel>();
                for (int i = 0; i < lstProducts.Count; )
                {
                    ProductInWishListViewModel item = lstProducts[i];
                    if (item.Delete)
                        // xóa sản phẩm cần xóa
                        lstProducts.Remove(item);
                    else
                    {
                        // thêm sản phẩm vào Danh sách sản phẩm muốn mua
                        result.Add(item.ProductID, item);
                        i++;
                    }
                }

                return new SuccessAndMsg(true, WishListDAOMsg.UpdateProdInWishListSuccessful, result);
            }


            return new SuccessAndMsg(false, WishListDAOMsg.UpdateProdInWishListFailed);
        }

        /// <summary>
        /// Cập nhật wishlist trong CSDL
        /// </summary>
        /// <param name="lstProducts"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public SuccessAndMsg UpdateWishListInDb(List<ProductInWishListViewModel> lstProducts, string username)
        {
            if(lstProducts != null)
            {
                // kiểm tra xem người dùng có tồn tại không?
                bool isUsernameExists = userDAO.IsUsernameExist(username);
                if (isUsernameExists)
                {
                    // danh sách sản phẩm muốn mua sau khi cập nhật xong
                    Dictionary<int, ProductInWishListViewModel> result = new Dictionary<int, ProductInWishListViewModel>();
                    List<ProductInWishListViewModel> lstRemoveProds = new List<ProductInWishListViewModel>();
                    for (int i = 0; i < lstProducts.Count; )
                    {
                        ProductInWishListViewModel item = lstProducts[i];
                        if (item.Delete)
                        {
                            lstRemoveProds.Add(item);

                            // xóa sản phẩm cần xóa
                            lstProducts.Remove(item);
                        }
                        else
                        {
                            // thêm sản phẩm vào Danh sách sản phẩm muốn mua
                            result.Add(item.ProductID, item);
                            i++;
                        }
                    }

                    // xóa các sp được chọn
                    foreach(ProductInWishListViewModel removeProd in lstRemoveProds)
                    {
                        var product = db.WishLists.FirstOrDefault(wl => wl.Username == username && wl.ProductID == removeProd.ProductID);
                        if (product != null)
                            db.WishLists.Remove(product);
                    }

                    db.SaveChanges();

                    return new SuccessAndMsg(true, WishListDAOMsg.UpdateProdInWishListSuccessful, result);
                }
            }

            return new SuccessAndMsg(false, WishListDAOMsg.UpdateProdInWishListFailed);
        }

        /// <summary>
        /// Lấy Danh sách sản phẩm muốn mua trong CSDL
        /// </summary>
        /// <param name="username">Tên người dùng</param>
        /// <returns></returns>
        public SuccessAndMsg GetWishListInDb(string username)
        {
            try
            {
                var wishList = db.WishLists.Where(wl => wl.Username == username);
                if(wishList != null)
                {
                    Dictionary<int, ProductInWishListViewModel> dictWishList = new Dictionary<int,ProductInWishListViewModel>();
                    List<WishList> lstProdsInWishList = wishList.ToList();
                    for(int i = 0; i < lstProdsInWishList.Count; i++)
                    {
                        WishList item = lstProdsInWishList[i];
                        // lấy thông tin người dùng đăng sản phẩm này
                        SuccessAndMsg getImageUrl = imageDAO.GetFirstUrlString(item.ProductID);
                        SuccessAndMsg getProduct = productDAO.GetProduct(item.ProductID);
                        SuccessAndMsg getUser = userDAO.GetUser(item.Username);
                        // lấy đường dẫn hình ảnh của sản phẩm thất bại
                        if (!getImageUrl.IsSuccess)
                            return new SuccessAndMsg(false, getImageUrl.Message);

                        // lấy sản phẩm thất bại
                        if (!getProduct.IsSuccess)
                            return new SuccessAndMsg(false, getProduct.Message);

                        // lấy thông tin người dùng thất bại
                        if (!getUser.IsSuccess)
                            return new SuccessAndMsg(false, getUser.Message);

                        ProductInWishListViewModel prod = new ProductInWishListViewModel(getProduct.Value as Product, getUser.Value as User, getImageUrl.Value as string);
                        dictWishList.Add(prod.ProductID, prod);
                    }
                    return new SuccessAndMsg(true, WishListDAOMsg.GetWishListSuccessfull, dictWishList);
                }
            }
            catch
            {

            }

            return new SuccessAndMsg(false, WishListDAOMsg.GetWishListFailed);
        }

        public SuccessAndMsg CombineWishList(Dictionary<int, ProductInWishListViewModel> wishListDb, Dictionary<int, ProductInWishListViewModel> wishList)
        {
            try
            {
                if (wishListDb != null && wishList != null)
                {
                    // gộp các sản phẩm vào wishListDb
                    foreach (KeyValuePair<int, ProductInWishListViewModel> item in wishList)
                    {
                        if (!wishListDb.ContainsKey(item.Key))
                        {
                            wishListDb.Add(item.Key, item.Value);

                            // thêm sản phẩm vào CSDL
                            db.WishLists.Add(new WishList(item.Value.Username, item.Value.ProductID));
                        }
                    }

                    // lưu thay đổi
                    db.SaveChanges();

                    return new SuccessAndMsg(true, WishListDAOMsg.CombineWishListSuccessfull, wishListDb);
                }
            }
            catch
            {

            }

            return new SuccessAndMsg(false, WishListDAOMsg.CombineWishListFailed);
        }
    }
}