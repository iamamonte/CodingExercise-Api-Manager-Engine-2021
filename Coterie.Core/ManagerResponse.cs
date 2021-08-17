using System;
using System.Collections.Generic;
using System.Text;

namespace Coterie.Core
{
    public class ManagerResponse<T> where T : class
    {
        public ManagerResponse(T data) 
        {
            ResponseCode = 200;
            Result = data;
        }

        public ManagerResponse(List<string> validationErrors) 
        {
            ResponseCode = 400;
            ValidationErrors = validationErrors;
            Message = "Invalid request.";
        }

        public ManagerResponse(Exception e) 
        {
            Message = e.Message;
            ResponseCode = 500;
        }

        public bool Succeeded => ResponseCode == 200;
        public int ResponseCode { get; }
        public T Result { get; set; }

        public string Message { get; }
        public List<string> ValidationErrors { get; } = new List<string>();
    }
}
