using eGovernmernt_Service.Models;
using eGovernmernt_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eGovernmernt_Service.Pages.Traffic
{
    [Authorize(Roles = "Traffic")]
    public class ViewRegistationApplicationsModel : PageModel
    {
        private readonly ApplicationContext context;
        public ViewRegistationApplicationsModel(ApplicationContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public TrafficRegistrationApplication Record { get; set; }

        public IActionResult OnGet(int id)
        {
            Record = context.TrafficRegistrationApplication.FirstOrDefault(r => r.AppID == id);
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

            var recordInDb = context.TrafficRegistrationApplication.FirstOrDefault(r => r.AppID == Record.AppID);
            if (recordInDb == null)
            {
                return NotFound();
            }

            recordInDb.Status = Record.Status;
            context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
