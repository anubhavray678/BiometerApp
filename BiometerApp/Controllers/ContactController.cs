using BiometerApp.Data;
using BiometerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiometerApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactDbContext _db;
        public ContactController(ContactDbContext db)
        {
            _db = db;
        }
        //get
        //public IActionResult Index()
        //{
        //   IEnumerable<Contact> objContactList = _db.ContactData;
        //    return View(objContactList);
        //}
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Contact obj)
        {

            if (ModelState.IsValid)
            {
                _db.ContactData.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "message sent successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
