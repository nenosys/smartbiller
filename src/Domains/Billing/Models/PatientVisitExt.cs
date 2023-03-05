using System;
using System.Linq;
using Domains.Billing.Exceptions;

namespace Domains.Billing.Models
{
    public partial class PatientVisit
    {
        protected internal void Validate()
        {
            if (StartDate == DateTime.MinValue || StartDate > DateTime.UtcNow)
            {
                throw new BillingException("Invalid in date");
            }
        }

        public decimal TotalCost
        {
            get { return PatientVisitCosts.Sum(x => x.TotalCost); }
        }
    }
}
