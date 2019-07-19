using Axolotl.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Axolotl
{
    public class ApiGeneratorService
    {
        private readonly List<ApiDescriptionGroup> apiDescriptionGroups;
        public ApiGeneratorService()
        {
            apiDescriptionGroups = new List<ApiDescriptionGroup>();
        }
        public void AddApiDescriptionGroupCollection(ApiDescriptionGroupCollection apiDescriptionGroupCollection)
        {
            apiDescriptionGroups.AddRange(apiDescriptionGroupCollection.Items);
        }

        public FlatApiDescription GenerateFlatApi()
        {
            var allMetas = apiDescriptionGroups
                .SelectMany(x => x.Items)
                .SelectMany(x => x.ParameterDescriptions
                    .Select(y => y.ModelMetadata)
                    .Concat(x.SupportedResponseTypes.Select(y => y.ModelMetadata)))
                .Aggregate(new List<ModelMetadata>(), (acc, meta) =>
                {
                    AddType(acc, meta);
                    return acc;
                });
            return new FlatApiDescription
            {
                Objects = allMetas,
                ApiDescriptionGroups = apiDescriptionGroups,
            };
        }

        private void AddType(List<ModelMetadata> visitedTypes, ModelMetadata meta)
        {
            if (meta == null) // void
            {
                return;
            }
            if (meta.ElementMetadata != null)
            {
                AddType(visitedTypes, meta.ElementMetadata);
            }
            if (meta.ModelType.IsGenericType && typeof(KeyValuePair<,>) == meta.ModelType.GetGenericTypeDefinition())
            {
                VisitProperties(visitedTypes, meta);
                return;
            }
            if (!visitedTypes.Any(x => meta.ModelType.Name == x.ModelType.Name))
            {
                visitedTypes.Add(meta);
                VisitProperties(visitedTypes, meta);
            }
        }

        private void VisitProperties(List<ModelMetadata> visitedTypes, ModelMetadata meta)
        {
            foreach (var property in meta.Properties)
            {
                AddType(visitedTypes, property);
            }
        }
    }
}
