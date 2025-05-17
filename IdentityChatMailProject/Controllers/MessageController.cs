using IdentityChatMailProject.Context;
using IdentityChatMailProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IdentityChatMailProject.Controllers
{
    public class MessageController : Controller
    {
        private readonly IdentityMailContext _context;
        private readonly UserManager<AppUser> _userManager;
     
        public MessageController(IdentityMailContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
           
        }

       
        public async Task<IActionResult> Inbox(string searchText)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var query = _context.Messages.Where(x => x.RecevierEmail == values.Email && x.IsTrash == false);

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x =>
                    x.Subject.Contains(searchText.ToLower()) ||
                    x.MessageDetail.Contains(searchText.ToLower()) ||
                    x.SenderEmail.Contains(searchText.ToLower()));
            }

            var messageList = query.ToList();            
            return View(messageList);
        }


        public async Task<IActionResult> Sendbox(string searchText)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            string email = values.Email;
            var query = _context.Messages.Where(x => x.SenderEmail == email && x.IsTrash == false);

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x =>
                    x.Subject.Contains(searchText.ToLower()) ||
                    x.MessageDetail.Contains(searchText.ToLower()) ||
                    x.SenderEmail.Contains(searchText.ToLower()));
            }

            var sendMessageList = query.ToList();
            
            return View(sendMessageList);
        }


        public async Task<IActionResult> TrashInbox()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);           
            var messageList = _context.Messages.Where(x => (x.RecevierEmail == values.Email && x.IsTrash == true)||(x.SenderEmail == values.Email && x.IsTrash == true)).ToList();
            return View(messageList);
        }

        public async Task<IActionResult> Profile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
           
            ViewBag.v1 = values.Name + " " + values.Surname;
            ViewBag.v2 = values.Email;          
            ViewBag.v3 = values.City;
            ViewBag.v4 = values.PhoneNumber;
            ViewBag.v5 = values.ProfileImageUrl;

            return View();
        }

        public async Task<IActionResult> MessageDetail(int id)
        {
            var sendMessageDetail = _context.Messages.FirstOrDefault(x => x.MessageId == id);
            return View(sendMessageDetail);

        }


        public async Task<IActionResult> MoveToTrash(int id)
        {
            var value = _context.Messages.FirstOrDefault(x => x.MessageId == id);
            value.IsTrash = true;
            _context.SaveChanges();
            return RedirectToAction("Inbox");

        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var value = _context.Messages.FirstOrDefault(x => x.MessageId == id);
            _context.Messages.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Inbox");
        }


        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            string x = values.Email;

            message.SenderEmail = x;
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();

            TempData["MessageSent"] = "true";

            return RedirectToAction("CreateMessage");
        }

        public IActionResult ChangeIsReadToTrue(int id)
        {
            var value = _context.Messages.Find(id);
            value.IsRead = true;
            _context.SaveChanges();
            return RedirectToAction("Inbox");
        }

        public IActionResult ChangeIsReadToFalse(int id)
        {
            var value = _context.Messages.Find(id);
            value.IsRead = false;
            _context.SaveChanges();
            return RedirectToAction("Inbox");
        }
                
    }
}
