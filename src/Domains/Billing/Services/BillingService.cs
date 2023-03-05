using System;
using System.Collections.Generic;
using System.Linq;
using Domains.Billing.Models;
using Domains.Billing.Repositories;

namespace Domains.Billing.Services
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _billingRepository;

        public BillingService(IBillingRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }

        public User CurentUser
        {
            get
            {
                var currentPrincipal = System.Threading.Thread.CurrentPrincipal;
                return _billingRepository.GetUser(currentPrincipal.Identity.Name);
            }
        }

        public List<Patient> GetPatients()
        {
            return _billingRepository.GetPatients(CurentUser.HospitalId);
        }

        public void CreateUser(User user)
        {
            _billingRepository.CreateUser(user);
        }

        public void AddPatient(Patient patient)
        {
            patient.HospitalId = CurentUser.HospitalId;
            patient.AddedBy = CurentUser.Id;
            patient.AddedDate = DateTime.UtcNow;
            patient.Validate();
            _billingRepository.AddPatient(patient);
        }

        public void UpdatePatient(Patient patient)
        {
            var currentPatient = _billingRepository.GetPatient(patient.Id);
            currentPatient.Update(patient);
            currentPatient.HospitalId = CurentUser.HospitalId;
            currentPatient.LastUpdatedBy = CurentUser.Id;
            currentPatient.LastUpdateDate = DateTime.UtcNow;
            currentPatient.Validate();
            _billingRepository.UpdatePatient(currentPatient);
        }

        public Patient GetPatient(int id)
        {
            return _billingRepository.GetPatient(id);
        }

        public void DeletePatient(int id)
        {
            _billingRepository.DeletePatient(id);
        }

        public void AddVisit(PatientVisit visit)
        {
            visit.AddedBy = CurentUser.Id;
            visit.AddedDate = DateTime.UtcNow;
            visit.Validate();
            _billingRepository.AddVisit(visit);
        }

        public void UpdateVisit(PatientVisit visit)
        {
            var existingVisit = _billingRepository.GetPatient(visit.PatientId)
                .PatientVisits.FirstOrDefault(x => x.Id == visit.Id);
            existingVisit.Settled = visit.Settled;
            existingVisit.StartDate = visit.StartDate;
            existingVisit.EndDate = visit.EndDate;
            existingVisit.LastUpdatedBy = CurentUser.Id;
            existingVisit.LastUpdateDate = DateTime.UtcNow;
            existingVisit.Validate();
            _billingRepository.UpdateVisit(existingVisit);
        }

        public PatientVisit GetPatientVisit(int id)
        {
            return _billingRepository.GetVisit(id);
        }

        public void DeleteVisit(int id)
        {
            _billingRepository.DeleteVisit(id);
        }

        public List<BillingCategory> GetCategories()
        {
            return _billingRepository.GetBillingCategories(CurentUser.HospitalId).Where(x => (x.Deleted ?? false) == false).ToList();
        }

        public void AddVisitCost(PatientVisitCost visitCost)
        {
            var category =
                _billingRepository.GetBillingCategories(CurentUser.HospitalId).FirstOrDefault(x => x.Id == visitCost.BillingCategoryId);
            visitCost.Quantity = 1;
            visitCost.ChargableUnits = 1;
            visitCost.BillingCategory = category;
            _billingRepository.AddVisitCost(visitCost);
        }

        public void UpdateVisitCost(PatientVisitCost visitCost)
        {
            var currentVisitCost =
                _billingRepository.GetVisit(visitCost.PatientVisitId)
                    .PatientVisitCosts.FirstOrDefault(x => x.Id == visitCost.Id);
            currentVisitCost.BillingCategoryId = visitCost.BillingCategoryId;
            currentVisitCost.StartDate = visitCost.StartDate;
            currentVisitCost.EndDate = visitCost.EndDate;
            if (visitCost.ChargableUnits > 0) currentVisitCost.ChargableUnits = visitCost.ChargableUnits;
            if(visitCost.Quantity > 0)  currentVisitCost.Quantity = visitCost.Quantity;
            _billingRepository.UpdateVisitCost(currentVisitCost);


        }

        public void DeleteCost(int id)
        {
            _billingRepository.DeleteCost(id);
        }

        public List<BillingDepartment> GetDepartments()
        {
            return _billingRepository.GetDepartments(CurentUser.HospitalId);
        }

        public List<BillingRateType> GetRateTypes()
        {
            return _billingRepository.GetRateTypes(CurentUser.HospitalId);
        }

        public void UpdateCategory(BillingCategory category)
        {
            var currentCategory =
                _billingRepository.GetBillingCategories(CurentUser.HospitalId).FirstOrDefault(x => x.Id == category.Id);
            currentCategory.Update(category);
            currentCategory.Validate();
            _billingRepository.UpdateCategory(currentCategory);

        }

        public void AddCategory(BillingCategory category)
        {
            category.HospitalId = CurentUser.HospitalId;
            category.Validate();
            _billingRepository.AddCategory(category);
        }

        public void DeleteCategory(int id)
        {
            var category = _billingRepository.GetBillingCategories(CurentUser.HospitalId).FirstOrDefault(x => x.Id == id);
            category.Deleted = true;
            _billingRepository.UpdateCategory(category);
        }
    }
}
