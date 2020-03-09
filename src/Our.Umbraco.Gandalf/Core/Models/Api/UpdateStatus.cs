
using Our.Umbraco.Gandalf.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Our.Umbraco.Gandalf.Core.Models
{

	public class UpdateStatus
	{
        public bool CurrentStatus { get; set; }
		public ResponseType ResponseType { get; set; }
		public bool Updated { get; set;}

    }
}
