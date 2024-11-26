using eGovernmernt_Service.Models;
using eGovernmernt_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eGovernmernt_Service.Pages.Social_Development
{
    [Authorize(Roles = "Social Development")]
    public class ViewAppointmentsModel : PageModel
    {
        private readonly ApplicationContext context;
        public ViewAppointmentsModel(ApplicationContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public SocialAppointment Record { get; set; }

        public IActionResult OnGet(int id)
        {
            Record = context.SocialAppointment.FirstOrDefault(r => r.AppointID == id);
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

            var recordInDb = context.SocialAppointment.FirstOrDefault(r => r.AppointID == Record.AppointID);
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
