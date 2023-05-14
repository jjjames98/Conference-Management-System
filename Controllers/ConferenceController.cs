using Conference_Management_System.Data;
using Conference_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Conference_Management_System.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ConferenceController(ApplicationDbContext db)
        {
            _db = db;   
        }

        public IActionResult Index()
        {
            IEnumerable<Conference> objConferenceList = _db.Conferences.ToList();
            return View(objConferenceList);
        }
    }
}
