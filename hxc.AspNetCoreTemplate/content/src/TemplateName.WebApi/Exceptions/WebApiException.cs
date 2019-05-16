using System;

namespace TemplateName.WebApi.Exceptions
{
    public class WebApiException : Exception
    {
        public WebApiException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public WebApiException(Exception innerException) : base(null, innerException)
        {

        }
    }
}
