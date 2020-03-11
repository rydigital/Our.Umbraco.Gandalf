using Our.Umbraco.Gandalf.Core.Models.DTOs;

namespace Our.Umbraco.Gandalf.Core.Models
{
    public class StatusResponse
    {
		public bool Success { get; set; }
		public bool Enabled { get; set; }
		public string Message { get; set; }
	}
}
