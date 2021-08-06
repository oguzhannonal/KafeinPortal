using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Model
{
   public class ApiResponse
    {
        public HttpStatusCode ResultCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
        public ApiResponse(HttpStatusCode _ResultCode,bool _IsSuccess, string message, object data)
        {
            ResultCode = _ResultCode;
            IsSuccess = _IsSuccess;
            Message = message;
            Data = data;
        }
    }
}
