using Microsoft.AspNetCore.Mvc.Rendering;
using Picklr.Models;

namespace Picklr.ViewModels
{
    public class HomeViewModel
    {
        public int ClubId { get; set; }

        public string? Date { get; set; }

        public string PageTitle { get; set; } = "";

        public List<SelectListItem> Clubs { get; set; } = new();

        public List<SelectListItem> Dates { get; set; } = new();

        public List<PicklProgram> Programs { get; set; } = new();
    }
}