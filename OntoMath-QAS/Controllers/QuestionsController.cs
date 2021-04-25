using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OntoMath_QAS.Controllers.Base;
using OntoMath_QAS.Services;

namespace OntoMath_QAS.Controllers
{
    [ApiController,
        Route("api/[controller]")]
    public sealed class QuestionsController : BaseController
    {
        public QuestionsService Service { private get; set; }

        // POST api/<QuestionsController>
        [HttpPost,
            ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult UpdateMap()
        {
            var comment = this.Service.UpdateMap();

            return this.Ok(comment);
        }

        // POST api/<QuestionsController>
        [HttpPost,
            ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult PostQuestion([FromBody] string question)
        {
            var query = this.Service.GetAnswer(question);

            return this.Ok(query);
        }
    }
}