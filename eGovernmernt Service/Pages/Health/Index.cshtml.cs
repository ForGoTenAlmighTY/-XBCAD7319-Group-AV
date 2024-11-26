using eGovernmernt_Service.Models;
using eGovernmernt_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eGovernmernt_Service.Pages.Health
{
    [Authorize (Roles= "Health")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext context;
        private readonly PdfService _pdfService;

        public List<HealthApplication> healthApplications { get; set; } = new List<HealthApplication>();

        public List<HealthAppointment> healthAppointments { get; set; } = new List<HealthAppointment>();

        public IndexModel(ApplicationContext context, PdfService pdfService)
        {
            this.context = context;
            _pdfService = pdfService;
        }

        public void OnGet()
        {
            healthAppointments = context.HealthAppointment.OrderByDescending(p => p.AppointID).ToList();
            healthApplications = context.HealthApplication.OrderByDescending(p => p.AppID).ToList();

        }

        public IActionResult OnPostDownloadHealthApplicationsPdf()
        {
            var data = context.HealthApplication.ToList();
            var pdfBytes = _pdfService.GenerateHealthApplicationsPdf(data);
            return File(pdfBytes, "application/pdf", "HealthApplications.pdf");
        }

    }
}
