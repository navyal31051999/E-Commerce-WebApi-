using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebapiDemo.Errors
{
    public class ApiResponse
    {
        public ApiResponse( int statusCode , string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
         private string DefaultMessageForStatusCode(int statusCode)
        {
            return  statusCode switch 
            {
                400 =>"A Bad Request, you have Bad",
                401 =>"authorized,You are not",
                404 =>"Resource found, it was not",
                500 => "Errors are to the path of dark side ,error leads to anger ,anger leads to hate,hate leads to career change",
                 _ =>null

            };
        }
    }
}