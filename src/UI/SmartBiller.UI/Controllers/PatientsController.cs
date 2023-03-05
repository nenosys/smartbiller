using System;
using System.Linq;
using System.Web.Mvc;
using Domains.Billing.Exceptions;
using Domains.Billing.Models;
using Domains.Billing.Services;
using System.Collections.Generic;
using SmartBiller.UI.Models;

namespace SmartBiller.UI.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly IBillingService _billingService;

        public PatientsController(IBillingService billingService)
        {
            _billingService = billingService;
        }
        // GET: Patients
        public ActionResult Patients()
        {
            var patients = _billingService.GetPatients();
            return View(patients);
        }

        public ActionResult BillingCategories()
        {
            var model = _billingService.GetCategories();
            return View("Categories",model);
        }

        public ActionResult Visits(int patientId)
        {
            var patient = _billingService.GetPatient(patientId);
            return View(patient);
        }

        public ActionResult PrintVisit(int visitId, int categoryId = -1)
        {
            var visit = _billingService.GetPatientVisit(visitId);
            var categories = _billingService.GetCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == categoryId
            }).ToList();

            categories.Insert(0,new SelectListItem
            {
                Text = "All Categories",
                Value = "-1"
            });

            categories.ForEach(x =>
            {
                x.Selected = (x.Value == categoryId.ToString());
            });

            if(categoryId > 0)
                visit.PatientVisitCosts = visit.PatientVisitCosts.Where(x => x.BillingCategoryId == categoryId).ToList();

            var model = new PatientVisitModel
            {
                Categories = categories,
                Visit = visit
            };
            return View("PrintVisit", model);
        }

        public PartialViewResult PatientDetails(int? id)
        {
            Patient model = null;
            if (id.HasValue)
            {
                model = _billingService.GetPatient(id.Value);
            }
            else
            {
                model = new Patient();
            }
            return PartialView("_PatientDetails", model);
        }


        public PartialViewResult EditVisit(int? id, int? patientId)
        {
            PatientVisit visit = null;
            if (id.HasValue && patientId.HasValue)
            {
                visit = _billingService.GetPatient(patientId.Value).PatientVisits.FirstOrDefault(x => x.Id == id.Value);
            }
            else
            {
                visit = new PatientVisit
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                };
            }
            
            return PartialView("_Visit", visit);
        }


        public PartialViewResult VisitCosts(int visitId)
        {
            var costs = _billingService.GetPatientVisit(visitId);
            return PartialView("_VisitCosts",costs);
        }


        public PartialViewResult EditCategory(int? id)
        {
            BillingCategory category = null;
            if (id.HasValue)
            {
                category = _billingService.GetCategories().FirstOrDefault(x => x.Id == id);
            }
            category = category ?? new BillingCategory();

            var departments = _billingService.GetDepartments();
            var rateTypes = _billingService.GetRateTypes();

            var model = new CategoryViewModel
            {
                Departments = departments.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = x.Id == (category.BillingDepartmentId)
                }).ToList(),
                Category = category,
                RateTypes = rateTypes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = x.Id == (category.BillingRateTypeId)
                }).ToList()
            };

            return PartialView("_Category", model);
        }

        public JsonResult SaveCategory(int? categoryId, string name, int departmentId, int rateTypeId, decimal? chargePerUnit,
            int? threshold, decimal? chargeAfterUpperBound)
        {
            var success = true;
            var message = string.Empty;
            try
            {
                var category = new BillingCategory
                {
                    Id = categoryId ?? 0,
                    Name = name,
                    BillingDepartmentId = departmentId,
                    BillingRateTypeId = rateTypeId,
                    ChargePerUnit = chargePerUnit,
                    ChargeUpperBound = threshold,
                    ChargeAfterUpperBound = chargeAfterUpperBound
                };
                if (category.Id > 0) _billingService.UpdateCategory(category);
                else _billingService.AddCategory(category);

            }
            catch (BillingException exception)
            {
                success = false;
                message = exception.Message;
            }
            return Json(new { success, message });
        }


        public JsonResult SavePatientDetails(int? patientId, string identifier, string firstName, string lastName, int? age)
        {
            var success = true;
            var message = string.Empty;
            try
            {
                var patient = new Patient
                {
                    Id = patientId??0,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age??0,
                    Identifier = identifier
                };
               if(patient.Id > 0) _billingService.UpdatePatient(patient);
               else _billingService.AddPatient(patient);

            }
            catch (BillingException exception)
            {
                success = false;
                message = exception.Message;
            }
            return Json(new {success, message});
        }



        public JsonResult SaveVisit(int patientId, int? visitId, DateTime? inDate, DateTime? outDate, bool discharged)
        {
            var success = true;
            var message = string.Empty;
            try
            {
                var visit = new PatientVisit
                {
                    Id = visitId ??0,
                   PatientId = patientId,
                   Settled = discharged,
                   StartDate = inDate ?? DateTime.MinValue,
                   EndDate = outDate
                };
                if (visit.Id > 0) _billingService.UpdateVisit(visit);
                else _billingService.AddVisit(visit);

            }
            catch (BillingException exception)
            {
                success = false;
                message = exception.Message;
            }
            return Json(new { success, message });
        }

        public JsonResult DeletePatient(int id)
        {
            var success = true;
            var message = string.Empty;
            try
            {
               _billingService.DeletePatient(id);

            }
            catch (BillingException exception)
            {
                success = false;
                message = exception.Message;
            }
            return Json(new { success, message });
        }

        public JsonResult DeleteCategory(int id)
        {
            var success = true;
            var message = string.Empty;
            try
            {
                _billingService.DeleteCategory(id);

            }
            catch (BillingException exception)
            {
                success = false;
                message = exception.Message;
            }
            return Json(new { success, message });
        }



        public PartialViewResult DeleteCost(int costId, int visitId)
        {
            _billingService.DeleteCost(costId);
            var patientVist = _billingService.GetPatientVisit(visitId);
            return PartialView("_VisitCosts", patientVist);
        }


        public JsonResult DeleteVisit(int id)
        {
            var success = true;
            var message = string.Empty;
            try
            {
                _billingService.DeleteVisit(id);

            }
            catch (BillingException exception)
            {
                success = false;
                message = exception.Message;
            }
            return Json(new { success, message });
        }

        public JsonResult Categories(string q)
        {
            var categories = _billingService.GetCategories();
            return Json(categories.Select(x => new {id=x.Id, name = x.Name}), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchPatients(string q)
        {
            var patients = _billingService.GetPatients().Where(x => x.Identifier.IndexOf(q,StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
            return Json(patients.Select(x => new { id = x.Id, name = x.Identifier }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateVisitCostRow(int visitId, int? visitCostId, int categoryId,  int? quantity, int? totalUnits, DateTime? startDate, DateTime? endDate )
        {
            var visitCost = new PatientVisitCost
            {
                PatientVisitId =  visitId,
                BillingCategoryId = categoryId,
                Id = visitCostId ?? 0,
                StartDate = startDate,
                EndDate = endDate,
                Quantity = quantity ?? 0,
                ChargableUnits = totalUnits ??0
            };
            if(visitCost.Id > 0) _billingService.UpdateVisitCost(visitCost);
            else _billingService.AddVisitCost(visitCost);
            var patientVist = _billingService.GetPatientVisit(visitId);
            var updatedVisitCost = (visitCostId ?? 0) > 0
                ? patientVist.PatientVisitCosts.FirstOrDefault(x => x.Id == (visitCostId ?? 0))
                : visitCost;
            return Json(new
            {
                id = updatedVisitCost.Id,
                rate = updatedVisitCost.BillingCategory.ChargePerUnit,
                totalCost = updatedVisitCost.TotalCost,
                totalPayable = patientVist.PatientVisitCosts.Sum(x => x.TotalCost)
            });
        }
    }
}