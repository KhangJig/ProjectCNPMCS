using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.ViewModels.ErrorViewModels
{
    /// <summary>
    /// Thông tin lỗi khi hiển thị ra ngoài màn hình
    /// </summary>
    public class ErrorViewModel
    {
        public string ErrorName { get; set; }
        public string ErrorContent { get; set; }

        public ErrorViewModel() 
        {
            this.ErrorName = "Lỗi";
            this.ErrorContent = "Lỗi xử lý";
        }
        public ErrorViewModel(string errorContent)
        {
            this.ErrorName = "Lỗi";
            this.ErrorContent = errorContent;
        }

        public ErrorViewModel(string errorName, string errorContent)
        {
            this.ErrorName = errorName;
            this.ErrorContent = errorContent;
        }
    }
}