using Microsoft.AspNetCore.Mvc;
using Picklr.Models;

namespace Picklr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClubController : Controller
    {
        private readonly PicklrContext context;

        public ClubController(PicklrContext ctx)
        {
            context = ctx;
        }

        // GET: /Admin/Club/List
        public IActionResult List()
        {
            var clubs = context.Clubs
                .OrderBy(c => c.Name)
                .ToList();

            return View(clubs);
        }

        // GET: /Admin/Club/AddEdit
        // GET: /Admin/Club/AddEdit/5
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            Club club;

            if (id == null)
            {
                club = new Club();
            }
            else
            {
                club = context.Clubs.Find(id) ?? new Club();
            }

            ViewBag.Action = (id == null) ? "Add" : "Edit";

            return View(club);
        }

        // POST: /Admin/Club/AddEdit
        [HttpPost]
        public IActionResult AddEdit(Club club)
        {
            if (ModelState.IsValid)
            {
                if (club.ClubID == 0)
                {
                    context.Clubs.Add(club);
                }
                else
                {
                    context.Clubs.Update(club);
                }

                context.SaveChanges();

                TempData["message"] = $"'{club.Name}' was saved successfully.";

                return RedirectToAction("List");
            }

            ViewBag.Action = (club.ClubID == 0) ? "Add" : "Edit";

            return View(club);
        }

        // GET: /Admin/Club/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var club = context.Clubs.Find(id);

            if (club == null)
            {
                return RedirectToAction("List");
            }

            return View(club);
        }

        // POST: /Admin/Club/Delete
        [HttpPost]
        public IActionResult Delete(Club club)
        {
            context.Clubs.Remove(club);

            context.SaveChanges();

            TempData["message"] = $"'{club.Name}' was deleted.";

            return RedirectToAction("List");
        }
    }
}