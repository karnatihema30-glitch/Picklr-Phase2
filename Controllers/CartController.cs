using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picklr.Extensions;
using Picklr.Models;

namespace Picklr.Controllers
{
    public class CartController : Controller
    {
        private readonly PicklrContext context;

        public CartController(PicklrContext ctx)
        {
            context = ctx;
        }

        // Display the cart
        public IActionResult Index()
        {
            Cart cart = GetCart();
            return View(cart);
        }

        // Add a reservation
        public IActionResult Add(int id, string date, int clubId = 0)
        {
            Cart cart = GetCart();

            var program = context.Programs
                .Include(p => p.Club)
                .FirstOrDefault(p => p.ProgramID == id);

            if (program != null)
            {
                cart.Add(new CartItem
                {
                    ProgramID = program.ProgramID,
                    ProgramName = program.Name,
                    ClubName = program.Club?.Name ?? "",
                    Date = date,
                    Fee = program.Fee
                });

                SaveCart(cart);

                DateTime reservationDate = DateTime.Parse(date);

                    TempData["message"] =
                    $"{program.Name} on {reservationDate:ddd, MMM d} added to your cart.";
            }

            return RedirectToAction("Index", "Home", new
            {
                clubId = clubId,
                date = date
            });
        }

        // Remove one reservation
        public IActionResult Remove(int id)
        {
            Cart cart = GetCart();

            var item = cart.Items.FirstOrDefault(i => i.ProgramID == id);

            if (item != null)
            {
                DateTime reservationDate = DateTime.Parse(item.Date);

                TempData["message"] =
                    $"{item.ProgramName} on {reservationDate:ddd, MMM d} was removed from your cart.";

                cart.Remove(id);

                SaveCart(cart);
            }

            return RedirectToAction(nameof(Index));
        }

        // Empty the cart
        public IActionResult Clear()
        {
            Cart cart = GetCart();

            cart.Clear();

            SaveCart(cart);

            return RedirectToAction(nameof(Index));
        }

        // Pay & Confirm
        public IActionResult PayConfirm()
            {
                Cart cart = GetCart();

                foreach (var item in cart.Items)
                {
                    Reservation reservation = new Reservation
                    {
                        ProgramID = item.ProgramID,
                        ClubName = item.ClubName,
                        ReservationDate = item.Date,
                        Fee = item.Fee,
                        ConfirmedOn = DateTime.Now
                    };

                    context.Reservations.Add(reservation);
                }

                context.SaveChanges();

                int count = cart.Count;

                cart.Clear();

                SaveCart(cart);

                TempData["message"] =
                    $"Payment confirmed! {count} reservation(s) saved.";

                return RedirectToAction("Index", "Home");
            }

        // -------------------------
        // Helper methods
        // -------------------------

        private Cart GetCart()
        {
            Cart? cart =
                HttpContext.Session.GetObject<Cart>("Cart");

            if (cart == null)
            {
                cart = new Cart();
            }

            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetObject("Cart", cart);
        }
    }
}