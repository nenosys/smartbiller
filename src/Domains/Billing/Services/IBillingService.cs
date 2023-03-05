using System.Collections.Generic;
using Domains.Billing.Models;

namespace Domains.Billing.Services
{
    public interface  IBillingService
    {
        List<Patient> GetPatients();
        void CreateUser(User user);
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);
        Patient GetPatient(int id);
        void DeletePatient(int id);
        void AddVisit(PatientVisit visit);
        void UpdateVisit(PatientVisit visit);
        PatientVisit GetPatientVisit(int id);
        void DeleteVisit(int id);
        List<BillingCategory> GetCategories();
        void AddVisitCost(PatientVisitCost visitCost);
        void UpdateVisitCost(PatientVisitCost visitCost);
        void DeleteCost(int id);
        List<BillingDepartment> GetDepartments();
        List<BillingRateType> GetRateTypes();
        void UpdateCategory(BillingCategory category);
        void AddCategory(BillingCategory category);
        void DeleteCategory(int id);
    }
}
