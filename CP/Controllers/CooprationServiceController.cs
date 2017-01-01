using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CP.Controllers
{
    public class CooprationServiceController : Controller
    {
        // GET: CooprationService
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ActivityApplication()
        {
            return View();
        }
    }
}