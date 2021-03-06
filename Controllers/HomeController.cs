﻿using System;
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
        private readonly UserContext _context;

        public HomeController(UserContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> TakeScreenShot([FromServices] INodeServices nodeServices, [FromServices] IHostingEnvironment env, string url, bool mobile = false)
        {
            var options = new
            {
                ScreenSize = new
                {
                    Width = 320,
                    Height = 480
                },
                ShotSize = new
                {
                    Width = 320,
                    Height = "all"
                },
                UserAgent = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_2 like Mac OS X; en-us) AppleWebKit/531.21.20 (KHTML, like Gecko) Mobile/7B298g"
            };

            if(!mobile)
            {
                options = new
                {
                    ScreenSize = new
                    {
                        Width = 1360,
                        Height = 768
                    },
                    ShotSize = new
                    {
                        Width = 1360,
                        Height = "all"
                    },
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36"
                };
            }
            var result = await nodeServices.InvokeAsync<string>("./node_scripts/screenshot.js", url, options);

            return Json(new { ImagePath = result });
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
