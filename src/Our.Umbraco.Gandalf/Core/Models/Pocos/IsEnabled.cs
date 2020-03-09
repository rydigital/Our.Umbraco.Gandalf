using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Our.Umbraco.Gandalf.Core.Models.Pocos
{
	[TableName("IsEnabled")]
	[PrimaryKey("Id", AutoIncrement = true)]
	[ExplicitColumns]
	public class IsEnabled
	{
		[Column("Id")]
		[PrimaryKeyColumn(AutoIncrement = true)]
		public int Id { get; set; }

		[Column("Enabled")]
		public bool Enabled { get; set; }

	}
}
