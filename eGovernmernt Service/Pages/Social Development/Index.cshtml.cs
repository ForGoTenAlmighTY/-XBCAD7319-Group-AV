using eGovernmernt_Service.Models;
using eGovernmernt_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eGovernmernt_Service.Pages.Social_Development
{
    [Authorize(Roles = "Social Development")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext context;
        private readonly PdfService _pdfService;

        public List<SocialAppointment> socialAppointments { get; set; } = new List<SocialAppointment>();

        public List<SocialApplication> socialApplications { get; set; } = new List<SocialApplication>();

        public IndexModel(ApplicationContext context, PdfService pdfService)
        {
            this.context = context;
            _pdfService = pdfService;
        }

        public void OnGet()
        {
            socialAppointments = context.SocialAppointment.OrderByDescending(p => p.AppointID).ToList();
            socialApplications = context.SocialApplication.OrderByDescending(p => p.AppID).ToList();

        }

        public IActionResult OnPostDownloadSocialApplicationsPdf()
        {
            var data = context.SocialApplication.ToList();
            var pdfBytes = _pdfService.GenerateSocialApplicationsPdf(data);
            return File(pdfBytes, "application/pdf", "SocialApplications.pdf");
        }
    }
}
