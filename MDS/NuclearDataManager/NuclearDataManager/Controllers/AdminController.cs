using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this
using NuclearDataManager.Data;
using NuclearDataManager.Models;
using System.Threading.Tasks; // Add this

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Dashboard() => View();

    // User Management
    public async Task<IActionResult> ManageUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }

    // Form Submissions
    public async Task<IActionResult> ViewSubmissions()
    {
        var submissions = await _context.ResearchSubmissions
            .OrderByDescending(s => s.DateSubmitted)
            .ToListAsync();

        return View(submissions);
    }

    [HttpPost]
    public async Task<IActionResult> PromoteUser(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            TempData["ErrorMessage"] = "User not found!";
            return RedirectToAction("ManageUsers");
        }

        await _userManager.AddToRoleAsync(user, "Admin");
        TempData["SuccessMessage"] = $"{userEmail} promoted to Admin!";
        return RedirectToAction("ManageUsers");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            TempData["ErrorMessage"] = "User not found!";
            return RedirectToAction("ManageUsers");
        }

        await _userManager.DeleteAsync(user);
        TempData["SuccessMessage"] = $"{userEmail} deleted!";
        return RedirectToAction("ManageUsers");
    }
}