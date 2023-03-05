using Domains.Billing.Exceptions;

namespace Domains.Billing.Models
{
    public partial class Patient
    {
        public string FullName => FirstName + " " + LastName;

        public void Validate()
        {
            if (string.IsNullOrEmpty(FirstName)) throw new BillingException("First name is mandator");
            if (string.IsNullOrEmpty(LastName)) throw new BillingException("Last is mandator");
            if (string.IsNullOrEmpty(Identifier)) throw new BillingException("Identifier is mandator");
            if (Age <= 0) throw new BillingException("Age is invalid");
        }

        protected internal void Update(Patient patient)
        {
            FirstName = patient.FirstName;
            LastName = patient.LastName;
            Identifier = patient.Identifier;
            Age = patient.Age;
        }
    }
}
