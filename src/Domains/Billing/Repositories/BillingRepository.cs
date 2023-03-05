using System;
using System.Collections.Generic;
using System.Linq;
using Domains.Billing.Models;

namespace Domains.Billing.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly BillingDbContext _context;

        public BillingRepository()
        {
            _context = new BillingDbContext();
        }

        public Patient GetPatient(int patientId)
        {
            return _context.Patients.FirstOrDefault(x => x.Id == patientId);
        }

        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public void DeletePatient(int patientId)
        {
            var patientToDelete = _context.Patients.FirstOrDefault(x => x.Id == patientId);
            _context.Patients.Remove(patientToDelete);
            _context.SaveChanges();
        }

        public void UpdatePatient(Patient patient)
        {
            _context.SaveChanges();
        }

        public List<Patient> GetPatients(int hospitalId)
        {
            return _context.Patients.Where(x => x.HospitalId == hospitalId).ToList();
        }

        public PatientVisit GetVisit(int id)
        {
            return _context.PatientVisits.FirstOrDefault(x => x.Id == id);
        }

        public void AddVisit(PatientVisit visit)
        {
            _context.PatientVisits.Add(visit);
            _context.SaveChanges();
        }

        public void UpdateVisit(PatientVisit visit)
        {
            _context.SaveChanges();
        }

        public void DeleteVisit(int id)
        {
            var visit = _context.PatientVisits.FirstOrDefault(x => x.Id == id);
            _context.PatientVisitCosts.RemoveRange(visit.PatientVisitCosts);
            _context.PatientVisits.Remove(visit);
            _context.SaveChanges();
        }

        public void AddVisitCost(PatientVisitCost visitCost)
        {
            _context.PatientVisitCosts.Add(visitCost);
            _context.SaveChanges();
            _context.Entry(visitCost).Reload();
        }

        public void UpdateVisitCost(PatientVisitCost visitCost)
        {
            _context.SaveChanges();
            _context.Entry(visitCost).Reload();
        }

        public void DeleteCost(int id)
        {
            var cost = _context.PatientVisitCosts.FirstOrDefault(x => x.Id == id);
            _context.PatientVisitCosts.Remove(cost);
            _context.SaveChanges();
        }

        public void AddBillingDepartment(BillingDepartment department)
        {
            throw new System.NotImplementedException();
        }

        public void AddBillingCategory(BillingCategory billingCategory)
        {
            throw new System.NotImplementedException();
        }

        public List<BillingCategory> GetBillingCategories(int hospitalId)
        {
            return _context.BillingCategories.Where(x => x.HospitalId == hospitalId).ToList();
        }

        public User GetUser(string aspUserName)
        {
            var aspnetUser =
                _context.AspNetUsers.FirstOrDefault(
                    x => x.UserName.ToUpper() == aspUserName.ToUpper());
            return aspnetUser?.Users.FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<BillingDepartment> GetDepartments(int hospitalId)
        {
            return _context.BillingDepartments.Where(x => x.HospitalId == hospitalId).ToList();
        }

        public List<BillingRateType> GetRateTypes(int hospitalId)
        {
            return _context.BillingRateTypes.ToList();
        }

        public void AddCategory(BillingCategory category)
        {
            _context.BillingCategories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(BillingCategory category)
        {
            _context.SaveChanges();
        }
    }
}
