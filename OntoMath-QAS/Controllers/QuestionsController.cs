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

        // POST api/<QuestionsController>
        [HttpPost,
            ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult PortAnswer([FromBody] string answer)
        {
            var foo =
                @$"
                    SELECT ?name ?comment
                    WHERE {{
                        ?target rdfs:label ?name.
                        ?target rdfs:comment ?comment.
                        ?target rdfs:subClassOf ?parent.
                        ?parent rdfs:label ?label.
                        filter(?label = ""{answer}""@ru)
                        filter langMatches(lang(?name),""ru"")
                    }}
                    LIMIT 10
                ";

            var result = this.Generator.GetSet(foo);

            return this.Ok(result);
        }
    }
}