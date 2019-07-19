using System.ComponentModel.DataAnnotations;

namespace Example.WebApi.Dtos
{
    public class NextLevel
    {
        public ThirdLevel Deep { get; set; }
        public int? NullableInt { get; set; }
        public int NotNullableInt { get; set; }
        [Required]
        public ThirdLevel Required { get; set; }
    }
}