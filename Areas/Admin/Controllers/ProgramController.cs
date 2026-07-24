using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProgramController : Controller
    {
        private readonly PicklrContext context;

        public ProgramController(PicklrContext ctx)
        {
            context = ctx;
        }

        // Display all programs
        public IActionResult List()
        {
            var programs = context.Programs
                .Include(p => p.Club)
                .OrderBy(p => p.Name)
                .ToList();

            return View(programs);
        }

        // Display Add or Edit page
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            PopulateClubDropDown();

            PicklProgram program;

            if (id.HasValue)
            {
                program = context.Programs
                    .FirstOrDefault(p => p.ProgramID == id.Value);

                if (program == null)
                {
                    return RedirectToAction(nameof(List));
                }

                ViewBag.Action = "Edit";
            }
            else
            {
                program = new PicklProgram();
                ViewBag.Action = "Add";
            }

            return View(program);
        }

        // Save Add/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEdit(PicklProgram program, string[] SelectedDays)
        {
            program.AvailableDays = SelectedDays != null
                ? string.Join(", ", SelectedDays)
                : "";
            ModelState.Remove(nameof(program.AvailableDays));
            if (string.IsNullOrWhiteSpace(program.AvailableDays))
            {
                ModelState.AddModelError(nameof(program.AvailableDays),
                    "Please select at least one day.");
            }

            if (ModelState.IsValid)
            {
                if (program.ProgramID == 0)
                {
                    context.Programs.Add(program);
                }
                else
                {
                    context.Programs.Update(program);
                }

                context.SaveChanges();

                TempData["message"] = $"'{program.Name}' was saved successfully.";

                return RedirectToAction(nameof(List));
            }

            PopulateClubDropDown();

            ViewBag.Action = program.ProgramID == 0 ? "Add" : "Edit";

            return View(program);
        }

        // Display Delete page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var program = context.Programs
                .Include(p => p.Club)
                .FirstOrDefault(p => p.ProgramID == id);

            if (program == null)
            {
                return RedirectToAction(nameof(List));
            }

            return View(program);
        }

        // Delete Program
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PicklProgram program)
        {
            context.Programs.Remove(program);
            context.SaveChanges();

            TempData["message"] = $"'{program.Name}' was deleted.";

            return RedirectToAction(nameof(List));
        }

        private void PopulateClubDropDown()
        {
            ViewBag.Clubs = new SelectList(
                context.Clubs.OrderBy(c => c.Name),
                "ClubID",
                "Name");
        }
    }
}