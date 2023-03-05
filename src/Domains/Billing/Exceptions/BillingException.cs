using System;

namespace Domains.Billing.Exceptions
{
    public class BillingException :Exception
    {
        public BillingException(string message) : base(message)
        {
            
        }
    }
}
