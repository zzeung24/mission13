using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySQLFun.Models;

namespace MySQLFun.Controllers
{
    public class HomeController : Controller
    {
        private BowlingDbContext bowler { get; set; }

        public HomeController(BowlingDbContext someName)
        {
            bowler = someName;
        }

        public IActionResult Index(string teamName)
        {
            ViewBag.TeamName = teamName ?? "Home";
            var applications = bowler.Bowlers
                .Include(x => x.Team)
                .Where(x => x.Team.TeamName == teamName || teamName == null)
                .ToList();
            return View(applications);
        }

        [HttpGet]
        public IActionResult BowlingForm()
        {
            ViewBag.Teams = bowler.Teams.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult BowlingForm(Bowler b)
        {
            int max = 0;
            foreach (var s in bowler.Bowlers)
            {
                if (max < s.BowlerID)
                {
                    max = s.BowlerID;
                }
            }
            b.BowlerID = max + 1;

            if (ModelState.IsValid)
            {
                bowler.Add(b);
                bowler.SaveChanges();
                return RedirectToAction("Index", new { teamName = "" });
            }
            else
            {
                ViewBag.Teams = bowler.Teams.ToList();
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int bowlerId)
        {
            ViewBag.Teams = bowler.Teams.ToList();
            var application = bowler.Bowlers.Single(x => x.BowlerID == bowlerId);
            return View("BowlingForm", application);

        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            bowler.Update(b);
            bowler.SaveChanges();
            return RedirectToAction("Index", new { teamName = "" });
        }

        public IActionResult Delete(int bowlerId)
        {
            var application = bowler.Bowlers.Single(x => x.BowlerID == bowlerId);
            bowler.Bowlers.Remove(application);
            bowler.SaveChanges();
            return RedirectToAction("Index", new { teamName = "" });
        }
    }
}