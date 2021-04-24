using Microsoft.OpenApi.Models;

using static OntoMath_QAS.AppConstants;

namespace OntoMath_QAS
{
    public sealed partial class Startup
    {
        private OpenApiReference Reference
            => new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id   = API.Bearer
            };
    }
}