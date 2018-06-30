using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.CustomTypes
{
    /// <summary>
    /// Kiểu dữ liệu trả về: kết quả + thông điệp
    /// </summary>
    public class SuccessAndMsg
    {
        /// <summary>
        /// Có thành công hay không?
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Thông điệp gởi tới người dùng
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Hàm khởi tạo lớp SuccessAndMsg
        /// </summary>
        public SuccessAndMsg(bool isSuccess = false, string message = "")
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
    }
}