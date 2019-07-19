using Axolotl.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Axolotl.TypeScriptWriter
{
    public class InitialValueWriter : IAxolotlWriter
    {
        public void WriteApi(FlatApiDescription flatApiDescription, StreamWriter streamWriter)
        {
            foreach (var meta in flatApiDescription.Objects.Where(x => x.IsComplexType && !x.IsEnumerableType))
            {
                streamWriter.Write($"export const {GetInitialName(meta)}: Readonly<{meta.ModelType.Name}> = ");
                WriteNullValue(meta, streamWriter);
                streamWriter.WriteLine(";");
                streamWriter.WriteLine();
            }
        }

        private string GetInitialName(ModelMetadata model)
        {
            return $"initial{model.ModelType.Name}";
        }

        private void WriteNullValue(ModelMetadata model, StreamWriter writer, int depth = 1)
        {
            if (model.IsEnumerableType)
            {
                writer.Write("[]");
                return;
            }
            if (model.IsComplexType)
            {
                writer.WriteLine("{");
                foreach (var property in model.Properties.Where(x => x.IsRequired))
                {
                    writer.Write(new string(' ', depth * 2));
                    writer.Write($"{property.Name}: ");
                    WriteNullValue(property, writer, depth + 1);
                    writer.WriteLine(",");
                }
                writer.Write(new string(' ', (depth - 1) * 2));
                writer.Write("}");
                return;
            }
            writer.Write("null");
        }
    }
}

