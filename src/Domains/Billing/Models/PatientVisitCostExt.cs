namespace Domains.Billing.Models
{
    public partial class PatientVisitCost
    {
        public decimal TotalCost
        {
            get
            {
                if (BillingCategory.BillingRateTypeId == 1) return Quantity*(BillingCategory.ChargePerUnit ?? 0);
                if (BillingCategory.ChargeUpperBound.HasValue)
                {
                    if (ChargableUnits >= BillingCategory.ChargeUpperBound.Value)
                    {
                        return ((BillingCategory.ChargePerUnit ?? 0) * BillingCategory.ChargeUpperBound.Value +
                               (ChargableUnits - BillingCategory.ChargeUpperBound.Value)*
                               (BillingCategory.ChargeAfterUpperBound ?? 0)) * Quantity;
                    }
                    return (BillingCategory.ChargePerUnit ?? 0) * Quantity * ChargableUnits;
                }
                return (BillingCategory.ChargePerUnit ?? 0)*Quantity*ChargableUnits;
            }
        }
    }
}
