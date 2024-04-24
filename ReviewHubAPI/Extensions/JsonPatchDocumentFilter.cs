using Microsoft.AspNetCore.JsonPatch;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ReviewHubAPI.Extensions
{
    public class JsonPatchDocumentFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.OperationId == "PatchUser") // Endre til faktisk OperationId fra din Swagger konfig
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json-patch+json"] = new OpenApiMediaType
                        {
                            Example = new OpenApiString(@"[
                            {
                                ""op"": ""replace"",
                                ""path"": ""/Firstname"",
                                ""value"": ""NyttFornavn""
                            },
                            {
                                ""op"": ""replace"",
                                ""path"": ""/Lastname"",
                                ""value"": ""NyttEtternavn""
                            }
                        ]")
                        }
                    }
                };
            }
        }
    }
}
