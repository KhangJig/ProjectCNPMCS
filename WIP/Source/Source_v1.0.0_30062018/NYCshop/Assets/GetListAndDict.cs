using NYCshop.Models;
using NYCshop.ViewModels.CommentViewModel;
using NYCshop.ViewModels.ReplyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCshop.Assets
{
    public class GetListAndDict
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private Dictionary<int, List<SelectListItem>> dictCategories = new Dictionary<int, List<SelectListItem>>();
        private List<SelectListItem> lstCategories = new List<SelectListItem>();
        private Dictionary<int, CommentInProductViewModel> dictComments = new Dictionary<int, CommentInProductViewModel>(); // phản hồi theo từng bình luận

        public GetListAndDict()
        {
            // lấy danh sách các loại sản phẩm
            var categories = (from c in db.Categories
                              select c).ToList();

            foreach (Category category in categories)
            {
                this.lstCategories.Add(new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString() });
                
                // lấy danh sách các loại sản phẩm con
                var subCategories = (from p in db.SubCategories
                                     where p.CategoryID == category.CategoryID
                                     select p).ToList();

                List<SelectListItem> lstSubCategories = new List<SelectListItem>();
                foreach (SubCategory subCategory in subCategories)
                    lstSubCategories.Add(new SelectListItem { Text = subCategory.SubCategoryName, Value = subCategory.SubCategoryID.ToString() });

                dictCategories.Add(category.CategoryID, lstSubCategories);
            }

            // lấy tất cả các bình luận
            var comments = (from c in db.Comments
                            select c).ToList();

            // thêm những phản hồi vào những bình luận tương ứng
            foreach(Comment cmt in comments)
            {
                User user = db.Users.FirstOrDefault(u => u.Username == cmt.Username);

                string name = user != null ? user.Name : "";

                var replies = (from r in db.Replies
                               where r.CommentID == cmt.CommentID
                               select r).ToList();

                List<ReplyViewModel> lstReplies = new List<ReplyViewModel>();
                foreach(Reply reply in replies)
                {
                    var replyUser = db.Users.FirstOrDefault(u => u.Username == reply.Username);
                    string replyName = user != null ? user.Name : "";

                    ReplyViewModel replyViewModel = new ReplyViewModel();
                    replyViewModel.ReplyID = reply.ReplyID;
                    replyViewModel.ReplyContent = reply.ReplyContent;
                    replyViewModel.Name = replyName;
                    replyViewModel.CommentID = reply.CommentID;
                    replyViewModel.TimeReply = reply.TimeReply;

                    lstReplies.Add(replyViewModel);
                }

                CommentInProductViewModel cmtViewModel = new CommentInProductViewModel();
                cmtViewModel.CommentID = cmt.CommentID;
                cmtViewModel.Name = name;
                cmtViewModel.ProductID = cmt.ProductID;
                cmtViewModel.Replies = lstReplies;
                cmtViewModel.TimeComment = cmt.TimeComment;
                cmtViewModel.CommentContent = cmt.CommentContent;

                dictComments.Add(cmt.CommentID, cmtViewModel);
            }  
        }

        public Dictionary<int, List<SelectListItem>> GetDictCategories()
        {
            return dictCategories;
        }

        public Dictionary<int, List<SelectListItem>> GetDictCategories(int subCategoryID)
        {
            // tìm tới đúng loại sản phẩm đó
            var subCategory = db.SubCategories.FirstOrDefault(sc => sc.SubCategoryID == subCategoryID);
            if(subCategory != null)
            {
                if(this.dictCategories.ContainsKey(subCategory.CategoryID))
                    foreach(SelectListItem item in this.dictCategories[subCategory.CategoryID])
                        if (item.Value == subCategoryID.ToString())
                        {
                            item.Selected = true; // thiết lập mặc định người dùng chọn loại sản phẩm con này
                            break;
                        }
                            
            }

            return dictCategories;
        }

        public List<SelectListItem> GetListCategories(int categoryID)
        {
            foreach (SelectListItem item in lstCategories)
                if(item.Value == categoryID.ToString())
                {
                    item.Selected = true;
                    break;
                }

            return lstCategories;
        }

        public List<SelectListItem> GetListCategories()
        {
            return lstCategories;
        }

        public Dictionary<int, CommentInProductViewModel> GetCommentsByProductID(int productID)
        {
            Dictionary<int, CommentInProductViewModel> result = new Dictionary<int, CommentInProductViewModel>();

            var comments = (from c in db.Comments
                            where c.ProductID == productID
                            select c).ToList();

            foreach (Comment cmt in comments)
            {
                // tìm những bình luận trong sản phẩm đó
                if (dictComments.ContainsKey(cmt.CommentID))
                    result.Add(cmt.CommentID, dictComments[cmt.CommentID]);
            }

            return result;
        }
    }
}