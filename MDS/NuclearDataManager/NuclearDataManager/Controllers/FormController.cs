using Microsoft.AspNetCore.Mvc;
using NuclearDataManager.Data; // Ensure you reference your DbContext
using NuclearDataManager.Models; // Ensure you reference your model
using System.Threading.Tasks;

namespace NuclearDataManager.Controllers
{
    public class FormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm(ResearchSubmission submission)
        {
            if (ModelState.IsValid)
            {
                _context.ResearchSubmissions.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success");
            }
            return View("Form");
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}