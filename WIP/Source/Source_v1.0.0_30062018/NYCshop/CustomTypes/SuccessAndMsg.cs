using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCshop.CustomTypes
{
    /// <summary>
    /// Kiểu dữ liệu trả về: thành công hay không + thông điệp + dữ liệu trả về khi thành công
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
        /// Dữ liệu trả về khi thành công
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Hàm khởi tạo lớp SuccessAndMsg
        /// </summary>
        public SuccessAndMsg(bool isSuccess = false, string message = "", object value = null)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;

            if (value != null)
                this.Value = value;
        }
    }
}