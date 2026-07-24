using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;
using Picklr.ViewModels;

namespace Picklr.Controllers
{
    public class HomeController : Controller
    {
        private readonly PicklrContext _context;

        public HomeController(PicklrContext context)
        {
            _context = context;
        }

        public IActionResult Index(int clubId = 0, string? date = null)
        {
            HomeViewModel vm = new HomeViewModel();

            // -----------------------
            // Club dropdown
            // -----------------------
            vm.Clubs.Add(new SelectListItem
            {
                Text = "All Clubs",
                Value = "0",
                Selected = clubId == 0
            });

            foreach (var club in _context.Clubs.OrderBy(c => c.Name))
            {
                vm.Clubs.Add(new SelectListItem
                {
                    Text = club.Name,
                    Value = club.ClubID.ToString(),
                    Selected = club.ClubID == clubId
                });
            }

            // -----------------------
            // Date dropdown
            // -----------------------
            DateTime selectedDate;

            if (string.IsNullOrEmpty(date))
            {
                selectedDate = DateTime.Today;
                date = selectedDate.ToString("yyyy-MM-dd");
            }
            else
            {
                selectedDate = DateTime.Parse(date);
            }

            for (int i = 0; i < 7; i++)
            {
                DateTime d = DateTime.Today.AddDays(i);

                vm.Dates.Add(new SelectListItem
                {
                    Text = i == 0
                        ? $"Today ({d:ddd, MMM d})"
                        : d.ToString("ddd, MMM d"),
                    Value = d.ToString("yyyy-MM-dd"),
                    Selected = d.ToString("yyyy-MM-dd") == date
                });
            }

            // -----------------------
            // Query Programs
            // -----------------------
            var programs = _context.Programs
                .Include(p => p.Club)
                .AsQueryable();

            if (clubId != 0)
            {
                programs = programs.Where(p => p.ClubID == clubId);
            }

            string day = selectedDate.DayOfWeek.ToString();

            programs = programs.Where(p =>
                p.AvailableDays.Contains(day));

            vm.Programs = programs
                .OrderBy(p => p.Name)
                .ToList();

            // -----------------------
            // Page Heading
            // -----------------------
            vm.PageTitle =
                $"Programs for {selectedDate:dddd, MMMM d}";

            vm.ClubId = clubId;
            vm.Date = date;

            return View(vm);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Club()
        {
            return View();
        }

        public IActionResult Program()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }
    }
}