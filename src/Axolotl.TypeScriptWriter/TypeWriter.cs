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
    public class TypeWriter : IAxolotlWriter
    {
        public void WriteApi(FlatApiDescription flatApiDescription, StreamWriter streamWriter)
        {
            foreach (var dtoType in flatApiDescription.Objects.Where(x => x.IsComplexType && !x.IsEnumerableType))
            {
                streamWriter.WriteLine($"export interface {dtoType.ModelType.Name} {{");
                foreach (var property in dtoType.Properties)
                {
                    streamWriter.Write($"  {property.Name}");
                    if (!property.IsRequired)
                    {
                        streamWriter.Write("?");
                    }
                    streamWriter.WriteLine($": {GetTypeName(property)};");
                }
                streamWriter.WriteLine("}");
                streamWriter.WriteLine();
            }
            foreach (var enumType in flatApiDescription.Objects.Where(x => x.IsEnum))
            {
                streamWriter.WriteLine($"export enum {enumType.ModelType.Name} {{");
                foreach (var value in enumType.EnumNamesAndValues)
                {
                    streamWriter.WriteLine($"  {value.Key} = {value.Value},");
                }
                streamWriter.WriteLine("}");
                streamWriter.WriteLine();
            }
        }

        private string GetTypeName(ModelMetadata model)
        {
            if (model.IsEnumerableType)
            {
                return GetTypeName(model.ElementMetadata) + "[]";
            }
            if (model.ElementMetadata != null)
            {
                return GetTypeName(model.ElementMetadata);
            }
            var type = model.ModelType;
            if (Nullable.GetUnderlyingType(type) != null)
            {
                type = type.GetGenericArguments()[0];
            }
            if (type == typeof(int) || type == typeof(decimal) || type == typeof(double) || type == typeof(float))
            {
                return "number";
            }
            if (type == typeof(string))
            {
                return "string";
            }
            if (type == typeof(bool))
            {
                return "boolean";
            }
            return type.Name;
        }
    }
}