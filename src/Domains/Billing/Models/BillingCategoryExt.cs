using System.ComponentModel.DataAnnotations;
using Domains.Billing.Exceptions;

namespace Domains.Billing.Models
{
    public partial class BillingCategory
    {
        protected internal void Update(BillingCategory category)
        {
            BillingDepartmentId = category.BillingDepartmentId;
            BillingRateTypeId = category.BillingRateTypeId;
            ChargeAfterUpperBound = category.ChargeAfterUpperBound;
            ChargePerUnit = category.ChargePerUnit;
            ChargeUpperBound = category.ChargeUpperBound;
            Name = category.Name;
        }

        protected internal void Validate()
        {
            if(string.IsNullOrEmpty(Name)) throw new BillingException("Name is mandatory");
            if (BillingRateTypeId <= 0) throw new BillingException("Rate type is mandatory");
            if(!ChargePerUnit.HasValue) throw new BillingException("Charge per unit is mandatory");
            if (ChargeUpperBound.HasValue && !ChargeAfterUpperBound.HasValue) throw new BillingException("Charge after upper bound is mandatory");
            if (ChargeAfterUpperBound.HasValue && !ChargeUpperBound.HasValue) throw new BillingException("Charge upper bound is mandatory");
        }
    }
}
