using eGovernmernt_Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eGovernmernt_Service
{
    [Route("Reports")]
    public class ReportsController : ControllerBase
    {
        private readonly PdfService _pdfService;
        private readonly ApplicationContext _context;

        public ReportsController(PdfService pdfService, ApplicationContext context)
        {
            _pdfService = pdfService;
            _context = context;
        }

        [HttpPost("DownloadTrafficRegistrationApplicationsPdf")]
        public async Task<IActionResult> DownloadTrafficRegistrationApplicationsPdf()
        {
            var data = await _context.TrafficRegistrationApplication.ToListAsync();
            var pdfBytes = _pdfService.GenerateTrafficRegistrationApplicationsPdf(data);
            return File(pdfBytes, "application/pdf", "TrafficRegistrationApplications.pdf");
        }

        public async Task<IActionResult> DownloadTrafficLicenceApplicationsPdf()
        {
            var data = await _context.TrafficLicenceApplication.ToListAsync();
            var pdfBytes = _pdfService.GenerateTrafficLicenceApplicationsPdf(data);
            return File(pdfBytes, "application/pdf", "TrafficLicenceApplications.pdf");
        }










    }
}
