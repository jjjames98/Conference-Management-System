using Conference_Management_System.Data;
using Conference_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Conference_Management_System.Controllers
{
    public class PaperController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly UserManager<ApplicationUser> UserManager;

        public PaperController(ApplicationDbContext db, SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> UserManager)
        {
            _db = db;
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
        }

        public IActionResult Index()
        {
            IEnumerable<Paper> objPaperList = _db.Papers.Where(p => p.Author == UserManager.GetUserAsync(User).Result.FullName).ToList();
            return View(objPaperList);
        }

        public IActionResult View(int? id)
        {
            // Create new object that will store paper and papers in that conference
            dynamic paper = new ExpandoObject();

            if (id == null || id == 9)
            {
                return NotFound();
            }

            var paperFromDb = _db.Papers.Find(id);

            if (paperFromDb == null)
            {
                return NotFound();
            }

            paper.Paper = paperFromDb;

            //Get papers where the name of the conference matches the name of the conference the papers are assigned to
            var reviewsOfPaper = _db.Reviews.Where(r => r.Paper == id).ToList();

            paper.Reviews = reviewsOfPaper;

            return View(paper);
        }
        
        public IActionResult Create() 
        { 
            return View();
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Paper obj)
		{
			if (ModelState.IsValid)
			{
				_db.Papers.Add(obj);
				_db.SaveChanges();
				TempData["Success"] = "Paper added successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}
	}
}
