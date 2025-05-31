using HelpDesk.Models;
using HelpDesk.Data;
using Microsoft.AspNetCore.Mvc;
using HelpDesk.Services;

namespace HelpDesk.Controllers
{
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public ContactsController(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Check(Contact contact)
        {
            var emailExists = _context.Emails.Any(e => e.Email == contact.Email);

            if (!emailExists)
            {
                ModelState.AddModelError("Email", "Отправлять форму могут только зарегистрированные пользователи");
                return View("Index", contact);
            }

            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                try
                {
                    string subject = "Ваша заявка принята";
                    string body = $"Уважаемый {contact.Email},<br><br>" +
                                 "Мы получили ваше сообщение:<br>" +
                                 $"<blockquote>{contact.Message}</blockquote>" +
                                 "<br>С уважением,<br>Команда Help Desk";

                    await _emailService.SendEmailAsync(contact.Email, subject, body);
                }
                catch
                {
                    TempData["ToastMessage"] = "Письмо не было отправлено на вашу почту";
                    TempData["ToastType"] = "error";

                    var queueItem = new Queue
                    {
                        Email = contact.Email,
                        Message = contact.Message,
                        CreatedAt = DateTime.Now
                    };

                    _context.Queue.Add(queueItem);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Home");
            }

            return View("Index", contact);
        }

    }
}