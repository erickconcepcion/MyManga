using System;
using System.Collections.Generic;
using System.Text;

namespace MyManga.Utils
{
    public class UnsuccessfulRequestException : Exception
    {
        public UnsuccessfulRequestException(System.Net.HttpStatusCode statusCode) 
            : base($"Is not a success status code. Code: {statusCode.ToString()}.")
        {
        }
        public UnsuccessfulRequestException(string message = ""): base($"Not internet connection. {message}")
        {

        }
    }
}
