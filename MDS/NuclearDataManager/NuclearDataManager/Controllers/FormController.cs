using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuclearDataManager.Data; // Ensure you reference your DbContext
using NuclearDataManager.Models; // Ensure you reference your model
using System.Threading.Tasks;

namespace NuclearDataManager.Controllers
{
    [Authorize]
    public class FormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Form()
        {
            Console.WriteLine($"🔹 User is authenticated before loading form: {User.Identity?.IsAuthenticated}");
            Console.WriteLine($"🔹 User name: {User.Identity?.Name}");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm(ResearchSubmission submission)
        {
            Console.WriteLine($"User is authenticated: {User.Identity?.IsAuthenticated}");
            Console.WriteLine($"User email: {User.Identity?.Name}");

            if (ModelState.IsValid)
            {
                submission.UserEmail = User.Identity?.Name ?? "Unknown";
                //submission.UserEmail = "test@example.com";
                Console.WriteLine($"TEST");
                Console.WriteLine($"Saving Submission: Temp = {submission.Temperature}, Email = {submission.UserEmail}, Date = {submission.DateSubmitted}");
                _context.ResearchSubmissions.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success");
            }

            Console.WriteLine($"User is authenticated: {User.Identity?.IsAuthenticated}");
            Console.WriteLine($"User name: {User.Identity?.Name}");

            return View("Form");
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}