using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project;
using System.Collections.ObjectModel;

namespace ProjectSite.Controllers
{
    public class HomeController : Controller
    {
        private ObservableCollection<Band> GetRandomBands()
        {
            ObservableCollection<Band> allbands = new ObservableCollection<Band>();
            ObservableCollection<Band> selectedbands = new ObservableCollection<Band>();
            List<int> randoms = new List<int>();
            Random rnd = new Random();
            bool ok = false;
            allbands = Band.GetBands();
            for (int i = 0; i < 4; i++)
            {
                int rand = rnd.Next(0, allbands.Count);
                if (randoms.Contains(rand))
                {
                    i--;
                }
                else {
                    randoms.Add(rand);
                    selectedbands.Add(allbands[rand]);
                }
            }
            return selectedbands;
        }

        public ActionResult Index()
        {
            return View(GetRandomBands());
        }

        public FileContentResult GetFoto(string id)
        {
            Band b = Band.GetBands(id.ToString());
            return new FileContentResult(b.Picture, "image/jpeg");
        }
    }
}
