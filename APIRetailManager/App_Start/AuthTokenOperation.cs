using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace APIRetailManager.App_Start
{
    public class AuthTokenOperation : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add("/token", new PathItem
            {
                post = new Operation
                {
                    tags = new List<string>
                    {
                        {"Auth" }
                    },
                    consumes = new List<string>
                    {
                        "application/x-www-form-urlencoded"
                    },

                    parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                           name = "grant_type",
                           type = "string",
                            @in = "formData",
                            @default = "password",
                            required = true
                        },
                        new Parameter
                        {
                            name = "username",
                            type = "string",
                            @in = "formData",
                            required = false
                        },
                            new Parameter
                        {
                            name = "password",
                            type = "string",
                            @in = "formData",
                            required = false
                        }
                    }
                }
            
            });
        }
    }
}