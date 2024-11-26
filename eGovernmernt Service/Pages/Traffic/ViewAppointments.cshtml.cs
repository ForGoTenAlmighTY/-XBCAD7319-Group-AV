using eGovernmernt_Service.Models;
using eGovernmernt_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eGovernmernt_Service.Pages.Traffic
{
    [Authorize(Roles = "Traffic")]
    public class ViewAppointmentsModel : PageModel
    {
        private readonly ApplicationContext context;
        public ViewAppointmentsModel(ApplicationContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public TrafficAppointment Record { get; set; }

        public IActionResult OnGet(int id)
        {
            Record = context.TrafficAppointment.FirstOrDefault(r => r.AppointID == id);
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

            var recordInDb = context.TrafficAppointment.FirstOrDefault(r => r.AppointID == Record.AppointID);
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
