using System.Configuration;
using System.Data.Entity.Infrastructure;

namespace Domains.Billing.Models
{
    public partial class SmartBillerEntities
    {
        public SmartBillerEntities(string connectionString) : base(connectionString)
        {
            
        }
    }

    public class BillingDbContext : SmartBillerEntities
    {
        const string ConnectionStringFormat =
                "metadata = res://*/Models.BillingModel.csdl|res://*/Models.BillingModel.ssdl|res://*/Models.BillingModel.msl;provider=System.Data.SqlClient;provider connection string=\"{0}MultipleActiveResultSets=True;App=EntityFramework\"";
        public BillingDbContext() : base(string.Format(ConnectionStringFormat,ConfigurationManager.ConnectionStrings["DefaultConnection"]))
        {
            
        }

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
        {
            return false;
        }
    }
}
