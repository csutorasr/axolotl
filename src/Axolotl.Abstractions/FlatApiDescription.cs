using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Axolotl.Abstractions
{
    public class FlatApiDescription
    {
        public IList<ModelMetadata> Objects { get; set; }
        public IList<ApiDescriptionGroup> ApiDescriptionGroups { get; set; }
    }
}
