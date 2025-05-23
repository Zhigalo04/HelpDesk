using HelpDesk.Models;
using HelpDesk.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                return RedirectToAction("Success");
            }

            return View("Index", contact);
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}