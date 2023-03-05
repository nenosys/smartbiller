using Domains.Billing.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SmartBiller.UI.Models
{
    public class PatientVisitModel
    {
        public List<SelectListItem> Categories { get; set; }
        public PatientVisit Visit { get; set; }
    }
}