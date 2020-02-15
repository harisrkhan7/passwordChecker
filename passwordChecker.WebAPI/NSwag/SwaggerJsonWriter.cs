using System;
using System.IO;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace passwordChecker.WebAPI.NSwag
{
    public class SwaggerJsonWriter : IDocumentProcessor
    {
        private const string SwaggerFilePath = "../swagger.json";
        public SwaggerJsonWriter()
        {
        }

        public void Process(DocumentProcessorContext context)
        {
            var document = context.Document.ToJson();
            File.WriteAllText(SwaggerFilePath, document);
        }
    }
}
