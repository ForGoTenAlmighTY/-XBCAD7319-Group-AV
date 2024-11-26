using eGovernmernt_Service.Models;
using eGovernmernt_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eGovernmernt_Service.Pages.Home_Affairs
{
    [Authorize(Roles = "Home Affairs")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext context;
        private readonly PdfService _pdfService;

        public List<HomeAffairsAppointment> HomeAffairsAppointments { get; set; } = new List<HomeAffairsAppointment>();

        public List<HomeAffairsApplication> HomeAffairsApplications { get; set; } = new List<HomeAffairsApplication>();

        public IndexModel(ApplicationContext context, PdfService pdfService)
        {
            this.context = context;
            _pdfService = pdfService;
        }

        public void OnGet()
        {
            HomeAffairsAppointments = context.HomeAffairsAppointment.OrderByDescending(p=>p.AppointID).ToList();
            HomeAffairsApplications = context.HomeAffairsApplication.OrderByDescending(p => p.AppID).ToList();

        }
        public IActionResult OnPostDownloadHomeAffairsApplicationsPdf()
        {
            var data = context.HomeAffairsApplication.ToList();
            var pdfBytes = _pdfService.GenerateHomeAffairsApplicationsPdf(data);
            return File(pdfBytes, "application/pdf", "HomeAffairsApplications.pdf");
        }
    }
}
