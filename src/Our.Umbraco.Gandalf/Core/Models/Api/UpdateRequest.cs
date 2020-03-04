using Our.Umbraco.Gandalf.Core.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.Gandalf.Core.Models
{
    public class UpdateRequest

    {
        [Required]
        public AllowedIpDto Item { get; set; }
    }
}
