using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetNodeDemo.Models;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;

namespace AspNetNodeDemo.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
            return View(_context.Users);
        }

        public async Task<IActionResult> GeneratePdf([FromServices] INodeServices nodeServices, [FromServices] IHostingEnvironment env, int id)
        {
            var templatePath = env.WebRootFileProvider.GetFileInfo("template.html");

            var html = System.IO.File.ReadAllText(templatePath.PhysicalPath);

            var user = _context.Users.FirstOrDefault(u => u.ID == id);

            if (user == null)
            {
                return NotFound();
            }

            html = html.Replace("%conference%", "DevArena 2017");
            html = html.Replace("%name%", user.FirstName + " " + user.LastName);
            html = html.Replace("%date%", DateTime.Now.ToShortDateString());

            var options = new
            {
                Format = new
                {
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

        public async Task<IActionResult> ExportToExcel([FromServices] INodeServices nodeServices, [FromServices] IHostingEnvironment env)
        {
            var data = JsonConvert.SerializeObject(_context.Users);

            var filePath = await nodeServices.InvokeAsync<string>("./node_scripts/excel.js", data);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/octet-stream", "Users.xlsx");
        }

        #region Unused code 

        // GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.SingleOrDefault(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,FirstName,LastName,Email,BirthDate,Active")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.SingleOrDefault(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,FirstName,LastName,Email,BirthDate,Active")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.SingleOrDefault(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.SingleOrDefault(m => m.ID == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }

        #endregion
    }
}