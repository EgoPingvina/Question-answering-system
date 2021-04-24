using System;

using Microsoft.AspNetCore.Mvc;

using Serilog;

namespace OntoMath_QAS.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        public Lazy<ILogger> Logger { protected get; set; } = null!;
    }
}