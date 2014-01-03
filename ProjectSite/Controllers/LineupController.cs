using Project;
using ProjectSite.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSite.Controllers
{
    public class LineupController : Controller
    {
        //
        // GET: /Lineup/

        public ActionResult Index()
        {
            LineUpVM luvm = new LineUpVM();
            return View(luvm);
        }
        public ActionResult ChangeDate(DateTime day)
        {
            LineUpVM luvm = new LineUpVM(day);
            return View("Index",luvm);
        }
        public ActionResult GoToBand(string id)
        {
            Band b = new Band();
            b = Band.GetBands(id);
            return View(b);
        }
        public FileContentResult GetFoto(string id)
        {
            Band b = Band.GetBands(id.ToString());
            return new FileContentResult(b.Picture, "image/jpeg");
        }
    }
}
