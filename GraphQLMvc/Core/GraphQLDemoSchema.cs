using GraphQL.Types;
using GraphQLMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace GraphQLMvc.Core
{
    public class GraphQLDemoSchema : Schema, ISchema
    {
        public GraphQLDemoSchema(IDependencyResolver
        resolver) : base(resolver)
        {
            Query = resolver.Resolve<AuthorQuery>();
        }
    }
}
