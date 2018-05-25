﻿using NYCshop.Models;
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

        public GetListAndDict()
        {
            // lấy danh sách các loại sách
            var categories = (from c in db.Categories
                              select c).ToList();

            foreach (Category category in categories)
            {
                this.lstCategories.Add(new SelectListItem { Text = category.CategoryName, Value = category.CategoryID.ToString() });
                
                // lấy danh sách các nhà xuất bản
                var subCategories = (from p in db.SubCategories
                                     where p.CategoryID == category.CategoryID
                                     select p).ToList();

                List<SelectListItem> lstSubCategories = new List<SelectListItem>();
                foreach (SubCategory subCategory in subCategories)
                    lstSubCategories.Add(new SelectListItem { Text = subCategory.SubCategoryName, Value = subCategory.SubCategoryID.ToString() });

                dictCategories.Add(category.CategoryID, lstSubCategories);
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
    }
}