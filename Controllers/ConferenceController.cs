using Conference_Management_System.Data;
using Conference_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

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

        public IActionResult View(int? id) {
            // Create new object that will store conference and papers in that conference
            dynamic conference = new ExpandoObject();

            if(id == null || id == 9) 
            {
                return NotFound();
            }

            var conferenceFromDb = _db.Conferences.Find(id);

            if (conferenceFromDb == null) 
            { 
                return NotFound();
            }

            conference.Conference = conferenceFromDb;

            //Get papers where the name of the conference matches the name of the conference the papers are assigned to
            var papersInConference = _db.Papers.Where(p => p.Conference == conferenceFromDb.Name).ToList();

            conference.Papers = papersInConference;

            return View(conference);
        }
    }
}
