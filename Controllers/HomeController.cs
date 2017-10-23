using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetNodeDemo.Models;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AspNetNodeDemo.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> GeneratePdf([FromServices] INodeServices nodeServices, [FromServices] IHostingEnvironment env)
        {
            var templatePath = env.WebRootFileProvider.GetFileInfo("template.html");

            var html = System.IO.File.ReadAllText(templatePath.PhysicalPath);

            html = html.Replace("{#conference}", "DevArena 2017");
            html = html.Replace("{#name}", "Josip Klarić");
            html = html.Replace("{#date}", DateTime.Now.ToShortDateString());

            var options = new {
                Format = new {
                    Width = "640px",
                    Height = "450px",
                    Margin = "0px"
                },
                Orientation = "portrait"
            };

            var data = new { html };

            var result = await nodeServices.InvokeAsync<Stream>("./node_scripts/pdf.js", options, data);

            return File(result, "application/pdf");
        }

        private object GetHtmlTicket(IHostingEnvironment env)
        {
            var data = new { Conference = "DevArena 2017", Name = "Josip Klarić", Date = DateTime.Now.ToShortDateString() };

            var reportPath = env.WebRootFileProvider.GetFileInfo("template.html");

            if (reportPath == null)
                return null;

            var html = System.IO.File.ReadAllText(reportPath.PhysicalPath);

            html = html.Replace("{#conference}", data.Conference);
            html = html.Replace("{#name}", data.Name);
            html = html.Replace("{#date}", data.Date);

            return new { html };
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
