using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.Response
{
    public class BaseResponse<T>
    {
        /// <summary>
        /// Result code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Thông báo
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public T Result { get; set; }
    }
}
