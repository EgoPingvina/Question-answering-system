using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OntoMath_QAS.Controllers.Base;
using OntoMath_QAS.Ontology;

namespace OntoMath_QAS.Controllers
{
    [ApiController,
        Route("api/[controller]")]
    public sealed class QuestionsController : BaseController
    {
        public RequestGenerator Generator { private get; set; } = null!;

        // GET api/<QuestionsController>
        [HttpGet,
            ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Get([FromBody] string answer)
        {
            var result = this.Generator.GetSet(answer);

            return this.Ok(result);
        }
    }
}