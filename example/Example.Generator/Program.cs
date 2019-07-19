using Axolotl;
using Axolotl.AspNetCore22;
using Axolotl.TypeScriptWriter;
using Example.WebApi;
using System.IO;

namespace Example.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiGenerator = new ApiGeneratorService();
            using (var provider = new ApiDescriptionProvider<Startup>())
            {
                apiGenerator.AddApiDescriptionGroupCollection(provider.ApiDescriptionGroups);
            }
            var flat = apiGenerator.GenerateFlatApi();
            using (var sw = new StreamWriter("index.ts"))
            {
                var writer = new TypeWriter();
                writer.WriteApi(flat, sw);
                var initialWriter = new InitialValueWriter();
                initialWriter.WriteApi(flat, sw);
                sw.Close();
            }
        }
    }
}
