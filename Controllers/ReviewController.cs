using Conference_Management_System.Data;
using Conference_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;
using System.Dynamic;

namespace Conference_Management_System.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly UserManager<ApplicationUser> UserManager;

        public ReviewController(ApplicationDbContext db, SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> UserManager)
        {
            _db = db;
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
        }

        public IActionResult Index()
        {
            // Define a object to store a list of papers that the current user can review and reviews that the current user has assigned themselves
            dynamic availableReviews = new ExpandoObject();

            List<Paper> papersInExpertise = new List<Paper>();
            List<Paper> allPapers = _db.Papers.Where(p => p.Author != UserManager.GetUserAsync(User).Result.FullName).ToList();
            string[] userExpertises = UserManager.GetUserAsync(User).Result?.Expertises.Split(',');

            List<Review> acceptedReviews = _db.Reviews.Where(r => r.ReviewerName == UserManager.GetUserAsync(User).Result.FullName).ToList();

            // Add papers to be available to review if the user has one or more expertises that match the paper's expertises
            foreach (Paper paper in allPapers)
            {
                if (!paper.Expertises.Split(',').Intersect(userExpertises).IsNullOrEmpty()
                    && !acceptedReviews.Any(r => r.Paper == paper.Id))
                {
                    papersInExpertise.Add(paper);
                }
            }

            availableReviews.Papers = papersInExpertise;
            availableReviews.Reviews = acceptedReviews;

            
            return View(availableReviews);
        }

        public IActionResult AcceptReview(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            _db.Reviews.Add(new Review { Paper = (int)id, Status = "In Progress", ReviewerName = UserManager.GetUserAsync(User).Result.FullName, Comments = "" });
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
