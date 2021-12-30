using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }


        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request, client side error",
                401 => "Please sign in and try again",
                404 => "The content you are searching for does not exist and may have been deleted by the author",
                500 => "Unknown error from the server",
                _ => null
            };
        }
    }
}
