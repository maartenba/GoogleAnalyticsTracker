using System.Diagnostics;
using GoogleAnalyticsTracker.AspNet;
using Microsoft.AspNetCore.Mvc;
using GoogleAnalyticsTracker.SampleWebApplication.Models;

namespace GoogleAnalyticsTracker.SampleWebApplication.Controllers
{
    [TrackPageView(Enabled = false)]
    public class HomeController : Controller
    {
        [TrackPageView(Enabled = true, CustomTitle = "HOME - INDEX")]
        public IActionResult Index()
        {
            return View();
        }

        [TrackPageView(Enabled = false, CustomTitle = "HOME - ABOUT")]
        [TrackEvent]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [TrackPageView(Enabled = true)]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [TrackPageView(Enabled = true)]
        public IActionResult Privacy()
        {
            return View();
        }

        [TrackPageView(Enabled = false)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
