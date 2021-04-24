using System;
using System.Net;

namespace OntoMath_QAS.Models
{
    public sealed class NotFoundException : WebAppException
    {
        public NotFoundException(HttpStatusCode code, object errors = null)
             : base(code, errors)
        { }
    }

    public class WebAppException : Exception
    {
        public HttpStatusCode Code { get; }

        public object Errors { get; set; }

        public WebAppException(HttpStatusCode code, object errors = null)
        {
            Errors = errors;
            Code = code;
        }
    }
}