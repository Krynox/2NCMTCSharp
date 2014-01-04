using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using System.Collections.ObjectModel;

namespace ProjectSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            ObservableCollection<NewsFeed> feed= new ObservableCollection<NewsFeed>();
            feed = NewsFeed.GetNews();
            return View(feed);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Delete(string id)
        {
            NewsFeed.DeleteNews(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(NewsFeed mess)
        {
            if (mess.Message != "" || mess.Message != null)
            {
                NewsFeed.CreateNews(mess);
                return RedirectToAction("Index");
            }
            else {
                return View();
            }
        }

    }
}
