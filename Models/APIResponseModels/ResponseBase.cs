using System;
using System.Collections.Generic;
using System.Text;

namespace Models.APIResponseModels
{
    public class ResponseBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Status ErrorCode { get; set; }
    }
}
