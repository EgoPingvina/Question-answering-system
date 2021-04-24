using System.Net;

namespace OntoMath_QAS.Models.Exceptions
{
    public sealed class NotFoundException : WebAppException
    {
        public NotFoundException(HttpStatusCode code, object errors = null)
             : base(code, errors)
        { }
    }
}