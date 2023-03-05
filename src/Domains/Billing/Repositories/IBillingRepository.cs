using System.Collections.Generic;
using Domains.Billing.Models;

namespace Domains.Billing.Repositories
{
    public interface IBillingRepository
    {
        Patient GetPatient(int patientId);
        void AddPatient(Patient patient);
        void DeletePatient(int patientId);
        void UpdatePatient(Patient patient);
        List<Patient> GetPatients(int hospitalId);

        PatientVisit GetVisit(int id);
        void AddVisit(PatientVisit visit);
        void UpdateVisit(PatientVisit visit);
        void DeleteVisit(int id);

        void AddVisitCost(PatientVisitCost visitCost);
        void UpdateVisitCost(PatientVisitCost visitCost);
        void DeleteCost(int id);

        void AddBillingDepartment(BillingDepartment department);
        void AddBillingCategory(BillingCategory billingCategory);
        List<BillingCategory> GetBillingCategories(int hospitalId);

        User GetUser(string aspUserName);
        void CreateUser(User user);

        List<BillingDepartment> GetDepartments(int hospitalId);
        List<BillingRateType> GetRateTypes(int hospitalId);

        void AddCategory(BillingCategory category);
        void UpdateCategory(BillingCategory category);

    }
}
