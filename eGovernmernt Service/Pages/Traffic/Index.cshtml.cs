using eGovernmernt_Service.Models;
using eGovernmernt_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eGovernmernt_Service.Pages.Traffic
{
    [Authorize(Roles = "Traffic")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext context;
        private readonly PdfService _pdfService;

        public List<TrafficAppointment> trafficAppointments { get; set; } = new List<TrafficAppointment>();

        public List<TrafficLicenceApplication> licenceApplications { get; set; } = new List<TrafficLicenceApplication>();
        public List<TrafficRegistrationApplication> registrationApplications { get; set; } = new List<TrafficRegistrationApplication>();

        public IndexModel(ApplicationContext context, PdfService pdfService)
        {
            this.context = context;
            this._pdfService = pdfService;

        }

        public void OnGet()
        {
            trafficAppointments = context.TrafficAppointment.OrderByDescending(p => p.AppointID).ToList();
            licenceApplications = context.TrafficLicenceApplication.OrderByDescending(p => p.AppID).ToList();
            registrationApplications = context.TrafficRegistrationApplication.OrderByDescending(p => p.AppID).ToList();

        }

        public IActionResult OnPostDownloadTrafficRegistrationApplicationsPdf()
        {
            var data = context.TrafficRegistrationApplication.ToList();
            var pdfBytes = _pdfService.GenerateTrafficRegistrationApplicationsPdf(data);
            return File(pdfBytes, "application/pdf", "TrafficRegistrationApplications.pdf");
        }

        public IActionResult OnPostDownloadTrafficLicenceApplicationsPdf()
        {
            var data = context.TrafficLicenceApplication.ToList();
            var pdfBytes = _pdfService.GenerateTrafficLicenceApplicationsPdf(data);
            return File(pdfBytes, "application/pdf", "TrafficLicenceApplications.pdf");
        }
    }
}
