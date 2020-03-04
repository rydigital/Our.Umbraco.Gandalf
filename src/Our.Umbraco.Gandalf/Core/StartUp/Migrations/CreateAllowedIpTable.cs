using Our.Umbraco.Gandalf.Core.Models.Pocos;
using Umbraco.Core.Migrations;

namespace Our.Umbraco.Gandalf.Core.StartUp.Migrations
{
	public class CreateAllowedIpTable : MigrationBase
    {
        private const string TABLE_NAME = "AllowedIp";

        public CreateAllowedIpTable(IMigrationContext context) : base(context)
        { }

        public override void Migrate()
        {
            if (!this.TableExists(TABLE_NAME))
            {
                this.Create.Table<AllowedIp>().Do();
            }
        }
    }

    public class CreateAllowedIpTableMigrationPlan : MigrationPlan
    {
        public CreateAllowedIpTableMigrationPlan() : base("Our.Umbraco.Gandalf")
        {
            From(string.Empty).To<CreateAllowedIpTable>("first-migration");
        }
    }
}
