using System;
using MySQLFun.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace MySQLFun.Components
{
    public class TeamsViewComponents : ViewComponent
    {
        private BowlingDbContext bowler { get; set; }

        public TeamsViewComponents(BowlingDbContext temp)
        {
            bowler = temp;
        }

        public IViewComponentResult Invoke()
        {
            // get the route data of teamName and assign that in ViewBag.SelectedTeam
            ViewBag.SelectedTeam = RouteData?.Values["teamName"] ?? "";

            // get team names from context
            var teams = bowler.Bowlers
                .Select(x => x.Team.TeamName)
                .Distinct()
                .OrderBy(x => x);

            return View(teams);
        }
    }
}