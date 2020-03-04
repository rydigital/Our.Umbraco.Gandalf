
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.Gandalf.Core.Models
{
    public class AddRequest
    {
        [Required]
        public string ipAddress { get; set; }

        public string Notes { get; set; }
    }
}
