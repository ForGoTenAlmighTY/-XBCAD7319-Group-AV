using eGovernmernt_Service.Models;
using eGovernmernt_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eGovernmernt_Service.Pages.Home_Affairs
{
    [Authorize(Roles = "Home Affairs")]
    public class ViewApplicationsModel : PageModel
    {
        private readonly ApplicationContext context;
        public ViewApplicationsModel(ApplicationContext context)
        {
            this.context = context;   
        }

        [BindProperty]
        public HomeAffairsApplication Record { get; set; }

        public IActionResult OnGet(int id)
        {
            Record = context.HomeAffairsApplication.FirstOrDefault(r => r.AppID == id);
            if (Record == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var recordInDb = context.HomeAffairsApplication.FirstOrDefault(r => r.AppID == Record.AppID);
            if (recordInDb == null)
            {
                return NotFound();
            }

            // Update field
            recordInDb.Status = Record.Status;

            context.SaveChanges();

            return RedirectToPage("Home Affairs/Index");
        }
    }
}
