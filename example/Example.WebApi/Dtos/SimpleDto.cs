using Example.WebApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.WebApi.Dtos
{
    public class SimpleDto
    {
        public string Text { get; set; }
        public SimpleEnum Enum { get; set; }
    }
}
