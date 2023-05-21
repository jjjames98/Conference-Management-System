using Conference_Management_System.Data;
using Conference_Management_System.Models;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;

namespace Conference_Management_System.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly UserManager<ApplicationUser> UserManager;

        public ConferenceController(ApplicationDbContext db, SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> UserManager)
        {
            _db = db;   
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
        }

        public IActionResult Index()
        {
            IEnumerable<Conference> objConferenceList = _db.Conferences.Where(c => c.ConferenceChair == UserManager.GetUserAsync(User).Result.FullName).ToList();
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


        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Conference obj) 
        {
            if (ModelState.IsValid) 
            {
                _db.Conferences.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Conference added successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Decline the paper from the conference
        public IActionResult Decline(int? id) 
        { 
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var paper = _db.Papers.Find(id);

            if (paper != null) 
            {
                paper.Conference = "None";
                _db.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }

            return View();
        }
    }

    
}
