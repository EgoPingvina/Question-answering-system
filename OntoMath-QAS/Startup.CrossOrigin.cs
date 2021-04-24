using System;

using Microsoft.AspNetCore.Cors.Infrastructure;

namespace OntoMath_QAS
{
    public sealed partial class Startup
    {
        private readonly string AllowAnyPolicyName = "Any";

        private Action<CorsPolicyBuilder> AllowAny
         => new Action<CorsPolicyBuilder>(builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
    }
}