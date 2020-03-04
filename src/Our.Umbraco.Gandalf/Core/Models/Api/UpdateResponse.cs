using Our.Umbraco.Gandalf.Core.Models.DTOs;

namespace Our.Umbraco.Gandalf.Core.Models
{
    public class UpdateResponse
    {
        public AllowedIpDto Item { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
