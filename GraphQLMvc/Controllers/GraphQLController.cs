using GraphQL;
using GraphQL.Types;
using GraphQLMvc.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLMvc.Controllers
{
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        public GraphQLController(ISchema schema,
        IDocumentExecuter executer)
        {
            _schema = schema;
            _executer = executer;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]
        GraphQLQueryDTO query)
        {

            //var result = await new DocumentExecuter().ExecuteAsync(_ =>
            //{
            //    _.Schema = schema;
            //    _.Query = query.Query;
            //    _.OperationName = query.OperationName;
            //    _.Inputs = inputs;
            //});
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.Inputs = query.Variables?.ToInputs();

            });
            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }
            return Ok(result.Data);
        }
    }
}
