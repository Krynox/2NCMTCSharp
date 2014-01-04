using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Model;
using System.ServiceModel.Syndication;
using Models;
using System.Collections.ObjectModel;

namespace ProjectSite.Controllers
{
    public class RssController : ApiController
    {
        public Rss20FeedFormatter Get()
        {
            ObservableCollection<NewsFeed> messages = NewsFeed.GetNews();
            List<SyndicationItem> items = new List<SyndicationItem>();

            var feed = new SyndicationFeed(" Electronic Rampage Feed", "Dit is de feed van Electronic Rampage", new Uri("localhost:8080/Rss"));
            feed.Authors.Add(new SyndicationPerson("FestivalManager"));
            feed.Categories.Add(new SyndicationCategory("Nieuwsberichten van Electronic Rampage"));

            foreach (NewsFeed message in messages)
            {
                SyndicationItem item = new SyndicationItem("Bericht van"+ message.EntryDate.ToShortDateString(), message.Message, new Uri("localhost:8080/Rss"), Convert.ToString(message.ID), message.EntryDate);
                items.Add(item);
            }

            feed.Items = items;

            return new Rss20FeedFormatter(feed);

        }
    }
}
