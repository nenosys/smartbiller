using System.Collections.Generic;
using System.Web.Mvc;
using Domains.Billing.Models;

namespace SmartBiller.UI.Models
{
    public class CategoryViewModel
    {
        public List<SelectListItem> Departments { get; set; }
        public List<SelectListItem> RateTypes { get; set; }
        public BillingCategory Category { get; set; }
    }
}