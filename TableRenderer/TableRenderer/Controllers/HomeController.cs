using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableRenderer.Engine;

namespace TableRenderer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Engine.TableRenderer Renderer = new Engine.TableRenderer();
            Renderer.AddColumn("Name");
            Renderer.AddColumn("Salary");
            Renderer.AddColumn("Full / Part Time");
            Renderer.AddRow(new string[] { "Brian", "30000", "Full Time" });
            Renderer.AddRow(new string[] { "Joseph", "50000", "Full Time" });
            Renderer.AddRow(new string[] { "Bill", "100000", "Part Time" });
            ViewBag.Table = Renderer.Render();
            return View();
        }
    }
}