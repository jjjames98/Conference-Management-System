using Conference_Management_System.Data;
using Conference_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Dynamic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

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

        [HttpGet]
        public IActionResult Download(int? id)
        {
            var obj = _db.Papers.Find(id);
            if (obj != null)
            {
                string filepath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/paper_files"), obj.Title + ".docx");
                if (System.IO.File.Exists(filepath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                    return File(fileBytes, "application/x-msdownload", Path.GetFileName(filepath));
                }
            }
            return RedirectToAction("View");
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
                if(obj.File != null) 
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/paper_files");
                    string fileNameWithPath = Path.Combine(path, obj.File.FileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        obj.File.CopyTo(stream);
                    }
				}

				_db.Papers.Add(obj);
				_db.SaveChanges();
				TempData["Success"] = "Paper added successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}
	}
}
