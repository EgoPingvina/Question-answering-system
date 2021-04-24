using System;

using Microsoft.AspNetCore.Cors.Infrastructure;

namespace OntoMath_QAS
{
    /// <summary>
    /// Настройка CORS-политики.
    /// </summary>
    public sealed partial class Startup
    {
        /// <summary>
        /// Имя политики безопасности.
        /// </summary>
        private readonly string AllowAnyPolicyName = "Any";

        /// <summary>
        /// Конфигурирование политики безопасности.
        /// </summary>
        private Action<CorsPolicyBuilder> AllowAny
         => new Action<CorsPolicyBuilder>(builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
    }
}